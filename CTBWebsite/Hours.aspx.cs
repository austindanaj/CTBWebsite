using System.Data.SqlClient;
using Date = System.DateTime;
using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;

namespace CTBWebsite
{
    public partial class Hours : HoursPage
    {
        private DataTable projectData, projectHoursData, vehicleHoursData, vehiclesData, datesData;

        protected void Page_Load(object sender, EventArgs e)
        {
            
			if (Session["Alna_num"] == null) {
				redirectSafely("~/Default");
                promptAlertToUser("Need to be logged in to access Hours page", Color.DarkGoldenrod);
			    return;
			}

            if (!IsPostBack)
            {
                if (Session["Date"] == null)
                    initDate();
                if (Session["Active"] == null)
                    Session["Active"] = true;
                if (Session["SelectedDate"] == null)
                    Session["SelectedDate"] = 0;

                getDate();
                getData();
                ddlInit();
            }
            populateTables();                     
        }

        private void getDate()
        {
            Date date = (Date)Session["Date"];
            lblWeekOf.Text = "Week of " + date.ToString("M/d/yyyy");
        }

        private void getData()
        {
            object[] o = { Session["Date_ID"], DBNull.Value };
            if ((bool)Session["Active"])
                o[1] = true;

            projectData = getDataTable("select Project_ID, Name, Category from Projects where (Active=@value1 OR @value1 IS NULL) order by Projects.PriorityOrder", o[1]);
            vehiclesData = getDataTable("select ID, Name from Vehicles where (Active=@value1 OR @value1 IS NULL)", o[1]);
            projectHoursData = getDataTable("SELECT * FROM SelectProjectHours(@value1, @value2)", o);
            vehicleHoursData = getDataTable("SELECT * FROM SelectVehicleHours(@value1, @value2)", o);
            datesData = getDataTable("select ID, Dates from Dates order by Dates desc");
         
            if (projectData == null || vehiclesData == null || projectHoursData == null || vehicleHoursData == null || datesData == null)
            {
                promptAlertToUser("Problem accessing database; contact an admin");
            }
        }

        private void ddlInit()
        {
            ddlselectWeek.Items.Clear();
            ddlProjects.Items.Clear();
            ddlVehicles.Items.Clear();

            foreach (DataRow d in datesData.Rows)
                ddlselectWeek.Items.Add( new ListItem(((Date)d[1]).ToShortDateString(), d[0].ToString()));

            ddlselectWeek.SelectedIndex = (int) Session["selectedDate"];

            string id; string name, globalATesting = "BLE_Key_Pass_Global_A_Testing";
            foreach (DataRow r in projectData.Rows)
            {
                id = r[0].ToString();
                name = r[1].ToString();
                if (!(bool)Session["Full_time"] && name.Equals(globalATesting))
                    continue;
                ddlProjects.Items.Add(new ListItem(name.Replace("_", " "), id));
            }

            bool hasHoursWorked = false;
            int alna = (int)Session["Alna_num"];
            foreach (DataRow d in projectHoursData.Rows)
            {
                if (alna == (int)d[4])
                {
                    hasHoursWorked = true;
                    if (!(bool)Session["Full_time"] & d[2].Equals(globalATesting))
                        continue;
                    ddlWorkedHours.Items.Add(new ListItem(d[6] + " hours on " + d[2].ToString().Replace("_", " "), "P" + d[0].ToString()));
                }
            }
            if (hasHoursWorked) pnlDeleteHours.Visible = true;
            hoursUpdate();
            if (!(bool)Session["Vehicle"]) return;
            foreach (DataRow r in vehiclesData.Rows)
            {
                id = r[0].ToString();
                name = r[1].ToString();
                ddlVehicles.Items.Add(new ListItem(name.Replace("_", " "), id));
            }

            foreach (DataRow d in vehicleHoursData.Rows)
            {
                if (alna == (int)d[4])
                {
                    hasHoursWorked = true;
                    ddlWorkedHours.Items.Add(new ListItem(d[6] + " hours on " + d[2].ToString().Replace("_", " "), "V" + d[0].ToString()));
                }
            }
            if (hasHoursWorked) pnlDeleteHours.Visible = true;
            lblVehicleTitle.Visible = true;
            ddlVehicles.Visible = true;
            ddlHoursVehicles.Visible = true;
            btnSubmitVehicles.Visible = true;
            leftButton.Visible = true;
            rightButton.Visible = true;
        }

        protected void TriggerEvent(object sender, EventArgs e)
        {
            if (sender.Equals(btnselectWeek))
            {
                Session["Active"] = !chkInactive.Checked;
                Session["Date"] = Date.Parse(ddlselectWeek.SelectedItem.Text);
                Session["Date_ID"] = int.Parse(ddlselectWeek.SelectedValue);
                Session["Active"] = !chkInactive.Checked;
                Session["selectedDate"] = ddlselectWeek.SelectedIndex;
              
            }
            else if (sender.Equals(btnSubmitPercent))
            {
                insertRecord(int.Parse(ddlProjects.SelectedValue), int.Parse(ddlHours.SelectedValue), true);
            }
            else if (sender.Equals(btnSubmitVehicles))
            {
                insertRecord(int.Parse(ddlVehicles.SelectedValue), int.Parse(ddlHoursVehicles.SelectedValue), false);
            }
            else if (sender.Equals(btnDelete))
            {
                if (!txtDelete.Text.ToLower().Equals("yes"))
                    return;

                string selection = ddlWorkedHours.SelectedValue;
                string table = selection.Substring(0, 1).Equals("V") ? "VehicleHours" : "ProjectHours";
                int id = int.Parse(selection.Substring(1));

                object[] o;
                if (table.Equals("VehicleHours"))
                {
                    o = new [] { id, "BLE_Key_Pass_Global_A_Testing", Session["Alna_num"], Session["Date_ID"] };

                    if (!(bool)Session["Full_time"])
                        executeVoidSQLQuery("update ProjectHours set Hours_worked=Hours_worked-(select Hours_worked from VehicleHours where ID=@value1) where Proj_ID=(select Project_ID from Projects where Name=@value2) and Alna_num=@value3 and Date_ID=@value4", o);
                    executeVoidSQLQuery("delete from VehicleHours where ID=@value1", id);
                }
                else
                {
                    o = new object[] { Session["Alna_num"], id };
                    executeVoidSQLQuery("delete from " + table + " where Alna_num=@value1 and ID=@value2", o);
                }
               
            }
            redirectSafely("~/Hours");
        }

