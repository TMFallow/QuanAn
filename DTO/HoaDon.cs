using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DTO
{
    public class HoaDon
    {
        int tongSoHoaDon;

        public int demDSHoaDon()
        {
            string sqlloadMonAn = @"select count(*) from HOADON";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                tongSoHoaDon = int.Parse(dt.Rows[0][0].ToString());
            }
            return tongSoHoaDon;
        }

        public DataTable getCTHoaDonTheoMa(string maHD)
        {
            string sqlLoadDSHoaDonBan = @"SELECT TENMONAN, SOLUONG, GIA FROM CHITIETHOADON AS CT, MONAN AS MA where CT.MAMONAN = MA.MAMONAN AND maHOADON = "+ maHD +"";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void themHoaDon(string thanhTien)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            int maHD = demDSHoaDon() + 1;
            int nam = DateTime.Now.Year;
            int thang = DateTime.Now.Month;
            int ngay = DateTime.Now.Day;

            string ngayLap = nam + "-" + thang + "-" + ngay;

            string sqlThemHoaDon = @"INSERT INTO HOADON
                                    VALUES(" + maHD + ", 1, 0, N'" + ngayLap + "'," + thanhTien + ")";
            execSQL(sqlThemHoaDon);
            conn.Close();
        }

        public void themChiTietHoaDon(string maMonAn, string soLuong)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            int maHD = demDSHoaDon();

            string sqlThemChiTietHoaDon = @"INSERT INTO CHITIETHOADON
                                    VALUES(" + maHD + ", " + maMonAn + "," + soLuong + ")";
            execSQL(sqlThemChiTietHoaDon);
            conn.Close();
        }

        int thanhTienHDX = 0;
        public int layTongTienHoaDonBan()
        {
            string sqlLayTongTienHDX = @"SELECT SUM(THANHTIEN) FROM HOADON";
            SqlDataAdapter da = new SqlDataAdapter(sqlLayTongTienHDX, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                thanhTienHDX = int.Parse(dt.Rows[0][0].ToString());
            }
            return thanhTienHDX;
        }

        int thanhTienHDN = 0;
        public int layTongTienHoaDonNhap()
        {
            string sqlLayTongTienHDX = @"SELECT SUM(THANHTIEN) FROM NHAPMONAN";
            SqlDataAdapter da = new SqlDataAdapter(sqlLayTongTienHDX, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                thanhTienHDN = int.Parse(dt.Rows[0][0].ToString());
            }
            return thanhTienHDN;
        }

        public DataTable loadDSHoaDonBan()
        {
            string sqlLoadDSHoaDonBan = @"select MAHOADON, NGAYTHANHTOAN, THANHTIEN from HOADON";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable loadDSHoaDonBanTheoNgay(string tuNgay, string denNgay)
        {
            string sqlLoadDSHoaDonBan = @"select MAHOADON, NGAYTHANHTOAN, THANHTIEN from HOADON WHERE NGAYTHANHTOAN between '" + tuNgay + "' and '" + denNgay + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable loadDSChiTietHoaDonBan(string maHoaDon)
        {
            string sqlLoadDSHoaDonBan = @"SELECT MAHOADON, TENMONAN, SOLUONG FROM CHITIETHOADON AS CT, MONAN AS MA WHERE CT.MAMONAN = MA.MAMONAN AND MAHOADON = "+ maHoaDon +"";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }



        public DataTable loadDSHoaDonNhap()
        {
            string sqlLoadDSHoaDonBan = @"SELECT MADONNHAP, NGAYNHAP, THANHTIEN FROM NHAPMONAN";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable loadDSChiTietHoaDonNhap(string maHoaDon)
        {
            string sqlLoadDSHoaDonBan = @"select MADONNHAP, TENNGUYENLIEU, CT.SOLUONG from CHITIETNHAPMONAN AS CT, NGUYENLIEU AS NL WHERE CT.MANGUYENLIEU = NL.MANGUYENLIEU AND MADONNHAP = " + maHoaDon + "";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public int layTongTienTheoNgay(string tuNgay, string denNgay)
        {
            string sqlLoadDSHoaDonBan = @"select SUM(THANHTIEN) from HOADON WHERE NGAYTHANHTOAN between '"+ tuNgay +"' and '"+ denNgay +"'";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(int.Parse(dt.Rows[0][0].ToString())!=null)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public int layTongTienTheoNgayHDN(string tuNgay, string denNgay)
        {
            string sqlLoadDSHoaDonBan = @"select SUM(THANHTIEN) from NHAPMONAN WHERE NGAYNHAP between '" + tuNgay + "' and '" + denNgay + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (int.Parse(dt.Rows[0][0].ToString()) != null)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public DataTable LayHDTheoMaHD(string maHD)
        {
            string sqlLoadDSHoaDonBan = @"select TENNV, MAHOADON, NGAYTHANHTOAN from HOADON AS HD, NHANVIEN AS NV WHERE HD.MANV = NV.MANV AND MAHOADON = "+maHD+"";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable LayCTHDTheoMaHD(string maHD)
        {
            string sqlLoadDSHoaDonBan = @"select TENMONAN, SOLUONG, GIA from MONAN AS MA, CHITIETHOADON AS CT WHERE MA.MAMONAN = CT.MAMONAN AND MAHOADON = "+maHD+"";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void execSQL(string sql)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return;
            }
            cmd.Dispose();
            cmd = null;
            conn.Close();
        }
    }
}
