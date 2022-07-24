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
    public partial class ThemNLMoi : Form
    {

        MonAnBLL monAnBLL = new MonAnBLL();
        NguyenLieuBLL nguyenLieuBLL = new NguyenLieuBLL();

        public ThemNLMoi()
        {
            InitializeComponent();
        }

        private void ThemNLMoi_Load(object sender, EventArgs e)
        {
            loadComboBoxTenMonAn();
            LoadComboboxDonViTinh();
        }

        public void loadComboBoxTenMonAn()
        {
            DataTable dt = new DataTable();
            dt = monAnBLL.loadDSTenMonAnBLL();
            int i = 0;
            foreach(DataRow r in dt.Rows)
            {
                cmbTenMonAn.Items.Add(dt.Rows[i][0].ToString());
                i++;
            }
        }

        public void LoadComboboxDonViTinh()
        {
            DataTable dt = new DataTable();
            dt = nguyenLieuBLL.LoadDSDVT();
            int i = 0;
            foreach (DataRow r in dt.Rows)
            {
                cmbDonViTinh.Items.Add(dt.Rows[i][0].ToString());
                i++;
            }
        }

        private void txtNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (nguyenLieuBLL.kiemTraNguyenLieuBLL(txtTenNguyenLieu.Text) == 0)
                {
                    nguyenLieuBLL.ThemNguyenLieuMoiBLL(cmbTenMonAn.Text, txtTenNguyenLieu.Text, int.Parse(txtSL.Text), cmbDonViTinh.Text, int.Parse(txtGia.Text));
                    MessageBox.Show("Thêm Nguyên Liệu Mới Thành Công");
                    txtTenNguyenLieu.Text = "";
                    txtTenNguyenLieu.Focus();
                }
                else
                {
                    MessageBox.Show("Nguyên Liệu Đã Có");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
