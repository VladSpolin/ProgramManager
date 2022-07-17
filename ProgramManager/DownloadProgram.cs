using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProgramManager
{
    internal class DownloadProgram
    {
        DataBase dataBase = new DataBase();
        public void Download(string name)
        {
            dataBase.openConnection();
            SqlCommand SqlCommand = new SqlCommand($"select link from Programs where programName = '{name}'", dataBase.GetConnection());
            SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();
            while (SqlDataReader.Read())
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFileAsync(new Uri(SqlDataReader.GetValue(0).ToString()), $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{name}.exe");
                }
            }
           dataBase.closeConnection();
        }
    }
}
