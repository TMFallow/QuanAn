using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace DTO
{
    public class ConfigDatabase
    {
        public DataTable layTenServer()
        {
            DataTable dt = new DataTable();
            dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            return dt;
        }

        public DataTable getDBName(string server, string user, string pass)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("select name from sys.Databases", "Data Source=" + server + ";Initial Catalog=master;User ID=" + user + ";pwd = " + pass + "");
            da.Fill(dt);
            return dt;
        }

        public void luuLaiChuoiCauHinh(string server, string user, string pass, string dbName)
        {
            DTO.Properties.Settings.Default.Connection = "Data Source=" + server + ";Initial Catalog=" + dbName + ";User ID=" + user + ";pwd = " + pass + "";
            DTO.Properties.Settings.Default.Save();
        }
    }
}
