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
using System.IO;

namespace GUI
{
    public partial class ThongKe : Form
    {
        HoaDonBLL hdBLL = new HoaDonBLL();

        public ThongKe()
        {
            InitializeComponent();
            chuyenThanhFiletxt();
            randomTrongSo();
            docFile();
            DocData();
            trainNhieuDongDuLieu();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            cmbLoaiHoaDon.Items.Add("");
            cmbLoaiHoaDon.Items.Add("Hóa Đơn Bán");
            cmbLoaiHoaDon.Items.Add("Hóa Đơn Nhập");
            lblTongTienBanHang.Text = hdBLL.layTongTienHDX().ToString();
            lblTongTienNhapHoaDon.Text = hdBLL.layTongTienHDN().ToString();

            int doanhThu = 0;

            doanhThu = int.Parse(lblTongTienBanHang.Text) - int.Parse(lblTongTienNhapHoaDon.Text);

            lblDoanhThu.Text = doanhThu.ToString();


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbLoaiHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLoaiHoaDon.Text == "Hóa Đơn Bán")
            {
               dataGridView_HoaDon.DataSource = hdBLL.DSHoaDonBanBLL();
            }
            
            if(cmbLoaiHoaDon.Text == "Hóa Đơn Nhập")
            {
                dataGridView_HoaDon.DataSource = hdBLL.DSHoaDonNhapBLL();
            }

            if (cmbLoaiHoaDon.Text == "")
            {
                dataGridView_HoaDon.DataSource = null;
            }
        }

