using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace SendCom
{
    public partial class MainForm : Form
    {
        String ServerName = Properties.Settings.Default.server;
        String DBName = Properties.Settings.Default.database;
        String NameLogin = Properties.Settings.Default.user;
        String Password = Properties.Settings.Default.password;
        List<DataSendCom> dataSendCom = new List<DataSendCom>();
        bool Data_Exist;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'demoSendComDataSet.Documents' table. You can move, or remove it, as needed.
            ///this.documentsTableAdapter.Fill(this.demoSendComDataSet.Documents);
            load_Data();
            dataGridView2.Columns["ファイル番号"].Width = 170;
            if (Data_Exist)
            {
                dataGridView2.EnableHeadersVisualStyles = false;
                this.dataGridView2.CurrentCell.Selected = false;
                btnSend.Enabled = true;
            }
            else
            {
                btnSend.Enabled = false;
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            SendData(ConvertDataSendPortCom(dataSendCom));

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            if (loginForm.TestConnection())
            {
                MessageBox.Show("接続エラー", "接続エラー",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                load_Data();
                if (Data_Exist)
                {
                    dataGridView2.EnableHeadersVisualStyles = false;
                    this.dataGridView2.CurrentCell.Selected = false;
                    btnSend.Enabled = true;
                }
                else
                {
                    btnSend.Enabled = false;
                }

            }

        }

        private void load_Data()
        {
            dataSendCom.Clear();
            // Tạo connection string để kết nối
            string connectionString = "server=" + ServerName + ";database=" + DBName + ";Integrated Security=False;user=" + NameLogin + ";pwd=" + Password + ";MultipleActiveResultSets=true";
            // Tạo một connection tới máy chủ
            SqlConnection conn = new SqlConnection(connectionString);

            // QUA TRINH KET NOI
            try
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select  (RACK_NO + FILE_NO) AS ファイル番号, CHASSIS_NO AS 車台番号 from TT_DOC_AUTO_SEARCH", conn);
                DataTable dtable = new DataTable();
                sda.Fill(dtable);
                dataGridView2.DataSource = dtable;


                SqlCommand sda1 = new SqlCommand("select RACK_NO,FILE_NO  from TT_DOC_AUTO_SEARCH", conn);
                SqlDataReader reader = sda1.ExecuteReader();
                Data_Exist = false;
                while (reader.Read())
                {
                    var DataSendCom = new DataSendCom();
                    DataSendCom.Rack_No = reader["RACK_NO"].ToString();
                    DataSendCom.File_No = reader["File_No"].ToString();
                    dataSendCom.Add(DataSendCom);
                    Data_Exist = true;
                }


                sda.Dispose();
                dtable.Dispose();
                conn.Dispose();
            }
            catch (SqlException sqle)
            {
                // Thông báo biến cố khi kết nối
                MessageBox.Show("接続エラー", "接続エラー",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> ConvertDataSendPortCom(List<DataSendCom> dataSendCom)
        {
            List<string> dataConvert = new List<string>();
            dataConvert.Clear();
            for (int i = 0; i < dataSendCom.Count; i++)
            {
                dataConvert.Add("WR0" + dataSendCom[i].Rack_No.ToString() + dataSendCom[i].File_No.ToString() + "&");
            }

            return dataConvert;
        }
        private void SendData(List<string> dataConvert)
        {
            SerialPort ComPort = new SerialPort(Properties.Settings.Default.noCom, int.Parse(Properties.Settings.Default.baudRate), Parity.Even, 8, StopBits.One);
            try
            {      
                ComPort.Handshake = Handshake.None;
                //string[] ListClearFile = { "0001", "0002", "0003", "0004" };
                //Get all port on this system 
                string[] numberPort = SerialPort.GetPortNames();
                string store = string.Empty;
           
                    foreach (string portName in numberPort) 
                    { 
                        store = store + portName; 
                    }
                    if (!string.IsNullOrEmpty(store))
                    {
                                   
                            if (!ComPort.IsOpen)
                                ComPort.Open();
                            for (int i = 0; i < dataConvert.Count; i++)
                            {
                                //ComPort.Write(ConvertToHex(dataConvert[i]));
                                byte[] array = Encoding.ASCII.GetBytes(ConvertToHex(dataConvert[i]));
                                ComPort.Write(array, 0, 14);
                                if (Properties.Settings.Default.timeSend == null)
                                {
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    Thread.Sleep(int.Parse(Properties.Settings.Default.timeSend));
                                }

                            }
                            MessageBox.Show("データ送信完了");               
                    }
            }
            catch
            {
                //show message eror: Error  Open ComPort 
                MessageBox.Show("Comポートオープン時のエラー", "Comポートオープン時のエラー",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ComPort.Dispose();
                ComPort.Close();

            }
        }

        public string ConvertToHex(string asciiString)
        {
            string hex = "";
            int tmpsumRack = 0;
            string sumRack = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
                tmpsumRack += tmp;
            }
            sumRack = String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmpsumRack.ToString()));
            sumRack = sumRack.Remove(0, (sumRack.Length - 2));
            sumRack = sumRack.ToUpper();
            hex = "(" + asciiString + sumRack;
            hex += ")\r";
            return hex;
        }

    }
}
