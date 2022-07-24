using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class NhanVienDAL
    {
        NhanVien nv = new NhanVien();
        public DataTable getAllNhanVienDAL()
        {
            return nv.layDanhSachNhanVien();
        }

        public DataTable getAllNhanVienDAL(string maNV)
        {
            return nv.layDanhSachNhanVienTheoMa(maNV);
        }

        public void capNhatNhanVienTheoTenDAL(string maNV, string tenNV)
        {
            nv.capNhatNhanVienTheoTen(maNV, tenNV);
        }

        public void capNhatNhanVienTheoSDTDAL(string maNV, string SDT)
        {
            nv.capNhatNhanVienTheoSDT(maNV, SDT);
        }

        public void capNhatNhanVienTheoChucVu(string maNV, string chucVu)
        {
            nv.capNhatNhanVienTheoChucVu(maNV, chucVu);
        }

        public void themNhanVienVoiDAL(string tenNV, string SDT, string chucVu)
        {
            nv.themNhanVienMoi(tenNV, SDT, chucVu);
        }

        public string getTenNhanVienDuaTheoTenDangNhapDAL(string tenDN)
        {
            return nv.getTenNhanVienDuaTheoTenDangNhap(tenDN);
        }

        public string getChucVuDuaVaoTenNhanVienDAL(string tenNV)
        {
            return nv.getChucVuDuaVaoTenNhanVien(tenNV);
        }

        public DataTable getTaiKhoanDAL(string maNV)
        {
            return nv.getTaiKhoan(maNV);
        }

        public void suaTaiKhoan(string tenTK, string matKhau, string maNV)
        {
            nv.updateTaiKhoanTheoTenDN(tenTK, maNV);
            nv.updateTaiKhoanTheoMatKhau(matKhau, maNV);
        }
    }
}
