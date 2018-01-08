﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Date = System.DateTime;

namespace CTBWebsite {
	public delegate void Lambda(object o);
	public class SuperPage : Page {
		private readonly static string LOCALHOST_CONNECTION_STRING = "Data Source=(LocalDB)\\v13.0;Server = (localdb)\\MSSQLLocalDB;Database=Alps;";
		private readonly static string DEPLOYMENT_CONNECTION_STRING = "Server=(local);Database=CTBwebsite;User Id=admin;Password=alnatest;";
		public  readonly static string LOCAL_TO_SERVER_CONNECTION_STRING = "Data Source=ahfreya;Initial Catalog=CTBwebsite;Integrated Security=False;User ID=Admin;Password=alnatest;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public SqlConnection objConn { get; set; }
        private enum SqlTypes { DataTable, VoidQuery, DataReader };

        //Need to be deleted:
        public void successDialog(System.Web.UI.WebControls.TextBox txtSuccessBox)
        {
            if (Session["success?"] != null)
                txtSuccessBox.Visible = (bool)Session["success?"];
            Session["success?"] = false;
        }

        public void throwJSAlert(string s)
        {
            try
            {
                Response.Write("<script>alert('" + s + "');</script>");
            }
            catch (System.Web.HttpException h)
            {
                writeStackTrace("http exception throwingJs alert", h);
            }
        }

        //==========================================================
        // Basic functionality
        //==========================================================
        public void writeStackTrace(string s, Exception ex) {
			if (!File.Exists(@"" + Server.MapPath("~/Debug/StackTrace.txt"))) {
				File.Create(@"" + Server.MapPath("~/Debug/StackTrace.txt"));
			}
			using (StreamWriter file = new StreamWriter(@"" + Server.MapPath("~/Debug/StackTrace.txt"))) {
				file.WriteLine(Date.Today.ToString() + s + ex.ToString());
				file.Close();
			}
		}

		public void openDBConnection() {
			//this.objConn = new SqlConnection(LOCALHOST_CONNECTION_STRING);
			//this.objConn = new SqlConnection(DEPLOYMENT_CONNECTION_STRING);
			this.objConn = new SqlConnection(LOCAL_TO_SERVER_CONNECTION_STRING);
		}

        public void redirectSafely(string path)
        {
            try
            {
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception e)
            {
                writeStackTrace("problem redirecting", e);
            }
        }

        public void initDate()
        {
            bool state = objConn.State == ConnectionState.Closed;
            if (state)
                objConn.Open();

            SqlDataReader reader = getReader("select top 1 Dates, ID from Dates order by ID DESC;");
            if (reader == null) return;
            reader.Read();
            Date date = (Date)reader.GetValue(0);
            int id = (int)reader.GetValue(1);
            reader.Close();
            if (Date.Today > date.AddDays(6))
            {
                date = date.AddDays(7);
                while (Date.Today > date.AddDays(6))
                    date = date.AddDays(7);

                string sqlDateString = date.Year + "-" + date.Month + "-" + date.Day;
                executeVoidSQLQuery("insert into Dates (Dates.[Dates]) values (@value1)", sqlDateString);
                reader = getReader("select top 1 ID, Dates from Dates order by ID desc");

                if (reader == null) return;

                reader.Read();
                Session["Date_ID"] = (int)reader.GetValue(0);
                Session["Date"] = (Date)reader.GetValue(1);
                reader.Close();
            }
            else
            {
                Session["Date"] = date;
                Session["Date_ID"] = id;
            }

            if (state)
                objConn.Close();
        }

