using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Ini;
using System.Windows.Forms;

namespace Connection
{
    public class clsCMScon 
    {
        public SqlConnection sqlConStr;
      
        public void Connect()
        {
            String getConString;

            sqlConStr = new SqlConnection();
            getConString = GetConString();
            sqlConStr.ConnectionString = getConString; 
            sqlConStr.Open();
        }

        public String GetConString()
        {
            String vUsername;
            String vPassword;
            String vDatasource;
            String vInitialCatalog;
            String sqlConString;

            IniFile inifle = new IniFile(@"C:\Dashboard.ini");

            vUsername = inifle.IniReadValue("wowrox", "User_ID");
            vPassword = inifle.IniReadValue("wowrox", "Password");
            vDatasource = inifle.IniReadValue("wowrox", "Data_Source");
            vInitialCatalog = inifle.IniReadValue("wowrox", "Initial_Catalog");

            sqlConString = "Data Source=" + vDatasource + ";User ID=" + vUsername + ";Password=" + vPassword + ";Initial Catalog=" + vInitialCatalog ;

            return sqlConString; 
        }

        public void DisConnect()
        {
            try
            {
                sqlConStr.Close();
                sqlConStr.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }
    }
}
