using System;
using System.Drawing;

namespace CTBWebsite
{
    public partial class Schedule : SchedulePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (Session["Alna_num"] == null) {
				redirectSafely("~/Default");
				return;
			}

         
            if (!IsPostBack)
            {
                populateInternSchedules(dgvSchedule, ddlSelectScheduleDay);
                populateScheduledHoursDdl();
            }
        }

        private void populateScheduledHoursDdl()
        {
            ddlScheduledHours.Items.Clear();

            object[] o = { Session["Alna_num"], Session["weekday"] };
            getReader("select ID, TimeStart, TimeEnd from Schedule where Alna_num=@value1 and DayOfWeek=@value2", o);

            while (reader.Read())
            {
                ddlScheduledHours.Items.Add("ID#" + reader.GetInt32(0) + ">" + military_to_standard(reader.GetInt16(1)) + " - " + military_to_standard(reader.GetInt16(2)));
            }
            killConnections();
        }

        //----------------------------------------------------------------
        // HTML events
        //----------------------------------------------------------------
        protected void changeScheduleDay(object sender, EventArgs e)
        {
            Session["weekday"] = ddlSelectScheduleDay.SelectedIndex + 1;
            populateInternSchedules(dgvSchedule, ddlSelectScheduleDay);
            populateScheduledHoursDdl();
        }

        protected void saveOrDelete(object sender, EventArgs e)
        {
            if (sender.Equals(btnConfirmTime))
            {
                int temp = -1;
                Lambda parse = new Lambda(delegate (object o) {
                    bool isStartTime = (bool)o;
                    try
                    {
                     
                        if (isStartTime)
                        {
                            string[] valueStart = txtStartTime.Value.Split(' ');
                            int mStart = 0;
                            if (valueStart[1] == "PM")
                            {
                                mStart = 1;
                            }

                            temp = int.Parse(valueStart[0].Replace(":", ""));
                            temp += temp >= 1200 ? 0 : (mStart * 1200);
                        }
                        else
                        {
                            string[] valueEnd = txtEndTime.Value.Split(' ');
                            int mEnd = 0;
                            if (valueEnd[1] == "PM")
                            {
                                mEnd = 1;
                            }
                            temp = int.Parse(valueEnd[0].Replace(":", ""));
                            temp += temp >= 1200 ? 0 : (mEnd * 1200);
                        }

                        if (temp < 1859 & temp >= 700 | temp % 100 < 60) return;
                    }
                    catch
                    {
                        writeStackTrace("Looks like 1 of 2 things happened here: someone tried hacking the parser for the time in saveOrDelete or there's a logic messup somewhere, but the first case is more likely", new ArgumentException());
                    }
                    temp = -1;
                });
                parse(true);
                if (temp == -1)
                {
                    promptAlertToUser("Start time is not a correct time format", Color.DarkGoldenrod);
                    return;
                }
                int start = temp;
                parse(false);
                if (temp == -1)
                {
                    promptAlertToUser("End time is not a correct time format", Color.DarkGoldenrod);
                    return;
                }
                int end = temp;

                if (start >= end)
                {
                    promptAlertToUser("End time cannot be before start time", Color.DarkGoldenrod);
                    return;
                }

                if (start + 100 > end)
                {
                    promptAlertToUser("You should schedule yourself for over an hour at least", Color.DarkGoldenrod);
                    return;
                }

                if (start < 600 | end > 1900)
                {
                    promptAlertToUser("You are starting too early or ending too late. Earliest start is 6am, latest time you can be here is 7pm.", Color.DarkGoldenrod);
                    return;
                }
              
                object[] obj = { Session["Alna_num"], ddlDay.SelectedIndex + 1 };
                getReader("select TimeStart, TimeEnd from Schedule where Alna_num=@value1 and DayOfWeek=@value2", obj);
                while (reader.Read())
                {
                    int compareStart = reader.GetInt16(0), compareEnd = reader.GetInt16(1);
                    if ((compareStart <= start & compareEnd >= start) | (compareStart <= end & compareEnd >= end) | (compareStart >= start & end >= compareEnd))
                    {
                        killConnections();
                        promptAlertToUser("Error: Time conflicts with your schedule");
                        return;
                    }
                }
                killConnections();

                obj = new object[] { Session["Alna_num"], start, end, ddlDay.SelectedIndex + 1 };
                executeVoidSQLQuery("insert into Schedule (Alna_num, TimeStart, TimeEnd, DayOfWeek) values (@value1, @value2, @value3, @value4)", obj);
                promptAlertToUser("Successfully added new schedule entry", Color.DarkGreen);
             //   throwJSAlert("Successfully added new schedule entry");                
            }
            else
            {
                string string_id = ddlScheduledHours.SelectedValue.Substring(3, ddlScheduledHours.SelectedValue.IndexOf(">") - ddlScheduledHours.SelectedValue.IndexOf("#") - 1);
                if (!int.TryParse(string_id, out int id))
                {
                    promptAlertToUser("Error: Something was wrong with the dropdown selection. It wasn't an integer...contact admin");
                    return;
                }
             
                executeVoidSQLQuery("delete from Schedule where ID=@value1", id);
                populateInternSchedules(dgvSchedule, ddlSelectScheduleDay);
                populateScheduledHoursDdl();
            }
        }
    }
}