        private object sqlExecuter(object o, SqlTypes type) {
			try {
				switch (type) {
					case SqlTypes.DataReader:
						return ((SqlCommand)o).ExecuteReader();
					case SqlTypes.VoidQuery:
						return ((SqlCommand)o).ExecuteNonQuery();
					case SqlTypes.DataTable:
						object[] adapterAndDataSet = (object[])o;
						SqlDataAdapter objAdapter = (SqlDataAdapter)adapterAndDataSet[0];
						DataSet objDataSet = (DataSet)adapterAndDataSet[1];
						objAdapter.Fill(objDataSet);
						return objDataSet;
				}
			}
			catch (ObjectDisposedException e) {
				writeStackTrace("either the object you're using is closed or something insane happened like a race condition", e);
			}
			catch (InvalidOperationException e) {
				writeStackTrace("some sort of simple error just occurred (data was edited between execution, something was closed that shouldn't have been, etc.", e);
			}
			catch (InvalidCastException e) {
				writeStackTrace("Someone may have screwed the code up. Tell someone to check the stack traces", e);
			}
			catch (SqlException e) {
				SqlErrorCollection errors = e.Errors;

				bool failure = false, codeError = false;
				foreach (SqlError error in errors) {
					if (error.Class > 16) {
						failure = true;
						break;
					}
					else if (error.Class > 10) {
						codeError = true;
						break;
					}
				}

				writeStackTrace("sql exception in executeVoidQuery. Error codes from Microsoft are below. Error code above 16?" + failure + "; Else, error code above 10?" + codeError + "\n" +
								"Severity level of 10 or less: mistakes in information that a user has entered.\n" +
								"Severity levels from 11 through 16 are generated by the user, and can be corrected by the user\n" +
								"Severity levels from 17 through 25 indicate software or hardware errors\n" +
								"When a level 17, 18, or 19 error occurs, you can continue working, although you might not be able to execute a particular statement.\n", e);
			}
			catch (IOException e) {
				writeStackTrace("io in executeVoidQuery", e);
			}
			catch (Exception e) {
				writeStackTrace("executeVoidSQLQuery", e);
			}
			return null;
		}

        //==========================================================
        // Execute a query with no return value
        //==========================================================
        public void executeVoidSQLQuery(string command)
        {
            bool state = this.objConn.State == ConnectionState.Closed;
            if (state)
                this.objConn.Open();

            SqlCommand objCmd = new SqlCommand(command, this.objConn);
            sqlExecuter(objCmd, SqlTypes.VoidQuery);

            if (state)
                this.objConn.Close();
        }

        public void executeVoidSQLQuery(string command, object parameter)
        {
            bool state = this.objConn.State == ConnectionState.Closed;
            if (state)
                this.objConn.Open();

            SqlCommand objCmd = new SqlCommand(command, this.objConn);

            if (null != parameter)
            {
                objCmd.Parameters.AddWithValue("@value1", parameter);
            }
            sqlExecuter(objCmd, SqlTypes.VoidQuery);

            if (state)
                this.objConn.Close();
        }

        public void executeVoidSQLQuery(string command, object[] parameters) {
			bool state = this.objConn.State == ConnectionState.Closed;
			if (state)
				this.objConn.Open();

			SqlCommand objCmd = new SqlCommand(command, this.objConn);

			int i = 1;
			foreach (object s in parameters) {
				objCmd.Parameters.AddWithValue("@value" + i, s);
				i++;
			}
			sqlExecuter(objCmd, SqlTypes.VoidQuery);
			if (state)
				this.objConn.Close();
		}

        //==========================================================
        // Return datatable
        //==========================================================
        public DataTable getDataTable(string command) {
			if (objConn.State == ConnectionState.Closed)
				throw new Exception("You forgot to open the object connection. You have to leave it open until you're done with the data reader.");

			SqlDataAdapter objAdapter = new SqlDataAdapter();
			DataSet objDataSet = new DataSet();
			SqlCommand cmd = new SqlCommand(command, objConn);
			objAdapter.SelectCommand = cmd;
			object[] o = { objAdapter, objDataSet };
			objDataSet = (DataSet)sqlExecuter(o, SqlTypes.DataTable);

			return objDataSet == null ? null : objDataSet.Tables[0];
		}

