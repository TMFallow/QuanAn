using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class HoaDonDAL
    {
        string sqlconn = @"Data Source=LAPTOP-O3QUUSD1\SQLEXPRESS;Initial Catalog=BAN_QUAN_AN_NHANH;User ID=sa;Password=123";
        int tongSoHoaDon;

        HoaDon hdon = new HoaDon();

        public int demDSHoaDon()
        {
            SqlConnection conn = new SqlConnection(sqlconn);
            conn.Open();
            string sqlloadMonAn = @"select count(*) from HOADON";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, sqlconn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count >0)
            {
                tongSoHoaDon = int.Parse(dt.Rows[0][0].ToString());
            }
            conn.Close();
            return tongSoHoaDon;
        }

        public void themHoaDon(string thanhTien)
        {
            hdon.themHoaDon(thanhTien);
        }

        public void themChiTietHoaDon(string maMonAn, string soLuong)
        {
            hdon.themChiTietHoaDon(maMonAn, soLuong);
        }

        public int layTongTienHoaDonBan()
        {
            return hdon.layTongTienHoaDonBan();
        }

        public int layTongTienHoaDonNhap()
        {
            return hdon.layTongTienHoaDonNhap();
        }

        public DataTable DSHoaDonBanDAL()
        {
            return hdon.loadDSHoaDonBan();
        }

        public DataTable DSChiTietHoaDonBanDAL(string maHoaDon)
        {
            return hdon.loadDSChiTietHoaDonBan(maHoaDon);
        }

        public DataTable DSHoaDonNhapDAL()
        {
            return hdon.loadDSHoaDonNhap();
        }

        public DataTable DSChiTietHoaDonNhapDAL(string maHoaDon)
        {
            return hdon.loadDSChiTietHoaDonNhap(maHoaDon);
        }

        public int layTongTienTheoNgayDAL(string tuNgay, string denNgay)
        {
            return hdon.layTongTienTheoNgay(tuNgay, denNgay);
        }

        public int layTongTienTheoNgayHDNDAL(string tuNgay, string denNgay)
        {
            return hdon.layTongTienTheoNgayHDN(tuNgay, denNgay);
        }

        public DataTable loadDSHoaDonBanTheoNgayDAL(string tuNgay, string denNgay)
        {
            return hdon.loadDSHoaDonBanTheoNgay(tuNgay, denNgay);
        }

        public DataTable layHoaDonTheoMaDAL(string maHoaDon)
        {
            return hdon.LayHDTheoMaHD(maHoaDon);
        }

        public DataTable layChiTietHoaDonTheoMaDAL(string maHoaDon)
        {
            return hdon.LayCTHDTheoMaHD(maHoaDon);
        }

        public int getDSHoaDon()
        {
            return hdon.demDSHoaDon();
        }

        public DataTable getCTHDTheoMaDAL(string maHD)
        {
            return hdon.getCTHoaDonTheoMa(maHD);
        }
    }
}
