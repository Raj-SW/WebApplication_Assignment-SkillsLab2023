using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
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
        //public async Task<DataTable> GetDataWithConditionsAsync(string query, List<SqlParameter> parameters)
        //{
        //    DataAccessLayer dataAccessLayer = new DataAccessLayer();
        //    //await dataAccessLayer.OpenConnectionAsync();
        //    DataTable datatable = new DataTable();

        //    using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
        //    {
        //        command.CommandType = CommandType.Text;

        //        if (parameters != null)
        //        {
        //            parameters.ForEach(parameter =>
        //            {
        //                command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
        //            });
        //        }

        //        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
        //        {
        //            await Task.Run(() => sqlDataAdapter.Fill(datatable));
        //        }
        //    }

        //    dataAccessLayer.CloseConnection();
        //    return datatable;
        //}
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
        public async Task<SqlDataReader> GetDataReaderAsync(string query)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            await dataAccessLayer.OpenConnectionAsync();

            using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader dataReader = await Task.Run(() => command.ExecuteReader(CommandBehavior.CloseConnection));
                return dataReader;
            }
        }
        public async Task<bool> IsRowExistsAsync(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            try
            {
                using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
                {
                    command.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        return await reader.ReadAsync();
                    }
                }
            }
            finally
            {
                dataAccessLayer.CloseConnection();
            }
        }
        public async Task<List<T>> GetDataWithConditionsAsync<T>(string query, List<SqlParameter> parameters) where T : new()
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            List<T> resultList = new List<T>();
            try
            {
                using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
                {
                    command.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            T mappedObject = MapToObject<T>(reader);
                            resultList.Add(mappedObject);
                        }
                    }
                }
            }
            finally
            {
                dataAccessLayer.CloseConnection();
            }
            return resultList;
        }
        private T MapToObject<T>(SqlDataReader reader)
        {
            T result = Activator.CreateInstance<T>();
            var type = typeof(T);
            foreach (var propertyName in GetColumnNames(reader))
            {
                var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null && property.CanWrite)
                {
                    var value = reader[propertyName];
                    if (value != DBNull.Value)
                    {
                        property.SetValue(result, value);
                    }
                }
            }
            return result;
        }
        private IEnumerable<string> GetColumnNames(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                yield return reader.GetName(i);
            }
        }
    }
}
