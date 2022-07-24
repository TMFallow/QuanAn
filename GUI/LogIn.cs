using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;

namespace GUI
{
    public partial class LogIn : Form
    {
        Settings st = new Settings();
        TaiKhoanBLL tk_bll = new TaiKhoanBLL();

        public LogIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtUser.Text.Trim()))
            {
                MessageBox.Show("Username Không Được Trống");
                this.txtUser.Focus();
                return;
            }

            if(string.IsNullOrEmpty(txtPass.Text.Trim()))
            {
                MessageBox.Show("Password Không Được Trống");
                this.txtPass.Focus();
                return;
            }

            int kq = tk_bll.Check_TK_BLL(txtUser.Text, txtPass.Text);
            
            if(kq==1)
            {
                MessageBox.Show("Đăng Nhập Thành Công!");
                Main fm = new Main();
                fm.Show();
                txtPass.Clear();
                txtUser.Clear();
            }
            else
            {
                MessageBox.Show("Tên Đăng Nhập Hoặc Mật Khẩu Sai!");
            }
        }

        private void btnConfigDatabase_Click(object sender, EventArgs e)
        {
            CauHinh fCauHinh = new CauHinh();
            fCauHinh.ShowDialog();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn đóng chương trình.", "Thông Báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtUser_Click(object sender, EventArgs e)
        {
            txtUser.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = SystemColors.Control;
            txtPass.BackColor = SystemColors.Control;
        }

        private void txtPass_Click(object sender, EventArgs e)
        {
            txtUser.BackColor = SystemColors.Control;
            panel3.BackColor = SystemColors.Control;
            panel4.BackColor = Color.White;
            txtPass.BackColor = Color.White;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            txtPass.UseSystemPasswordChar = false; 
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            txtPass.UseSystemPasswordChar = true; 
        }
    }
}
