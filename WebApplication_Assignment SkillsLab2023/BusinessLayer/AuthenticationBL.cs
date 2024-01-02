using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class AuthenticationBL : IAuthenticationBL
    {
        private readonly IAuthenticationDAL _authenticationDAL;
        public AuthenticationBL(IAuthenticationDAL authenticationDAL) {
            _authenticationDAL = authenticationDAL;
        }
        public TaskResult RegisterUser(UserAndCredentialDTO dto)
        {
            TaskResult result = IsUserModelUnique(dto);
            if (result.isSuccess)
            {
                dto.credentialModel.Salt = GenerateTimestampSalt();
                dto.credentialModel.HashedPassword = HashPassword(dto.credentialModel.Password, dto.credentialModel.Salt);
                result.isSuccess = _authenticationDAL.InsertUserModelCredentialModel(dto.userModel, dto.credentialModel);
                if (result.isSuccess)
                {
                    result.AddResultMessage("Successfull Registration. Wait For Admin to Activate your Account");
                }
                else
                {
                    result.AddResultMessage("Internal Server Error. We'll get back to you soon");
                }
            }
            return result;
        }

        public DataModelResult<UserModel> LoginUser(CredentialModel model)
        {
            DataModelResult<UserModel> UserDataModelResult = new DataModelResult<UserModel>();
            var retrieveCredentialModelByEmailResult = GetCredentialModelByEmail(model);
            if (!retrieveCredentialModelByEmailResult.ResultTask.isSuccess)
            {
                UserDataModelResult.ResultTask.AddResultMessage("User not Found!");
                UserDataModelResult.ResultTask.isSuccess = false;
                return UserDataModelResult;
            }
            else if (!retrieveCredentialModelByEmailResult.ResultObject.Activated) 
            {
                UserDataModelResult.ResultTask.AddResultMessage("User not Activated Yet! Please contact Admin.");
                UserDataModelResult.ResultTask.isSuccess = false;
                return UserDataModelResult;
            }
            else
            {
                model.HashedPassword = HashPassword(model.Password, retrieveCredentialModelByEmailResult.ResultObject.Salt);
                if (model.HashedPassword.SequenceEqual(retrieveCredentialModelByEmailResult.ResultObject.HashedPassword))
                {
                    UserDataModelResult.ResultObject = GetUserModelByID(retrieveCredentialModelByEmailResult.ResultObject.UserId);
                    UserDataModelResult.ResultTask.AddResultMessage("Logged In Successfully");
                    UserDataModelResult.ResultTask.isSuccess = true;
                }
                else
                {
                    UserDataModelResult.ResultTask.AddResultMessage("Wrong Credentials");
                    UserDataModelResult.ResultTask.isSuccess = false;
                }
            }
            return UserDataModelResult;
        }
        public void Logout()
        {
            throw new NotImplementedException();
        }
      
        public TaskResult IsUserModelUnique(UserAndCredentialDTO dto)
        {
            TaskResult taskResult = new TaskResult();
            taskResult.isSuccess = true;
            if (isEmailUnique(dto))
            {
                taskResult.AddResultMessage("Email is already taken");
                taskResult.isSuccess = false;
            }
            if (isNicUnique(dto))
            {
                taskResult.AddResultMessage("NIC is already taken");
                taskResult.isSuccess = false;
            }
            if (isMobileNumUnique(dto))
            {
                taskResult.AddResultMessage("Mobile Number is already taken");
                taskResult.isSuccess = false;
            }

            return taskResult;
        }
        public bool IsCredentialsExists(CredentialModel model)
        {
            return _authenticationDAL.IsCredentialsExists(model);
        }
        public bool InsertUserModel(UserModel model)
        {
            return _authenticationDAL.InsertUserModel(model);
        }
        public bool IsUserActivated(CredentialModel model)
        {
            throw new NotImplementedException();
        }
        public bool InsertCredentialModel(CredentialModel model)
        {
            return _authenticationDAL.InsertCredentialModel(model);
        }
        public int GetUserModelIDbyNIC(UserModel model)
        {
            return _authenticationDAL.GetUserModelIDbyNIC(model);
        }
        public bool isEmailUnique(UserAndCredentialDTO dto)
        {

           return _authenticationDAL.isEmailUnique(dto);
        }
        public bool isNicUnique(UserAndCredentialDTO dto)
        {
            return _authenticationDAL.isNicUnique(dto);
        }
        public bool isMobileNumUnique(UserAndCredentialDTO dto)
        {
            return _authenticationDAL.isMobileNumUnique(dto);
        }
        public int GetUserIdByCredentials(CredentialModel model) 
        {
            return _authenticationDAL.GetUserIdByCredentials(model);    
        }
        public UserModel GetUserModelByID(int id)
        {
            return _authenticationDAL.GetUserModelByID(id);
        }
        public DataModelResult<CredentialModel> GetCredentialModelByEmail(CredentialModel model) 
        {
            DataModelResult<CredentialModel> result= _authenticationDAL.GetCredentialModelByEmail(model);
           return result;
        }
        public byte[] HashPassword(string password, byte[] salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return hashedBytes;
            }
        }
        public byte[] GenerateTimestampSalt()
        {
            long ticks = DateTime.UtcNow.Ticks;
            byte[] saltBytes = BitConverter.GetBytes(ticks);
            return saltBytes;
        }

        public List<RoleModel> GetAllRoles()
        {
            return _authenticationDAL.GetAllRoles();
        }
    }
}






























//public UserModel Login(CredentialModel model)
//{
//    UserModel requestUserModel;
//    GenericDAL<CredentialModel> credentialDAL = new GenericDAL<CredentialModel>();
//    var requestModel = credentialDAL.GetByID(model.UserId);
//    if (requestModel != null)
//    {
//        GenericDAL<UserModel> UserModelDAL = new GenericDAL<UserModel>();
//        requestUserModel = UserModelDAL.GetByID(requestModel.UserId);
//        return requestUserModel;
//    }
//    throw new Exception();
//}