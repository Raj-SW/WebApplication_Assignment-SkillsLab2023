using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.Common;

namespace WebApplication_Assignment_SkillsLab2023.DAL.Common
{
    public class DBCommand: IDBCommand
    {
        //private readonly IDataAccessLayer _dal;
        //public DBCommand(IDataAccessLayer dal) { 
        //    _dal = dal;
        //}
        //todos..

        public DataTable GetData(string query)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.OpenConnection();
            DataTable datatable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
            {
                command.CommandType = CommandType.Text;
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                {
                    sqlDataAdapter.Fill(datatable);
                }
            }
            //list of --generic list T, scan through DT   
            dataAccessLayer.CloseConnection();
            return datatable;
        }
        public void InsertUpdateData(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.OpenConnection();
            using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
            {
                command.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter => {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                command.ExecuteNonQuery();
            }
            dataAccessLayer.CloseConnection();
        }
        public DataTable GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.OpenConnection();
            DataTable datatable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
            {
                command.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter =>
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                {
                    sqlDataAdapter.Fill(datatable);
                }
            }
            dataAccessLayer.CloseConnection();
            return datatable;
        }
        public void UpdateDataNoConditions(String query)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.OpenConnection();  
            using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
            {
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
            dataAccessLayer.CloseConnection();
        }
        public object ExecuteScalar(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.OpenConnection();

            using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
            {
                command.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    parameters.ForEach(parameter =>
                    {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }

                object result = command.ExecuteScalar();

                dataAccessLayer.CloseConnection();

                return result;
            }
        }
    }
}