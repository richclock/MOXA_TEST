using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MOXA_CSharp_MXIO;

namespace MOXA_TEST
{
    public partial class Form1 : Form
    {
        Int32[] _hConnection = new Int32[1];
        string _password = "";
        uint _timeOut = 3000;
        bool _isConnected = false;
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            string ip = textBox1.Text;
            bool status = false;
            int ret;
            // 擷取卡預設PORT為"502".
            ret = MXIO_CS.MXEIO_Init();
            ret = MXIO_CS.MXEIO_E1K_Connect(System.Text.Encoding.UTF8.GetBytes(ip), 502, _timeOut, _hConnection, Encoding.UTF8.GetBytes(_password));
            //ioLogik.CheckErr(ret, "MXEIO_E1K_Connect");
            if (ret == MXIO_CS.MXIO_OK) {
                _isConnected = true;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (!_isConnected) {
                MessageBox.Show("Not Connect");
                return;
            }

            byte bytStartChannel = Convert.ToByte(int.Parse(textBox2.Text));
            byte byteCount = 1;
            uint[] dwGetDIValue = new uint[1];
            var ret = MXIO_CS.E1K_DI_Reads(_hConnection[0], bytStartChannel, byteCount, dwGetDIValue);
            if (ret == MXIO_CS.MXIO_OK)
            {
                MessageBox.Show(dwGetDIValue[0].ToString());
            }
        }
    }
}
