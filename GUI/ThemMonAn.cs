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
using DTO;

namespace GUI
{
    public partial class ThemMonAn : Form
    {
        MonAnBLL monAnBLL = new MonAnBLL();

        public ThemMonAn()
        {
            InitializeComponent();
        }

        private void ThemMonAn_Load(object sender, EventArgs e)
        {
            loadComBobox();
        }

        public void loadComBobox()
        {
            cmbLoai.Items.Add("Đồ Ăn");
            cmbLoai.Items.Add("Thức Uống");

            cmbDonViTinh.Items.Add("Cái");
            cmbDonViTinh.Items.Add("Ly");
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                monAnBLL.themMonAnBLL(txtTenMon.Text, cmbLoai.Text, cmbDonViTinh.Text, int.Parse(txtGia.Text));
                MessageBox.Show("Thêm Thành Công!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
