using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class NguyenLieuBLL
    {
        NguyenLieuDAL nguyenLieuDAL = new NguyenLieuDAL();
        
        public DataTable loadNguyenLieu()
        {
            return nguyenLieuDAL.loadNguyenLieu();
        }

        public void cheBienMonAnBLL(int soLuong, string tenMonAn)
        {
            nguyenLieuDAL.cheBienMonAn(soLuong, tenMonAn);
        }

        public DataTable getTenNguyenLieuBLL(string tenMonAn)
        {
            return nguyenLieuDAL.getTenNguyenLieu(tenMonAn);
        }

        public void NguyenLieuDaCheBienBLL(string tenMonAn)
        {
            nguyenLieuDAL.nguyenLieuDaCheBien(tenMonAn);
        }

        public void ThemHoaDonNhapBLL(string tenNL, int soluong)
        {
            nguyenLieuDAL.themHoaDonNhapNL(tenNL, soluong);
        }

        public DataTable LoadDSDVT()
        {
            return nguyenLieuDAL.loadDSDVT();
        }

        public void ThemNguyenLieuMoiBLL(string tenMonAn, string tenNL, int sLuong, string DVT, int Gia)
        {
            nguyenLieuDAL.themNguyenLieuMoi(tenMonAn, tenNL, sLuong, DVT, Gia);
        }

        public DataTable loadDSNguyenLieuCanNhapBLL()
        {
            return nguyenLieuDAL.loadDSNguyenLieuCanNhap();
        }

        public int kiemTraNguyenLieuBLL(string tenNL)
        {
            return nguyenLieuDAL.kiemTraNguyenLieuDAL(tenNL);
        }
    }
}
