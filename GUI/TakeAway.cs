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
    public partial class TakeAway : Form
    {
        MonAnBLL monAnBLL = new MonAnBLL();
        HoaDonBLL hoaDonBLL = new HoaDonBLL();

        public DataTable dataCTHD = null;
        public int maHoaDon;

        public TakeAway()
        {
            InitializeComponent();
        }

        private void TakeAway_Load(object sender, EventArgs e)
        {
            loadDSMonAnLenDataGridView();
            loadLoaiMonAnLenCombobox();
            cmbLoaiMonAn.Items.Add("*");
            
        }

        

        public void loadLoaiMonAnLenCombobox()
        {
            DataTable dt = new DataTable();
            dt = monAnBLL.loadDSLoaiMonAnBLL();
            int j =0;
            foreach(DataRow i in dt.Rows)
            {
                cmbLoaiMonAn.Items.Add(dt.Rows[j][0].ToString());
                j++;
            }
        }

        public void loadDSMonAnLenDataGridView()
        {
           dataGridView_DSMonAn.DataSource = monAnBLL.LayMonAnBLL();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_DSMonAn_SelectionChanged(object sender, EventArgs e)
        {
            int row = dataGridView_DSMonAn.CurrentCell.RowIndex;

            txtTenMonAn.Text = dataGridView_DSMonAn.Rows[row].Cells[1].Value.ToString();
            txtGiaMonAn.Text = dataGridView_DSMonAn.Rows[row].Cells[5].Value.ToString();
        }

        private void cmbLoaiMonAn_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cmbLoaiMonAn.Text != null)
            {
                dataGridView_DSMonAn.DataSource = monAnBLL.loadDSMonAntheoLoaiBLL(cmbLoaiMonAn.Text);
            }
            if (cmbLoaiMonAn.Text == "*")
            {
                dataGridView_DSMonAn.DataSource = monAnBLL.LayMonAnBLL();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView_CTHoaDon.SelectedCells.Count == 0)
            {
                MessageBox.Show("Chưa chọn món hủy");
            }
            else
            {
                int row = dataGridView_CTHoaDon.CurrentCell.RowIndex;
                int maMonAn = int.Parse(dataGridView_CTHoaDon.Rows[row].Cells[0].Value.ToString());
                if(int.Parse(dataGridView_CTHoaDon.Rows[row].Cells[4].Value.ToString())>1)
                {
                    dataGridView_CTHoaDon.Rows[row].Cells[4].Value = (int.Parse(dataGridView_CTHoaDon.Rows[row].Cells[4].Value.ToString()) - 1).ToString();
                    monAnBLL.thucHienChinhSuaLaiSLKhiHuyMonAn(maMonAn);
                    loadDSMonAnLenDataGridView();
                }
                else
                {
                    dataGridView_CTHoaDon.Rows.RemoveAt(row);
                    monAnBLL.thucHienChinhSuaLaiSLKhiHuyMonAn(maMonAn);
                    loadDSMonAnLenDataGridView();
                }
            }
        }

        private void btnGoiMon_Click(object sender, EventArgs e)
        {
            int r = dataGridView_DSMonAn.CurrentCell.RowIndex;

            int sluong = int.Parse(dataGridView_DSMonAn.Rows[r].Cells[4].Value.ToString());
            if (sluong > 0)
            {
                DataTable dt = new DataTable();
                int maMonAn = int.Parse(dataGridView_DSMonAn.Rows[r].Cells[0].Value.ToString());
                dt = monAnBLL.loadDSTenMonAnTheoMaBLL(int.Parse(dataGridView_DSMonAn.Rows[r].Cells[0].Value.ToString()));
                int sl = 1;
                int j;
                bool kt = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dataGridView_CTHoaDon.Rows.Count > 0)
                    {
                        for (j = 0; j < dataGridView_CTHoaDon.Rows.Count; j++)
                        {
                            if (dataGridView_CTHoaDon.Rows[j].Cells[0].Value.ToString() == dt.Rows[0][0].ToString())
                            {
                                sl++;
                                dataGridView_CTHoaDon.Rows[j].Cells[4].Value = (int.Parse(dataGridView_CTHoaDon.Rows[j].Cells[4].Value.ToString()) + 1).ToString();
                                kt = true;
                                monAnBLL.thucHienChinhSuaLaiSLKhiThemMonAn(maMonAn);
                                loadDSMonAnLenDataGridView();
                                return;
                            }
                        }
                        if (kt == false)
                        {
                            dataGridView_CTHoaDon.Rows.Add(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), 1, dt.Rows[0][5].ToString());
                            monAnBLL.thucHienChinhSuaLaiSLKhiThemMonAn(maMonAn);
                            loadDSMonAnLenDataGridView();
                            return;
                        }
                    }
                    else
                    {
                        dataGridView_CTHoaDon.Rows.Add(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), 1, dt.Rows[0][5].ToString());
                        monAnBLL.thucHienChinhSuaLaiSLKhiThemMonAn(maMonAn);
                        loadDSMonAnLenDataGridView();
                    }
                }
            }
            else
            {
                MessageBox.Show("Món Ăn Cần Bán Đã Hết!");
            }
        }

        private void dataGridView_CTHoaDon_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            txtThanhTien.Text = loadTien().ToString();
        }

        public int loadTien()
        {
            int thanhTien = 0;
            int soLuong, giaBan;
            for(int i = 0; i < dataGridView_CTHoaDon.Rows.Count; i++)
            {
                soLuong = int.Parse(dataGridView_CTHoaDon.Rows[i].Cells[4].Value.ToString());
                giaBan = int.Parse(dataGridView_CTHoaDon.Rows[i].Cells[5].Value.ToString());
                thanhTien = thanhTien + (soLuong * giaBan);
            }
            return thanhTien;
        }

        private void dataGridView_CTHoaDon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            txtThanhTien.Text = loadTien().ToString();
        }

        private void dataGridView_CTHoaDon_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtThanhTien.Text = loadTien().ToString();
        }


        private void txtTienKhachDua_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtThanhTien.Text))
            {
                if (!string.IsNullOrEmpty(txtTienKhachDua.Text))
                {
                    int thanhTien = int.Parse(txtThanhTien.Text);
                    int tienMat = int.Parse(txtTienKhachDua.Text);

                    if (tienMat < thanhTien)
                    {
                        MessageBox.Show("Tiền Khách Đưa Không Đủ");
                        txtTienKhachDua.Clear();
                        return;
                    }
                    else
                    {
                        int tq = tienMat - thanhTien;
                        txtTienThua.Text = tq.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Cần Nhập Tiền Của Khách Hàng");
                }
            }
            else
            {
                MessageBox.Show("Bạn Chưa Mua Sản Phẩm! Xin Vui Lòng Mua Sản Phẩm!");
                txtTienKhachDua.Clear();
                return;
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if(txtTienKhachDua.Text != "")
            {
                if(dataGridView_CTHoaDon.Rows.Count > 0)
                {

                    hoaDonBLL.themHoaDonBLL(txtThanhTien.Text);
                    int j = 0;

                    for(int i = 0; i<dataGridView_CTHoaDon.Rows.Count; i++)
                    {
                        hoaDonBLL.themCTHDBLL(dataGridView_CTHoaDon.Rows[j].Cells[0].Value.ToString(), dataGridView_CTHoaDon.Rows[j].Cells[4].Value.ToString());
                        j++;
                    }

                    maHoaDon = hoaDonBLL.getDSHoaDonBLL();
                    dataCTHD = hoaDonBLL.getCTHDTheoMaBLL(maHoaDon.ToString());

                    btnInHoaDon_Click(sender, e);

                    MessageBox.Show("Thanh Toán Thành Công!");
                    dataGridView_CTHoaDon.Rows.Clear();
                    dataGridView_CTHoaDon.Refresh();
                    txtGiamGia.Text = "0";
                    txtTienKhachDua.Text = "";
                    txtTienThua.Text = "";
                }
                else
                {
                    MessageBox.Show("Bạn Chưa Chọn Món");
                }
            }
            else
            {
                MessageBox.Show("Khách Hàng Cần Phải Đưa Tiền Để Tiến Hành Thanh Toán!");
            }
        }

        private void txtTienKhachDua_Enter(object sender, EventArgs e)
        {

        }

        private void txtTienKhachDua_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if(dataGridView_CTHoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Bạn Phải Chọn Món");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm hóa đơn.", "Thông Báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SaveFileDialog saveFileDialolg = new SaveFileDialog() { Filter = "Excel|*.xls", ValidateNames = true })
                    {
                        // creating Excel Application  
                        Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();

                        // creating new WorkBook within Excel application  
                        Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

                        // creating new Excelsheet in workbook  
                        Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                        // see the excel sheet behind the program  
                        app.Visible = true;

                        // get the reference of first sheet. By default its name is Sheet1.  
                        // store its reference to worksheet  
                        worksheet = workbook.Sheets["Sheet1"];
                        worksheet = workbook.ActiveSheet;

                        // changing the name of active sheet  
                        worksheet.Name = "Hóa Đơn Bán Hàng";

                        // storing header part in Excel  
                        for (int i = 1; i < dataGridView_CTHoaDon.Columns.Count + 1; i++)
                        {
                            worksheet.Cells[1, i] = dataGridView_CTHoaDon.Columns[i - 1].HeaderText;
                        }

                        // storing Each row and column value to excel sheet  
                        for (int i = 0; i < dataGridView_CTHoaDon.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView_CTHoaDon.Columns.Count; j++)
                            {
                                worksheet.Cells[i + 2, j + 1] = dataGridView_CTHoaDon.Rows[i].Cells[j].Value.ToString();
                            }
                        }

                        int dong = dataGridView_CTHoaDon.Rows.Count + 4;
                        int donggiamGia = dataGridView_CTHoaDon.Rows.Count + 5;
                        int dongTienKhachDua = dataGridView_CTHoaDon.Rows.Count + 6;
                        int dongTienThua = dataGridView_CTHoaDon.Rows.Count + 7;

                        worksheet.Cells[dong, "F"] = txtThanhTien.Text;
                        worksheet.Cells[dong, "E"] = "Thành Tiền";

                        worksheet.Cells[donggiamGia, "F"] = txtGiamGia.Text;
                        worksheet.Cells[donggiamGia, "E"] = "Giảm Giá";

                        worksheet.Cells[dongTienKhachDua, "F"] = txtTienKhachDua.Text;
                        worksheet.Cells[dongTienKhachDua, "E"] = "Tiền Khách Đưa";

                        worksheet.Cells[dongTienThua, "F"] = txtTienThua.Text;
                        worksheet.Cells[dongTienThua, "E"] = "Tiền Thối Lại";

                    }

                }
                // save the application  
                //workbook.SaveAs("D:\\Nam 3\\Phat Trien PhanMem Ung Dung Thong Minh\\DoAnQuanLyBanHang\\HoaDonBanHang\\HoaDonMonAn.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
             
            }
        }

        private void btnTimMonAn_Click(object sender, EventArgs e)
        {
            if(txtTimKiem.Text != "")
            {
               dataGridView_DSMonAn.DataSource = monAnBLL.timKiemTheoMaMonAnBLL(txtTimKiem.Text);
            }
            else
            {
                loadDSMonAnLenDataGridView();
            }

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtGiamGia_MouseLeave(object sender, EventArgs e)
        {
            if (txtThanhTien.Text == null)
            {
                MessageBox.Show("Chưa Đặt Món");
                return;
            }
            else
            {
                if (txtGiamGia.Text == null)
                {
                    txtGiamGia.Text = "0";
                }
                else if(int.Parse(txtGiamGia.Text) <0 && int.Parse(txtGiamGia.Text) >100)
                {
                    MessageBox.Show("Giảm Giá Không Vượt Quá 100%");
                }
                else
                {
                    float chietKhau = 100 - int.Parse(txtGiamGia.Text);
                    float giaDaGiam = int.Parse(txtThanhTien.Text) * (chietKhau / 100);
                    txtThanhTien.Text = giaDaGiam.ToString();
                }
            }
        }
    }
}
