using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class TaiKhoanDAL
    {
        TaiKhoan taiKhoan = new TaiKhoan();
        public int Check_Login(string user, string pass)
        {
            return taiKhoan.Check_Login(user, pass);
        }
    }
}