        public DataTable getDataTable(string command, object parameter) {
			if (this.objConn.State == ConnectionState.Closed)
				throw new Exception("You forgot to open the object connection. You have to leave it open until you're done with the data reader.");

			SqlDataAdapter objAdapter = new SqlDataAdapter();
			DataSet objDataSet = new DataSet();
			SqlCommand cmd = new SqlCommand(command, objConn);
			if (null != parameter)
				cmd.Parameters.AddWithValue("@value1", parameter);
			objAdapter.SelectCommand = cmd;
			object[] o = { objAdapter, objDataSet };
			objDataSet = (DataSet)sqlExecuter(o, SqlTypes.DataTable);

			return objDataSet == null ? null : objDataSet.Tables[0];
		}

		public DataTable getDataTable(string command, object[] parameters) {
			if (objConn.State == ConnectionState.Closed)
				throw new Exception("You forgot to open the object connection. You have to leave it open until you're done with the data reader.");

			SqlDataAdapter objAdapter = new SqlDataAdapter();
			DataSet objDataSet = new DataSet();
			SqlCommand cmd = new SqlCommand(command, objConn);
			int i = 1;
			foreach (object s in parameters) {
				cmd.Parameters.AddWithValue("@value" + i, s);
				i++;
			}
			objAdapter.SelectCommand = cmd;
			object[] o = { objAdapter, objDataSet };
			objDataSet = (DataSet)sqlExecuter(o, SqlTypes.DataTable);

			return objDataSet == null ? null : objDataSet.Tables[0];
		}

        //==========================================================
        // Returns data reader
        //==========================================================
        public SqlDataReader getReader(string query)
        {
            if (objConn.State == ConnectionState.Closed)
                throw new Exception("You forgot to open the object connection. You have to leave it open until you're done with the data reader.");

            SqlCommand cmd = new SqlCommand(query, objConn);

            return (SqlDataReader)sqlExecuter(cmd, SqlTypes.DataReader); ;
        }

        public SqlDataReader getReader(string query, object parameters) {
			if (objConn.State == ConnectionState.Closed)
				throw new Exception("You forgot to open the object connection. You have to leave it open until you're done with the data reader.");

			SqlCommand cmd = new SqlCommand(query, objConn);
			if (parameters != null) {
				cmd.Parameters.AddWithValue("@value1", parameters);
			}

			return (SqlDataReader)sqlExecuter(cmd, SqlTypes.DataReader); ;
		}

		public SqlDataReader getReader(string query, object[] parameters) {
			if (objConn.State == ConnectionState.Closed)
				throw new Exception("You forgot to open the object connection. You have to leave it open until you're done with the data reader.");

			SqlCommand cmd = new SqlCommand(query, objConn);
			int i = 1;
			foreach (object o in parameters) {
				cmd.Parameters.AddWithValue("@value" + i, o);
				i++;
			}

			return (SqlDataReader)sqlExecuter(cmd, SqlTypes.DataReader);
		}
	}

	public class HoursPage : SuperPage {
		public DataTable getProjectHours(object date, bool includeFullTimers, bool isActive) {
			return getFormattedDataTable(date, includeFullTimers, true, isActive);
		}

		public DataTable getVehicleHours(object date, bool isActive) {
			return getFormattedDataTable(date, true, false, isActive);
		}

