using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebApplication_Assignment_SkillsLab2023.DAL.Common
{
    public interface IDBCommand
    {
        Task<List<T>> GetDataAsync<T>(string query) where T : new();
        Task<List<T>> GetDataWithConditionsAsync<T>(string query, List<SqlParameter> parameters) where T : new();
        Task<bool> InsertUpdateDataAsync(string query, List<SqlParameter> parameters);
        Task<bool> IsRowExistsAsync(string query, List<SqlParameter> parameters);

    }
}
