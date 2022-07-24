using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class HoaDonBLL
    {
        HoaDonDAL hd = new HoaDonDAL();
        public void themHoaDonBLL(string thanhTien)
        {
            hd.themHoaDon(thanhTien);
        }

        public void themCTHDBLL(string maMonAn, string sl)
        {
            hd.themChiTietHoaDon(maMonAn, sl);
        }

        public int layTongTienHDX()
        {
            return hd.layTongTienHoaDonBan();
        }

        public int layTongTienHDN()
        {
            return hd.layTongTienHoaDonNhap();
        }

        public DataTable DSHoaDonBanBLL()
        {
            return hd.DSHoaDonBanDAL();
        }

        public DataTable DSChiTietHoaDonBanBLL(string maHoaDon)
        {
            return hd.DSChiTietHoaDonBanDAL(maHoaDon);
        }

        public DataTable DSHoaDonNhapBLL()
        {
            return hd.DSHoaDonNhapDAL();
        }

        public DataTable DSChiTietHoaDonNhapBLL(string maHoaDon)
        {
            return hd.DSChiTietHoaDonNhapDAL(maHoaDon);
        }

        public int layTongTienTheoNgayBLL(string tuNgay, string denNgay)
        {
            return hd.layTongTienTheoNgayDAL(tuNgay, denNgay);
        }

        public int layTongTienTheoNgayHDNBLL(string tuNgay, string denNgay)
        {
            return hd.layTongTienTheoNgayHDNDAL(tuNgay, denNgay);
        }

        public DataTable layHoaDonTheoMaBLL(string maHoaDon)
        {
            return hd.layHoaDonTheoMaDAL(maHoaDon);
        }

        public DataTable layChiTietHoaDonTheoMaBLL(string maHoaDon)
        {
            return hd.layChiTietHoaDonTheoMaDAL(maHoaDon);
        }

        public int getDSHoaDonBLL()
        {
            return hd.getDSHoaDon();
        }

        public DataTable getCTHDTheoMaBLL(string maHD)
        {
            return hd.getCTHDTheoMaDAL(maHD);
        }
    }
}
