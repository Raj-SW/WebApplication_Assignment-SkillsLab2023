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
        public const string INSERT_USER_CREDENTIAL_MODEL_QUERY = @"INSERT INTO [Credential] (UserId,Email,Password) SELECT @UserId,@Email,@Password";
        public AuthenticationDAL(IDBCommand command)
        {
            _command = command;
        }
        public bool IsCredentialsExists(CredentialModel model)
        {
            const string RETRIEVE_USER_CREDENTIALS_QUERY = @"SELECT * FROM [Credential] WHERE @Password = Password AND @Email=Email";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Password", model.Password));
            parameters.Add(new SqlParameter("@Email", model.Email));
            var dt = _command.GetDataWithConditions(RETRIEVE_USER_CREDENTIALS_QUERY, parameters);
            return dt.Rows.Count > 0;
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
            return dt.Rows.Count > 0;
        }
        public bool InsertUserModelCredentialModel(UserModel userModel,CredentialModel credentialModel)
        {
            const string INSERT_USER_MODEL_CREDENTIAL_MODEL_QUERY
                = @"INSERT INTO [User] (NIC, UserFirstName, UserLastName, MobileNum)
                    SELECT @NIC,@UserFirstName,@UserLastName,@MobileNum;

                    DECLARE @UserId INT;
                    SET @UserId = SCOPE_IDENTITY();

                    INSERT INTO [Credential] (UserId, Email, [Password])
                    SELECT @UserId, @Email, @Password;";
            try
            {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", userModel.NIC));
            parameters.Add(new SqlParameter("@UserFirstName", userModel.UserFirstName));
            parameters.Add(new SqlParameter("@UserLastName", userModel.UserLastName));
            parameters.Add(new SqlParameter("@MobileNum", userModel.MobileNum));
            parameters.Add(new SqlParameter("@Email", credentialModel.Email));
            parameters.Add(new SqlParameter("@Password", credentialModel.Password));
            _command.InsertUpdateData(INSERT_USER_MODEL_CREDENTIAL_MODEL_QUERY, parameters);
            return true;
            }
            catch (Exception ex)
            { 
                throw ex;
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
                throw ex;
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
        public bool InsertUserModel(UserModel model)
        {

            throw new NotImplementedException();
        }
        public bool isEmailUnique(RegistrationDTO dto)
        {
            const string GET_MODEL_BY_EMAIL = @"SELECT * FROM [CREDENTIAL] WHERE Email=@Email";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", dto.credentialModel.Email));
            var dt = _command.GetDataWithConditions(GET_MODEL_BY_EMAIL, parameters);
            return dt.Rows.Count > 0;
        }
        public bool isNicUnique(RegistrationDTO dto)
        {
            const string GET_MODEL_BY_NIC = @"SELECT * FROM [User] WHERE NIC=@NIC";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NIC", dto.userModel.NIC));
            var dt = _command.GetDataWithConditions(GET_MODEL_BY_NIC, parameters);
            return dt.Rows.Count > 0;
        }
        public bool isMobileNumUnique(RegistrationDTO dto)
        {
            const string GET_MODEL_BY_MONILE_NUMBER = @"SELECT * FROM [User] WHERE MobileNum=@MobileNum";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MobileNum", dto.userModel.MobileNum));
            var dt = _command.GetDataWithConditions(GET_MODEL_BY_MONILE_NUMBER, parameters);
            return dt.Rows.Count > 0;
        }
        public int GetUserIdByCredentials(CredentialModel model)
        {
            const string GET_USER_ID_BY_CREDENTIAL = @"SELECT UserId FROM [Credential] WHERE Email=@Email AND Password=@Password";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email));
            parameters.Add(new SqlParameter("@Password", model.Password));
            var dt = _command.GetDataWithConditions(GET_USER_ID_BY_CREDENTIAL, parameters);
            return (int)dt.Rows[0]["UserId"];
        }
        public UserModel GetUserModelByCredentials(CredentialModel model) 
        {
            const string RETRIEVE_USER_MODEL_BY_CREDENTIALS_QUERY = @"SELECT u.*
                    FROM [User] u
                    INNER JOIN [Credential] c 
                    ON u.UserId = c.UserId
                    WHERE c.Email=@Email AND c.Password=@Password;";
            UserModel userModel = new UserModel();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email));
            parameters.Add(new SqlParameter("@Password", model.Password));
            var dt = _command.GetDataWithConditions(RETRIEVE_USER_MODEL_BY_CREDENTIALS_QUERY, parameters);
            foreach (DataRow row in dt.Rows)
            {
                userModel.UserId = (int)row["UserId"];
                userModel.UserFirstName = (string)row["UserFirstName"];
                userModel.UserLastName = (string)row["UserLastName"];
                userModel.Role = (string)row["Role"];
            }
            return userModel;
        }
        public UserModel GetUserModelByID(int id)
        {
            throw new NotImplementedException();
        }
        public DataModelResult<CredentialModel> GetCredentialModelByEmailAndPassword(CredentialModel model)
        {
            const string RETRIEVE_USER_CREDENTIALS_BY_EMAIL_AND_PASSWORD_QUERY = @"SELECT * FROM [Credential] WHERE Email = @Email AND Password = @Password";
            DataModelResult<CredentialModel> result = new DataModelResult<CredentialModel>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", model.Email));
            parameters.Add(new SqlParameter("@Password", model.Password));
            var dt = _command.GetDataWithConditions(RETRIEVE_USER_CREDENTIALS_BY_EMAIL_AND_PASSWORD_QUERY, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                result.ResultObject.AccessId = (int)row["AccessId"];
                result.ResultObject.UserId = (int)row["UserId"];
                result.ResultObject.Email = (string)row["Email"];
                result.ResultObject.Password = (string)row["Password"];
                result.ResultObject.Activated = Convert.ToBoolean(row["Activated"]);
            }
            return result;
        }
    }
}