		//Can be date or dateID
		private DataTable getFormattedDataTable(object date, bool includeFullTimers, bool isProjectHours, bool isActive) {
			/*
			 * Need to put the partTimeEmployee/vehicle records into gridview.
			 * Due to really annoying limitations of SQL and C#, this is
			 * hard to do. Performance is also something I wanted to keep in mind because
			 * network latency and many SQL queries is an issue with this page more than any other.
			 * 
			 * We need this:
			 * +---------+---------+---------
			 * |theName  |project1 |moreProj  ...
			 * +---------+---------+---------
			 * |employee1|# of hrs | # of hrs ...
			 * +---------+---------+---------
			 * |employee2|# of hrs | # of hrs ...
			 * +---------+---------+---------
			 * |employee3|# of hrs | # of hrs ...
			 * +---------+---------+---------
			 * |employee4|# of hrs | # of hrs ...
			 * +---------+---------+---------
			 * You might think "We could do this in SQL with complicated joins", but you can't because it requires using rows in the Projects tables as the columns.
			 * Sql doesn't allow that, so we do it in C#, but it's a pain and potentially slow; BUT since the # of hours cells can be empty if the hours worked are 0,
			 * we can pull off ~Omega(2*#ofEmployees + #ofProjects + #ofVehicles), which is pretty damn good (still O(n^2), but this is 2D though, what can you expect)
			 * 
			 * 
			 * 
			 * Here's how it works:
			 * Step 1: take all the projects and put them in two things: hash table and the DataTable, as columns			 
			 * +------+------+-----+
			 * |name  |proj1 |proj2|	HashTable1(ProjID1 -> proj1'sIndexInTable (which is 1), ProjID2 -> proj2'sIndexInTable (which is 2), ...)
			 * +------+------+-----+
			 * 
			 * Step 2: This was the annoying part thanks to C#. The next intuitive step would be to start populating rows, but if we do that,
			 * then we cant edit them. So what I did instead was make a List<DataRow> so they can be edited. While I put them in there, I also put them in their own Hashtable.
			 * 
			 * The Datatable
			 * +------+------+-----+
			 * |name  | proj1|proj2|	HashTable1(ProjID1 -> proj1'sIndexInTable (which is 1), ProjID2 -> proj2'sIndexInTable (which is 2), ...)
			 * +------+------+-----+	HashTable2(
			 * 
			 * The List<DataRow>
			 * 
			 * List[0] = DataRow(Name: "Anthony Hewins", proj1: null, proj2: null, ...)
			 * List[1] = DataRow(Name: "Austin Danaj", proj1: null, proj2: null, ...)
			 * etc.
			 * 
			 * Step 3: using the hashtable, finally fill the DataTable:
			 * 
			 * Pretend Austin worked 3 hours for proj2. It would go like this
			 * 
			 * foreach DataRow in ProjectHours
			 *	 row# = Hashtable1.getWhatRowThisAlnaNumberIs(alna_num_supplied_from_ProjectHoursTable) //Remember: hash table takes the employee Alna and returns what row they are in the List
			 *	 col# = Hashtable2.getWhatColThisProjectIs(proj_ID_supplied_from_ProjectHoursTable)
			 *	 tempDatatable[row#][col#] = hours_spent_on_project_supplied_from_ProjectHoursTable
			 * 
			 *																  col#
			 *																	|
			 *																	V
			 * 
			 *			List[0] = DataRow(Name: "Anthony Hewins", proj1: null, proj2: null, ...)
			 *	row#->	List[1] = DataRow(Name: "Austin Danaj", proj1: null, proj2: 3, ...)
			 *	
			 *	
			 *	When done, the whole thing is filled.
			 *
			 *
			 * Step 4: Now the easy part, forall datarows in the List, add them to the DataTable
			 * Step 5: bind the data to the gridview at the very end
			 * Step 6: repeat for vehicles
			 */

			string constraint;
			if (date is Date) {
				date = (Date)date;
				constraint = "(select ID from Dates where Dates=@value1)";
			}
			else if (date is int) {
				date = (int)date;
				constraint = "@value1";
			}
			else
				return null;

			string hoursTable, innerID;
			if (isProjectHours) {
				hoursTable = "ProjectHours";
				innerID = "Proj_ID";
			}
			else {
				hoursTable = "VehicleHours";
				innerID = "Vehicle_ID";
			}


			if (objConn.State == ConnectionState.Closed)
                openDBConnection();
			bool state = objConn.State == ConnectionState.Closed;
			if (state) objConn.Open();
			DataTable employeesData;
			DataTable modelData;

			if (isActive) {
				if (isProjectHours) {
					modelData = getDataTable("select Project_ID, Abbreviation from Projects where Active=@value1 order by Projects.PriorityOrder", true);
					employeesData = getDataTable("select Alna_num, Name, Full_time from Employees where Active=@value1 Order By Name", true);
				}
				else {
					employeesData = getDataTable("select Alna_num, Name, Full_time from Employees where Active=@value1 and Vehicle=@value1 Order By Name", true);
					modelData = getDataTable("select ID, Abbreviation from Vehicles where Active=@value1", true);
				}
			}
			else {
				if (isProjectHours) {
					modelData = getDataTable("select Project_ID, Abbreviation from Projects order by Projects.PriorityOrder");
					employeesData = getDataTable("select Alna_num, Name, Full_time from Employees Order By Name");
				}
				else {
					modelData = getDataTable("select ID, Abbreviation from Vehicles");
					employeesData = getDataTable("select Alna_num, Name, Full_time from Employees where Vehicle=@value1 Order By Name", true);
				}
			}

			DataTable hoursData = getDataTable("select Alna_num, " + innerID + ", Hours_worked from " + hoursTable + " where Date_ID=" + constraint, date);
			if (state) objConn.Close();

			if (null == employeesData || null == modelData || null == hoursData)
				return null;

			int colAndRowTracker = 1;
			Dictionary<int, int> employeeHashTable = new Dictionary<int, int>();
			Dictionary<int, int> modelHashTable = new Dictionary<int, int>();        //I had to make 3 separate hash tables because there might be collisions

			DataTable modelDataTable = new DataTable();
			modelDataTable.Columns.Add("Name");
			foreach (DataRow d in modelData.Rows) {
				modelHashTable.Add((int)d[0], colAndRowTracker); //Add one because column 0 is name
				modelDataTable.Columns.Add((string)d[1]);
				colAndRowTracker++;
			}

			colAndRowTracker = 0;

			DataRow temp;
			List<DataRow> tempMatrix = new List<DataRow>();
			foreach (DataRow d in employeesData.Rows) {
                if (d[2].Equals(DBNull.Value))
                    continue;
				if ((bool)d[2] & !includeFullTimers)
					continue;
				employeeHashTable.Add((int)d[0], colAndRowTracker);
				temp = modelDataTable.NewRow();
				temp["Name"] = d[1];
				tempMatrix.Add(temp);
				colAndRowTracker++;
			}

			int whatCol = 0, whatRow;
			foreach (DataRow d in hoursData.Rows) {
				int alna = (int)d[0];
				if (!employeeHashTable.ContainsKey(alna))   //We skip full time employees since they will not appear in the Hashtable
					continue;
				try {
					whatCol = modelHashTable[(int)d[1]];
				}
				catch (KeyNotFoundException) {
					continue; //The project must be inactive that you're looking at, just skip it
				}
				whatRow = employeeHashTable[alna];
				tempMatrix[whatRow][whatCol] = d[2];
			}

			foreach (DataRow d in tempMatrix)
				modelDataTable.Rows.Add(d);

			return modelDataTable;
		}
	}

