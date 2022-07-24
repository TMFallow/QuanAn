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
    public partial class Main : Form
    {
        NhanVienBLL nv = new NhanVienBLL();

        public Main()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn đăng xuất?", "Thông Báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn3Lines_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CollapseMenu()
        {
            if (this.panelMain.Width > 200)
            {
                panelMain.Width = 100;
                lblMain.Visible = false;
                btn3Lines.Dock = DockStyle.Top;
                foreach(Button listButton in panelMain.Controls.OfType<Button>())
                {
                    listButton.Text = "";
                    listButton.ImageAlign = ContentAlignment.MiddleCenter;
                    listButton.Padding = new Padding(0);
                }
            }
            else
            {
                panelMain.Width = 230;
                lblMain.Visible = true;
                btn3Lines.Dock = DockStyle.None;
                foreach (Button listButton in panelMain.Controls.OfType<Button>())
                {
                    listButton.Text = "   " + listButton.Tag.ToString();
                    listButton.ImageAlign = ContentAlignment.MiddleCenter;
                    listButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
        }

        private void btnTakeAway_Click(object sender, EventArgs e)
        {
            TakeAway f_TakeAway = new TakeAway();
            f_TakeAway.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string tenNV;
            System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["LogIn"];
            tenNV = ((LogIn)f).txtUser.Text;
            lblTen.Text = nv.getTenNhanVienDuaTheoTenDangNhapBLL(tenNV);

            string chucVu = nv.getChucVuDuaVaoTenNhanVienBLL(lblTen.Text);
            if(chucVu == "Admin")
            {
                btnMonAn.Enabled = btnTakeAway.Enabled = btnThongKe.Enabled = btnNhanVien.Enabled = true;
            }
            else
            {
                btnMonAn.Enabled = btnTakeAway.Enabled = true;
                btnThongKe.Enabled = btnNhanVien.Enabled = false;
            }

        }

        private void btnMonAn_Click(object sender, EventArgs e)
        {
            MonAn f_MonAn = new MonAn();
            f_MonAn.ShowDialog();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ThongKe f_TK = new ThongKe();
            f_TK.ShowDialog();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            NhanVien f_NV = new NhanVien();
            f_NV.ShowDialog();
        }
    }
}
