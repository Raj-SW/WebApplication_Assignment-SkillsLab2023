using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;

namespace WebApplication_Assignment_SkillsLab2023.Common
{
    public class DataAccessLayer : IDataAccessLayer
    {
        //public const string connectionString = @"server=localhost;database=TrainingAssignment;uid=wbpoc;pwd=sql@tfs2008";
        //public SqlConnection connection;
        public SqlConnection connection { get; set; }

        public DataAccessLayer()
        {
            OpenConnection();
        }
        public void OpenConnection()
        {
            try
            {
                var connecionString = ConfigurationManager.AppSettings["DBConnection"];
                if (!string.IsNullOrEmpty(connecionString))
                {
                    connection = new SqlConnection(connecionString);
                    connection.Open();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
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