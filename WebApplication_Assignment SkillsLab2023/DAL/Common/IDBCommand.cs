﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebApplication_Assignment_SkillsLab2023.DAL.Common
{
    public interface IDBCommand
    {
        Task<DataTable> GetDataAsync(string query);
        Task InsertUpdateDataAsync(string query, List<SqlParameter> parameters);
        //Task<DataTable> GetDataWithConditionsAsync(string query, List<SqlParameter> parameters);
        Task<List<T>> GetDataWithConditionsAsync<T>(string query, List<SqlParameter> parameters) where T : new();
        Task<bool> IsRowExistsAsync(string query, List<SqlParameter> parameters);

    }
}
