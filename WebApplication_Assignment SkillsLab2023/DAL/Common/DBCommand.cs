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
        private readonly IDataAccessLayer _dal;
        public DBCommand(IDataAccessLayer dal) { 
            _dal = dal;
        }
        public DataTable GetData(string query)
        {
            

            _dal.OpenConnection();
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, _dal.connection))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }
            _dal.CloseConnection();
            return dt;
        }

        public void InsertUpdateData(string query, List<SqlParameter> parameters)
        {
            DataAccessLayer dal = new DataAccessLayer();
            using (SqlCommand cmd = new SqlCommand(query, dal.connection))
            {
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter => {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                cmd.ExecuteNonQuery();
            }
            dal.CloseConnection();
        }

        public DataTable GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            //DataAccessLayer dal = new DataAccessLayer();
            _dal.OpenConnection();
            DataTable dt = new DataTable();
            try
            {

            using (SqlCommand cmd = new SqlCommand(query, _dal.connection))
            {
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter =>
                    {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            _dal.CloseConnection();
            return dt;
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        //public void UpdateDataNoConditions(String query)
        //{
        //    DAL dal = new DAL();
        //    using (SqlCommand cmd = new SqlCommand(query, dal.connection))
        //    {
        //        cmd.CommandType = CommandType.Text;

        //        cmd.ExecuteNonQuery();
        //    }
        //    dal.CloseConnection();
        //}
    }
}