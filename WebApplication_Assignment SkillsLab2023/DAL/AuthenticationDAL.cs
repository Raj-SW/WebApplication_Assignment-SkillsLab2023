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

        #region Get
        public async Task<UserModel> GetUserModelByIDAsync(CredentialModel model)
        {
            const string RETRIEVE_USER_MODEL_QUERY_BY_ID = @"SELECT * FROM [User] WHERE UserId = @UserId";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", model.UserId));
            var result = await _command.GetDataWithConditionsAsync<UserModel>(RETRIEVE_USER_MODEL_QUERY_BY_ID, parameters);
            return result.FirstOrDefault();
        }
        public async Task<byte> GetUserModelIDbyNICAsync(UserModel model)
        {
            const string RETRIEVE_USER_MODEL_QUERY_BY_NIC = @"SELECT * FROM [User] WHERE NIC = @NIC";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            var result = await _command.GetDataWithConditionsAsync<byte>(RETRIEVE_USER_MODEL_QUERY_BY_NIC, parameters);
            return result.FirstOrDefault();
        }
        public async Task<byte> GetUserIdByCredentialsAsync(CredentialModel model)
        {
            const string GET_USER_ID_BY_CREDENTIAL = @"SELECT UserId FROM [Credential] WHERE Email=@Email AND Password=@Password";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email.ToLower()));
            parameters.Add(new SqlParameter("@Password", model.Password));
            var result = await _command.GetDataWithConditionsAsync<byte>(GET_USER_ID_BY_CREDENTIAL, parameters);
            return result.FirstOrDefault();
        }
        public async Task<UserModel> GetUserModelByCredentialsAsync(CredentialModel model) 
        {
            const string RETRIEVE_USER_MODEL_BY_CREDENTIALS_QUERY = @"SELECT u.*
                    FROM [User] u
                    INNER JOIN [Credential] c 
                    ON u.UserId = c.UserId
                    WHERE c.Email=@Email AND c.Password=@Password;";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email.ToLower()));
            parameters.Add(new SqlParameter("@Password", model.Password));
            var result = await _command.GetDataWithConditionsAsync<UserModel>(RETRIEVE_USER_MODEL_BY_CREDENTIALS_QUERY, parameters);
            return result.FirstOrDefault();
        }
        public async Task<UserModel> GetUserModelByIDAsync(int id)
        {
            const string GET_USER_MODEL_BY_ID_QUERY = @"SELECT TOP 1 * FROM [User] WHERE UserId = @UserId";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", id));
            var result = await _command.GetDataWithConditionsAsync<UserModel>(GET_USER_MODEL_BY_ID_QUERY, parameters);
            return result.FirstOrDefault();
        }
        public async Task<DataModelResult<CredentialModel>> GetCredentialModelByEmailAsync(CredentialModel model)
        {
            const string RETRIEVE_USER_CREDENTIALS_BY_EMAIL_QUERY = @"SELECT TOP 1 * FROM [Credential] WHERE Email = @Email";
            DataModelResult<CredentialModel> result = new DataModelResult<CredentialModel>();
            result.ResultTask.isSuccess = false;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email.ToLower()));
            List<CredentialModel> credentialList = await _command.GetDataWithConditionsAsync<CredentialModel>(RETRIEVE_USER_CREDENTIALS_BY_EMAIL_QUERY, parameters);
            if (credentialList.Count > 0)
            {
                result.ResultTask.isSuccess = true;
                result.ResultObject = credentialList[0];
              return result;
            }
            result.ResultTask.isSuccess = false;
            return result;
        }
        public async Task<List<UserRolesModel>> GetUserRolesByUserIdAsync(int UserId)
        {
            const string RETRIEVE_USER_ROLES_BY_USER_ID_QUERY = @"SELECT * FROM Roles r INNER JOIN User_Roles ur ON ur.RoleId=r.RoleId WHERE ur.UserId=@UserId";
            List<SqlParameter> parameters= new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId",UserId));
            return await _command.GetDataWithConditionsAsync<UserRolesModel>(RETRIEVE_USER_ROLES_BY_USER_ID_QUERY,parameters);
        }
        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            const string GET_ALL_ROLES_MODEL_QUERY = @"SELECT * FROM Roles";
            var result = await _command.GetDataAsync<RoleModel>(GET_ALL_ROLES_MODEL_QUERY);
            return result;
        }
        public async Task<bool> IsUserModelUnique(UserModel model)
        {
            const string CHECK_UNIQUE_USER_MODEL_QUERY = @"SELECT * FROM [User] WHERE NIC = @NIC OR MobileNum=@MobileNum";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", model.NIC));
            parameters.Add(new SqlParameter("@MobileNum", model.MobileNum));
            return await _command.IsRowExistsAsync(CHECK_UNIQUE_USER_MODEL_QUERY, parameters);
        }
        public async Task<bool> isEmailUniqueAsync(UserAndCredentialDTO dto)
        {
            const string GET_MODEL_BY_EMAIL = @"SELECT * FROM [CREDENTIAL] WHERE Email=@Email";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", dto.credentialModel.Email));
            return await  _command.IsRowExistsAsync(GET_MODEL_BY_EMAIL, parameters);
        }
        public async Task<bool> isNicUniqueAsync(UserAndCredentialDTO dto)
        {
            const string GET_MODEL_BY_NIC = @"SELECT * FROM [User] WHERE NIC=@NIC";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", dto.userModel.NIC));
            return await _command.IsRowExistsAsync(GET_MODEL_BY_NIC, parameters);
        }
        public async Task<bool> isMobileNumUniqueAsync(UserAndCredentialDTO dto)
        {
            const string GET_MODEL_BY_MOBILE_NUMBER = @"SELECT * FROM [User] WHERE MobileNum=@MobileNum";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MobileNum", dto.userModel.MobileNum));
            return await _command.IsRowExistsAsync(GET_MODEL_BY_MOBILE_NUMBER, parameters);
        }
        #endregion

        #region Insert
        public async Task<bool> InsertCredentialModelAsync(CredentialModel model)
        {
                const string INSERT_USER_CREDENTIAL_MODEL_QUERY = @"INSERT INTO [Credential] (UserId,Email,Password) SELECT @UserId,@Email,@Password";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", model.UserId));
                parameters.Add(new SqlParameter("@Email", model.Email.ToLower()));
                parameters.Add(new SqlParameter("@Password", model.Password));
                return await _command.InsertUpdateDataAsync(INSERT_USER_CREDENTIAL_MODEL_QUERY, parameters);
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
            parameters.Add(new SqlParameter("@Password", credentialModel.Password));
            parameters.Add(new SqlParameter("@Salt", credentialModel.Salt));
            return await _command.InsertUpdateDataAsync(INSERT_USER_MODEL_CREDENTIAL_MODEL_QUERY, parameters);
        }
        #endregion

    }
}