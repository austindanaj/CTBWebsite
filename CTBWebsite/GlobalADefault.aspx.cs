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
            set {
                ViewState["directionState"] = value;
            }
        }

        private enum FILE_TYPE
        {
            CALIBRATION = 0,
            TD1 = 1,
            TD2 = 2,
            TD3 = 3,
            TD4 = 4
        }

        //===========================================================
        // Init object
        //===========================================================
        protected void Page_Load(object sender, EventArgs e)
        {
        
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
        // Load static content
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

            //   SqlCommand sql = new SqlCommand("SELECT * FROM SelectReport() WHERE Active='1'");
            //    //  SqlDataAdapter adp = new SqlDataAdapter(sql);
            //    DataSet ds = new DataSet();
            //      adp.Fill(ds);
            /*
            ds.Tables[0].Columns.Add("FormDate", typeof(string));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DateTime date = (DateTime)ds.Tables[0].Rows[i]["Date_Created"];
                string format = "MMM d, yyyy";
                ds.Tables[0].Rows[i]["FormDate"] = date.ToString(format);
            }
            */
            // dgvReports.DataSource = ds;
            //   dgvReports.DataBind();
            //   sql.Dispose();
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




            /*

            DataTable dt = getDataTable("SELECT * FROM SelectFile() WHERE Active='1'");
            //ds.Tables[0].Columns.Add("FormDate", typeof(string));
            dt.Columns.Add("Type", typeof(string));
           
                //DateTime date = (DateTime)ds.Tables[0].Rows[i]["Date_Created"];
              //  string format = "MMM d, yyyy";
               // ds.Tables[0].Rows[i]["FormDate"] = date.ToString(format);
            }
           
            return dt;

            */
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








            /*




            SqlCommand sql = new SqlCommand("SELECT * FROM SelectImages() WHERE Active='1'");
            SqlDataAdapter adp = new SqlDataAdapter(sql);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            /*
            ds.Tables[0].Columns.Add("FormDate", typeof(string));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DateTime date = (DateTime)ds.Tables[0].Rows[i]["Date_Created"];
                string format = "MMM d, yyyy";
                ds.Tables[0].Rows[i]["FormDate"] = date.ToString(format);
            }
            
            dgvImages.DataSource = ds;
            dgvImages.DataBind();
            sql.Dispose();
            */
        }

        public void LoadTools()
        {
            SqlCommand sql = new SqlCommand("SELECT * FROM Tools ORDER BY Name ASC", objConn);

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
                string type = (string)ds.Tables[0].Rows[i]["ContentType"];
                ds.Tables[0].Rows[i]["IconType"] = GetImageIcon(type);
                ds.Tables[0].Rows[i]["Comment"] = ds.Tables[0].Rows[i]["Description"].ToString().Replace("\r\n", Environment.NewLine);
            }
            lstTools.DataSource = ds;
            lstTools.DataBind();
            sql.Dispose();



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
                if (e.CommandName == "Download_File")
                {
                    LinkButton lnkView = (LinkButton)e.CommandSource;
                    string[] args = lnkView.CommandArgument.Split(',');
                    string filePath = args[0];
                    string extension = args[1];

                    Response.ClearContent();
                    Response.Clear();
                    Response.ContentType = "application/" + extension;
                    Response.AddHeader("Content-Disposition",
                        "attachment; filename=" + Path.GetFileName(filePath) + ";");
                    Response.TransmitFile(filePath);
                    Response.Flush();
                    Response.End();

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUploadTool_Click(object sender, EventArgs e)
        {
            openDBConnection();
            this.objConn.Open();

            object filename = DBNull.Value, contentType = DBNull.Value;

            filename = toolUpload.FileName;
            contentType = toolUpload.PostedFile.ContentType;
            object[] o;
            /*
            o = new [] { txtFileName.Text, txtFileDescription.Text, txtVersion.Text, DateTime.Now, Session["Alna_num"], filename, contentType, Session["Alna_num"]};
            executeVoidSQLQuery("INSERT INTO Tools (Name, Comment, Version, Date_updated, Alna_num, Path, Extension) values" +
                                                      "(@value1, @value2, @value3, @value4, @value5, @value6, @value7)", o, objConn);
                                                      */

        }
        protected void UploadTool_OnClick(object sender, EventArgs e)
        {
            mpeTools.Show();
            // Maybe needs logic
        }

        protected void lstTools_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (String.Equals(e.CommandName, "Download_Tool"))
            {
                ListViewDataItem item = (ListViewDataItem)e.Item;
                string id = e.CommandArgument.ToString();

                openDBConnection();
                objConn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Tools WHERE ID=@toolId ORDER BY Name ASC");
                cmd.Parameters.AddWithValue("@toolId", int.Parse(id));
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                string filename = reader.GetString(6);
                string extension = filename == null ? null : filename.Substring(filename.LastIndexOf('.'));
                string contentType = reader.GetString(7);
                byte[] blob = (byte[])reader["Attachment"];

                reader.Close();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", $"attachment; filename=\"{filename}\"");
                Response.BinaryWrite(blob);
                Response.Flush();
                Response.End();
            }
        }

        protected void btnSubmitReport_OnClick(object sender, EventArgs e)
        {
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

            object[] id_buffer = {
                int.Parse(ddlCalibration.SelectedValue),
                int.Parse(ddlTD1.SelectedValue),
                int.Parse(ddlTD2.SelectedValue),
                int.Parse(ddlTD3.SelectedValue),
                int.Parse(ddlTD4.SelectedValue),
                int.Parse(ddlVehicles.SelectedValue),
                int.Parse(ddlPhones.SelectedValue),
                int.Parse(ddlAuthor1.SelectedValue),
                int.Parse(ddlAuthor2.SelectedValue), //Need a way to return null
                lblDateSelected.Value, //this is the date created, if the user does not default it to today
                Path.GetExtension(reportUpload.FileName),
                comment //Comment if the user created one
            };

            write(IOPage.Tables.Report, id_buffer, reportUpload.FileBytes);
        }
        protected void CreateReport_OnClick(object sender, EventArgs e)
        {
            mpeReports.Show();
            LoadReportDropdowns();
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

            objConn.Open();
            SqlDataReader reader = getReader("SELECT * FROM Vehicles  WHERE Active='1' ORDER BY Name ASC");
            while (reader.Read())
            {
                ddlVehicles.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }
            reader.Close();
            reader = getReader("SELECT * FROM Phones  WHERE Active='1' ORDER BY Name ASC");
            while (reader.Read())
            {
                ddlPhones.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }
            reader.Close();
            reader = getReader("SELECT * FROM Employees WHERE Active='1' ORDER BY Name ASC");
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
            objConn.Dispose();

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

        protected void btnSubmitFile_OnClick(object sender, EventArgs e)
        {
            object comment = txtFileComment.Text;
            if (((string)comment).Length > 255)
            {
                //this throws an exception until there's a way to give user feedback
                throw new ArgumentException("Filename is too long, database only accepts 255 or less");
            }
            else if (((string)comment).Equals(""))
            {
                comment = DBNull.Value;
            }

            object[] id_buffer = {
                Path.GetExtension(fileUpload.FileName),
                ddlFileFilterType.SelectedValue,
                int.Parse(ddlPhones.SelectedValue),
                lblDateSelected.Value, //this is the date created, if the user does not default it to today
                int.Parse(ddlAuthor1.SelectedValue),
                int.Parse(ddlAuthor2.SelectedValue), //Need a way to return null
                int.Parse(ddlVehicles.SelectedValue),
                comment //Comment if the user created one
            };

            write(Tables.File, reportUpload.FileBytes);
        }
        protected void UploadFile_OnClick(object sender, EventArgs e)
        {
            mpeFiles.Show();
            LoadFileDropdowns();
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
            SqlDataReader reader = getReader("SELECT * FROM Vehicles  WHERE Active='1' ORDER BY Name ASC");
            while (reader.Read())
            {
                ddlFileVehicle.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }
            reader.Close();
            reader = getReader("SELECT * FROM Phones  WHERE Active='1' ORDER BY Name ASC");
            while (reader.Read())
            {
                ddlFilePhone.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }
            reader.Close();
            reader = getReader("SELECT * FROM Employees  WHERE Active='1' ORDER BY Name ASC");
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
            objConn.Dispose();

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

        protected void UploadImage_OnClick(object sender, EventArgs e)
        {
            mpeImages.Show();
            LoadImageDropdowns();
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
            objConn.Dispose();
        }

        //===========================================================
        // Code to be refactored with row commands
        //===========================================================
        protected void dgvReports_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExecuteCommand(e);
        }
        protected void dgvFiles_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExecuteCommand(e);
        }
        protected void dgvImages_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExecuteCommand(e);
        }
        protected void ddlVehicles_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FillReportFileTypes();
        }
        protected void ddlPhones_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FillReportFileTypes();
        }

        public void FillReportFileTypes()
        {
            if (ddlVehicles.SelectedIndex != 0 && ddlPhones.SelectedIndex != 0)
            {
                ClearReportDropdowns();

                openDBConnection();
                objConn.Open();
                object[] o = { int.Parse(ddlPhones.SelectedValue), int.Parse(ddlVehicles.SelectedValue) };

                SqlDataReader reader = getReader("SELECT * FROM GA_File WHERE Phone_ID=@value1 AND Vehicle_ID=@value2 ORDER BY Name ASC", o);
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

        //===========================================================
        // Code to be refactored
        //===========================================================
        protected void dgvFiles_OnSorting(object sender, GridViewSortEventArgs e)
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
            DataView sortedView = new DataView(LoadFiles());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;

            dgvFiles.DataSource = sortedView;
            dgvFiles.DataBind();


        }

        protected void dgvReports_OnSorting(object sender, GridViewSortEventArgs e)
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
            DataView sortedView = new DataView(LoadReports());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;

            dgvReports.DataSource = sortedView;
            dgvReports.DataBind();
        }

        protected void txtReportFilterDate_OnTextChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = txtReportFilterDate.Text;
            ViewState["DateCreated"] = selectedValue;
            dgvReports.DataSource = LoadReports();
            dgvReports.DataBind();
            objConn.Close();
        }

        protected void ddlVehicleReportFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = ddlVehicleReportFilter.SelectedItem.Value;
            ViewState["VehicleName"] = selectedValue;
            dgvReports.DataSource = LoadReports();
            dgvReports.DataBind();
            objConn.Close();
        }

        protected void ddlPhoneReportFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = ddlPhoneReportFilter.SelectedItem.Value;
            ViewState["PhoneName"] = selectedValue;
            dgvReports.DataSource = LoadReports();
            dgvReports.DataBind();
            objConn.Close();
        }

        protected void ddlEmployeeReportFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = ddlEmployeeReportFilter.SelectedItem.Value;
            ViewState["EmployeeName"] = selectedValue;
            dgvReports.DataSource = LoadReports();
            dgvReports.DataBind();
            objConn.Close();
        }

        protected void ddlFileFilterType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = ddlFileFilterType.SelectedItem.Value;
            ViewState["FileType"] = selectedValue;
            dgvFiles.DataSource = LoadFiles();
            dgvFiles.DataBind();
            objConn.Close();
        }

        protected void ddlFileVehicleFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = ddlFileVehicleFilter.SelectedItem.Value;
            ViewState["VehicleFileName"] = selectedValue;
            dgvFiles.DataSource = LoadFiles();
            dgvFiles.DataBind();
            objConn.Close();
        }

        protected void ddlFilePhoneFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = ddlFilePhoneFilter.SelectedItem.Value;
            ViewState["PhoneFileName"] = selectedValue;
            dgvFiles.DataSource = LoadFiles();
            dgvFiles.DataBind();
            objConn.Close();
        }

        protected void ddlFileAuthorFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = ddlFileAuthorFilter.SelectedItem.Value;
            ViewState["EmployeeFileName"] = selectedValue;
            dgvFiles.DataSource = LoadFiles();
            dgvFiles.DataBind();
            objConn.Close();
        }

        protected void txtFileDateFilter_OnTextChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = txtFileDateFilter.Text;
            ViewState["DateFileCreated"] = selectedValue;
            dgvFiles.DataSource = LoadFiles();
            dgvFiles.DataBind();
            objConn.Close();
        }

        protected void ddlImageVehicleFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = ddlImageVehicleFilter.SelectedItem.Value;
            ViewState["VehicleImageName"] = selectedValue;
            dgvImages.DataSource = LoadImages();
            dgvImages.DataBind();
            objConn.Close();
        }

        protected void ddlImageAuthorFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = ddlImageAuthorFilter.SelectedItem.Value;
            ViewState["EmployeeImageName"] = selectedValue;
            dgvImages.DataSource = LoadImages();
            dgvImages.DataBind();
            objConn.Close();
        }

        protected void txtImageDateFilter_OnTextChanged(object sender, EventArgs e)
        {
            openDBConnection();
            objConn.Open();
            string selectedValue = txtImageDateFilter.Text;
            ViewState["DateImageCreated"] = selectedValue;
            dgvImages.DataSource = LoadImages();
            dgvImages.DataBind();
            objConn.Close();
        }

        protected void dgvImages_OnSorting(object sender, GridViewSortEventArgs e)
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
            DataView sortedView = new DataView(LoadImages());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;

            dgvImages.DataSource = sortedView;
            dgvImages.DataBind();
        }
    }
}