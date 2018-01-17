using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using Date = System.DateTime;
using System.Web.UI.HtmlControls;

namespace CTBWebsite
{
    public partial class SiteMaster : MasterPage
    {
      //  private enum SqlTypes { DataTable, VoidQuery, DataReader };
        private SuperPage sql;
        SqlConnection objConn;

        protected void Page_Load(object sender, EventArgs e)
        {
            sql = new SuperPage();
            if (Session["loginStatus"] == null)
            {
                Session["loginStatus"] = "Sign In";
            }
            else if ((string)Session["loginStatus"] == "Sign In")
            {
                btnLogin.Visible = true;
                btnLogout.Visible = false;
            }
            else
            {
                btnLogin.Visible = false;
                btnLogout.Visible = true;
            }


            if (Session["Alna_num"] == null)
                Session["admin"] = false;

            if (Session["admin"] != null)
                if ((bool)Session["admin"])
                    admin.Visible = true;

            if (Session["Full_time"] != null)
                if (!(bool)Session["Full_time"])
                    lstSchedule.Visible = true;

            if (Session["Vehicle"] == null)
                Session["Vehicle"] = false;

            if (Session["UserMessageText"] != null)
            {
                userMessage.Style.Add("display", "block");
                txtUserMessage.Text = (string)Session["UserMessageText"];
                Session["UserMessageText"] = null;
                if (Session["UserMessageColor"] != null)
                {
                    userMessage.Style.Add("background", (string) Session["UserMessageColor"]);
                    Session["UserMessageColor"] = null;
                }
            }
            
            if (!IsPostBack)
            {
                if (Request.Cookies["userName"] != null && Session["Alna_num"] == null)
                {
                    string userName = Request.Cookies["userName"].Value;
                    Response.Cookies["userName"].Value = userName;
                    Response.Cookies["userName"].Expires = DateTime.Now.AddDays(10);
             
                    SqlDataReader reader = sql.getReader("Select Alna_num, Name, Full_time, Vehicle from Employees where Employees.[Name]=@value1;", Server.HtmlEncode(Request.Cookies["userName"].Value));
                    reader.Read();
                    Session["Alna_num"] = reader.GetValue(0);
                    Session["Name"] = reader.GetValue(1);
                    Session["Full_time"] = reader.GetValue(2);
                    Session["Vehicle"] = reader.GetValue(3);
                    Session["loginStatus"] = Session["Name"];

                    btnLogin.Visible = false;
                    btnLogout.Visible = true;
                }
            }

        }


        private void loadEmployees()
        {
            SqlDataReader reader = sql.getReader("SELECT Name FROM Employees where Active=@value1 Order By Name;", true);

            while (reader.Read())
            {
                ddl.Items.Add(reader.GetString(0));
            }
        }
    
        protected void Open_Login(Object sender, EventArgs e)
        {            
            mpeLogin.Show();
            loadEmployees();

        }
        protected void Is_SignedIn(Object sender, EventArgs e)
        {
            if (Session["Alna_num"] == null)
            {
                mpeLogin.Show();
                loadEmployees();
            }
            else
            {
                HtmlAnchor a = (HtmlAnchor)sender;
                switch (a.InnerText)
                {
                    case "Hours":
                        sql.redirectSafely("~/Hours");
                        break;
                    case "Time Off":
                        sql.redirectSafely("~/TimeOff");
                        break;
                    case "Purchase List":
                        sql.redirectSafely("~/List");
                        break;
                    case "Phone Checkout":
                        sql.redirectSafely("~/PhoneCheckOut");
                        break;
                    case "Issues":
                        sql.redirectSafely("~/IssueList");
                        break;
                    case "Global A":
                        sql.redirectSafely("~/GlobalADefault");
                        break;
                }
            }
        }
        protected void Sign_Out(Object sender, EventArgs e)
        {

            HttpCookie aCookie = Request.Cookies["userName"];
            aCookie.Value = null;
            aCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(aCookie);
            Session.Clear();
            Session["loginStatus"] = "Sign In";
            sql.redirectSafely("~/");
            
        }
        protected void Login_Clicked(Object sender, EventArgs e)
        {
            try
            {
                object[] o = { inputUsername.Value, inputPassword.Value };
                SqlDataReader reader = sql.getReader("SELECT User, Admin FROM Accounts WHERE Accounts.[User]=@value1 and Accounts.[Pass]=@value2", o);
                if (reader == null)
                {
                    sql.promptAlertToUser("Error: Cannot access database, please contact admin", Color.Empty);
                    sql.redirectSafely("~/");
                    return;
                }
                if (!reader.HasRows)
                {
                    sql.promptAlertToUser("Error: Incorrect username or password", Color.Empty);
                    sql.redirectSafely("~/");
                    reader.Close();
                    return;
                }
                reader.Read();
                Session["Admin"] = reader.GetBoolean(1);
                reader.Close();
                reader = sql.getReader("Select Alna_num, Name, Full_time, Vehicle from Employees where Employees.[Name]=@value1;", ddl.Text);
                reader.Read();
                Session["Alna_num"] = reader.GetValue(0);
                Session["Name"] = reader.GetValue(1);
                Session["Full_time"] = reader.GetValue(2);
                Session["Vehicle"] = reader.GetValue(3);
                Session["loginStatus"] = Session["Name"];

                HttpCookie aCookie = new HttpCookie("userName");
                aCookie.Value = reader.GetValue(1).ToString();
                aCookie.Expires = DateTime.Now.AddDays(10);
                Response.Cookies.Add(aCookie);
                sql.redirectSafely("~/");
            }
            catch (Exception ex)
            {
                sql.promptAlertToUser("Error: Cannot login at the time please try again later...If problem persits, please contact admin", Color.Empty);
                writeStackTrace("Login", ex);
                sql.redirectSafely("~/");
            }
        }
        private void writeStackTrace(string s, Exception ex)
        {
            if (!File.Exists(@"" + Server.MapPath("~/Debug/StackTrace.txt")))
            {
                File.Create(@"" + Server.MapPath("~/Debug/StackTrace.txt"));
            }
            using (StreamWriter file = new StreamWriter(@"" + Server.MapPath("~/Debug/StackTrace.txt")))
            {
                file.WriteLine(Date.Today.ToString() + s + ex.ToString());
                file.Close();
            }
        }
     
    }
}