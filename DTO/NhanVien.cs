using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DTO
{
    public class NhanVien
    {

        public DataTable layDanhSachNhanVien()
        {
            string sqlLoadDSHoaDonBan = @"select * from NHANVIEN";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }


        public DataTable layDanhSachNhanVienTheoMa(string maNV)
        {
            string sqlLoadDSHoaDonBan = @"select * from NHANVIEN WHERE MANV = "+ maNV +"";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void capNhatNhanVienTheoTen(string maNV, string tenNV)
        {
            string sqlcapNhat = @"update NHANVIEN set TENNV = N'" + tenNV + "' Where MaNV = " + maNV + "";
            execSQL(sqlcapNhat);
        }

        public void capNhatNhanVienTheoSDT(string maNV, string SDT)
        {
            string sqlcapNhat = @"update NHANVIEN set SDT = N'" + SDT + "' Where MaNV = " + maNV + "";
            execSQL(sqlcapNhat);
        }

        public void capNhatNhanVienTheoChucVu(string maNV, string chucVu)
        {
            string sqlcapNhat = @"update NHANVIEN set CHUCVU = N'" + chucVu + "' where MANV = " + maNV + "";
            execSQL(sqlcapNhat);
        }

        public void themNhanVienMoi(string tenNV, string SDT, string chucVu)
        {
            int maNV = getMaNV() + 1;
            string sqlThem = @"insert into NHANVIEN values(" + maNV.ToString() + ", N'" + tenNV + "', N'"+ SDT +"', N'"+ chucVu +"')";
        }

        public int getMaNV()
        {
            string sqlLoadDSHoaDonBan = @"select count(*) from NHANVIEN";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public string getTenNhanVienDuaTheoTenDangNhap(string tenDangNhap)
        {
            string sqlLoadDSHoaDonBan = @"select TENNV from NHANVIEN AS NV, TAIKHOAN AS TK WHERE NV.MANV = TK.MANV AND TENDN = N'"+ tenDangNhap +"'";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string getChucVuDuaVaoTenNhanVien(string tenNV)
        {
            string sqlLoadDSHoaDonBan = @"select ChucVu from NHANVIEN where TENNV = N'"+ tenNV +"'";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public DataTable getTaiKhoan(string maNV)
        {
            string sqlLoadDSHoaDonBan = @"select TENDN, MATKHAU from TAIKHOAN where MANV = "+ maNV +"";
            SqlDataAdapter da = new SqlDataAdapter(sqlLoadDSHoaDonBan, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void updateTaiKhoanTheoTenDN(string tenTK, string maNV)
        {
            string sqlcapNhat = @"update TAIKHOAN set TENDN = N'"+ tenTK +"' WHERE MANV = "+ maNV +"";
            execSQL(sqlcapNhat);
        }

        public void updateTaiKhoanTheoMatKhau(string matKhau, string maNV)
        {
            string sqlcapNhat = @"update TAIKHOAN set MATKHAU = N'" + matKhau + "' WHERE MANV = " + maNV + "";
            execSQL(sqlcapNhat);
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
