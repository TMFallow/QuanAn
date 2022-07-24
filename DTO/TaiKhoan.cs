using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DTO
{
    public class TaiKhoan
    {
        public int Check_Login(string user, string pass)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TAIKHOAN where TENDN ='" + user + "' and MATKHAU ='" + pass + "'", Properties.Settings.Default.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
