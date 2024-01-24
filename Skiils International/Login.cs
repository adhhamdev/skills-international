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

namespace Skiils_International
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=JGGUFWXBGP;Initial Catalog=Student;Integrated Security=True;");

        private void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string username = loginUsername.Text;
                string password = loginPass.Text;
                string query_select = $"SELECT * FROM Login WHERE username = '{username}' AND password = '{password}'";
                SqlCommand cmnd = new SqlCommand(query_select, conn);
                SqlDataReader row = cmnd.ExecuteReader();
                if (row.HasRows)
                {
                    this.Hide();
                    Registration regForm = new Registration();
                    regForm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Login Credentials, Please check Username, Password and try again ! ", "Invalid Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            } catch(Exception ex)
            {
                conn.Close();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            loginUsername.Clear();
            loginPass.Clear();
            loginUsername.Focus();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure, do you really want to exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
