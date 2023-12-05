using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
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
        public UserModel LoginUser(CredentialModel model)
        {
            UserModel UserModel;
            try
            {
                model.UserId = IsCredentialsExists(model);
                if (model.UserId!=0)
                {
                    UserModel = _authenticationDAL.GetUserModelByID(model);
                    return UserModel;
                }
            }
            catch(Exception exception) 
            {
                throw exception;
            }
            return null;
        }
        public void Logout()
        {
            throw new NotImplementedException();
        }
        public bool RegisterUser(RegistrationModel model)
        {
            var unique = IsUserModelUnique(model.userModel);
            if (unique)
            { 
                var insertUsermodel = InsertUserModel(model.userModel);
                if (insertUsermodel)
                { var usermodel=_authenticationDAL.GetUserModelbyNIC(model.userModel);
                    model.credentialModel.UserId = usermodel.UserId;
                    //get UserId here
                    var insert = InsertCredentialModel(model.credentialModel);
                    if (insert)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsUserModelUnique(UserModel model)
        {
            return _authenticationDAL.IsUserModelUnique(model);
        }
        public int IsCredentialsExists(CredentialModel model)
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
        public UserModel GetUserModelbyNIC(UserModel model)
        {
            return _authenticationDAL.GetUserModelbyNIC(model);
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