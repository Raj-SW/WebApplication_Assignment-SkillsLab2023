using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;

namespace WebApplication_Assignment_SkillsLab2023.Common
{
    public class DBCommand : IDBCommand
    {
        public async Task<DataTable> GetDataAsync(string query)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            //await dataAccessLayer.OpenConnectionAsync();
            DataTable datatable = new DataTable();
            
            using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
            {
                command.CommandType = CommandType.Text;
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                {
                    await Task.Run(() => sqlDataAdapter.Fill(datatable));
                }
            }

            dataAccessLayer.CloseConnection();
            return datatable;
        }

        public async Task InsertUpdateDataAsync(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            await dataAccessLayer.OpenConnectionAsync();

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

                await Task.Run(() => command.ExecuteNonQuery());
            }

            dataAccessLayer.CloseConnection();
        }

        public async Task<DataTable> GetDataWithConditionsAsync(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            //await dataAccessLayer.OpenConnectionAsync();
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
                    await Task.Run(() => sqlDataAdapter.Fill(datatable));
                }
            }

            dataAccessLayer.CloseConnection();
            return datatable;
        }

        public async Task UpdateDataNoConditionsAsync(string query)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            await dataAccessLayer.OpenConnectionAsync();

            using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
            {
                command.CommandType = CommandType.Text;
                await Task.Run(() => command.ExecuteNonQuery());
            }

            dataAccessLayer.CloseConnection();
        }

        public async Task<object> ExecuteScalarAsync(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            await dataAccessLayer.OpenConnectionAsync();

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

                object result = await Task.Run(() => command.ExecuteScalar());

                dataAccessLayer.CloseConnection();

                return result;
            }
        }
    }
}
