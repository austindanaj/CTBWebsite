using System.Data.SqlClient;
using Date = System.DateTime;
using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Linq;


namespace CTBWebsite
{
    public partial class GlobalADefault : SuperPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTools();
            }
          
        }
        public void LoadTools()
        {
            SqlConnection objConn = openDBConnection();
            objConn.Open();

            SqlCommand sql = new SqlCommand("SELECT * FROM Tools", objConn);
            SqlDataAdapter adp = new SqlDataAdapter(sql);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ds.Tables[0].Columns.Add("FormDate", typeof(string));
            ds.Tables[0].Columns.Add("IconType", typeof(string));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DateTime date = (DateTime)ds.Tables[0].Rows[i]["Tool_Date"];
                string format = "MMM d, yyyy";
                ds.Tables[0].Rows[i]["FormDate"] = date.ToString(format);
                string type = (string)ds.Tables[0].Rows[i]["Tool_ContentType"];
                ds.Tables[0].Rows[i]["IconType"] = GetImageIcon(type);
                ds.Tables[0].Rows[i]["Tool_Description"] = ds.Tables[0].Rows[i]["Tool_Description"].ToString().Replace("\r\n", Environment.NewLine);
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
        protected void Trigger_FileUpload(object sender, EventArgs e)
        {
         //   mpeFileUpload.Show();

            if (sender.Equals(btnCalibrationFile))
            {
                lblTitle.Text = "Upload Calibration File";
            }
            else if (sender.Equals(btnTD1))
            {
                lblTitle.Text = "Upload TD1 File";
            }
            else if (sender.Equals(btnTD2))
            {
                lblTitle.Text = "Upload TD2 File";
            }
            else if (sender.Equals(btnTD3))
            {
                lblTitle.Text = "Upload TD3 File";
            }
            else if (sender.Equals(btnTD4))
            {
                lblTitle.Text = "Upload TD4 File";
            }
        }

        protected void btnUploadTool_Click(object sender, EventArgs e)
        {
            SqlConnection objConn = openDBConnection();
            objConn.Open();

            object file, filename = DBNull.Value, contentType = DBNull.Value;

            using (var binReader = new BinaryReader(fileUpload.FileContent))
            {
                file = binReader.ReadBytes((int)fileUpload.FileContent.Length);
            }
            filename = fileUpload.FileName;
            contentType = fileUpload.PostedFile.ContentType;
            object[] o;
            o = new object[] { txtFileName.Text, txtFileDescription.Text, txtVersion.Text, DateTime.Now, file, filename, contentType, Session["Alna_num"]};
            executeVoidSQLQuery("INSERT INTO Tools (Tool_Name, Tool_Description, Tool_Version, Tool_Date, Tool_Attachment, Tool_FileName, Tool_ContentType, Tool_EmployeeID) values" +
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM Tools WHERE Tool_ID=@toolId", objConn);
                cmd.Parameters.AddWithValue("@toolId", int.Parse(id));
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                string filename = reader.GetString(6);
                string extension = filename == null ? null : filename.Substring(filename.LastIndexOf('.'));
                string contentType = reader.GetString(7);
                byte[] blob = (byte[])reader["Tool_Attachment"];

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
    }
}