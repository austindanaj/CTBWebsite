using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Date = System.DateTime;
using System.Web;
using System.Web.UI.WebControls;

namespace CTBWebsite
{
    public partial class _Default : SchedulePage
    {
        private delegate string Lambda1(int time);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                openDBConnection();
                objConn.Open();
                SqlDataReader reader = getReader("select Dates from Dates order by Dates desc");
                if (reader == null)
                {
                    throwJSAlert("Can't connect to DB; contact admin");
                    return;
                }
                while (reader.Read())
                    ddlselectWeek.Items.Add(reader.GetDateTime(0).ToShortDateString());
                reader.Close();
                populatePieChart(objConn);
                populateDaysOffTable(objConn);
                populateInternSchedules(dgvSchedule, ddlSelectScheduleDay);
                objConn.Close();
            }
        }



        //----------------------------------------------------------------
        // Inits
        //----------------------------------------------------------------
        private void populatePieChart(SqlConnection objConn)
        {
            SqlDataReader reader = getReader("select p1.[Hours_worked], p2.Category from ProjectHours p1 inner join Projects p2 on p2.Project_ID=p1.Proj_ID where p1.Date_ID=(select top 1 ID from Dates order by Dates desc);");
            if (!reader.HasRows)
            {

                //chartPercent.Visible = false;
                reader.Close();
                return;
            }
            double[] projectHours = new double[4];

            int hours, totalHours = 0;
            while (reader.Read())
            {
                hours = reader.GetInt32(0);
                if (reader.GetString(1).Equals("A"))
                    projectHours[0] += hours;
                else if (reader.GetString(1).Equals("B"))
                    projectHours[1] += hours;
                else if (reader.GetString(1).Equals("C"))
                    projectHours[2] += hours;
                else
                    projectHours[3] += hours;
                totalHours += hours;
            }
            reader.Close();

            for (int i = 0; i < projectHours.Length; i++)
                projectHours[i] /= totalHours;

            prgAdvDev.Style.Add("width", "" + (100 *Math.Round(projectHours[0], 2)) + "%");
            spanAdvDev.InnerText = (100 * Math.Round(projectHours[0], 2)) + "%";

            prgProd.Style.Add("width", "" + (100 * Math.Round(projectHours[2], 2)) + "%");
            spanProd.InnerText = (100 * Math.Round(projectHours[2], 2)) + "%";

            prgDiM.Style.Add("width", "" + (100 * Math.Round(projectHours[3], 2)) + "%");
            spanDiM.InnerText = (100 * Math.Round(projectHours[3], 2)) + "%";

            prgTOff.Style.Add("width", "" + (100 * Math.Round(projectHours[1], 2)) + "%");
            spanTOff.InnerText = (100 * Math.Round(projectHours[1], 2)) + "%";
        }

        private void populateDaysOffTable(SqlConnection objConn)
        {
            dgvOffThisWeek.DataSource = getDataTable("select e.Name as 'Name' from Employees e where e.Alna_num in (select Alna_num from TimeOff where TimeOff.[Start] >= (select top 1 Dates from Dates order by Dates desc) and TimeOff.[End] <=  (select dateadd(dd, 4, (select top 1 Dates from Dates order by Dates desc)) as NewDate));");
            dgvOffThisWeek.DataBind();
        }

        //----------------------------------------------------------------
        // HTML events
        //----------------------------------------------------------------
        protected void toetruck(object sender, EventArgs e)
        {
            redirectSafely("~/ToeTruck");
        }

        protected void download(object sender, EventArgs e)
        {
            if (!Date.TryParse(ddlselectWeek.SelectedValue, out Date date))
            {
                throwJSAlert("Not a valid date");
                return;
            }

            HoursPage h = new HoursPage();
            DataTable projectDataTable = h.getProjectHours(date, true, true);
            DataTable vehicleDataTable = h.getVehicleHours(date, true);

            if (projectDataTable == null | vehicleDataTable == null)
            {
                throwJSAlert("Data could not be downloaded; some sort of error");
                return;
            }

            //Write file then transmit it
            try
            {
                string s, fileName = @"" + Server.MapPath("~/Logs/" + Date.Today.Year + "-" + Date.Today.Month + "-" + Date.Today.Day + "_DBLog.csv");
                File.Create(fileName).Dispose();
                StreamWriter file = new StreamWriter(fileName);

                Lambda addColumns = new Lambda(delegate (object o) {
                    DataTable tmp = (DataTable)o;
                    s = "";
                    foreach (DataColumn d in tmp.Columns)
                        s += d.ToString() + ",";
                    file.Write(s);
                    file.WriteLine();
                });

                Lambda insertRows = new Lambda(delegate (object o) {
                    DataTable tmp = (DataTable)o;
                    foreach (DataRow d in tmp.Rows)
                    {
                        s = "";
                        foreach (object obj in d.ItemArray)
                            s += obj.ToString() + ",";
                        file.Write(s);
                        file.WriteLine();
                    }
                });

                addColumns(projectDataTable);
                insertRows(projectDataTable);
                file.WriteLine();
                addColumns(vehicleDataTable);
                insertRows(vehicleDataTable);

                file.Close();

                Response.ContentType = "Application/txt";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.TransmitFile(fileName);

                HttpResponse response = HttpContext.Current.Response; //These 4 lines kill the response without any exceptions
                response.Flush();
                response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                writeStackTrace("Something wrong in file writing", ex);
                throwJSAlert("Something wrong with the directory structure/file IO; contact an admin");
            }
        }

        protected void changeScheduleDay(object sender, EventArgs e)
        {
            Session["weekday"] = ddlSelectScheduleDay.SelectedIndex + 1;
            
            objConn.Open();
            populateInternSchedules(dgvSchedule, ddlSelectScheduleDay);
            objConn.Close();
        }

        protected void dgvOffThisWeek_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            dgvOffThisWeek.PageIndex = e.NewPageIndex;
            dgvOffThisWeek.DataBind();
        }
    }
}