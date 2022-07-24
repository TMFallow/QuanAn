using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace GUI
{
    public partial class CauHinh : Form
    {
        ConfigDatabase conf = new ConfigDatabase();
        public CauHinh()
        {
            InitializeComponent();
        }

        private void CauHinh_Load(object sender, EventArgs e)
        {
            
        }

        private void cmbDatabase_DropDown(object sender, EventArgs e)
        {
            cmbDatabase.DataSource = conf.getDBName(cmbServer.Text, txtUserName.Text, txtPass.Text);
            cmbDatabase.DisplayMember = "Name";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            conf.luuLaiChuoiCauHinh(cmbServer.Text, txtUserName.Text, txtPass.Text, cmbDatabase.Text);
            MessageBox.Show("Đã thay đổi cấu hình.");
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbServer_DropDown(object sender, EventArgs e)
        {
            cmbServer.DataSource = conf.layTenServer();
            cmbServer.DisplayMember = "Server Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



     
    }
}
