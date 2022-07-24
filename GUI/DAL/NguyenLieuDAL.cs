using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class NguyenLieuDAL
    {
        NguyenLieu nguyenlieu = new NguyenLieu();


        public DataTable loadNguyenLieu()
        {
            return nguyenlieu.loadNguyenLieu();
        }

        public DataTable loadDSNguyenLieuCanNhap()
        {
            return nguyenlieu.loadDSNguyenLieuCanNhap();
        }

        public void cheBienMonAn(int soLuong, string tenMonAn)
        {
            nguyenlieu.cheBienMonAn(soLuong, tenMonAn);
        }

        public void nguyenLieuDaCheBien(string tenMonAn)
        {
            nguyenlieu.nguyenLieuDaCheBien(tenMonAn);
        }


        public DataTable getTenNguyenLieu(string tenMonAn)
        {
            return nguyenlieu.getTenNguyenLieu(tenMonAn);
        }

        public void themHoaDonNhapNL(string tenNL, int soLuong)
        {
            nguyenlieu.themHoaDonNhapNL(tenNL, soLuong);
        }

        public void themNguyenLieuMoi(string tenMonAn, string tenNL, int sLuong, string DVT, int Gia)
        {
            nguyenlieu.themNguyenLieuMoi(tenMonAn, tenNL, sLuong, DVT, Gia);
        }

         public DataTable loadDSDVT()
        {
            return nguyenlieu.loadDSDVT();
        }

        public int kiemTraNguyenLieuDAL(string tenNL)
        {
             return nguyenlieu.kiemTraNguyenLieu(tenNL);
        }
    }
}
