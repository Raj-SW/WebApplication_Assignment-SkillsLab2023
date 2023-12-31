using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class UserDAL:IUserDAL
    {
        private readonly IDBCommand _command;
        public UserDAL(IDBCommand command)
        {
            _command = command;
        }
        public List<UserRolesModel> GetAllUserRolesModelByUserId(int UserId)
        {
            const string RETRIEVE_USER_ROLES_BY_USER_ID_QUERY = @"SELECT * FROM Roles r INNER JOIN User_Roles ur ON ur.RoleId=r.RoleId WHERE ur.UserId=@UserId";
            List<UserRolesModel> UserRolesList = new List<UserRolesModel>();
            UserRolesModel userRolesModel;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", UserId));
            var dt = _command.GetDataWithConditions(RETRIEVE_USER_ROLES_BY_USER_ID_QUERY, parameters);
            foreach (DataRow row in dt.Rows)
            {
                userRolesModel = new UserRolesModel();
                userRolesModel.RoleId = (byte)row["RoleId"];
                userRolesModel.RoleName = (string)row["RoleName"];
                UserRolesList.Add(userRolesModel);
            }
            return UserRolesList;
        }
    }
}