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
using System.Data;
using System.Data.SqlClient;

namespace GUI
{
    public partial class NhanVien : Form
    {
        NhanVienBLL nv = new NhanVienBLL();
        public NhanVien()
        {
            InitializeComponent();
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            loadDSNhanVienLenDataGridview();
        }

        public void loadDSNhanVienLenDataGridview()
        {
            dataGridView_DanhSachNhanVien.DataSource = nv.getAllNhanVienBLL();
        }

        private void dataGridView_DanhSachNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView_DanhSachNhanVien.CurrentCell.RowIndex;
            txtMaNV.Text = dataGridView_DanhSachNhanVien.Rows[row].Cells[0].Value.ToString();
            txtTenNV.Text = dataGridView_DanhSachNhanVien.Rows[row].Cells[1].Value.ToString();
            txtSDT.Text = dataGridView_DanhSachNhanVien.Rows[row].Cells[2].Value.ToString();
            cmbChucVu.Text = dataGridView_DanhSachNhanVien.Rows[row].Cells[3].Value.ToString();
        }

        private void btnSuaThongTinNhanVien_Click(object sender, EventArgs e)
        {
            cmbChucVu.Enabled = txtSDT.Enabled = txtTenNV.Enabled = true;
            btnLuu.Enabled = true;
            btnSuaTaiKhoan.Enabled = false;
            txtTenNV.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(txtMaNV.Text == "" && txtTenNV.Text ==""&& txtSDT.Text==""&& cmbChucVu.Text =="")
            {
                MessageBox.Show("Thông tin nhân viên không được để trống!");
            }
            else
            {
                nv.capNhatNhanVienTheoTenBLL(txtMaNV.Text, txtTenNV.Text);
                nv.capNhatNhanVienTheoSDTBLL(txtMaNV.Text, txtSDT.Text);
                nv.capNhatNhanVienTheoChucVuBLL(txtMaNV.Text, cmbChucVu.Text);
                MessageBox.Show("Thay Đổi Thành Công!");
                btnLuu.Enabled = false;
                btnSuaTaiKhoan.Enabled = true;
                cmbChucVu.Enabled = txtSDT.Enabled = txtTenNV.Enabled = false;
                loadDSNhanVienLenDataGridview();
            }
        }

        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            if( txtSDT.Text == "" || txtTenNV.Text == "" || cmbChucVu.Text == "")
            {
                MessageBox.Show("Bạn Chưa Nhập Đầy Đủ Thông Tin"); 
            }
            else
            {
                nv.themNhanVienVoiBLL(txtTenNV.Text, txtSDT.Text, cmbChucVu.Text);
                MessageBox.Show("Thêm Nhân Viên Mới Thành Công");
                loadDSNhanVienLenDataGridview();
            }
        }

        private void btnSuaTaiKhoan_Click(object sender, EventArgs e)
        {
            TaiKhoan f_TaiKhoan = new TaiKhoan();
            f_TaiKhoan.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
