﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Text.RegularExpressions;

using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace CTBWebsite
{
    public partial class Admin : SuperPage
    {
        //SqlConnection objConn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Alna_num"] == null)
            {
                redirectSafely("~/Default");
                return;
            }
            
            if (!IsPostBack)
            {
                populateTables();
            }
        }

        protected void User_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                promptAlertToUser("Error: name blank! Please fill in all fields!");
                return;
            }

            if (!int.TryParse(txtAlna.Text, out int alna))
            {
                promptAlertToUser("Input is not a number", Color.DarkGoldenrod);
                redirectSafely("~/Admin");
                return;
            }

            string text = txtName.Text;
            if (!Regex.IsMatch(text, @"[A-z]+ [A-z]+"))
            {
                promptAlertToUser("Name may only contain letters", Color.DarkGoldenrod);
                redirectSafely("~/Admin");
                return;
            }

            object[] o = { alna, txtName.Text, !chkPartTime.Checked, chkUseVehicle.Checked | chkPartTime.Checked };

            executeVoidSQLQuery("INSERT INTO Employees (Alna_num, Name, Full_Time, Vehicle) VALUES (@value1, @value2, @value3, @value4);", o);
            promptAlertToUser("Success", Color.ForestGreen);
        }

        protected void Project_Clicked(object sender, EventArgs e)
        {
            string text = txtProject.Text;
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            char projectCategory;
            switch (category.SelectedIndex)
            {
                case 0:
                    projectCategory = 'A';
                    break;
                case 1:
                    projectCategory = 'B';
                    break;
                case 2:
                    projectCategory = 'C';
                    break;
                case 3:
                    projectCategory = 'D';
                    break;
                default:
                    return;
            }

            object[] parameters = { text.Replace(" ", "_"), projectCategory, txtAbbreviation.Text };
            executeVoidSQLQuery("INSERT INTO Projects (Name, Category, Abbreviation) VALUES (@value1, @value2, @value3);", parameters);

            promptAlertToUser("Success", Color.ForestGreen);
        }

        protected void Car_Clicked(object sender, EventArgs e)
        {
            string text = txtCar.Text;
            if (string.IsNullOrEmpty(text) | string.IsNullOrEmpty(txtCarAbbreviation.Text))
            {
                promptAlertToUser("Car needs a name and an abbreviation", Color.DarkGoldenrod);
                redirectSafely("~/Admin");
                return;
            }

            executeVoidSQLQuery("INSERT INTO Vehicles (Name, Abbreviation) VALUES (@value1, @value2);", new object[] { text.Replace(" ", "_"), txtCarAbbreviation.Text });
            promptAlertToUser("Success", Color.ForestGreen);
        }
        protected void remove(object sender, EventArgs e)
        {
            string command;
            string text;

            if (sender.Equals(btnRemoveVehicle))
            {
                command = "Update Vehicles set Active=@value1 WHERE ID=@value2;";
                text = txtRemoveVehicle.Text;
            }
            else if (sender.Equals(btnRemoveUser))
            {
                command = "Update Employees set Active=@value1 where Alna_num=@value2";
                text = txtRemoveUser.Text;
            }
            else if (sender.Equals(btnRemoveProject))
            {
                command = "Update Projects set Active=@value1 WHERE Project_ID=@value2;";
                text = txtRemoveProject.Text;
            }
            else
            {
                writeStackTrace("Not implemented", new ArgumentException("This button isn't implemented"));
                return;
            }

            if (!int.TryParse(text, out int id))
            {
                promptAlertToUser("Not an integer!", Color.Empty);
                redirectSafely("~/Admin");    
                return;
            }
            object[] args = { false, id };
            executeVoidSQLQuery(command, args);
            promptAlertToUser("Success", Color.ForestGreen);
        }

        private void populateTables()
        {
            /*
			 * this method takes care of populating all tables in a generic way.
			 * pass in an object[] with param 1 being the SQL command, the second being
			 * the gridview to populate and it'll do all 3.
			 */
            Lambda populate = new Lambda(delegate (object o) {
                object[] args = (object[])o;
                GridView g = (GridView)args[1];
                g.DataSource = getDataTable((string)args[0], true);
                g.DataBind();
            });
          
            object[] parameters = { "SELECT Alna_num, Employees.[Name], Full_time, Vehicle from Employees where Active=@value1 ORDER BY Alna_num", dgvUsers };
            populate(parameters);
            parameters[0] = "SELECT ID, Vehicles.[Name] FROM Vehicles where Active=@value1;";
            parameters[1] = dgvCars;
            populate(parameters);
            parameters[0] = "SELECT Project_ID, Name, Category FROM Projects where Active=@value1;";
            parameters[1] = dgvProjects;
            populate(parameters);
        }
    }
}