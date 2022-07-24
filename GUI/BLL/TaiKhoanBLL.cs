using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class TaiKhoanBLL
    {
        TaiKhoanDAL tkdal = new TaiKhoanDAL();
        
        public int Check_TK_BLL(string user, string pass)
        {
            return tkdal.Check_Login(user, pass);
        }
    }
}
