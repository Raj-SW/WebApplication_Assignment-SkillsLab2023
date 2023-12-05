using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using System.ComponentModel.DataAnnotations;
using WebApplication_Assignment_SkillsLab2023.Common;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class GenericDAL<T> : IGenericDAL<T>
    {
        public bool DeleteByID(int Id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetByID(int id)
        {
            DBCommand cmd = new DBCommand(new DataAccessLayer());
            string tableName = GetTableName<T>();
            string key = GetPrimaryKeyName<T>();
            string SELECT_QUERY= $"SELECT * FROM [{tableName}] WHERE {key}= {id}";
            var dt = cmd.GetData(SELECT_QUERY);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return mapRow(row);
            }
            return default; 
        }

        public bool Insert(T model)
        {
            throw new NotImplementedException();
        }

        public bool Update(T model)
        {
            throw new NotImplementedException();
        }

        public string GetTableName<T>()
        {
            Type type = typeof(T);
            TableAttribute tableAttribute = type.GetCustomAttribute<TableAttribute>();

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }
            throw new InvalidOperationException($"Type {type.FullName} does not have a Table attribute.");
        }

        public T mapRow(DataRow row)
        {
            T instance = Activator.CreateInstance<T>();

            foreach (DataColumn column in row.Table.Columns)
            {
                string propertyName = column.ColumnName;

                if (typeof(T).GetProperty(propertyName) != null)
                {
                    object value = row[propertyName];
                    typeof(T).GetProperty(propertyName).SetValue(instance, value);
                }
            }

            return instance;
        }
        public string GetPrimaryKeyName<T>()
        {
            var type = typeof(T);
            var primaryKeyProperty = type.GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)));
            if (primaryKeyProperty != null)
            {
                return primaryKeyProperty.Name;
            }
            return null;
        }
    }
}