using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;

namespace WebApplication_Assignment_SkillsLab2023.Common
{
    public class DataAccessLayer : IDataAccessLayer
    {
        public SqlConnection connection;
        string connectionString;

        public DataAccessLayer()
        {
            connectionString = ConfigurationManager.AppSettings["DBConnection"];
            connection = new SqlConnection(connectionString);
            OpenConnection(); // Wait for the asynchronous operation to complete
        }
        public async Task OpenConnectionAsync()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                await connection.OpenAsync();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void OpenConnection()
        {
            Task.Run(() => OpenConnectionAsync()).Wait(); // Wait for the asynchronous operation to complete
            //if (connection.State == System.Data.ConnectionState.Open)
            //{
            //    connection.Close();
            //}
            //connection.Open();
        }
        public void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
