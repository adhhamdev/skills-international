﻿using System;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            regBtn.Enabled = true;
            updateBtn.Enabled = false;
            delBtn.Enabled = false;
            try
            {
                conn.Open();
                string query_select = "SELECT * FROM Registration";
                SqlCommand command = new SqlCommand(query_select, conn);
                SqlDataReader row = command.ExecuteReader();
                regNoInput.Items.Add("New Register");
                while (row.Read())
                {
                    regNoInput.Items.Add(row[0].ToString());
                }
                regNoInput.SelectedIndex = 0;
                conn.Close();
            }
            catch (Exception ex)
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
                regNoInput.Items.Clear();
                string query_select = "SELECT * FROM Registration";
                SqlCommand command = new SqlCommand(query_select, conn);
                SqlDataReader row = command.ExecuteReader();
                regNoInput.Items.Add("New Register");
                while (row.Read())
                {
                    regNoInput.Items.Add(row[0].ToString());
                }
                conn.Close();
                MessageBox.Show("Record Added Successfully!", "Registered Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                conn.Close();
                string msg = "Insert Error: ";
                msg += ex.Message;
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string no = regNoInput.Text;
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
                SqlCommand command = new SqlCommand(query_insert, conn);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record Updated Successfully!", "Updated Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
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
        }

        private void logoutLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            Login obj = new Login();
            obj.Show();
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string no = regNoInput.Text;
                var result = MessageBox.Show("Are you sure, do you really want to delete this record...?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query_insert = "DELETE from Registration WHERE regNo = " + no + "";
                    conn.Open();
                    SqlCommand cmnd = new SqlCommand(query_insert, conn);
                    cmnd.ExecuteNonQuery();
                    MessageBox.Show("Record Deleted Successfully!", "Deleted Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    regNoInput.Items.Clear();
                    string query_select = "SELECT * FROM Registration";
                    SqlCommand command = new SqlCommand(query_select, conn);
                    SqlDataReader row = command.ExecuteReader();
                    regNoInput.Items.Add("New Register");
                    while (row.Read())
                    {
                        regNoInput.Items.Add(row[0].ToString());
                    }
                    clearBtn.PerformClick();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
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

        private void regBtn_EnabledChanged(object sender, EventArgs e)
        {
            if (regBtn.Enabled)
            {
                regBtn.ForeColor = System.Drawing.Color.White;
                regBtn.BackColor = System.Drawing.Color.RoyalBlue;
            }
            else
            {
                regBtn.ForeColor = System.Drawing.Color.Gray;
                regBtn.BackColor = System.Drawing.Color.DarkGray;
            }
        }

        private void updateBtn_EnabledChanged(object sender, EventArgs e)
        {
            if (updateBtn.Enabled)
            {
                updateBtn.ForeColor = System.Drawing.Color.RoyalBlue;
                updateBtn.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            }
            else
            {
                updateBtn.ForeColor = System.Drawing.Color.Gray;
                updateBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            }
        }

        private void delBtn_EnabledChanged(object sender, EventArgs e)
        {
            if (delBtn.Enabled)
            {
                delBtn.ForeColor = System.Drawing.Color.White;
                delBtn.BackColor = System.Drawing.Color.Crimson;
            }
            else
            {
                delBtn.ForeColor = System.Drawing.Color.Gray;
                delBtn.BackColor = System.Drawing.Color.DarkGray;
            }
        }

        private void regNoInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string selectedIndex = regNoInput.SelectedItem.ToString();
                if (selectedIndex != "New Register")
                {
                    regBtn.Enabled = false;
                    updateBtn.Enabled = true;
                    delBtn.Enabled = true;
                    string query_select = "SELECT * FROM Registration WHERE regNo = '" + selectedIndex + "'";
                    SqlCommand command = new SqlCommand(query_select, conn);
                    SqlDataReader row = command.ExecuteReader();
                    while (row.Read())
                    {
                        firstNameInput.Text = row[1].ToString();
                        lastNameInput.Text = row[2].ToString();
                        dobInput.Format = DateTimePickerFormat.Custom;
                        dobInput.CustomFormat = "yyyy-MM-dd hh:mm:ss";
                        dobInput.Text = row[3].ToString();
                        if (row[4].ToString() == "Male")
                        {
                            maleRadio.Checked = true;
                            femaleRadio.Checked = false;
                        }
                        else
                        {
                            femaleRadio.Checked = true;
                            maleRadio.Checked = false;
                        }
                        addressInput.Text = row[5].ToString();
                        emailInput.Text = row[6].ToString();
                        mobilePhoneInput.Text = row[7].ToString();
                        homePhoneInput.Text = row[8].ToString();
                        parentNameInput.Text = row[9].ToString();
                        nicInput.Text = row[10].ToString();
                        contactNumberInput.Text = row[11].ToString();
                    }
                }
                else
                {
                    clearBtn.PerformClick();
                    regBtn.Enabled = true;
                    updateBtn.Enabled = false;
                    delBtn.Enabled = false;
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }
    }
}
