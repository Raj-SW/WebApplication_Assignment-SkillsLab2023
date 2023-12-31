﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using System.Reflection;
using System.Data;
using WebApplication_Assignment_SkillsLab2023.Common;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using System.Threading.Tasks;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class AuthenticationDAL : IAuthenticationDAL
    {
        private readonly IDBCommand _command;
        public AuthenticationDAL(IDBCommand command)
        {
            _command = command;
        }
        public async Task<bool> IsCredentialsExistsAsync(CredentialModel model)
        {
            const string RETRIEVE_USER_CREDENTIALS_QUERY = @"SELECT * FROM [Credential] WHERE @Password = Password AND @Email=Email";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Password", model.Password));
            parameters.Add(new SqlParameter("@Email", model.Email.ToLower()));
            var dt = await _command.GetDataWithConditionsAsync(RETRIEVE_USER_CREDENTIALS_QUERY, parameters);
            return dt.Rows.Count > 0;
        }
        public async Task<UserModel> GetUserModelByIDAsync(CredentialModel model)
        {
            const string RETRIEVE_USER_MODEL_QUERY_BY_ID = @"SELECT * FROM [User] WHERE UserId = @UserId";
            UserModel userModel = new UserModel();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", model.UserId));
            var dt = await _command.GetDataWithConditionsAsync(RETRIEVE_USER_MODEL_QUERY_BY_ID, parameters);
            foreach (DataRow row in dt.Rows)
            {
                userModel.UserId = (byte)row["UserId"];
                userModel.UserFirstName = (string)row["UserFirstName"];
                userModel.UserLastName = (string)row["UserLastName"];
            }
            return userModel;
        }
        public async Task<bool> IsUserModelUnique(UserModel model)
        {
            const string CHECK_UNIQUE_USER_MODEL_QUERY = @"SELECT * FROM [User] WHERE NIC = @NIC OR MobileNum=@MobileNum";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            parameters.Add(new SqlParameter("@MobileNum", model.MobileNum));
            var dt =await _command.GetDataWithConditionsAsync(CHECK_UNIQUE_USER_MODEL_QUERY, parameters);
            return dt.Rows.Count > 0;
        }
        public async Task<bool> InsertUserModelCredentialModelAsync(UserModel userModel,CredentialModel credentialModel)
        {
            const string INSERT_USER_MODEL_CREDENTIAL_MODEL_QUERY=
                    @"INSERT INTO [User] (NIC, UserFirstName, UserLastName, MobileNum)
                    SELECT @NIC,@UserFirstName,@UserLastName,@MobileNum;
                    DECLARE @UserId INT;
                    SET @UserId = SCOPE_IDENTITY();
                    INSERT INTO [Credential] (UserId, Email, [Password],Salt)
                    SELECT @UserId, @Email, @Password, @Salt;";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", userModel.NIC));
            parameters.Add(new SqlParameter("@UserFirstName", userModel.UserFirstName));
            parameters.Add(new SqlParameter("@UserLastName", userModel.UserLastName));
            parameters.Add(new SqlParameter("@MobileNum", userModel.MobileNum));
            parameters.Add(new SqlParameter("@Email", credentialModel.Email.ToLower()));
            parameters.Add(new SqlParameter("@Password", credentialModel.HashedPassword));
            parameters.Add(new SqlParameter("@Salt", credentialModel.Salt));
            await _command.InsertUpdateDataAsync(INSERT_USER_MODEL_CREDENTIAL_MODEL_QUERY, parameters);
            return true;
        }
        public async Task<bool> InsertCredentialModelAsync(CredentialModel model)
        {
                const string INSERT_USER_CREDENTIAL_MODEL_QUERY = @"INSERT INTO [Credential] (UserId,Email,Password) SELECT @UserId,@Email,@Password";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", model.UserId));
                parameters.Add(new SqlParameter("@Email", model.Email.ToLower()));
                parameters.Add(new SqlParameter("@Password", model.Password));
                await _command.InsertUpdateDataAsync(INSERT_USER_CREDENTIAL_MODEL_QUERY, parameters);
                return true;
        }
        public async Task<int> GetUserModelIDbyNICAsync(UserModel model)
        {
            const string RETRIEVE_USER_MODEL_QUERY_BY_NIC = @"SELECT * FROM [User] WHERE NIC = @NIC";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            var dt = await _command.GetDataWithConditionsAsync(RETRIEVE_USER_MODEL_QUERY_BY_NIC, parameters);
            var userId =(int) dt.Rows[0]["UserId"];
            return userId;
        }
        public async Task<bool> isEmailUniqueAsync(UserAndCredentialDTO dto)
        {
            const string GET_MODEL_BY_EMAIL = @"SELECT * FROM [CREDENTIAL] WHERE Email=@Email";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", dto.credentialModel.Email));
            var dt = await  _command.GetDataWithConditionsAsync(GET_MODEL_BY_EMAIL, parameters);
            return dt.Rows.Count > 0;
        }
        public async Task<bool> isNicUniqueAsync(UserAndCredentialDTO dto)
        {
            const string GET_MODEL_BY_NIC = @"SELECT * FROM [User] WHERE NIC=@NIC";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", dto.userModel.NIC));
            var dt = await _command.GetDataWithConditionsAsync(GET_MODEL_BY_NIC, parameters);
            return dt.Rows.Count > 0;
        }
        public async Task<bool> isMobileNumUniqueAsync(UserAndCredentialDTO dto)
        {
            const string GET_MODEL_BY_MONILE_NUMBER = @"SELECT * FROM [User] WHERE MobileNum=@MobileNum";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MobileNum", dto.userModel.MobileNum));
            var dt = await _command.GetDataWithConditionsAsync(GET_MODEL_BY_MONILE_NUMBER, parameters);
            return dt.Rows.Count > 0;
        }
        public async Task<int> GetUserIdByCredentialsAsync(CredentialModel model)
        {
            const string GET_USER_ID_BY_CREDENTIAL = @"SELECT UserId FROM [Credential] WHERE Email=@Email AND Password=@Password";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email.ToLower()));
            parameters.Add(new SqlParameter("@Password", model.Password));
            var dt = await _command.GetDataWithConditionsAsync(GET_USER_ID_BY_CREDENTIAL, parameters);
            return (int)dt.Rows[0]["UserId"];
        }
        public async Task<UserModel> GetUserModelByCredentialsAsync(CredentialModel model) 
        {
            const string RETRIEVE_USER_MODEL_BY_CREDENTIALS_QUERY = @"SELECT u.*
                    FROM [User] u
                    INNER JOIN [Credential] c 
                    ON u.UserId = c.UserId
                    WHERE c.Email=@Email AND c.Password=@Password;";
            UserModel userModel = new UserModel();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email.ToLower()));
            parameters.Add(new SqlParameter("@Password", model.Password));
            var dt = await _command.GetDataWithConditionsAsync(RETRIEVE_USER_MODEL_BY_CREDENTIALS_QUERY, parameters);
            foreach (DataRow row in dt.Rows)
            {
                userModel.UserId =  (byte)row["UserId"];
                userModel.UserFirstName = (string)row["UserFirstName"];
                userModel.UserLastName = (string)row["UserLastName"];
                userModel.Role = (string)row["Role"];
            }
            return userModel;
        }
        public async Task<UserModel> GetUserModelByIDAsync(int id)
        {
            const string GET_USER_MODEL_BY_ID_QUERY = @"SELECT TOP 1 * FROM [User] WHERE UserId = @UserId";
            UserModel userModel = new UserModel();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", id));
            var dt = await _command.GetDataWithConditionsAsync(GET_USER_MODEL_BY_ID_QUERY, parameters);
            foreach (DataRow row in dt.Rows)
            {
                userModel.UserId = (byte)row["UserId"];
                userModel.UserFirstName = (string)row["UserFirstName"];
                userModel.UserLastName = (string)row["UserLastName"];
            }
            return userModel;
        }
        public async Task<DataModelResult<CredentialModel>> GetCredentialModelByEmailAsync(CredentialModel model)
        {
            const string RETRIEVE_USER_CREDENTIALS_BY_EMAIL_QUERY = @"SELECT TOP 1 * FROM [Credential] WHERE Email = @Email";
            DataModelResult<CredentialModel> result = new DataModelResult<CredentialModel>();
            result.ResultTask.isSuccess = false;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email.ToLower()));
            var dt = await _command.GetDataWithConditionsAsync (RETRIEVE_USER_CREDENTIALS_BY_EMAIL_QUERY, parameters);
            if (dt.Rows.Count > 0)
            {
                result.ResultTask.isSuccess= true;
                DataRow row = dt.Rows[0];
                result.ResultObject.AccessId = (byte)row["AccessId"];
                result.ResultObject.UserId = (byte)row["UserId"];
                result.ResultObject.Email = row["Email"].ToString();
                result.ResultObject.HashedPassword = (byte[])row["Password"];
                result.ResultObject.Salt = (byte[])row["Salt"];
                result.ResultObject.Activated = Convert.ToBoolean(row["Activated"]);
            }
            return result;
        }
        public async Task<List<UserRolesModel>> GetUserRolesByUserIdAsync(int UserId)
        {
            const string RETRIEVE_USER_ROLES_BY_USER_ID_QUERY = @"SELECT * FROM Roles r INNER JOIN User_Roles ur ON ur.RoleId=r.RoleId WHERE ur.UserId=@UserId";
            List<UserRolesModel> UserRolesList = new List<UserRolesModel>();
            UserRolesModel userRolesModel;
            List<SqlParameter> parameters= new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId",UserId));
            var dt = await _command.GetDataWithConditionsAsync(RETRIEVE_USER_ROLES_BY_USER_ID_QUERY,parameters);
            foreach (DataRow row in dt.Rows)
            {
                userRolesModel = new UserRolesModel();
                userRolesModel.RoleId= (byte)row["RoleId"];
                userRolesModel.RoleName= (string)row["RoleName"];
                UserRolesList.Add(userRolesModel);
            }
            return UserRolesList;
        }
        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            const string GET_ALL_ROLES_MODEL_QUERY = @"SELECT * FROM Roles";
            List<RoleModel> RolesList = new List<RoleModel>();
            RoleModel RoleModel;
            var dt = await _command.GetDataAsync(GET_ALL_ROLES_MODEL_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                RoleModel = new RoleModel();
                RoleModel.RoleId = (byte)row["RoleId"];
                RoleModel.RoleName = (string)row["RoleName"];
                RolesList.Add(RoleModel);
            }
            return RolesList;
        }
    }
}