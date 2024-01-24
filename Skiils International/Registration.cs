﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;

namespace Skiils_International
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=JGGUFWXBGP;Initial Catalog=Student;Integrated Security=True;");

        private void Registration_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query_select = "SELECT * from Registration";
                SqlCommand cmnd = new SqlCommand(query_select, conn);
                SqlDataReader row = cmnd.ExecuteReader();
                regNoInput.Items.Add("New Register");
                while (row.Read())
                {
                    regNoInput.Items.Add(row[0].ToString());
                }
                conn.Close();
            } catch(Exception ex)
            {
                conn.Close();
            }
        }

        private void regBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = firstNameInput.Text;
                string lastName = lastNameInput.Text;
                dobInput.Format = DateTimePickerFormat.Custom;
                dobInput.CustomFormat = "yyyy-MM-dd hh:mm:ss";
                string gender;
                if (maleRadio.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                string address = addressInput.Text;
                string email = emailInput.Text;
                int mobilePhone = int.Parse(mobilePhoneInput.Text);
                int homePhone = int.Parse(homePhoneInput.Text);
                string parentsName = parentNameInput.Text;
                string nic = nicInput.Text;
                int contactNo = int.Parse(contactNumberInput.Text);
                int regNo = new Random().Next(1, 10000);
                string query_insert = $"insert into Registration (regNo, firstName, lastName, dateOfBirth, gender, address, email, mobilePhone, homePhone, parentName, nic, contactNo) values ('{regNo}','{firstName}','{lastName}','{dobInput.Text}','{gender}','{address}','{email}',{mobilePhone},{homePhone},'{parentsName}','{nic}',{contactNo})";
                conn.Open();
                SqlCommand insertCmnd = new SqlCommand(query_insert, conn);
                insertCmnd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record Added Successfully!", "Registered Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string msg = "Insert Error: ";
                msg += ex.Message;
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string no = regNoInput.Text;
                if (no != "New Register" && no != "")
                {
                    string firstName = firstNameInput.Text;
                    string lastName = lastNameInput.Text;
                    dobInput.Format = DateTimePickerFormat.Custom;
                    dobInput.CustomFormat = "yyyy-MM-dd hh:mm:ss";
                    string gender;
                    if (maleRadio.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {
                        gender = "Female";
                    }
                    string address = addressInput.Text;
                    string email = emailInput.Text;
                    int mobilePhone = int.Parse(mobilePhoneInput.Text);
                    int homePhone = int.Parse(homePhoneInput.Text);
                    string parentName = parentNameInput.Text;
                    string nic = nicInput.Text;
                    int contactNo = int.Parse(contactNumberInput.Text);
                    string query_insert = $"update Registration set firstName = '{firstName}', lastName = '{lastName}', dateOfBirth = '{dobInput.Text}', gender = '{gender}', address = '{address}', email = '{email}', mobilePhone = '{mobilePhone}', homePhone = '{homePhone}', parentName = '{parentName}', nic = '{nic}', contactNo = '{contactNo}' WHERE regNo = '{no}'";
                    conn.Open();
                    SqlCommand cmnd = new SqlCommand(query_insert, conn);
                    cmnd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record Updated Successfully!", "Updated Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Select a Registration No!", "Invalid Registration Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } catch(Exception ex)
            {
                conn.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure, do you really want to exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {
                this.Close();
            }
        }

        private void logoutLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Close();
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string no = regNoInput.Text;
                if (no != "New Register" && no != "")
                {
                    var result = MessageBox.Show("Are you sure, do you really want to delete this record...?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string query_insert = "DELETE from Registration WHERE regNo = " + no + "";
                        conn.Open();
                        SqlCommand cmnd = new SqlCommand(query_insert, conn);
                        cmnd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Record Deleted Successfully!", "Deleted Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select a Registration No!", "Invalid Registration Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } catch(Exception ex)
            {
                conn.Close();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            regNoInput.Text = "";
            firstNameInput.Text = "";
            lastNameInput.Text = "";
            dobInput.Format = DateTimePickerFormat.Custom;
            dobInput.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            DateTime thisDay = DateTime.Today;
            dobInput.Text = thisDay.ToString();
            maleRadio.Checked = false;
            femaleRadio.Checked = false;
            addressInput.Text = "";
            emailInput.Text = "";
            mobilePhoneInput.Text = "";
            homePhoneInput.Text = "";
            parentNameInput.Text = "";
            nicInput.Text = "";
            contactNumberInput.Text = "";
        }

        private void regNoInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string selectedIndex = regNoInput.SelectedItem.ToString();
                string query_select = "SELECT * FROM Registration WHERE regNo = '" + selectedIndex + "'";
                SqlCommand cmnd = new SqlCommand(query_select, conn);
                SqlDataReader row = cmnd.ExecuteReader();
                while (row.Read())
                {
                    firstNameInput.Text = row[0].ToString();
                    lastNameInput.Text = row[1].ToString();
                    dobInput.Format = DateTimePickerFormat.Custom;
                    dobInput.CustomFormat = "yyyy-MM-dd hh:mm:ss";
                    dobInput.Text = row[2].ToString();
                    if (row[3].ToString() == "Male")
                    {
                        maleRadio.Checked = true;
                        femaleRadio.Checked = false;
                    }
                    else
                    {
                        femaleRadio.Checked = true;
                        maleRadio.Checked = false;
                    }
                    addressInput.Text = row[4].ToString();
                    emailInput.Text = row[5].ToString();
                    mobilePhoneInput.Text = row[6].ToString();
                    homePhoneInput.Text = row[7].ToString();
                    parentNameInput.Text = row[8].ToString();
                    nicInput.Text = row[9].ToString();
                    contactNumberInput.Text = row[10].ToString();
                }
                conn.Close();
            } catch(Exception ex)
            {
                conn.Close();
            }
        }

        private void populateFields(SqlDataReader row)
        {

        }
    }
}