	public class SchedulePage : SuperPage {
		protected void populateInternSchedules(GridView gridView, DropDownList ddl) {
			/* This function is responsible for populating the schedule tables. It's rather complicated and needs to be fast, so I will break it down here because
			 * commenting in-line will just be confusing. (Runtime: Omicron(n^3), Omega(n^2))
			 * 
			 * 1. First we just get employee information. Nothing special here. We just need to use Linkedlists first because the amount of employees we have may change.
			 *	  Then we convert them to arrays for fast access. Important property to note is when they get converted to arrays it looks like this:
			 *	  
			 *					Index 0 of either
			 *					array is my alna
			 *					and name, important
			 *					property later on
			 *						|
			 *						V
			 *	  int[] alna = { 173017			,...
			 *	  int[] name = {"Anthony Hewins",...
			 *	  
			 *						 same here too
			 *	  int[] alna =		{173017,173017,...				<-This is alna #
			 *	  int[] startTime = {800   ,800   ,...				<-This is military time (least significant digits cannot ever be greater than 59, database checks for it)
			 *	  int[] endTime =	{1700  ,1700  ,...				<-This is military time
			 *	  int[] dayofweek = {1	   ,2	  ,...				<-Day of the week (Sunday=0, Monday=1, ...)
			 *	  
			 * 2. Next is schedule info, same exact thing.
			 * 3. Afterwards we start populating the table with columns. Columns should look like this:
			 *		+-------------------------+-------------------------+-------------------------+
			 *		|	(Day of the week)	  |			Anthony Hewins	|		Employee 2		  | .....
			 *		+-------------------------+-------------------------+-------------------------+
			 * 4. Afterwards we have to add rows. Since we get the schedule information and the employee info with ORDER BY ID ASC, we know that the table columns will almost correspond to
			 *	  the schedule data in the array. I say almost because having two rows in the database for the same day screws it up a bit since you need to check more records. Here's what happens:
			 *	  
			 *	  
			 *		Pretend we're working on this cell		int i = currentRecordInTheScheduleArray		
			 *				   |							if alna[i] == row10am.column[1]
			 *				   v								if youre_working_during_this_time(starttime[i], endtime[i])
			 *		  +------+----+----------+						row10am[1] = working
			 *		  |Monday|You |Other dude|					else
			 *		  +------+----+----------+						continue
			 *		  |8:00am|Work|Off		 |
			 *		  |9:00am|Off |Work		 |
			 *	->	  |10:00 |	  |			 |
			 *			...
			 *			...
			 *			...
			 */

			if (Session["weekday"] == null) {
				int dayOfWeek = (int)Date.Today.DayOfWeek;
				if (dayOfWeek == 0 | dayOfWeek == 6)
					Session["weekday"] = 1;
				else
					Session["weekday"] = dayOfWeek;
			}
			ddl.SelectedIndex = (int)Session["weekday"] - 1;

			//1. First get Alna nums and names
			List<int> temp_alna_nums = new List<int>();
			List<string> temp_names = new List<string>();
			SqlDataReader reader = getReader("select Alna_num, Name from Employees where Active=@value1 and Full_time!=@value1 order by Alna_num asc", true);
			while (reader.Read()) {
				temp_alna_nums.Add(reader.GetInt32(0));
				temp_names.Add(reader.GetString(1));
			}
			reader.Close();
			int[] alna_nums = temp_alna_nums.ToArray();     //We want fast O(1) access because we are going to be doing a good amount of computation
			string[] names = temp_names.ToArray();

			//2. Get schedule data
			temp_alna_nums = new List<int>();
			List<int> temp_timestart_list = new List<int>();
			List<int> temp_timeend_list = new List<int>();
			reader = getReader("select Alna_num, TimeStart, TimeEnd from Schedule where DayOfWeek=@value1 order by Alna_num asc", Session["weekday"]);
			while (reader.Read()) {
				temp_alna_nums.Add(reader.GetInt32(0));
				temp_timestart_list.Add(reader.GetInt16(1));
				temp_timeend_list.Add(reader.GetInt16(2));
			}
			reader.Close();
			int[] schedule_alna_nums = temp_alna_nums.ToArray();
			int[] timestart = temp_timestart_list.ToArray();
			int[] timeend = temp_timeend_list.ToArray();

			//3. Populate columns
			DataTable table = new DataTable();
			string[] weekdays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
			table.Columns.Add(weekdays[(int)Session["weekday"] - 1]);
			foreach (string name in names)
				table.Columns.Add(name);

			//4. Table population
			DataRow d;
			int[] workday = { 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800 };
			int tableColumns = table.Columns.Count;
			int scheduleColumns = schedule_alna_nums.Length;
			int alnaNumLength = alna_nums.Length;
			bool endOfSearch = false;
			foreach (int i in workday) {
				d = table.NewRow();
				d[0] = military_to_standard(i);
				for (int j = 0; j < alnaNumLength; j++) {
					for (int k = 0; k < scheduleColumns; k++) {
						if (alna_nums[j] == schedule_alna_nums[k]) {
							endOfSearch = true;
							if (i <= timestart[k] & i + 100 > timestart[k])
								d[j + 1] = "In @" + military_to_standard(timestart[k]);
							else if (i <= timeend[k] & i + 100 > timeend[k])
								d[j + 1] = "Out @" + military_to_standard(timeend[k]);
							else if (i > timestart[k] & i <= timeend[k])
								d[j + 1] = "Working";
						}
						else if (endOfSearch) {
							endOfSearch = false;
							break;
						}
					}
				}
				table.Rows.Add(d);
			}

			gridView.DataSource = table;
			gridView.DataBind();
		}

