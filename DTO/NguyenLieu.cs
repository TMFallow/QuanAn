using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DTO
{
    public class NguyenLieu
    {
        int maMonAn;
        DataTable dtNguyenLieu = new DataTable();
        DataTable dtTenNguyenLieu = new DataTable();
        int SoLuongNguyenLieu;
        int maHoaDonNhapNL;
        int maNLNhap;
        int GiaNL;
        int soLuongNL;
        int maNLMoi;

        public DataTable loadNguyenLieu()
        {
            string sqlloadMonAn = @"select * from NGUYENLIEU";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable loadDSNguyenLieuCanNhap()
        {
            string sqlloadMonAn = @"SELECT * FROM NGUYENLIEU WHERE SOLUONG < 10";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void cheBienMonAn(int soLuong, string tenMonAn)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            string sqlThemMonAn = @"update MONAN
                                    set SLHIENCO = " + soLuong + " + 1 where TENMONAN = N'" + tenMonAn + "'";
            execSQL(sqlThemMonAn);
        }
        //-------------------------------------------------------------------------------------------------------------

        public DataTable getCongThucCanNau(string tenMonAn)
        {
            DataTable dt = new DataTable();
            dt = getMaNguyenLieu(getMaMonAn(tenMonAn));
            dtNguyenLieu = dt;
            return dtNguyenLieu;
        }

        public int getMaMonAn(string tenMonAn)
        {
            string sqlGetMaMonAn = @"select MAMONAN from MONAN where TENMONAN = N'" + tenMonAn + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlGetMaMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                maMonAn = int.Parse(dt.Rows[0][0].ToString());
            }
            return maMonAn;
        }

        public DataTable getMaNguyenLieu(int maMonAn)
        {
            string sqlGetMaNguyenLieu = @"select MANGUYENLIEU from CONGTHUCCHEBIENMONAN where MAMONAN = " + maMonAn + "";
            SqlDataAdapter da = new SqlDataAdapter(sqlGetMaNguyenLieu, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void nguyenLieuDaCheBien(string tenMonAn)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            DataTable dtdsTenNguyenLieu = new DataTable();
            if (dtdsTenNguyenLieu.Rows.Count != 0)
            {
                dtdsTenNguyenLieu.Clear();
            }
            dtdsTenNguyenLieu = getTenNguyenLieu(tenMonAn);
            int j = dtdsTenNguyenLieu.Rows.Count;
            int i = 0;
            while (j > 0)
            {
                string sqlUpdateSLNguyenLieu = @"update NGUYENLIEU
                                                 set SOLUONG = " + getSoLuongNguyenLieu(dtdsTenNguyenLieu.Rows[i][0].ToString()) + " - " + laySoLuongDuaVaoMa(LayMaNguyenLieu(dtdsTenNguyenLieu.Rows[i][0].ToString())) + " where TENNGUYENLIEU = N'" + dtdsTenNguyenLieu.Rows[i][0].ToString() + "'";
                execSQL(sqlUpdateSLNguyenLieu);
                i++;
                j--;
            }
            conn.Close();
        }

        public int LayMaNguyenLieu(string tenNguyenLieu)
        {
            string sqlGetMaNguyenLieu = @"select MANGUYENLIEU FROM NGUYENLIEU WHERE TENNGUYENLIEU = N'"+ tenNguyenLieu +"'";
            SqlDataAdapter da = new SqlDataAdapter(sqlGetMaNguyenLieu, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int laySoLuongDuaVaoMa(int maNL)
        {
            string sqlGetMaNguyenLieu = @"select SLNGUYENLIEU FROM CONGTHUCCHEBIENMONAN WHERE MANGUYENLIEU = "+maNL+" GROUP BY SLNGUYENLIEU";
            SqlDataAdapter da = new SqlDataAdapter(sqlGetMaNguyenLieu, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int getSoLuongNguyenLieu(string tenMonAn)
        {
            DataTable dttbSL = new DataTable();
            string sqlGetTenNguyenLieu = @"select SOLUONG from NGUYENLIEU WHERE TENNGUYENLIEU = N'"+tenMonAn+"'";
            SqlDataAdapter da = new SqlDataAdapter(sqlGetTenNguyenLieu, Properties.Settings.Default.Connection);
            da.Fill(dttbSL);
            SoLuongNguyenLieu = int.Parse(dttbSL.Rows[0][0].ToString());
            return SoLuongNguyenLieu;
        }

        public DataTable getTenNguyenLieu(string tenMonAn)
        {
            DataTable dtMaNL = new DataTable();
            dtMaNL = getMaNguyenLieu(getMaMonAn(tenMonAn));
            int j = dtMaNL.Rows.Count;
            if (dtTenNguyenLieu.Rows.Count != 0)
            {
                dtTenNguyenLieu.Clear();
            }
            int i = 0;
            while (j > 0)
            {
                string sqlGetTenNguyenLieu = @"select TENNGUYENLIEU from NGUYENLIEU where MANGUYENLIEU = " + dtMaNL.Rows[i][0].ToString() + "";
                SqlDataAdapter da = new SqlDataAdapter(sqlGetTenNguyenLieu, Properties.Settings.Default.Connection);
                da.Fill(dtTenNguyenLieu);
                i++;
                j--;
            }
            return dtTenNguyenLieu;
        }

        public void themHoaDonNhapNL(string tenNL, int soLuong)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            int giaTienNhap = soLuong * 5000;
            string thanhTienNhap = giaTienNhap.ToString();
            int maHoaDonNhap = LayMaHoaDonNhapNguyenLieu() + 1;
            string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();

            string insertHoaDonNhap = @"insert into NHAPMONAN
                                        values(" + maHoaDonNhap + ", 1, N'" + date + "', " + thanhTienNhap + ")";
            execSQL(insertHoaDonNhap);

            string insertChiTietNhapHang = @"insert into CHITIETNHAPMONAN
                                             values(" + maHoaDonNhap + ", " + getMaNguyenLieuTheoTen(tenNL) + ", " + soLuong + ")";
            execSQL(insertChiTietNhapHang);

            string updateTongTienNhap = @"update NHAPMONAN
                                          set THANHTIEN = " + soLuong + " * " + getGiaNguyenLieu(tenNL) + " where MADONNHAP = " + maHoaDonNhap + "";
            execSQL(updateTongTienNhap);

            string updateSoLuongNguyenLieu = @"update NGUYENLIEU
                                               set SOLUONG = " + getSoLuongCuaNguyenLieu(tenNL) + " + " + soLuong + " where TENNGUYENLIEU = N'" + tenNL + "'";
            execSQL(updateSoLuongNguyenLieu);

            conn.Close();
        }

        public int getSoLuongCuaNguyenLieu(string tenNL)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            DataTable dtSLNL = new DataTable();
            string sqlGetTenNguyenLieu = @"select SOLUONG from NGUYENLIEU where TENNGUYENLIEU = N'" + tenNL + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlGetTenNguyenLieu, Properties.Settings.Default.Connection);
            da.Fill(dtSLNL);
            soLuongNL = int.Parse(dtSLNL.Rows[0][0].ToString());
            conn.Close();
            return soLuongNL;
        }

        public int getGiaNguyenLieu(string tenNL)
        {
            string sqlGetMaNguyenLieu = @"select GIA from NguyenLieu where TENNGUYENLIEU = N'" + tenNL + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlGetMaNguyenLieu, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GiaNL = int.Parse(dt.Rows[0][0].ToString());
            }
            return GiaNL;
        }

        public int getMaNguyenLieuTheoTen(string tenNL)
        {
            string sqlGetMaNguyenLieu = @"select MANGUYENLIEU from NGUYENLIEU where TENNGUYENLIEU = N'" + tenNL + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlGetMaNguyenLieu, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public int LayMaHoaDonNhapNguyenLieu()
        {
            string sqlGetMaMonAn = @"select COUNT(*) from NHAPMONAN";
            SqlDataAdapter da = new SqlDataAdapter(sqlGetMaMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                maHoaDonNhapNL = int.Parse(dt.Rows[0][0].ToString());
            }
            return maHoaDonNhapNL;
        }

        public int layMaNguyenLieuMoi()
        {
            string sqlMaNL = @"select COUNT(*) from NGUYENLIEU";
            SqlDataAdapter da = new SqlDataAdapter(sqlMaNL, Properties.Settings.Default.Connection);
            DataTable dtNLMOI = new DataTable();
            da.Fill(dtNLMOI);
            if (dtNLMOI.Rows.Count > 0)
            {
                maNLMoi = int.Parse(dtNLMOI.Rows[0][0].ToString());
            }
            return maNLMoi;
        }

        public void themNguyenLieuMoi(string tenMonAn, string tenNL, int sLuong, string DVT, int Gia)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            int maNguyenLieu = layMaNguyenLieuMoi() + 1;
            string insertNguyenLieu = @"INSERT INTO NGUYENLIEU VALUES(" + maNguyenLieu + ", N'" + tenNL + "', 0, N'" + DVT + "', " + Gia + ")";
            execSQL(insertNguyenLieu);

            string insertCongThucNauMonAn = @"INSERT INTO CONGTHUCCHEBIENMONAN
                                              VALUES(" + getMaMonAn(tenMonAn) + ", " + maNguyenLieu + " , 1, "+sLuong+")";
            execSQL(insertCongThucNauMonAn);
            conn.Close();
        }

        int maNLThemMoi;
        public int layMaNguyenLieu()
        {
            string sqlSLTongNL = @"select COUNT(*) from NGUYENLIEU ";
            SqlDataAdapter da = new SqlDataAdapter(sqlSLTongNL, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                maNLThemMoi = int.Parse(dt.Rows[0][0].ToString());
            }
            return maNLThemMoi;
        }

        public DataTable loadDSDVT()
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            string sqlloadDVT = @" select DONVITINH from NGUYENLIEU group by DONVITINH";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadDVT, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public int kiemTraNguyenLieu(string tenNguyenLieu)
        {
            string sqlSLTongNL = @"select count(*) from NGUYENLIEU WHERE TENNGUYENLIEU = N'"+ tenNguyenLieu +"'";
            SqlDataAdapter da = new SqlDataAdapter(sqlSLTongNL, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
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
