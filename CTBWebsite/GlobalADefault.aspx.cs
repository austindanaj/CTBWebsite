using System.Data.SqlClient;
using System;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI;

namespace CTBWebsite
{
    public partial class GlobalADefault : IOPage
    {
        public SortDirection direction {
            get {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState["directionState"];
            }
            set { ViewState["directionState"] = value; }
        }

        private enum FILE_TYPE
        {
            CALIBRATION = 0,
            TD1 = 1,
            TD2 = 2,
            TD3 = 3,
            TD4 = 4
        }

        private static readonly string TOOLS_PATH = Path.Combine(HOME, "Tools/");

        //===========================================================
        // Init object
        //===========================================================
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Alna_num"] == null)
            {
                redirectSafely("~/Default");
                return;
            }

            if (!IsPostBack)
            {
                openDBConnection();
                objConn.Open();
                dgvReports.DataSource = LoadReports();
                dgvReports.DataBind();

                dgvFiles.DataSource = LoadFiles();
                dgvFiles.DataBind();

                dgvImages.DataSource = LoadImages();
                dgvImages.DataBind();

                LoadDD();

                //  LoadImages();
                LoadTools();
                //  objConn.Close();

            }

            RegisterPostBackControl();


        }

        private void RegisterPostBackControl()
        {
            foreach (GridViewRow row in dgvFiles.Rows)
            {
                LinkButton lnkFull = row.FindControl("lnkDownload") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
            }

            foreach (GridViewRow row in dgvReports.Rows)
            {
                LinkButton lnkReport = row.FindControl("lnkReportFile") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkReport);
                LinkButton lnkCal = row.FindControl("lnkReportCalibration") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkCal);
                LinkButton lnkTD1 = row.FindControl("lnkReportTD1") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkTD1);
                LinkButton lnkTD2 = row.FindControl("lnkReportTD2") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkTD2);
                LinkButton lnkTD3 = row.FindControl("lnkReportTD3") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkTD3);
                LinkButton lnkTD4 = row.FindControl("lnkReportTD4") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkTD4);
            }

            foreach (GridViewRow row in dgvImages.Rows)
            {
                LinkButton lnkImage = row.FindControl("lnkImageFile") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkImage);
            }
        }

        //===========================================================
        // Prep the page (Load DB contents/static content)
        //===========================================================
        public DataTable LoadReports()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("GetFilteredReport");
            cmd.CommandType = CommandType.StoredProcedure;

            if (ViewState["VehicleName"] != null && ViewState["VehicleName"].ToString() != "-1")
            {
                cmd.Parameters.AddWithValue("@Vehicle_Name", ViewState["VehicleName"].ToString());
            }

            if (ViewState["PhoneName"] != null && ViewState["PhoneName"].ToString() != "-1")
            {
                cmd.Parameters.AddWithValue("@Phone_Name", ViewState["PhoneName"].ToString());
            }

            if (ViewState["EmployeeName"] != null && ViewState["EmployeeName"].ToString() != "-1")
            {
                cmd.Parameters.AddWithValue("@Emp_Name", ViewState["EmployeeName"].ToString());
            }

            if (ViewState["DateCreated"] != null && ViewState["DateCreated"].ToString() != "")
            {
                cmd.Parameters.AddWithValue("@Date", ViewState["DateCreated"].ToString());
            }

            cmd.Connection = objConn;

            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            dt.Columns.Add("FormDate", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime date = (DateTime)dt.Rows[i]["Date_Created"];
                string format = "MMM d, yyyy";
                dt.Rows[i]["FormDate"] = date.ToString(format);
            }

            // DataTable dt = getDataTable("GetFilteredReport");
            return dt;

        }

        public DataTable LoadFiles()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("GetFilteredFiles");
            cmd.CommandType = CommandType.StoredProcedure;
            if (ViewState["FileType"] != null && ViewState["FileType"].ToString() != "-1")
            {
                cmd.Parameters.AddWithValue("@File_Type", ViewState["FileType"].ToString());
            }

            if (ViewState["VehicleFileName"] != null && ViewState["VehicleFileName"].ToString() != "-1")
            {
                cmd.Parameters.AddWithValue("@Vehicle_Name", ViewState["VehicleFileName"].ToString());
            }

            if (ViewState["PhoneFileName"] != null && ViewState["PhoneFileName"].ToString() != "-1")
            {
                cmd.Parameters.AddWithValue("@Phone_Name", ViewState["PhoneFileName"].ToString());
            }

            if (ViewState["EmployeeFileName"] != null && ViewState["EmployeeFileName"].ToString() != "-1")
            {
                cmd.Parameters.AddWithValue("@Emp_Name", ViewState["EmployeeFileName"].ToString());
            }

            if (ViewState["DateFileCreated"] != null && ViewState["DateFileCreated"].ToString() != "")
            {
                cmd.Parameters.AddWithValue("@Date", ViewState["DateFileCreated"].ToString());
            }

            cmd.Connection = objConn;

            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            dt.Columns.Add("Type", typeof(string));
            if (dt.Rows.Count != 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int type = (int)dt.Rows[i]["F_Type"];
                    switch (type)
                    {
                        case (int)FILE_TYPE.CALIBRATION:
                            dt.Rows[i]["Type"] = "Calibration";
                            break;
                        case (int)FILE_TYPE.TD1:
                            dt.Rows[i]["Type"] = "TD1";
                            break;
                        case (int)FILE_TYPE.TD2:
                            dt.Rows[i]["Type"] = "TD2";
                            break;
                        case (int)FILE_TYPE.TD3:
                            dt.Rows[i]["Type"] = "TD3";
                            break;
                        case (int)FILE_TYPE.TD4:
                            dt.Rows[i]["Type"] = "TD4";
                            break;
                    } // dt.Rows.Add();
                }
            }

            // DataTable dt = getDataTable("GetFilteredReport");
            return dt;

        }

        public DataTable LoadImages()
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("GetFilteredImages");
            cmd.CommandType = CommandType.StoredProcedure;

            if (ViewState["VehicleImageName"] != null && ViewState["VehicleImageName"].ToString() != "-1")
            {
                cmd.Parameters.AddWithValue("@Vehicle_Name", ViewState["VehicleImageName"].ToString());
            }

            if (ViewState["EmployeeImageName"] != null && ViewState["EmployeeImageName"].ToString() != "-1")
            {
                cmd.Parameters.AddWithValue("@Emp_Name", ViewState["EmployeeImageName"].ToString());
            }

            if (ViewState["DateImageCreated"] != null && ViewState["DateImageCreated"].ToString() != "")
            {
                cmd.Parameters.AddWithValue("@Date", ViewState["DateImageCreated"].ToString());
            }

            cmd.Connection = objConn;

            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            dt.Columns.Add("FormDate", typeof(string));
            if (dt.Rows.Count != 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime date = (DateTime)dt.Rows[i]["Date_Created"];
                    string format = "MMM d, yyyy";
                    dt.Rows[i]["FormDate"] = date.ToString(format);
                }

                // dt.Rows.Add();
            }

            // DataTable dt = getDataTable("GetFilteredReport");
            return dt;
        }

        public void LoadFileDropdowns()
        {
            ddlFileVehicle.Items.Clear();
            ddlFileVehicle.Items.Add(new ListItem("-- Select a Vehicle --", "-1"));
            ddlFilePhone.Items.Clear();
            ddlFilePhone.Items.Add(new ListItem("-- Select a Phone --", "-1"));
            ddlFileAuthor1.Items.Clear();
            ddlFileAuthor1.Items.Add(new ListItem("-- Select Author 1 --", "-1"));
            ddlFileAuthor2.Items.Clear();
            ddlFileAuthor2.Items.Add(new ListItem("-- Select Author 2 --", "-2"));
            ddlFileAuthor2.Items.Add(new ListItem("N/A", "-1"));

            openDBConnection();
            objConn.Open();
            SqlDataReader reader = getReader("SELECT * FROM Vehicles  WHERE Active=@value1 ORDER BY Name ASC", true);
            while (reader.Read())
            {
                ddlFileVehicle.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }

            reader.Close();
            reader = getReader("SELECT * FROM Phones  WHERE Active=@value1 ORDER BY Name ASC", true);
            while (reader.Read())
            {
                ddlFilePhone.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }

            reader.Close();
            reader = getReader("SELECT * FROM Employees  WHERE Active=@value1 ORDER BY Name ASC", true);
            while (reader.Read())
            {
                string id = reader.GetValue(0).ToString();
                string name = reader.GetString(1);
                ddlFileAuthor1.Items.Add(new ListItem(name, id));
                ddlFileAuthor2.Items.Add(new ListItem(name, id));
            }

            reader.Close();
            reader.Dispose();

            objConn.Close();

        }

        public void LoadDD()
        {
            ddlVehicleReportFilter.Items.Clear();
            ddlFileVehicleFilter.Items.Clear();
            ddlImageVehicleFilter.Items.Clear();
            ddlVehicleReportFilter.Items.Add(new ListItem("-- Vehicle Filter --", "-1"));
            ddlFileVehicleFilter.Items.Add(new ListItem("-- Vehicle Filter --", "-1"));
            ddlImageVehicleFilter.Items.Add(new ListItem("-- Vehicle Filter --", "-1"));
            SqlDataReader reader = getReader("SELECT * FROM Vehicles WHERE Active=@value1 ORDER BY Name ASC", true);
            string id;
            string name;
            while (reader.Read())
            {
                id = reader.GetValue(0).ToString();
                name = reader.GetString(1);
                ddlVehicleReportFilter.Items.Add(new ListItem(name, id));
                ddlFileVehicleFilter.Items.Add(new ListItem(name, id));
                ddlImageVehicleFilter.Items.Add(new ListItem(name, id));
            }

            reader.Close();

            ddlPhoneReportFilter.Items.Clear();
            ddlFilePhoneFilter.Items.Clear();
            ddlPhoneReportFilter.Items.Add(new ListItem("-- Phone Filter --", "-1"));
            ddlFilePhoneFilter.Items.Add(new ListItem("-- Phone Filter --", "-1"));
            reader = getReader("SELECT * FROM Phones WHERE Active=@value1 ORDER BY Name ASC", true);
            while (reader.Read())
            {
                id = reader.GetValue(0).ToString();
                name = reader.GetString(1);
                ddlPhoneReportFilter.Items.Add(new ListItem(name, id));
                ddlFilePhoneFilter.Items.Add(new ListItem(name, id));
            }

            reader.Close();

            ddlEmployeeReportFilter.Items.Clear();
            ddlFileAuthorFilter.Items.Clear();
            ddlImageAuthorFilter.Items.Clear();
            ddlEmployeeReportFilter.Items.Add(new ListItem("-- Author Filter --", "-1"));
            ddlFileAuthorFilter.Items.Add(new ListItem("-- Author Filter --", "-1"));
            ddlImageAuthorFilter.Items.Add(new ListItem("-- Author Filter --", "-1"));
            reader = getReader("SELECT * FROM Employees  WHERE Active=@value1 ORDER BY Name ASC", true);
            while (reader.Read())
            {
                id = reader.GetValue(0).ToString();
                name = reader.GetString(1);
                ddlEmployeeReportFilter.Items.Add(new ListItem(name, id));
                ddlFileAuthorFilter.Items.Add(new ListItem(name, id));
                ddlImageAuthorFilter.Items.Add(new ListItem(name, id));
            }

            reader.Close();
            reader.Dispose();
        }

        public void LoadImageDropdowns()
        {
            ddlImageVehicle.Items.Clear();
            ddlImageVehicle.Items.Add(new ListItem("-- Select a Vehicle --", "-1"));

            openDBConnection();
            objConn.Open();
            SqlDataReader reader = getReader("SELECT * FROM Vehicles  WHERE Active=@value1 ORDER BY Name ASC", true);
            while (reader.Read())
            {
                ddlImageVehicle.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }

            reader.Close();
            reader.Dispose();

            objConn.Close();
        
        }

        public void LoadTools()
        {
            SqlCommand sql = new SqlCommand("SELECT * FROM Tools WHERE Active='1' ORDER BY Name ASC", objConn);

            SqlDataAdapter adp = new SqlDataAdapter(sql);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ds.Tables[0].Columns.Add("FormDate", typeof(string));
            ds.Tables[0].Columns.Add("IconType", typeof(string));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DateTime date = (DateTime)ds.Tables[0].Rows[i]["Date_updated"];
                string format = "MMM d, yyyy";
                ds.Tables[0].Rows[i]["FormDate"] = date.ToString(format);
                string type = (string)ds.Tables[0].Rows[i]["Extension"];
                ds.Tables[0].Rows[i]["IconType"] = GetImageIcon(type);
                ds.Tables[0].Rows[i]["Comment"] =
                    ds.Tables[0].Rows[i]["Comment"].ToString().Replace("\r\n", Environment.NewLine);
            }

            lstTools.DataSource = ds;
            lstTools.DataBind();
            sql.Dispose();


            foreach (ListViewDataItem item in lstTools.Items)
            {
                LinkButton lnkbutton = item.FindControl("lnkEditClicked") as LinkButton;
                if ((int)Session["Alna_num"] !=
                    int.Parse(lstTools.DataKeys[item.DataItemIndex].Values["Alna_num"].ToString()))
                {
                    lnkbutton.Visible = false;
                }

            }


            // lstTools.Items.Add();
        }

        public string GetImageIcon(string type)
        {
            if (type.Contains("doc"))
            {
                return "doc_icon.png";
            }
            else if (type.Contains("xls"))
            {
                return "excel_icon.png";
            }
            else if (type.Contains("zip") || type.Contains("7z"))
            {
                return "zip_icon.png";
            }

            if (type.Contains("pdf"))
            {
                return "pdf_icon.png";
            }
            else if (type.Contains("android"))
            {
                return "apk_icon.png";
            }
            else if (type.Contains("exe"))
            {
                return "exe_icon.png";
            }
            else if (type.Contains("png") || type.Contains("jpg") || type.Contains("jpeg"))
            {
                return "image_icon.png";
            }
            else if (type.Contains("ppt"))
            {
                return "ppwt_icon.png";
            }
            else
                return "unknown_icon.png";
        }

        //===========================================================
        // Listeners
        //===========================================================
        public void ExecuteCommand(GridViewCommandEventArgs e)
        {
            try
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                if (e.CommandName == "Download_File")
                {
                    Tables table;
                    string[] args = lnkView.CommandArgument.Split('_');
                    string type = args[0];
                    int id = int.Parse(args[1]);
                    switch (type)
                    {
                        case "F":
                            table = Tables.File;
                            break;
                        case "R":
                            table = Tables.Report;
                            break;
                        default:
                            table = Tables.Image;
                            break;

                    }
                    string path = getPath(id, table);

                    Response.ClearContent();
                    Response.Clear();
                    Response.ContentType = "application/" + Path.GetExtension(path);
                    Response.AddHeader("Content-Disposition",
                        "attachment; filename=" + Path.GetFileName(path) + ";");
                    Response.TransmitFile(path);
                    Response.Flush();
                    Response.End();

                }
                else if (e.CommandName == "Edit_Report")
                {


                    Session["CreateClicked"] = false;
                    mpeReports.Show();
                    LoadReportDropdowns();
                    lblReportTitle.Text = "Update Report";
                    btnSubmitReport.Text = "Save Report";

                    string id = lnkView.CommandArgument;
                    Session["Edit_ID"] = id;
                    string name = "";
                    string calibration = "-1";
                    string td1 = "-1";
                    string td2 = "-1";
                    string td3 = "-1";
                    string td4 = "-1";
                    string vID = "-1";
                    string pID = "-1";
                    string emp1 = "-1";
                    string emp2 = "-2";
                    string created = "";
                    string comment = "";

                    rfuDiv.Style.Add("display", "none");
                    rfuHasFile.Style.Add("display", "block");

                    openDBConnection();
                    if (objConn.State == ConnectionState.Closed)
                        objConn.Open();

                    SqlDataReader reader = getReader("select * from Report where ID=@value1", int.Parse(id));
                    if (reader.HasRows)
                    {
                        reader.Read();
                        name = reader.GetValue(15).ToString() + reader.GetValue(16).ToString();
                        calibration = reader.GetValue(1).ToString();
                        td1 = reader.GetValue(2).ToString();
                        td2 = reader.GetValue(3).ToString();
                        td3 = reader.GetValue(4).ToString();
                        td4 = reader.GetValue(5).ToString();
                        vID = reader.GetValue(6).ToString();
                        pID = reader.GetValue(7).ToString();
                        emp1 = reader.GetValue(8).ToString();
                        emp2 = reader.GetValue(9).ToString();
                        comment = reader.GetValue(17).ToString();
                        created = ((DateTime)reader.GetValue(11)).ToString("MM/dd/yyyy");
                        reader.Close();
                    }

                    // reportUpload.
                    ddlVehicles.SelectedValue = vID;
                    ddlPhones.SelectedValue = pID;
                    onSelectedIndexChanged(ddlPhones, EventArgs.Empty);
                    ddlAuthor1.SelectedValue = emp1;
                    ddlAuthor2.SelectedValue = emp2 == "" ? "-1" : emp2;
                    ddlCalibration.SelectedValue = calibration;
                    ddlTD1.SelectedValue = td1;
                    ddlTD2.SelectedValue = td2;
                    ddlTD3.SelectedValue = td3;
                    ddlTD4.SelectedValue = td4;
                    txtReportDate.Value = created;
                    lblDateSelected.Value = created;
                    txtReportComment.Text = comment;
                    lblRFU.Text = name;
                    fileSelected.Text = name;

                }
                else if (e.CommandName == "Edit_File")
                {

                    Session["CreateClicked"] = false;

                    mpeFiles.Show();
                    LoadFileDropdowns();

                    string id = lnkView.CommandArgument;
                    Session["Edit_ID"] = id;
                    string name = "";
                    string TD = "-1";
                    string pID = "-1";
                    string vID = "-1";
                    string created = "-1";
                    string emp1 = "-1";
                    string emp2 = "-2";
                    string comment = "";

                    ffuDiv.Style.Add("display", "none");
                    ffuHasFile.Style.Add("display", "block");


                    openDBConnection();
                    if (objConn.State == ConnectionState.Closed)
                        objConn.Open();

                    SqlDataReader reader = getReader("select * from GA_File where ID=@value1", int.Parse(id));
                    if (reader.HasRows)
                    {
                        reader.Read();
                        name = reader.GetValue(1).ToString() + reader.GetValue(2).ToString();
                        TD = reader.GetValue(3).ToString();
                        pID = reader.GetValue(4).ToString();
                        created = ((DateTime)reader.GetValue(5)).ToString("MM/dd/yyyy");
                        emp1 = reader.GetValue(8).ToString();
                        emp2 = reader.GetValue(9).ToString();
                        vID = reader.GetValue(10).ToString();
                        comment = reader.GetValue(13).ToString();
                        reader.Close();
                    }

                    ddlFileType.SelectedValue = TD;
                    ddlFileVehicle.SelectedValue = vID;
                    ddlFilePhone.SelectedValue = pID;
                    ddlFileAuthor1.SelectedValue = emp1;
                    ddlFileAuthor2.SelectedValue = emp2 == "" ? "-1" : emp2;
                    txtFileDate.Value = created;
                    lblDateSelected.Value = created;
                    txtFileComment.Text = comment;
                    lblFFU.Text = name;
                    fileSelected.Text = name;
                }
                else if (e.CommandName == "Edit_Image")
                {
                    Session["CreateClicked"] = false;

                    mpeImages.Show();
                    LoadImageDropdowns();

                    string id = lnkView.CommandArgument;
                    Session["Edit_ID"] = id;

                    string name = "";
                    string vID = "-1";
                    string created = "-1";
                    string comment = "";

                    ifuDiv.Style.Add("display", "none");
                    ifuHasFile.Style.Add("display", "block");


                    openDBConnection();
                    if (objConn.State == ConnectionState.Closed)
                        objConn.Open();

                    SqlDataReader reader = getReader("select * from Pictures where ID=@value1", int.Parse(id));
                    if (reader.HasRows)
                    {
                        reader.Read();
                        name = reader.GetValue(2).ToString() + reader.GetValue(8).ToString();
                        vID = reader.GetValue(1).ToString();
                        created = ((DateTime)reader.GetValue(3)).ToString("MM/dd/yyyy");
                        comment = reader.GetValue(7).ToString();
                        reader.Close();
                    }
                    ddlImageVehicle.SelectedValue = vID;
                    txtImageDate.Value = created;
                    lblDateSelected.Value = created;
                    txtImageComment.Text = comment;
                    lblIFU.Text = name;
                    fileSelected.Text = name;
                }
               
            }
            catch(Exception ex)
            {

            }
        }

        protected void uploadPanel(object sender, EventArgs e)
        {
            Session["CreateClicked"] = true;

            if (sender.Equals(UploadTool))
            {
                mpeTools.Show();
            }
            else if (sender.Equals(UploadFile))
            {
                mpeFiles.Show();
                LoadFileDropdowns();
            }
            else if (sender.Equals(UploadImage))
            {
                mpeImages.Show();
                LoadImageDropdowns();
            }
            else
            {
                mpeReports.Show();
                lblReportTitle.Text = "Create Report";
                btnSubmitReport.Text = "Submit Report";
                LoadReportDropdowns();
            }

        }
    

        protected void lstTools_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (String.Equals(e.CommandName, "Download_Tool"))
            {
                int id = int.Parse(e.CommandArgument.ToString());
                string path = getPath(id, Tables.Tool);
                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "application/" + Path.GetExtension(path);
                Response.AddHeader("Content-Disposition",
                    "attachment; filename=" + Path.GetFileName(path) + ";");
                Response.TransmitFile(path);
                Response.Flush();
                Response.End();
            }
            else if (e.CommandName == "Edit_Tool")
            {
                Session["CreateClicked"] = false;
                mpeTools.Show();

                string id = e.CommandArgument.ToString();
                Session["Edit_ID"] = id;

                string toolName = "";
                string name = "";
                string folder_name = "";
                string comment = "";
                string version = "";

                tfuDiv.Style.Add("display", "none");
                tfuHasFile.Style.Add("display", "block");

                openDBConnection();
                if (objConn.State == ConnectionState.Closed)
                    objConn.Open();

                SqlDataReader reader = getReader("select * from Tools where ID=@value1", int.Parse(id));
                if (reader.HasRows)
                {
                    reader.Read();
                    toolName = reader.GetValue(1).ToString();
                    folder_name = reader.GetValue(9).ToString();
                    name = toolName + reader.GetValue(8).ToString();
                    comment = reader.GetValue(2).ToString();
                    version = reader.GetValue(3).ToString();
                    // updated = ((DateTime)reader.GetValue(4)).ToString("MM/dd/yyyy");
                    reader.Close();
                }
                txtToolName.Text = folder_name;
                txtVersion.Text = version;
                lblTFU.Text = name;
                fileSelected.Text = name;
                txtReleaseNotes.Text = comment;
            }
        }

        protected void btnSubmitReport_OnClick(object sender, EventArgs e)
        {

            if (objConn == null)
                openDBConnection();

            object comment = txtReportComment.Text;
            if (((string)comment).Length > 255)
            {
                //this throws an exception until there's a way to give user feedback
                throw new ArgumentException("Filename is too long, database only accepts 255 or less");
            }
            else if (((string)comment).Equals(""))
            {
                comment = DBNull.Value;
            }

            object author2;
            if (ddlAuthor2.SelectedValue.Equals("-1"))
            {
                author2 = DBNull.Value;
            }
            else
            {
                author2 = int.Parse(ddlAuthor2.SelectedValue);
            }


            string date = lblDateSelected.Value == "" ? txtReportDate.Value : lblDateSelected.Value;

            if ((bool)Session["CreateClicked"])
            {
                object[] id_buffer = {
                    int.Parse(ddlCalibration.SelectedValue),
                    int.Parse(ddlTD1.SelectedValue),
                    int.Parse(ddlTD2.SelectedValue),
                    int.Parse(ddlTD3.SelectedValue),
                    int.Parse(ddlTD4.SelectedValue),
                    int.Parse(ddlVehicles.SelectedValue),
                    int.Parse(ddlPhones.SelectedValue),
                    int.Parse(ddlAuthor1.SelectedValue),
                    author2, //Need a way to return null
                    DateTime.Parse(date), //this is the date created, if the user does not default it to today
                    Path.GetExtension(rfu.PostedFile.FileName),
                    comment //Comment if the user created one
                };
                write(Tables.Report, id_buffer, rfu.PostedFile);
            }
            else
            {
                int id = int.Parse((string)Session["Edit_ID"]);
                if (rfu.Value == "")
                {
                    string rid = fileSelected.Text.Substring(0, fileSelected.Text.IndexOf('_'));
                    object[] id_buffer =
                    {
                        id,
                        rid,
                        int.Parse(ddlCalibration.SelectedValue),
                        int.Parse(ddlTD1.SelectedValue),
                        int.Parse(ddlTD2.SelectedValue),
                        int.Parse(ddlTD3.SelectedValue),
                        int.Parse(ddlTD4.SelectedValue),
                        int.Parse(ddlVehicles.SelectedValue),
                        int.Parse(ddlPhones.SelectedValue),
                        DateTime.Parse(date),
                        int.Parse(ddlAuthor1.SelectedValue),
                        author2, //Need a way to return null
                        comment //Comment if the user created one
                    };
                    update(Tables.Report, id_buffer, id);
                }
                else
                {
                    object[] id_buffer = {
                        int.Parse(ddlCalibration.SelectedValue),
                        int.Parse(ddlTD1.SelectedValue),
                        int.Parse(ddlTD2.SelectedValue),
                        int.Parse(ddlTD3.SelectedValue),
                        int.Parse(ddlTD4.SelectedValue),
                        int.Parse(ddlVehicles.SelectedValue),
                        int.Parse(ddlPhones.SelectedValue),
                        int.Parse(ddlAuthor1.SelectedValue),
                        author2, //Need a way to return null
                        DateTime.Parse(date), //this is the date created, if the user does not default it to today
                        Path.GetExtension(rfu.PostedFile.FileName),
                        comment //Comment if the user created one
                    };
                    write(Tables.Report, id_buffer, rfu.PostedFile);
                    inactive(Tables.Report, id);
                }
            }

            redirectSafely("~/GlobalADefault");

        }

    
        public void LoadReportDropdowns()
        {
            ddlVehicles.Items.Clear();
            ddlVehicles.Items.Add(new ListItem("-- Select a Vehicle --", "-1"));
            ddlPhones.Items.Clear();
            ddlPhones.Items.Add(new ListItem("-- Select a Phone --", "-1"));
            ddlAuthor1.Items.Clear();
            ddlAuthor1.Items.Add(new ListItem("-- Select Author 1 --", "-1"));
            ddlAuthor2.Items.Clear();
            ddlAuthor2.Items.Add(new ListItem("-- Select Author 2 --", "-2"));
            ddlAuthor2.Items.Add(new ListItem("N/A", "-1"));

            openDBConnection();

            if (objConn.State == ConnectionState.Closed)
                objConn.Open();

            SqlDataReader reader = getReader("SELECT * FROM Vehicles  WHERE Active=@value1 ORDER BY Name ASC", true);
            while (reader.Read())
            {
                ddlVehicles.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }

            reader.Close();
            reader = getReader("SELECT * FROM Phones  WHERE Active=@value1 ORDER BY Name ASC", true);
            while (reader.Read())
            {
                ddlPhones.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }

            reader.Close();
            reader = getReader("SELECT * FROM Employees WHERE Active=@value1 ORDER BY Name ASC", true);
            while (reader.Read())
            {
                string id = reader.GetValue(0).ToString();
                string name = reader.GetString(1);
                ddlAuthor1.Items.Add(new ListItem(name, id));
                ddlAuthor2.Items.Add(new ListItem(name, id));
            }

            reader.Close();
            reader.Dispose();

            objConn.Close();

        }

        public void ClearReportDropdowns()
        {
            ddlCalibration.Items.Clear();
            ddlCalibration.Items.Add(new ListItem("-- Select a Calibration --", "-1"));
            ddlTD1.Items.Clear();
            ddlTD1.Items.Add(new ListItem("-- Select a TD1 File --", "-1"));
            ddlTD2.Items.Clear();
            ddlTD2.Items.Add(new ListItem("-- Select a TD2 File --", "-1"));
            ddlTD3.Items.Clear();
            ddlTD3.Items.Add(new ListItem("-- Select a TD3 File --", "-1"));
            ddlTD4.Items.Clear();
            ddlTD4.Items.Add(new ListItem("-- Select a TD4 File --", "-1"));
        }

        //===========================================================
        // Upload to DB
        //===========================================================

        protected void btnSubmitFile_OnClick(object sender, EventArgs e)
        {
            if (objConn == null)
                openDBConnection();

            object comment = txtFileComment.Text;
            if (((string)comment).Length > 255)
            {
                //this throws an exception until there's a way to give user feedback
                throw new ArgumentException("Comment is too long, database only accepts 255 or less");
            }
            else if (((string)comment).Equals(""))
            {
                comment = DBNull.Value;
            }

            object author2;
            if (ddlFileAuthor2.SelectedValue.Equals("-1"))
            {
                author2 = DBNull.Value;
            }
            else
            {
                author2 = int.Parse(ddlFileAuthor2.SelectedValue);
            }


            string date = lblDateSelected.Value == "" ? txtFileDate.Value : lblDateSelected.Value;
            if ((bool)Session["CreateClicked"])
            {


                object[] id_buffer =
                {
                    Path.GetExtension(ffu.PostedFile.FileName),
                    int.Parse(ddlFileType.SelectedValue),
                    int.Parse(ddlFilePhone.SelectedValue),
                    DateTime.Parse(date), //this is the date created, if the user does not default it to today
                    int.Parse(ddlFileAuthor1.SelectedValue),
                    author2, //Need a way to return null
                    int.Parse(ddlFileVehicle.SelectedValue),
                    comment //Comment if the user created one,
                    //DBNull.Value
                };

                write(Tables.File, id_buffer, ffu.PostedFile);

            }
            else
            {
                if (ffu.Value == "")
                {
                    string fid = fileSelected.Text.Substring(0, fileSelected.Text.IndexOf('_'));
                    int id = int.Parse(fid.Substring(1));

                    object[] id_buffer =
                    {
                        id,
                        fid,
                        int.Parse(ddlFileType.SelectedValue),
                        int.Parse(ddlFilePhone.SelectedValue),
                        DateTime.Parse(date), //this is the date created, if the user does not default it to today
                        int.Parse(ddlFileAuthor1.SelectedValue),
                        author2, //Need a way to return null
                        int.Parse(ddlFileVehicle.SelectedValue),
                        comment //Comment if the user created one,

                    };
                    update(Tables.File, id_buffer, id);
                }
                else
                {
                    int id = int.Parse((string)Session["Edit_ID"]);
                    object[] id_buffer =
                    {
                        Path.GetExtension(ffu.PostedFile.FileName),
                        int.Parse(ddlFileType.SelectedValue),
                        int.Parse(ddlFilePhone.SelectedValue),
                        DateTime.Parse(date), //this is the date created, if the user does not default it to today
                        int.Parse(ddlFileAuthor1.SelectedValue),
                        author2, //Need a way to return null
                        int.Parse(ddlFileVehicle.SelectedValue),
                        comment //Comment if the user created one,
                        //DBNull.Value
                    };

                    write(Tables.File, id_buffer, ffu.PostedFile);
                    inactive(Tables.File, id);

                }


            }

            redirectSafely("~/GlobalADefault");
        }
     

        protected void uploadImage(object sender, EventArgs e)
        {
            if (objConn == null) openDBConnection();
            objConn.Open();

            object comment = txtImageComment.Text;
            if (((string) comment).Length > 255)
            {
                //this throws an exception until there's a way to give user feedback
                throw new ArgumentException("Filename is too long, database only accepts 255 or less");
            }
            else if (((string) comment).Equals(""))
            {
                comment = DBNull.Value;
            }

            string date = lblDateSelected.Value == "" ? txtImageDate.Value : lblDateSelected.Value;

            if ((bool) Session["CreateClicked"])
            {
                object[] id_buffer =
                {
                    int.Parse(ddlImageVehicle.SelectedValue),
                    Session["Alna_num"],
                    DateTime.Parse(date), //this is the date created, if the user does not default it to today
                    Path.GetExtension(ifu.PostedFile.FileName),
                    comment //Comment if the user created one
                };
                write(Tables.Image, id_buffer, ifu.PostedFile);
            }
            else
            {
                if (ifu.Value == "")
                {
                    string iid = fileSelected.Text.Substring(0, fileSelected.Text.IndexOf('_'));
                    int id = int.Parse(iid.Substring(1));
                    object[] id_buffer =
                    {
                        id,
                        iid,
                        int.Parse(ddlImageVehicle.SelectedValue),
                        Session["Alna_num"],
                        DateTime.Parse(date), //this is the date created, if the user does not default it to today
                        comment //Comment if the user created one
                    };
                    update(Tables.Image, id_buffer, id);
                }
                else
                {
                    int id = int.Parse((string)Session["Edit_ID"]);
                    object[] id_buffer =
                    {
                        int.Parse(ddlImageVehicle.SelectedValue),
                        Session["Alna_num"],
                        DateTime.Parse(date), //this is the date created, if the user does not default it to today
                        Path.GetExtension(ifu.PostedFile.FileName),
                        comment //Comment if the user created one
                    };
                    write(Tables.Image, id_buffer, ifu.PostedFile);
                    inactive(Tables.Image, id);
                }
            }
            redirectSafely("~/GlobalADefault");
        }

        protected void btnUploadTool_Click(object sender, EventArgs e)
        {
            if (objConn == null) openDBConnection();
            objConn.Open();

            // string path = Path.Combine(TOOLS_PATH, tfu.PostedFile.FileName);
            if ((bool) Session["CreateClicked"])
            {
                object[] id_buffer =
                {
                    txtToolName.Text,
                    txtReleaseNotes.Text,
                    txtVersion.Text,
                    Session["Alna_num"],
                    Path.GetExtension(tfu.PostedFile.FileName)
                };
                write(Tables.Tool, id_buffer, tfu.PostedFile);
            }
            else
            {

                int id = int.Parse((string)Session["Edit_ID"]);
                if (tfu.Value == "")
                {
                    string tid = fileSelected.Text.Substring(0, fileSelected.Text.IndexOf('_'));
                    object[] id_buffer =
                    {
                        id,
                        tid,
                        txtToolName.Text,
                        txtReleaseNotes.Text,
                        txtVersion.Text,
                    };
                    update(Tables.Tool, id_buffer, id);
                }
                else
                {
                    object[] id_buffer =
                    {
                        txtToolName.Text,
                        txtReleaseNotes.Text,
                        txtVersion.Text,
                        Session["Alna_num"],
                        Path.GetExtension(tfu.PostedFile.FileName)
                    };
                    write(Tables.Tool, id_buffer, tfu.PostedFile);
                    inactive(Tables.Tool, id);
                }
            }
            redirectSafely("~/GlobalADefault");
        }

        //===========================================================
        // Code to be refactored with row commands
        //===========================================================
        protected void onRowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExecuteCommand(e);
        }

        protected void onSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVehicles.SelectedIndex != 0 && ddlPhones.SelectedIndex != 0)
            {
                ClearReportDropdowns();

                openDBConnection();
                if (objConn.State == ConnectionState.Closed)
                    objConn.Open();
                object[] o = { int.Parse(ddlPhones.SelectedValue), int.Parse(ddlVehicles.SelectedValue) };

                SqlDataReader reader =
                    getReader(
                        "SELECT * FROM GA_File WHERE Phone_ID=@value1 AND Vehicle_ID=@value2 ORDER BY Name ASC", o);
                int tdNumber;
                while (reader.Read())
                {
                    tdNumber = (int)reader.GetValue(3);
                    string id = reader.GetValue(0).ToString();
                    string name = reader.GetValue(1).ToString();

                    switch (tdNumber)
                    {
                        case 0:
                            ddlCalibration.Items.Add(new ListItem(name, id));
                            break;
                        case 1:
                            ddlTD1.Items.Add(new ListItem(name, id));
                            break;
                        case 2:
                            ddlTD2.Items.Add(new ListItem(name, id));
                            break;
                        case 3:
                            ddlTD3.Items.Add(new ListItem(name, id));
                            break;
                        case 4:
                            ddlTD4.Items.Add(new ListItem(name, id));
                            break;
                    }
                }

                reader.Close();
                reader.Dispose();
                objConn.Close();
            }
        }

        protected void sort(object sender, GridViewSortEventArgs e)
        {
            openDBConnection();
            objConn.Open();

            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";
            }

            Tables tableBeingViewed;
            DataView sortedView;

            if (sender.Equals(dgvFiles))
            {
                tableBeingViewed = Tables.File;
                sortedView = new DataView(LoadFiles());
            }
            else if (sender.Equals(dgvReports))
            {
                tableBeingViewed = Tables.Report;
                sortedView = new DataView(LoadReports());
            }
            else
            {
                tableBeingViewed = Tables.Image;
                sortedView = new DataView(LoadImages());
            }

            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;

            switch (tableBeingViewed)
            {
                case Tables.File:
                    dgvFiles.DataSource = sortedView;
                    dgvFiles.DataBind();
                    break;
                case Tables.Report:
                    dgvReports.DataSource = sortedView;
                    dgvReports.DataBind();
                    break;
                default:
                    dgvImages.DataSource = sortedView;
                    dgvImages.DataBind();
                    break;
            }
        }

        protected void applyFilter(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();

            Tables tableBeingViewed = Tables.File;

            //if viewing a file
            if (sender.Equals(ddlFileFilterType))
            {
                ViewState["FileType"] = ddlFileFilterType.SelectedItem.Value;
            }
            else if (sender.Equals(ddlFileVehicleFilter))
            {
                ViewState["VehicleFileName"] = ddlFileVehicleFilter.SelectedItem.Value;
            }
            else if (sender.Equals(ddlFilePhoneFilter))
            {
                ViewState["PhoneFileName"] = ddlFilePhoneFilter.SelectedItem.Value;
            }
            else if (sender.Equals(ddlFileAuthorFilter))
            {
                ViewState["EmployeeFileName"] = ddlFileAuthorFilter.SelectedItem.Value;
            }
            else if (sender.Equals(txtFileDateFilter))
            {
                ViewState["DateFileCreated"] = txtFileDateFilter.Text;
            }
            //If viewing a report
            else if (sender.Equals(txtReportFilterDate))
            {
                ViewState["DateCreated"] = txtReportFilterDate.Text;
                tableBeingViewed = Tables.Report;
            }
            else if (sender.Equals(ddlVehicleReportFilter))
            {
                ViewState["VehicleName"] = ddlVehicleReportFilter.SelectedItem.Value;
                tableBeingViewed = Tables.Report;
            }
            else if (sender.Equals(ddlPhoneReportFilter))
            {
                ViewState["PhoneName"] = ddlPhoneReportFilter.SelectedItem.Value;
                tableBeingViewed = Tables.Report;
            }
            else if (sender.Equals(ddlEmployeeReportFilter))
            {
                ViewState["EmployeeName"] = ddlEmployeeReportFilter.SelectedItem.Value;
                tableBeingViewed = Tables.Report;
            }
            //If viewing an image
            else if (sender.Equals(ddlImageVehicleFilter))
            {
                ViewState["VehicleImageName"] = ddlImageVehicleFilter.SelectedItem.Value;
                tableBeingViewed = Tables.Image;
            }
            else if (sender.Equals(ddlImageAuthorFilter))
            {
                ViewState["EmployeeImageName"] = ddlImageAuthorFilter.SelectedItem.Value;
                tableBeingViewed = Tables.Image;
            }
            else
            {
                ViewState["DateImageCreated"] = txtImageDateFilter.Text;
                tableBeingViewed = Tables.Image;
            }

            switch (tableBeingViewed)
            {
                case Tables.File:
                    dgvFiles.DataSource = LoadFiles();
                    dgvFiles.DataBind();
                    break;
                case Tables.Report:
                    dgvReports.DataSource = LoadReports();
                    dgvReports.DataBind();
                    break;
                default:
                    dgvImages.DataSource = LoadImages();
                    dgvImages.DataBind();
                    break;
            }

            objConn.Close();
        }
    }
}