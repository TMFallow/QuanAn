using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace GUI
{
    public partial class MonAn : Form
    {
        MonAnBLL monAnBLL = new MonAnBLL();

        public MonAn()
        {
            InitializeComponent();
        }

        private void MonAn_Load(object sender, EventArgs e)
        {
            LoadMonAn();
        }

        public void LoadMonAn()
        {
            dataGridView_MonAn.DataSource = monAnBLL.LayMonAnBLL(); 
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            ThemMonAn fThemMon = new ThemMonAn();
            fThemMon.ShowDialog();
            LoadMonAn();
            dataGridView_MonAn.AllowUserToAddRows = false;
            dataGridView_MonAn.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnNhapMon_Click(object sender, EventArgs e)
        {
            NhapNguyenLieu fNhapNguyenLieu = new NhapNguyenLieu();
            fNhapNguyenLieu.ShowDialog();
            
        }

        private void dataGridView_MonAn_SelectionChanged(object sender, EventArgs e)
        {
            int rows = dataGridView_MonAn.CurrentCell.RowIndex;
            txtMaMonAn.Text = dataGridView_MonAn.Rows[rows].Cells[0].Value.ToString();
            cmbTenMonAn.Text = dataGridView_MonAn.Rows[rows].Cells[1].Value.ToString();
            cmbLoai.Text = dataGridView_MonAn.Rows[rows].Cells[2].Value.ToString();
            cmbDVT.Text = dataGridView_MonAn.Rows[rows].Cells[3].Value.ToString();
            txtSLNhap.Text = dataGridView_MonAn.Rows[rows].Cells[4].Value.ToString();
            txtGiaBan.Text = dataGridView_MonAn.Rows[rows].Cells[5].Value.ToString();
        }

        private void btnSuaGia_Click(object sender, EventArgs e)
        {
            txtGiaBan.Enabled = true;
            txtGiaBan.Focus();
            btnLưu.Enabled = true;
            btnNhapMon.Enabled = btnThemMon.Enabled = false;

        }

        private void txtGiaBan_Enter(object sender, EventArgs e)
        {
            
        }

        private void btnLưu_Click(object sender, EventArgs e)
        {
            try
            {
                monAnBLL.SuaGiaBanBLL(int.Parse(txtGiaBan.Text), int.Parse(txtMaMonAn.Text));
                LoadMonAn();
                MessageBox.Show("Sửa Thành Công");
                txtGiaBan.Enabled = false;
                btnLưu.Enabled = false;
                btnNhapMon.Enabled = btnThemMon.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