        private void dataGridView_HoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_HoaDon.DataSource != null)
            {
                int row = dataGridView_HoaDon.CurrentCell.RowIndex;
                if (cmbLoaiHoaDon.Text == "Hóa Đơn Bán" || cmbLoaiHoaDon.Text == "")
                {
                    dataGridView_ChiTietHoaDon.DataSource = hdBLL.DSChiTietHoaDonBanBLL(dataGridView_HoaDon.Rows[row].Cells[0].Value.ToString());
                }

                if (cmbLoaiHoaDon.Text == "Hóa Đơn Nhập")
                {
                    dataGridView_ChiTietHoaDon.DataSource = hdBLL.DSChiTietHoaDonNhapBLL(dataGridView_HoaDon.Rows[row].Cells[0].Value.ToString());
                }

                //if ()
                //{
                //    dataGridView_ChiTietHoaDon.DataSource = null;
                //}
            }
            
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            string tuNgay = dateTimePicker_TuNgay.Value.Year.ToString() + "-" + dateTimePicker_TuNgay.Value.Month.ToString() + "-" + dateTimePicker_TuNgay.Value.Day.ToString();
            string denNgay = dateTimePicker_DenNgay.Value.Year.ToString() + "-" + dateTimePicker_DenNgay.Value.Month.ToString() + "-" + dateTimePicker_DenNgay.Value.Day.ToString();

            if (tuNgay != denNgay)
            {

                int tongTienHDBanTuNgay = hdBLL.layTongTienTheoNgayBLL(tuNgay, denNgay);

                lblTongTienBanHang.Text = tongTienHDBanTuNgay.ToString();

                int tongTienHDNhap = hdBLL.layTongTienTheoNgayHDNBLL(tuNgay, denNgay);

                lblTongTienNhapHoaDon.Text = tongTienHDNhap.ToString();

                int doanhThu = tongTienHDBanTuNgay - tongTienHDNhap;

                lblDoanhThu.Text = doanhThu.ToString();
            }
            else
            {
                lblTongTienBanHang.Text = hdBLL.layTongTienHDX().ToString();
                lblTongTienNhapHoaDon.Text = hdBLL.layTongTienHDN().ToString();

                int doanhThu = 0;

                doanhThu = int.Parse(lblTongTienBanHang.Text) - int.Parse(lblTongTienNhapHoaDon.Text);

                lblDoanhThu.Text = doanhThu.ToString();
            }
        
        }

        public static double wa1, wb1, wc1, wd1, wa2, wb2, wc2, wd2, w13, w23; //Các Cạnh Của NơRon
        public static double A, B, C, D, Z;  // Đỉnh Của NơRon
        public static double ng1, ng2, ng3;  // Tính Hiệu Lỗi
        public static double n = 1; // Hệ số N
        public static List<double> Data = new List<double>();

        private void btnDuBao_Click(object sender, EventArgs e)
        {
            if(dataGridView_HoaDon.DataSource ==null)
            {
                MessageBox.Show("Cần Phải Có Danh Sách Hóa Đơn Bán.");
                return;
            }
            else
            {
                int ngay = int.Parse(dataGridView_HoaDon.Rows.Count.ToString()) - 6;
                DocDauVao(ngay);
                double kqdudoan = TraKQ(DuDoan(A, B, C, D), docMax(), docMin());
                MessageBox.Show(kqdudoan.ToString("#,#"), "Dự Báo Doanh Thu Ngày Kế Tiếp");
            }
        }

        public void chuyenThanhFiletxt()
        {
            try
            {
                TextWriter writer = new StreamWriter("data.txt");
                dataGridView_HoaDon.DataSource = hdBLL.DSHoaDonBanBLL();
                for (int i = 0; i < dataGridView_HoaDon.Rows.Count - 1; i++)
                {
                    writer.Write(dataGridView_HoaDon.Rows[i].Cells[2].Value.ToString());
                    if (i != dataGridView_HoaDon.Rows.Count - 2)
                    {
                        writer.WriteLine("");
                    }
                }
                writer.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Đọc Dữ Liệu
        public void DocData()
        {
            StreamReader read = new StreamReader("data.txt");
            List<double> mangData = new List<double>();
            for (int i = 0; i < dataGridView_HoaDon.Rows.Count-1; i++)
                mangData.Add(Math.Round(double.Parse(read.ReadLine()), 6));
            Data = mangData;
        }

        //Train Nhieu Dong Du Lieu
        public void trainNhieuDongDuLieu()
        {
            for (int i = 0; i < dataGridView_HoaDon.Rows.Count - 6; i++)
            {
                DocDauVao(i);
                train();
            }
        }
        public void train()
        {
            double t1 = Timt(A, B, wa1, wb1, C, D, wc1, wd1);
            Math.Round(t1, 6);
            double bien1 = DauRaNoron(A, B, wa1, wb1, C, D, wc1, wd1);
            Math.Round(bien1, 6);
            double t2 = Timt(A, B, wa2, wb2, C, D, wc2, wd2);
            Math.Round(t2, 6);
            double bien2 = DauRaNoron(A, B, wa2, wb2, C, D, wc2, wd2);
            Math.Round(bien2, 6);
            double t3 = Timt2(bien1, bien2, w13, w23);
            Math.Round(t3, 6);
            double bien3 = DauRaNoron2(bien1, bien2, w13, w23);
            Math.Round(bien3, 6);
            ng3 = Z - bien3;
            Math.Round(ng3, 6);
            ng1 = ng3 * w13;
            Math.Round(ng1, 6);
            ng2 = ng3 * w23;
            Math.Round(ng2, 6);
            wa1 = TinhTrongSo(A, wa1, n, ng1, t1);
            Math.Round(wa1, 6);
            wb1 = TinhTrongSo(B, wb1, n, ng1, t1);
            Math.Round(wb1, 6);
            wc1 = TinhTrongSo(C, wc1, n, ng1, t1);
            Math.Round(wc1, 6);
            wd1 = TinhTrongSo(D, wd1, n, ng1, t1);
            Math.Round(wd1, 6);
            wa2 = TinhTrongSo(A, wa2, n, ng2, t2);
            Math.Round(wa2, 6);
            wb2 = TinhTrongSo(B, wb2, n, ng2, t2);
            Math.Round(wb2, 6);
            wc2 = TinhTrongSo(C, wc2, n, ng2, t2);
            Math.Round(wc2, 6);
            wd2 = TinhTrongSo(D, wd2, n, ng2, t2);
            Math.Round(wd2, 6);
            w13 = TinhTrongSo(bien1, w13, n, ng3, t3);
            Math.Round(w13, 6);
            w23 = TinhTrongSo(bien2, w23, n, ng3, t3);
            Math.Round(w23, 6);
            XuatModel();
        }

        public double TraKQ(double x, double max, double min)
        {
            double kq = x * (max - min) + min;
            Math.Round(kq, 6);
            return kq;
        }

        public double DuDoan(double a1, double b1, double c1, double d1)
        {
            double t1 = Timt(a1, b1, wa1, wb1, c1, d1, wc1, wd1);
            Math.Round(t1, 6);
            double bien1 = DauRaNoron(a1, b1, wa1, wb1, c1, d1, wc1, wd1);
            Math.Round(bien1, 6);
            double t2 = Timt(a1, b1, wa2, wb2, c1, d1, wc2, wd2);
            Math.Round(t2, 6);
            double bien2 = DauRaNoron(a1, b1, wa2, wb2, c1, d1, wc2, wd2);
            Math.Round(bien2, 6);
            double t3 = Timt2(bien1, bien2, w13, w23);
            Math.Round(t3, 6);
            double bien3 = DauRaNoron2(bien1, bien2, w13, w23);
            Math.Round(bien3, 6);
            return Math.Round(bien3, 6);
        }

        public double TinhTrongSo(double a, double w, double n, double ng, double t)
        {
            return Math.Round((double)(w + n * ng * derivative_Sigmoid(t) * a), 6);
        }

        public double Timt(double a, double b, double wa1, double wb1, double c, double d, double wc1, double wd1)
        {
            double t = (a * wa1) + (b * wb1) + (c * wc1) + (d * wd1) + n;
            Math.Round(t, 6);
            return t;
        }

        public double DauRaNoron(double a, double b, double wa1, double wb1, double c, double d, double wc1, double wd1)
        {
            double t = Timt(a, b, wa1, wb1, c, d, wc1, wd1);
            Math.Round(t, 6);
            double Y = Sigmoid(t);
            Math.Round(Y, 6);
            return Y;
        }

        public double Timt2(double a, double b, double wa1, double wb1)
        {
            double t = (a * wa1) + (b * wb1) + n;
            Math.Round(t, 6);
            return t;
        }
        public double DauRaNoron2(double a, double b, double wa1, double wb1)
        {
            double t = Timt2(a, b, wa1, wb1);
            Math.Round(t, 6);
            double Y = Sigmoid(t);
            Math.Round(Y, 6);
            return Y;
        }

        public double Sigmoid(double value)
        {
            return (float)(1.0 / (1.0 + Math.Pow(Math.E, -value)));
        }

        public double derivative_Sigmoid(double value)
        {
            return Math.Round((double)(Math.Pow(Math.E, value) / (Math.Pow(1 + Math.Pow(Math.E, value), 2))), 6);
        }

        public void DocDauVao(int ngay)
        {
            A = Data[ngay];
            A = ChuanHoaDuLieu(A, docMax(), docMin());
            Math.Round(A, 6);
            B = Data[ngay + 1];
            B = ChuanHoaDuLieu(B, docMax(), docMin());
            Math.Round(B, 6);
            C = Data[ngay + 2];
            C = ChuanHoaDuLieu(C, docMax(), docMin());
            Math.Round(C, 6);
            D = Data[ngay + 3];
            D = ChuanHoaDuLieu(D, docMax(), docMin());
            Math.Round(D, 6);
            Z = Data[ngay + 4];
            Z = ChuanHoaDuLieu(Z, docMax(), docMin());
            Math.Round(Z, 6);
        }

        public double ChuanHoaDuLieu(double x, double max, double min)
        {
            double kq = (x - min) / (max - min);
            Math.Round(kq, 6);
            return kq;
        }

        public double docMax()
        {
            double max = Data[0];
            for (int i = 1; i < Data.Count; i++)
                if (max < Data[i])
                    max = Data[i];
            Math.Round(max, 6);
            return max;
        }

        public double docMin()
        {
            double min = Data[0];
            for (int i = 1; i < Data.Count; i++)
                if (min > Data[i])
                    min = Data[i];
            Math.Round(min, 6);
            return min;
        }

        public void randomTrongSo()
        {
            Random r = new Random();
            wa1 = r.NextDouble();
            wb1 = r.NextDouble();
            wc1 = r.NextDouble();
            wd1 = r.NextDouble();
            wa2 = r.NextDouble();
            wb2 = r.NextDouble();
            wc2 = r.NextDouble();
            wd2 = r.NextDouble();
            w13 = r.NextDouble();
            w23 = r.NextDouble();
            StreamWriter write = new StreamWriter("TrongSo_Weights.txt", false);
            write.WriteLine(Math.Round(wa1, 6).ToString());
            write.WriteLine(Math.Round(wb1, 6).ToString());
            write.WriteLine(Math.Round(wc1, 6).ToString());
            write.WriteLine(Math.Round(wd1, 6).ToString());
            write.WriteLine(Math.Round(wa2, 6).ToString());
            write.WriteLine(Math.Round(wb2, 6).ToString());
            write.WriteLine(Math.Round(wc2, 6).ToString());
            write.WriteLine(Math.Round(wd2, 6).ToString());
            write.WriteLine(Math.Round(w13, 6).ToString());
            write.WriteLine(Math.Round(w23, 6).ToString());
            write.Close();
        }

        public void XuatModel()
        {
            StreamWriter write = new StreamWriter("TrongSo_Weights.txt", false);
            write.WriteLine(Math.Round(wa1, 6).ToString());
            write.WriteLine(Math.Round(wb1, 6).ToString());
            write.WriteLine(Math.Round(wc1, 6).ToString());
            write.WriteLine(Math.Round(wd1, 6).ToString());
            write.WriteLine(Math.Round(wa2, 6).ToString());
            write.WriteLine(Math.Round(wb2, 6).ToString());
            write.WriteLine(Math.Round(wc2, 6).ToString());
            write.WriteLine(Math.Round(wd2, 6).ToString());
            write.WriteLine(Math.Round(w13, 6).ToString());
            write.WriteLine(Math.Round(w23, 6).ToString());
            write.Close();
        }

        public void docFile()
        {
            StreamReader read = new StreamReader("TrongSo_Weights.txt");
            wa1 = Math.Round(double.Parse(read.ReadLine()), 6);
            wb1 = Math.Round(double.Parse(read.ReadLine()), 6);
            wc1 = Math.Round(double.Parse(read.ReadLine()), 6);
            wd1 = Math.Round(double.Parse(read.ReadLine()), 6);
            wa2 = Math.Round(double.Parse(read.ReadLine()), 6);
            wb2 = Math.Round(double.Parse(read.ReadLine()), 6);
            wc2 = Math.Round(double.Parse(read.ReadLine()), 6);
            wd2 = Math.Round(double.Parse(read.ReadLine()), 6);
            w13 = Math.Round(double.Parse(read.ReadLine()), 6);
            w23 = Math.Round(double.Parse(read.ReadLine()), 6);
            read.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
