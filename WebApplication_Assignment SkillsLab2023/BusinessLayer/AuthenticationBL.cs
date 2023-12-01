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

        public UserModel Login(CredentialModel model)
        {
            UserModel RequestedUserModel;
            var request = _authenticationDAL.CheckCredentials(model);
            if (request)
            {
                //retrieve user model
                RequestedUserModel = _authenticationDAL.GetUserModel(model);
                return RequestedUserModel;
            }
            return null;
        }


        public void Logout()
        {
            throw new NotImplementedException();
        }

        public bool Register(UserModel model)
        {
            //check if employee nic, email an phone is unique
            var IsUserValid = _authenticationDAL.CheckUniqueness(model);
            //add to DB
            if(IsUserValid)
            {

            }
            return false;
        }
    }
}
