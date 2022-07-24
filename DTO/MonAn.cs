using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DTO
{
    public class MonAn
    {
        int maMonAn;

        public DataTable loadMonAn()
        {
            string sqlloadMonAn = @"select * from MONAN";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable loadDanhMucMonAnCanNhap()
        {
            string sqlloadMonAn = @"SELECT * FROM MONAN WHERE SLHIENCO < 10";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void themMonAn(string tenMonAn, string loai, string donViTinh, int gia)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            int maMonAn = layMaMonAnGanNhat() + 1;
            string sqlThemMonAn = @"INSERT INTO MONAN
                                    VALUES(" + maMonAn + ", N'" + tenMonAn + "', N'" + loai + "', N'" + donViTinh + "', 0, " + gia + ")";
            execSQL(sqlThemMonAn);
        }

        public int layMaMonAnGanNhat()
        {
            string sqlloadMonAn = @"select COUNT(*) from MONAN";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                maMonAn = int.Parse(dt.Rows[0][0].ToString());
            }
            return maMonAn;
        }

        public void suaGiaBan(int giaBan, int maMonAn)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            string sqlUpdateGiaMonAn = @"update MONAN
                                         set GIA = " + giaBan + " where MAMONAN = " + maMonAn + "";
            execSQL(sqlUpdateGiaMonAn);
            conn.Close();
        }

        public DataTable loadDSTenMonAn()
        {
            string sqlloadMonAn = @"select TENMONAN from MONAN";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable loadDsLoaiMonAn()
        {
            string sqlloadMonAn = @"select LOAI from MONAN group by LOAI";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable traVeDSMonAnTheoLoai(string Loai)
        {
            string sqlloadMonAn = @"select * from MONAN where LOAI = N'" + Loai + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable traVeMonAnTheoMa(int maMonAn)
        {
            string sqlloadMonAn = @"select * from MONAN where MAMONAN = " + maMonAn + "";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void updateLaiSoLuongTheoMaMonAn(int maMonAn)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            string sqlUpdateGiaMonAn = @"update MONAN
                                         set SLHIENCO = SLHIENCO - 1 where MAMONAN = " + maMonAn + "";
            execSQL(sqlUpdateGiaMonAn);
            conn.Close();
        }

        public void updateLaiSoLuongTheoMaMonAnKhiHuyGoiMon(int maMonAn)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.Connection);
            conn.Open();
            string sqlUpdateGiaMonAn = @"update MONAN
                                         set SLHIENCO = SLHIENCO + 1 where MAMONAN = " + maMonAn + "";
            execSQL(sqlUpdateGiaMonAn);
            conn.Close();
        }

        public DataTable timKiemTheoMaMonAn(string maMonAn)
        {
            string sqlloadMonAn = @"select * from MonAn where MAMONAN = "+ maMonAn +"";
            SqlDataAdapter da = new SqlDataAdapter(sqlloadMonAn, Properties.Settings.Default.Connection);
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
