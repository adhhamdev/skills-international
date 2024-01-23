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

        SqlConnection conn = new SqlConnection(@"Data Source=JGGUFWXBGP;Initial Catalog=Student;Integrated Security=True;Trust Server Certificate=True");

        private void loginBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
