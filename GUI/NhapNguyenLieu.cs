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
    public partial class NhapNguyenLieu : Form
    {
        MonAnBLL monAnBLL = new MonAnBLL();
        NguyenLieuBLL nguyenLieuBLL = new NguyenLieuBLL();
        DataTable dtTenNguyenLieu = new DataTable();

        public NhapNguyenLieu()
        {
            InitializeComponent();
        }

        private void NhapNguyenLieu_Load(object sender, EventArgs e)
        {
            dataGridView_MonAn.DataSource = loadMonAn();
            dataGridView_NguyenLieu.DataSource = loadNguyenLieu();
        }

        public DataTable loadMonAn()
        {
            return monAnBLL.LayMonAnBLL();
        }

        public DataTable loadNguyenLieu()
        {
            return nguyenLieuBLL.loadNguyenLieu();
        }

        private void dataGridView_MonAn_SelectionChanged(object sender, EventArgs e)
        {
            int rows = dataGridView_MonAn.CurrentCell.RowIndex;
            txtTenMonAn.Text = dataGridView_MonAn.Rows[rows].Cells[1].Value.ToString();
            txtSoLuong.Text = dataGridView_MonAn.Rows[rows].Cells[4].Value.ToString();
        }

        private void btnCheBen_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Nguyên Liệu Cần Tốn " + LayTenMonAn(txtTenMonAn.Text) + "", "Bạn Có Muốn Chế Biến", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    nguyenLieuBLL.cheBienMonAnBLL(int.Parse(txtSoLuong.Text), txtTenMonAn.Text);
                    nguyenLieuBLL.NguyenLieuDaCheBienBLL(txtTenMonAn.Text);
                    dataGridView_MonAn.DataSource = monAnBLL.LayMonAnBLL();
                    dataGridView_NguyenLieu.DataSource = nguyenLieuBLL.loadNguyenLieu();
                    MessageBox.Show("Chế Biến Thành Công!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string LayTenMonAn(string tenMonAn)
        {
            string tenNguyenLieu = "";
            DataTable dt = new DataTable();
            dt = nguyenLieuBLL.getTenNguyenLieuBLL(tenMonAn);
            int i = 0;
            foreach(DataRow r in dt.Rows)
            {
       
                tenNguyenLieu = tenNguyenLieu + dt.Rows[i][0].ToString() + ", " ;
                i++;
            }
            return tenNguyenLieu;
        }


        private void dataGridView_NguyenLieu_SelectionChanged(object sender, EventArgs e)
        {
            int rows = dataGridView_NguyenLieu.CurrentCell.RowIndex;
            cmbTenNguyenLieu.Text = dataGridView_NguyenLieu.Rows[rows].Cells[1].Value.ToString();
            txtSoLuongNguyenLieu.Text = dataGridView_NguyenLieu.Rows[rows].Cells[2].Value.ToString();
            cmbDVTNguyenLieu.Text = dataGridView_NguyenLieu.Rows[rows].Cells[3].Value.ToString();
            txtGiaNguyenLieu.Text = dataGridView_NguyenLieu.Rows[rows].Cells[4].Value.ToString();
        }

        private void btnNhapNguyenLieu_Click(object sender, EventArgs e)
        {
            try
            {
                txtSoLuongNguyenLieu.Text = "";
                txtSoLuongNguyenLieu.Focus();
                btnLuu.Enabled = true;
                btnThemMoiNguyenLieu.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoLuongNguyenLieu.Text))
            {
                MessageBox.Show("Không được để trống số lượng");
                this.txtSoLuongNguyenLieu.Focus();
                return;
            }

            try
            {
                nguyenLieuBLL.ThemHoaDonNhapBLL(cmbTenNguyenLieu.Text, int.Parse(txtSoLuongNguyenLieu.Text));
                dataGridView_NguyenLieu.DataSource = nguyenLieuBLL.loadNguyenLieu();
                btnLuu.Enabled = false;
                btnThemMoiNguyenLieu.Enabled = true;
                MessageBox.Show("Thêm Thành Công");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnThemMoiNguyenLieu_Click(object sender, EventArgs e)
        {
            ThemNLMoi fThemNLMoi = new ThemNLMoi();
            fThemNLMoi.ShowDialog();
            dataGridView_NguyenLieu.DataSource = nguyenLieuBLL.loadNguyenLieu();
        }

        private void cbMonAnCanNhap_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMonAnCanNhap.Checked)
            {
                dataGridView_MonAn.DataSource = monAnBLL.loadMonAnCanNhap();
            }
            else
            {
                dataGridView_MonAn.DataSource = loadMonAn();
            }
        }

        private void cbNguyenLieuCanNhap_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNguyenLieuCanNhap.Checked)
            {
                dataGridView_NguyenLieu.DataSource = nguyenLieuBLL.loadDSNguyenLieuCanNhapBLL();
            }
            else
            {
                dataGridView_NguyenLieu.DataSource = loadNguyenLieu();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
