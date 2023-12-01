using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_Assignment_SkillsLab2023.DAL.Common
{
    public interface IDBCommand
    {
        DataTable GetData(string query);
        void InsertUpdateData(string query, List<SqlParameter> parameters);
        DataTable GetDataWithConditions(string query, List<SqlParameter> parameters);
        //void UpdateDataNoConditions(String query);
    }
}
