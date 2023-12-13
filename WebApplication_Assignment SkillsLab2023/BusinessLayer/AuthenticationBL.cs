using System;
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
        public DataModelResult<UserModel> LoginUser(CredentialModel model)
        {
            DataModelResult<CredentialModel> CredentialModelResult = GetCredentialModelByEmailAndPassword(model);
            DataModelResult<UserModel> UserDataModelResult = new DataModelResult<UserModel>();
            if (CredentialModelResult.ResultTask.isSuccess)
            {
                if (CredentialModelResult.ResultObject.Activated)
                {
                    UserDataModelResult.ResultObject = GetUserModelByCredentials(model);
                    if (UserDataModelResult.ResultObject != null)
                    {
                        UserDataModelResult.ResultTask.isSuccess = true;
                        UserDataModelResult.ResultTask.AddResultMessage("Login Successful");
                    }
                    else
                    {
                        UserDataModelResult.ResultTask.isSuccess = false;
                        UserDataModelResult.ResultTask.AddResultMessage("Internal Server Error");
                    }
                }
                else
                {
                    UserDataModelResult.ResultTask.isSuccess=false;
                    UserDataModelResult.ResultTask.AddResultMessage("Your account has not yet been activated. Please contact the Admin");
                }
            }
            else
            {
                UserDataModelResult.ResultTask.isSuccess = false;
                UserDataModelResult.ResultTask.AddResultMessage("Sorry, Wrong Credentials!");
            }
          return UserDataModelResult;
        }
        public void Logout()
        {
            throw new NotImplementedException();
        }
        public TaskResult RegisterUser(RegistrationDTO dto)
        {
            TaskResult result = IsUserModelUnique(dto);
            if (result.isSuccess)
            {
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
        public TaskResult IsUserModelUnique(RegistrationDTO dto)
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
        public bool isEmailUnique(RegistrationDTO dto)
        {

           return _authenticationDAL.isEmailUnique(dto);
        }
        public bool isNicUnique(RegistrationDTO dto)
        {
            return _authenticationDAL.isNicUnique(dto);
        }
        public bool isMobileNumUnique(RegistrationDTO dto)
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
        public UserModel GetUserModelByCredentials(CredentialModel model)
        {
            DataModelResult<UserModel> result= new DataModelResult<UserModel>();
            return _authenticationDAL.GetUserModelByCredentials(model);
        }
        public DataModelResult<CredentialModel> GetCredentialModelByEmailAndPassword(CredentialModel model) 
        {
            DataModelResult<CredentialModel> result= _authenticationDAL.GetCredentialModelByEmailAndPassword(model);
            if (result.ResultObject!=null)
            {
                result.ResultTask.isSuccess=true;
            }
           return result;
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