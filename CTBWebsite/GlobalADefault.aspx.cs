using System.Data.SqlClient;
using Date = System.DateTime;
using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Linq;
using System.Web.UI;


namespace CTBWebsite
{
    public partial class GlobalADefault : SuperPage
    {
        private SqlConnection objConn;

        private enum FILE_TYPE
        {
            CALIBRATION = 0,
            TD1 = 1,
            TD2 = 2,
            TD3 = 3,
            TD4 = 4
        }
     

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                objConn = openDBConnection();
                objConn.Open();
               
                dgvReports.DataSource = LoadReports();
                dgvReports.DataBind();

                dgvFiles.DataSource = LoadFiles();
                dgvFiles.DataBind();
                
                LoadImages();
                LoadTools();
                objConn.Close();
                
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
                LinkButton lnkTD1= row.FindControl("lnkReportTD1") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkTD1);
                LinkButton lnkTD2= row.FindControl("lnkReportTD2") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkTD2);
                LinkButton lnkTD3 = row.FindControl("lnkReportTD3") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkTD3);
                LinkButton lnkTD4 = row.FindControl("lnkReportTD4") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkTD4);
            }
        }
        public DataTable LoadReports()
        {
            DataTable dt = getDataTable("SELECT * FROM SelectReport() WHERE Active='1'", null, objConn);
            return dt;

         //   SqlCommand sql = new SqlCommand("SELECT * FROM SelectReport() WHERE Active='1'", objConn);
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
            DataTable dt = getDataTable("SELECT * FROM SelectFile() WHERE Active='1'", null, objConn);
            //ds.Tables[0].Columns.Add("FormDate", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int type = (int) dt.Rows[i]["F_Type"];
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
                }
                //DateTime date = (DateTime)ds.Tables[0].Rows[i]["Date_Created"];
              //  string format = "MMM d, yyyy";
               // ds.Tables[0].Rows[i]["FormDate"] = date.ToString(format);
            }
           
            return dt;


        }

        public void LoadImages()
        {
            SqlCommand sql = new SqlCommand("SELECT * FROM SelectImages() WHERE Active='1'", objConn);
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
            */
            dgvImages.DataSource = ds;
            dgvImages.DataBind();
            sql.Dispose();
        }
        public void LoadTools()
        {
            SqlCommand sql = new SqlCommand("SELECT * FROM Tools", objConn);
         
            SqlDataAdapter adp = new SqlDataAdapter(sql);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ds.Tables[0].Columns.Add("FormDate", typeof(string));
            ds.Tables[0].Columns.Add("IconType", typeof(string));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DateTime date = (DateTime)ds.Tables[0].Rows[i]["Date"];
                string format = "MMM d, yyyy";
                ds.Tables[0].Rows[i]["FormDate"] = date.ToString(format);
                string type = (string)ds.Tables[0].Rows[i]["ContentType"];
                ds.Tables[0].Rows[i]["IconType"] = GetImageIcon(type);
                ds.Tables[0].Rows[i]["Description"] = ds.Tables[0].Rows[i]["Description"].ToString().Replace("\r\n", Environment.NewLine);
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
            else if (type.Contains("apk"))
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
      

        protected void btnUploadTool_Click(object sender, EventArgs e)
        {
            SqlConnection objConn = openDBConnection();
            objConn.Open();

            object file, filename = DBNull.Value, contentType = DBNull.Value;

            using (var binReader = new BinaryReader(toolUpload.FileContent))
            {
                file = binReader.ReadBytes((int)toolUpload.FileContent.Length);
            }
            filename = toolUpload.FileName;
            contentType = toolUpload.PostedFile.ContentType;
            object[] o;
            o = new [] { txtFileName.Text, txtFileDescription.Text, txtVersion.Text, DateTime.Now, file, filename, contentType, Session["Alna_num"]};
            executeVoidSQLQuery("INSERT INTO Tools (Name, Description, Version, Date, Attachment, FileName, ContentType, EmployeeID) values" +
                                                      "(@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8)", o, objConn);

        }


      
        protected void lstTools_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (String.Equals(e.CommandName, "Download_Tool"))
            {
                ListViewDataItem item = (ListViewDataItem) e.Item;
                string id = e.CommandArgument.ToString();


                SqlConnection objConn = openDBConnection();
                objConn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Tools WHERE ID=@toolId", objConn);
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




            SqlConnection objConn = openDBConnection();
            objConn.Open();
            SqlDataReader reader = getReader("SELECT * FROM Vehicles", null, objConn);
            while (reader.Read())
            {
                ddlVehicles.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }
            reader.Close();
            reader = getReader("SELECT * FROM Phones", null, objConn);
            while (reader.Read())
            {
                ddlPhones.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }
            reader.Close();
            reader = getReader("SELECT * FROM Employees WHERE Active='1'", null, objConn);
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

            SqlConnection objConn = openDBConnection();
            objConn.Open();
            SqlDataReader reader = getReader("SELECT * FROM Vehicles", null, objConn);
            while (reader.Read())
            {
                ddlFileVehicle.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }
            reader.Close();
            reader = getReader("SELECT * FROM Phones", null, objConn);
            while (reader.Read())
            {
                ddlFilePhone.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }
            reader.Close();
            reader = getReader("SELECT * FROM Employees", null, objConn);
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
        protected void UploadImage_OnClick(object sender, EventArgs e)
        {
            mpeImages.Show();
            LoadImageDropdowns();
        }
        public void LoadImageDropdowns()
        {
            ddlImageVehicle.Items.Clear();
            ddlImageVehicle.Items.Add(new ListItem("-- Select a Vehicle --", "-1"));

            SqlConnection objConn = openDBConnection();
            objConn.Open();
            SqlDataReader reader = getReader("SELECT * FROM Vehicles", null, objConn);
            while (reader.Read())
            {
                ddlImageVehicle.Items.Add(new ListItem(reader.GetString(1), reader.GetValue(0).ToString()));
            }
            reader.Close();
            reader.Dispose();

            objConn.Close();
            objConn.Dispose();

        }
        protected void UploadTool_OnClick(object sender, EventArgs e)
        {
            mpeTools.Show();
        }

        protected void dgvReports_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExecuteCommand(e);
        }
        protected void dgvFiles_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExecuteCommand(e);
        }

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

        protected void btnSubmitReport_OnClick(object sender, EventArgs e)
        {
           


        }

        public void FillReportFileTypes()
        {
            if (ddlVehicles.SelectedIndex != 0 && ddlPhones.SelectedIndex != 0)
            {
                ClearReportDropdowns();

                SqlConnection objConn = openDBConnection();
                objConn.Open();
                object [] o = {int.Parse(ddlPhones.SelectedValue), int.Parse(ddlVehicles.SelectedValue)};

                SqlDataReader reader = getReader("SELECT * FROM GA_File WHERE Phone_ID=@value1 AND Vehicle_ID=@value2", o, objConn);
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
        protected void ddlVehicles_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FillReportFileTypes();
        }

        protected void ddlPhones_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FillReportFileTypes();
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


        protected void dgvImages_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExecuteCommand(e);
        }

        protected void dgvFiles_OnSorting(object sender, GridViewSortEventArgs e)
        {
            objConn = openDBConnection();
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
        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }


        protected void dgvReports_OnSorting(object sender, GridViewSortEventArgs e)
        {
            objConn = openDBConnection();
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

        protected void ddlVehicleReportFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}