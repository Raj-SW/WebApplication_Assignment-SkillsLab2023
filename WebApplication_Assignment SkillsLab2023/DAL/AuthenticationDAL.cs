using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using System.Reflection;
using System.Data;
using WebApplication_Assignment_SkillsLab2023.Common;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class AuthenticationDAL : IAuthenticationDAL
    {
        private readonly IDBCommand _command;
        public AuthenticationDAL(IDBCommand command)
        {
            _command = command;
        }
        public const string AUTHENTICATE_USER_QUERY = @"
            SELECT * 
            FROM [Credential] 
            WHERE
            UserId = @UserId 
            AND
            Password = @Password";
        public const string RETRIEVE_USER_QUERY = @"
            SELECT * 
            FROM [User] 
            WHERE
            UserId = @UserId";
        public const string CHECK_UNIQUE_QUERY = @"
            SELECT *
            FROM Students 
            WHERE NIC = @NIC OR Email= @Email OR MobileNum=@MobileNum";
        public const string INSERT_USER = @"
            INSERT INTO Users 
            (NIC,UserName,DepartmentId,Email,MobileNum,GuardianName,Role,ManagerId)
            SELECT @NIC,@UserName,@DepartmentId,@Email,@MobileNum,@GuardianName,@Role,@ManagerId";

        public bool CheckCredentials(CredentialModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", model.UserId));
            parameters.Add(new SqlParameter("@Password", model.Password));
            var dt = _command.GetDataWithConditions(AUTHENTICATE_USER_QUERY, parameters);
            return dt.Rows.Count > 0;
        }

        public UserModel GetUserModel(CredentialModel model)
        {
            UserModel userModel = new UserModel();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", model.UserId));
            //DBCommand cmd = new DBCommand(new DataAccessLayer());
            var dt = _command.GetDataWithConditions(RETRIEVE_USER_QUERY, parameters);
            foreach (DataRow row in dt.Rows)
            {
                userModel.UserId = (int)row["UserId"];
                userModel.UserName = (string)row["UserName"];
                userModel.Role = (string)row["Role"];
            }
            return userModel;
        }

        public bool CheckUniqueness(UserModel model)
        {
            //this can be done in a more generic way whereby a generic methof
            //of type T can be used to query for each of attributes and this method can
            //be used many times 
            //for different attributes query
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@NIC", model.NIC));
            parameters.Add(new SqlParameter("@Email", model.Email));
            parameters.Add(new SqlParameter("@MobileNum", model.MobileNum));
            
            var dt = _command.GetDataWithConditions(CHECK_UNIQUE_QUERY, parameters);

            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void AddUser(UserModel model)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();

            //parameters.Add(new SqlParameter("@NIC", model.NIC));
            //parameters.Add(new SqlParameter("@UserName", model.UserName));
            //parameters.Add(new SqlParameter("@DepartmentId", model.DepartmentId));
            //parameters.Add(new SqlParameter("@Email", model.Email));
            //parameters.Add(new SqlParameter("@DOB", student.DOB));
            //parameters.Add(new SqlParameter("@GuardianName", student.GuardianName));
            //parameters.Add(new SqlParameter("@Email", student.Email));
            //parameters.Add(new SqlParameter("@NID", student.NID));
            //parameters.Add(new SqlParameter("@Status", student.Status));
            //parameters.Add(new SqlParameter("@TotalPoints", student.TotalPoints));

            //DBCommand.InsertUpdateData(INSERT_USER, parameters);
        }
    }
}