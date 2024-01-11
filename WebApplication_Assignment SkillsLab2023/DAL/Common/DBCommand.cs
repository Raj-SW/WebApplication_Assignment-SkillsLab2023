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
        public async Task<List<T>> GetDataAsync<T>(string query) where T : new()
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            List<T> resultList = new List<T>();
                using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection))
                {
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            T mappedObject = MapToObject<T>(reader);
                            resultList.Add(mappedObject);
                        }
                    }
                }
            return resultList;
        }
        public async Task<List<T>> GetDataWithConditionsAsync<T>(string query, List<SqlParameter> parameters=null) where T : new()
            {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            List<T> resultList = new List<T>();
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
            dataAccessLayer.CloseConnection();
            return resultList;
            }
        public async Task<bool> IsRowExistsAsync(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            using (SqlTransaction transaction = dataAccessLayer.connection.BeginTransaction())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection, transaction))
                    {
                        command.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            transaction.Commit();
                            return await reader.ReadAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public async Task<bool> InsertUpdateDataAsync(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();
            using (SqlTransaction transaction = dataAccessLayer.connection.BeginTransaction())
            {
                try
                {
                    await dataAccessLayer.OpenConnectionAsync();
                    int rowsAffected = 0;
                    using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection, transaction))
                    {
                        command.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            parameters.ForEach(parameter =>
                            {
                                command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                            });
                        }
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    transaction.Commit();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    transaction.Rollback();
                    return false;
                }
                finally
                {
                    dataAccessLayer.CloseConnection();
                }
            }
        }
        public async Task<bool> UpdateDataNoConditionsAsync(string query)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            using (SqlTransaction transaction = dataAccessLayer.connection.BeginTransaction())
            {
                try
                {
                    await dataAccessLayer.OpenConnectionAsync();
                    int rowsAffected = 0;
                    using (SqlCommand command = new SqlCommand(query, dataAccessLayer.connection, transaction))
                    {
                        command.CommandType = CommandType.Text;
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    transaction.Commit();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    transaction.Rollback();
                    return false;
                }
                finally
                {
                    dataAccessLayer.CloseConnection();
                }
            }
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
