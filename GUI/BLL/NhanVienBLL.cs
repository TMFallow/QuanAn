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
    public class NhanVienBLL
    {
        NhanVienDAL nv = new NhanVienDAL();

        public DataTable getAllNhanVienBLL()
        {
            return nv.getAllNhanVienDAL();
        }


        public DataTable getAllNhanVienBLL(string maNV)
        {
            return nv.getAllNhanVienDAL(maNV);
        }
        public void capNhatNhanVienTheoTenBLL(string maNV, string tenNV)
        {
            nv.capNhatNhanVienTheoTenDAL(maNV, tenNV);
        }

        public void capNhatNhanVienTheoSDTBLL(string maNV, string SDT)
        {
            nv.capNhatNhanVienTheoSDTDAL(maNV, SDT);
        }

        public void capNhatNhanVienTheoChucVuBLL(string maNV, string chucVu)
        {
            nv.capNhatNhanVienTheoChucVu(maNV, chucVu);
        }

        public void themNhanVienVoiBLL(string tenNV, string SDT, string chucVu)
        {
            nv.themNhanVienVoiDAL(tenNV, SDT, chucVu);
        }

        public string getTenNhanVienDuaTheoTenDangNhapBLL(string tenDN)
        {
            return nv.getTenNhanVienDuaTheoTenDangNhapDAL(tenDN);
        }

        public string getChucVuDuaVaoTenNhanVienBLL(string tenNV)
        {
            return nv.getChucVuDuaVaoTenNhanVienDAL(tenNV);
        }

        public DataTable getTaiKhoanBLL(string maNV)
        {
            return nv.getTaiKhoanDAL(maNV);
        }

        public void suaTaiKhoanBLL(string tenTK, string matKhau, string maNV)
        {
            nv.suaTaiKhoan(tenTK, matKhau, maNV);
        }
    }
}
