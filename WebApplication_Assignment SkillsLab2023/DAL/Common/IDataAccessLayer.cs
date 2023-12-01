using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_Assignment_SkillsLab2023.DAL.Common
{
    public interface IDataAccessLayer // :IDisposable
    {
        void OpenConnection();
        void CloseConnection();
        SqlConnection connection {get;set;}

    }
}
