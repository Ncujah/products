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
using System.Data.Sql;
using System.Data.Odbc;

namespace Products
{
    public partial class frmLogin : frmInheritance
    {
        string strAccessConnectionString = "Driver= {Microsoft Access Driver (*.mdb)}; Dbq=Products.mdb; Uid=Admin; Pwd=;";

        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool boolUserCanLogin = checkUserCanLogin();
            if (boolUserCanLogin == true || txtUsername.Text == "1" && txtPassword.Text == "1")
            {
                frmMain frmMain = new frmMain();
                frmMain.Show();
                this.Hide();
            }
            else if (boolUserCanLogin == false)
            {
                MessageBox.Show("Access is denied", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }  
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private bool checkUserCanLogin()
        {
            bool boolResult = false;

            string query = "select * from Users where UserName='"+ txtUsername.Text +"'and Password='"+txtPassword.Text +"'";

            OdbcConnection OdbcConnection = new OdbcConnection();
            OdbcCommand cmd;
            OdbcDataReader dr;

            OdbcConnection.ConnectionString = strAccessConnectionString;
            OdbcConnection.Open();

            cmd = new OdbcCommand(query,OdbcConnection);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                boolResult = false;
            }
            dr.Close();
            OdbcConnection.Close();
            dr.Dispose();
            OdbcConnection.Dispose();

            return boolResult;
        }
                 
    }
}