		protected void color(object sender, GridViewRowEventArgs e) {
			if (e.Row.RowType == DataControlRowType.DataRow) {
				for (int i = 1; i < e.Row.Cells.Count; i++) {
					string cellText = e.Row.Cells[i].Text;
					if (string.IsNullOrEmpty(cellText)) continue;
					if (cellText.Equals("Working")) {
						e.Row.Cells[i].BackColor = System.Drawing.Color.LightSlateGray;
						e.Row.Cells[i].ForeColor = System.Drawing.Color.WhiteSmoke;
					}
					else if (cellText.Contains("In ") | cellText.Contains("Out ")) {
						e.Row.Cells[i].BackColor = System.Drawing.Color.CornflowerBlue;
						e.Row.Cells[i].ForeColor = System.Drawing.Color.WhiteSmoke;
					}
				}
			}
		}

		protected string military_to_standard(int time) {
			string period_of_day = time >= 1200 ? "pm" : "am";
			if (time >= 1300)
				time -= 1200;
			string s = time.ToString();
			if (s.Length == 3)
				s = s[0] + ":" + s.Substring(1, 2);
			else
				s = s.Substring(0, 2) + ":" + s.Substring(2, 2);
			return s + period_of_day;
		}
	}

    public class IOPage : SuperPage
    {
        protected enum Tables {Report, File, Image};

