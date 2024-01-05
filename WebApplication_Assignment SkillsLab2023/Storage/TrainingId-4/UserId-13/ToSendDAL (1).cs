using Framework.AppLogger;
using Framework.Custom;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Framework.DAL
{
    public class ToSendDAL : IDataAccessLayer
    {
        public ToSendDAL()
        {
            Connect();
        }
        public SqlConnection Connection { get; private set; }


        public void Connect()
        {
            try
            {
                if (Connection == null)
                {
                    var connectionString = ConfigurationManager.AppSettings["DefaultConnectionString"];
                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        Connection = new SqlConnection(connectionString);
                        Connection.Open();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Connect(string connectionString)
        {
            try
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    Connection = new SqlConnection(connectionString);
                    Connection.Open();
                }
            }
            catch (Exception ex)
            {
                return "Unable to find the connection string " + ex.Message;
            }

            return "DB Connect: OK";
        }

        public void Disconnect()
        {
            if (Connection != null && Connection.State.Equals(ConnectionState.Open))
            {
                Connection.Close();
            }
        }

        public List<T> GetData<T>(string sql = "", List<SqlParameter> parameters = null)
        {
            sql = GenerateSelectQuery<T>();
            var result = new List<T>();
            using (var command = new SqlCommand(sql, Connection))
            {
                if (parameters != null && parameters.Any())
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }
                T item;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item = Activator.CreateInstance<T>();
                        Populate(item, reader);
                        result.Add(item);
                    }
                }

                reader.Close();
            }
            return result;
        }

        public int Insert<T>(T item)
        {
            int rowAdded = -1;

            try
            {
                string sql = GenerateInsertQuery<T>();
                List<SqlParameter> parameters = GenerateSqlParameters<T>(item);
                using (var command = new SqlCommand(sql, Connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    rowAdded = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                CustomException customException = new CustomException(ex.Message, ex);
                customException.Log();
            }
            return rowAdded;
        }

        private string GenerateSelectQuery<T>(bool includeCondition = false, List<string> conditionsProps = null)
        {
            StringBuilder columns = new StringBuilder();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!property.CanRead) continue;
                columns.Append(property.Name + ",");
            }
            columns.Remove(columns.Length - 1, 1); // Remove the last comma
            string condition = string.Empty;

            if (includeCondition && conditionsProps != null)
            {
                // build Where clause
                condition = " WHERE ";
            }

            string sql = $"SELECT {columns.ToString()} from {typeof(T).Name} {condition}";
            return sql;
        }
        private string GenerateInsertQuery<T>()
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties.Where(prop => !Attribute.IsDefined(prop, typeof(KeyAttribute))))
            {
                if (!property.CanRead) continue;
                columns.Append(property.Name + ",");
                values.Append("@" + property.Name + ",");
            }
            columns.Remove(columns.Length - 1, 1); // Remove the last comma
            values.Remove(values.Length - 1, 1); // Remove the last comma

            string query = $"INSERT INTO {typeof(T).Name} ({columns.ToString()}) VALUES ({values.ToString()})";
            return query;
        }

        private static void Populate<T>(T item, SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string propertyName = reader.GetName(i);
                PropertyInfo property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null && !reader.IsDBNull(i))
                {
                    Type targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    object value = Convert.ChangeType(reader.GetValue(i), targetType);
                    property.SetValue(item, value);
                }
            }
        }
        private List<SqlParameter> GenerateSqlParameters<T>(T item, bool includePK = false)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            var propLists = includePK ? properties.ToList() : properties.Where(prop => !Attribute.IsDefined(prop, typeof(KeyAttribute)))
            .ToList();

            foreach (PropertyInfo property in propLists)
            {
                if (!property.CanRead) continue;

                SqlParameter parameter = new SqlParameter("@" + property.Name, property.GetValue(item) ?? DBNull.Value);
                parameters.Add(parameter);
            }

            return parameters;
        }
    }
}
