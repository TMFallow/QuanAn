using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Settings
    {
        public int check_Config()
        {
            if (Properties.Settings.Default.Connection == string.Empty)
            {
                return 1;
            }
            SqlConnection _Sqlkn = new SqlConnection(Properties.Settings.Default.Connection);
            try
            {
                if (_Sqlkn.State == System.Data.ConnectionState.Closed)
                    _Sqlkn.Open();
                return 0;
            }
            catch
            {
                return 2;
            }
        }
    }
}
