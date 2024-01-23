using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            conn.Open();
            string query_select = "SELECT * from employee";
            SqlCommand cmnd = new SqlCommand(query_select, conn);
            SqlDataReader row = cmnd.ExecuteReader();
            regNoInput.Items.Add("New Register");
            while (row.Read())
            {
                regNoInput.Items.Add(row[0].ToString());
            }
            conn.Close();
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
                string contactNo = contactNumberInput.Text;
                string query_insert = $"insert into employee ('{firstName}','{lastName}','{dobInput.Text}','{gender}','{address}','{email}',{mobilePhone},{homePhone},'{parentsName}','{nic}','{contactNo}')";
                conn.Open();
                SqlCommand cmnd = new SqlCommand(query_insert, conn);
                cmnd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record Added Successfully!", "Regiestered Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string msg = "Insert Error: ";
                msg += ex.Message;
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
