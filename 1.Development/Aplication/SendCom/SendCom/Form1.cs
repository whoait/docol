using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace SendCom
{
    public partial class Login : Form
    {
         
        public Login()
        {
            InitializeComponent();
            //ConnectionStringSettings

        }

        private void Login_Load(object sender, EventArgs e)
        {
            

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetFields();
            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            CheckLogin();
        }


        private void ResetFields()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = (TextBox)ctrl;
                    if (tb != null)
                    {
                        tb.Text = string.Empty;
                    }
                }
                else if (ctrl is ComboBox)
                {
                    ComboBox dd = (ComboBox)ctrl;
                    if (dd != null)
                    {
                        dd.Text = string.Empty;
                        dd.SelectedIndex = -1;
                    }
                }
            }
        }

        public bool TestConnection()
        {
            // Tạo connection string để kết nối
            string connectionString = "server=" + Properties.Settings.Default.server + ";database=" + Properties.Settings.Default.database + ";Integrated Security=False;user=" + Properties.Settings.Default.user + ";pwd=" + Properties.Settings.Default.password + ";MultipleActiveResultSets=true";
            // Tạo một connection tới máy chủ
            SqlConnection conn = new SqlConnection(connectionString);

            // QUA TRINH KET NOI
            try
            {
                // Mở kết nối
                conn.Open();
                conn.Close();
                return false;
            }
            catch (SqlException sqle)
            {
                // Thông báo biến cố khi kết nối
                return true;
            }
           
        }
        private bool UserLogin()
        {
            // Tạo connection string để kết nối
            string connectionString = "server=" + Properties.Settings.Default.server + ";database=" + Properties.Settings.Default.database + ";Integrated Security=False;user=" + Properties.Settings.Default.user + ";pwd=" + Properties.Settings.Default.password + ";MultipleActiveResultSets=true";
            // Tạo một connection tới máy chủ
            SqlConnection conn = new SqlConnection(connectionString);
            string sqlSelect = "Select * from TM_TANTOSHA where TANTOSHA_CD = '" + txtNameLogin.Text + "' and PASSWORD = '" + txtPassword.Text + "'";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;          
            }
        }
        private void CheckLogin()
        {

            if (!TestConnection())
            {
                if (UserLogin())
                {
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("ログインが失敗しました。");
                    txtNameLogin.Text = "";
                    txtPassword.Text = "";
                    txtNameLogin.Focus();
                }
            }
            else
            {
                MessageBox.Show("接続エラー", "接続エラー",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNameLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckLogin();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckLogin();
            }
        }
 
    }

}