        protected void dgvProject_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            dgvProject.PageIndex = e.NewPageIndex;
            dgvProject.DataBind();
        }
 
        private bool insertRecord(int projectOrVehicle, int hours, bool isProject)
        {
            string table, readerQuery, insertionQuery;
            DataTable tableToUpdate;
            if (isProject)
            {
                table = "ProjectHours";
                readerQuery = "select ID, Hours_worked from ProjectHours where Alna_num=@value1 and Proj_ID=@value2 and Date_ID=@value3";
                insertionQuery = "insert into ProjectHours values(@value1, @value2, @value3, @value4)";
                tableToUpdate = projectData;
            }
            else
            {
                table = "VehicleHours";             
                readerQuery = "select ID, Hours_worked from VehicleHours where Alna_num=@value1 and Vehicle_ID=@value2 and Date_ID=@value3";
                insertionQuery = "insert into VehicleHours values(@value1, @value2, @value3, @value4)";
                tableToUpdate = vehiclesData;
            }

            try
            {
              //  objConn.Open();
                object[] o = { Session["Alna_num"], projectOrVehicle, Session["Date_ID"] };
                getReader(readerQuery, o);
                if (reader == null)
                    return false;

                if (reader.HasRows)
                {
                    reader.Read();
                    int hoursWorked = reader.GetInt32(1);
                    int otherRecordID = reader.GetInt32(0);
                    reader.Close();
                    executeVoidSQLQuery("delete from " + table + " where ID=@value1", otherRecordID);
                    hours += hoursWorked;
                }
                else
                {
                    reader.Close();
                }

                o = new object[] { o[0], projectOrVehicle, hours, o[2] };
                executeVoidSQLQuery(insertionQuery, o);
            }
            catch (Exception ex)
            {
                promptAlertToUser("Error: Cannot connect to database, check network connection, or contact admin");
                writeStackTrace("Hours Submit", ex);
                return false;
            }
            return true;
        }

        private void populateTables()
        {
            dgvProject.DataSource = getProjectHours(Session["Date_ID"], true, (bool)Session["Active"]);
            dgvProject.DataBind();
            dgvCars.DataSource = getVehicleHours(Session["Date_ID"], (bool)Session["Active"]);
            dgvCars.DataBind();
        }

        protected void color(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int n = e.Row.Cells.Count;
                int accumulator = 0;
                for (int i = 1; i < n; i++)
                {
                    string text = e.Row.Cells[i].Text;
                    if (text.Equals("&nbsp;"))  //If the cell is blank ASP puts this there for whatever reason
                        continue;
                    if (!int.TryParse(text, out int hour))
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7c7c");
                        e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                        return;
                    }
                    accumulator += hour;
                }
                if (accumulator != 40)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7c7c");
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#92f972");
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        private void hoursUpdate()
        {
            int hoursWorked = 0, totalHours = 0;

            Lambda howMuchHoursWorked = new Lambda(delegate (object o) {
                DataTable temp = (DataTable)o;
                int session = (int)Session["Alna_num"];
                foreach (DataRow d in temp.Rows)
                {
                    if ((int)d[4] == session)
                        hoursWorked += (int)d[6];
                    totalHours += (int)d[6];
                }
            });

            Lambda addDDLoptions = new Lambda(delegate (object o) {
                DropDownList temp = (DropDownList)o;
                float hoursSpent;
                for (int i = 1; i <= 40; i++)
                {
                    if (i + hoursWorked > 40)
                        break;
                    hoursSpent = ((float)i / 40) * 100;
                    temp.Items.Add(new ListItem("" + hoursSpent + "% -- " + i + " hours", i.ToString()));
                }
            });

            ddlHours.Items.Add(new ListItem("--Select A Percent (Out of 40 hrs)--", "-1"));
            howMuchHoursWorked(projectHoursData);
            addDDLoptions(ddlHours);
            lblUserHours.Text = "Your Hours: " + hoursWorked + "/40";
            if (hoursWorked == 40)
            {
                pnlAddHours.Style.Add("display", "none");
                Session["showProjectHours"] = false;
            }
            else
            {
                Session["showProjectHours"] = true;
            }

            if (!(bool)Session["Vehicle"])
            {
                Session["showVehicleHours"] = false;
                return;
            }

            Session["showVehicleHours"] = true;
            ddlHoursVehicles.Items.Add(new ListItem("--Select A Percent (Out of 40 hrs)--", "-1"));
            addDDLoptions(ddlHoursVehicles);
            lblUserHours.Text = "Logged " + hoursWorked + "/40";
        }
    }
}