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
    public partial class TaiKhoan : Form
    {
        NhanVienBLL nv = new NhanVienBLL();

        public TaiKhoan()
        {
            InitializeComponent();
        }

        System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["NhanVien"];

        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            LoadTaiKhoanLenForm();
        }

        public void LoadTaiKhoanLenForm()
        {
            DataTable dt = new DataTable();
            dt = nv.getTaiKhoanBLL(((NhanVien)f).txtMaNV.Text);
            txtTenTaiKhoan.Text = dt.Rows[0][0].ToString();
            txtMatKhau.Text = dt.Rows[0][1].ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(txtTenTaiKhoan.Text == "")
            {
                MessageBox.Show("Cần Phải Nhập Tài Khoản");
            }
            else if(txtMatKhau.Text == "")
            {
                MessageBox.Show("Bạn Cần Nhập Mật Khẩu");
            }
            else
            {
                nv.suaTaiKhoanBLL(txtTenTaiKhoan.Text, txtMatKhau.Text, ((NhanVien)f).txtMaNV.Text);
                MessageBox.Show("Đã Sửa Tài Khoản Cho Nhân Viên " + ((NhanVien)f).txtTenNV.Text + "");
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
