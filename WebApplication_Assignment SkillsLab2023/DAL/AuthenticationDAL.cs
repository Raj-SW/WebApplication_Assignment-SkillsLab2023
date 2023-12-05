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
        public const string RETRIEVE_USER_MODEL_QUERY_BY_ID = @"SELECT * FROM [User] WHERE UserId = @UserId";
        public const string RETRIEVE_USER_MODEL_QUERY_BY_NIC = @"SELECT * FROM [User] WHERE NIC = @NIC";
        public const string CHECK_UNIQUE_USER_MODEL_QUERY = @"SELECT * FROM [User] WHERE NIC = @NIC OR MobileNum=@MobileNum";
        public const string INSERT_USER_MODEL_QUERY = @"INSERT INTO [User](NIC,UserFirstName,UserLastName,MobileNum)
                                                        SELECT @NIC,@UserFirstName,@UserLastName,@MobileNum";
        public const string INSERT_USER_CREDENTIAL_MODEL_QUERY = @"INSERT INTO [Credential] (UserId,Email,Password) SELECT @UserId,@Email,@Password";
        public const string RETRIEVE_USER_CREDENTIALS_QUERY = @"SELECT * FROM [Credential] WHERE Email = @Email AND Password = @Password";

        public AuthenticationDAL(IDBCommand command)
        {
            _command = command;
        }
        public int IsCredentialsExists(CredentialModel model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Password", model.Password));
            parameters.Add(new SqlParameter("@Email", model.Email));
            var dt = _command.GetDataWithConditions(RETRIEVE_USER_CREDENTIALS_QUERY, parameters);
            if(dt.Rows.Count > 0)
            {
                return (int)dt.Rows[0]["UserId"];
            }
            return 0;
        }
        public UserModel GetUserModelByID(CredentialModel model)
        {
            UserModel userModel = new UserModel();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", model.UserId));
            var dt = _command.GetDataWithConditions(RETRIEVE_USER_MODEL_QUERY_BY_ID, parameters);
            foreach (DataRow row in dt.Rows)
            {
                userModel.UserId = (int)row["UserId"];
                userModel.UserFirstName = (string)row["UserFirstName"];
                userModel.UserLastName = (string)row["UserLastName"];
                userModel.Activated = Convert.ToBoolean(row["Activated"]);
                //this breaks the code
                userModel.Role = (string)row["Role"];
            }
            return userModel;
        }
        public bool IsUserModelUnique(UserModel model)
        {
            //this can be done in a more generic way whereby a generic method of type T can be used to query for each of attributes
            //and this method can be used many times for different attributes query
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            parameters.Add(new SqlParameter("@MobileNum", model.MobileNum));
            var dt = _command.GetDataWithConditions(CHECK_UNIQUE_USER_MODEL_QUERY, parameters);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool InsertUserModel(UserModel model)
        {
            try
            {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            parameters.Add(new SqlParameter("@UserFirstName", model.UserFirstName));
            parameters.Add(new SqlParameter("@UserLastName", model.UserLastName));
            parameters.Add(new SqlParameter("@MobileNum", model.MobileNum));
            _command.InsertUpdateData(INSERT_USER_MODEL_QUERY, parameters);
            return true;
            }
            catch (Exception ex)
            { 
                throw;
            }
        }
        public bool InsertCredentialModel(CredentialModel model)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", model.UserId));
                parameters.Add(new SqlParameter("@Email", model.Email));
                parameters.Add(new SqlParameter("@Password", model.Password));
                _command.InsertUpdateData(INSERT_USER_CREDENTIAL_MODEL_QUERY, parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int GetUserModelIDbyNIC(UserModel model)
        {
            int userId;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            var dt = _command.GetDataWithConditions(RETRIEVE_USER_MODEL_QUERY_BY_NIC, parameters);
            userId =(int) dt.Rows[0]["UserId"];
            return userId;
        }
    }
}