        protected void write(IOPage.Tables table, object[] insertionData, byte[] bytes)
        {
            string selectQuery, insertionQuery;
            switch (table)
            {
                case Tables.File:
                    selectQuery = "select top 1 ID, Name, Path from Report order by ID desc";
                    insertionQuery = "exec Insert_File @value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8";
                    break;
                case Tables.Report:
                    selectQuery = "select top 1 ID, Name, Path from GA_File order by ID desc";
                    insertionQuery = "exec Insert_Report @value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8, @value9, @value11, @value12";
                    break;
                default:
                    selectQuery = "";
                    insertionQuery = "";
                    break;
            }

            // Two things need to happen here: we need to insert and then try to write the file. If the insertion fails, then we stop there.
            // If the insertion succeeds and the write fails, we need to remove it from the database because the file already exists. This
            // requires that we delete the most recently inserted file.
            int id = executeVoidSQLQuery(insertionQuery, insertionData);

            try
            {
                SqlDataReader reader = getReader(selectQuery);

                id = reader.GetInt32(0);
                string path = reader.GetString(1);
                string filename = reader.GetString(2);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (BinaryWriter writer = new BinaryWriter(File.Open(path + filename, FileMode.Create)))
                {
                    writer.Write(bytes);
                }
            }
            catch (Exception ex)
            {
                writeStackTrace("Error writing file", ex);
                try
                {
                    if (id == -1)
                        new Exception("Couldn't figure out the id of the latest inserted file to write it to the server");
                    executeVoidSQLQuery("delete from Report where ID=@value1", id);
                } catch (Exception e)
                {
                    writeStackTrace("FATAL: couldn't write file and the record still exists in the DB!", e);
                }
            }
        }

        protected void open()
        {

        }
    }
}