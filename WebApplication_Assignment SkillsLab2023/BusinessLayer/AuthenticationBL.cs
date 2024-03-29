﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Services;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class AuthenticationBL : IAuthenticationBL
    {
        private readonly IAuthenticationDAL _authenticationDAL;
        public AuthenticationBL(IAuthenticationDAL authenticationDAL) {
            _authenticationDAL = authenticationDAL;
        }
        public async Task<DataModelResult<UserModel>> LoginUserAsync(CredentialModel model)
        {
            DataModelResult<UserModel> UserDataModelResult = new DataModelResult<UserModel>();
            var retrieveCredentialModelByEmailResult = await GetCredentialModelByEmailAsync(model);
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
                model.Password = PasswordHashing.HashPassword(model.RawPassword, retrieveCredentialModelByEmailResult.ResultObject.Salt);
                if (model.Password.SequenceEqual(retrieveCredentialModelByEmailResult.ResultObject.Password))
                {
                    UserDataModelResult.ResultObject = await GetUserModelByIDAsync(retrieveCredentialModelByEmailResult.ResultObject.UserId);
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
        public async Task<TaskResult> RegisterUserAsync(UserAndCredentialDTO dto)
        {
            TaskResult result = await IsUserModelUniqueAsync(dto);
            if (result.isSuccess)
            {
                dto.credentialModel.Salt = PasswordHashing.GenerateTimestampSalt();
                dto.credentialModel.Password = PasswordHashing.HashPassword(dto.credentialModel.RawPassword, dto.credentialModel.Salt);
                result.isSuccess = await InsertUserModelCredentialModelAsync(dto.userModel, dto.credentialModel);
                if (result.isSuccess)
                {
                    result.AddResultMessage("Successful Registration. Wait For Admin to Activate your Account");
                }
                else
                {
                    result.AddResultMessage("Internal Server Error. We'll get back to you soon");
                }
            }
            return result;
        }
        public void Logout()
        {
            throw new NotImplementedException();
        }
        public async Task<TaskResult> IsUserModelUniqueAsync(UserAndCredentialDTO dto)
        {
            TaskResult taskResult = new TaskResult();
            taskResult.isSuccess = true;
            if (await isEmailUniqueAsync(dto))
            {
                taskResult.AddResultMessage("Email is already taken\n");
                taskResult.isSuccess = false;
            }
            if (await isNicUniqueAsync(dto))
            {
                taskResult.AddResultMessage("NIC is already taken\n");
                taskResult.isSuccess = false;
            }
            if (await isMobileNumUniqueAsync(dto))
            {
                taskResult.AddResultMessage("Mobile Number is already taken\n");
                taskResult.isSuccess = false;
            }
            return taskResult;
        }
        public async Task<bool> isEmailUniqueAsync(UserAndCredentialDTO dto)
        {
            return await _authenticationDAL.isEmailUniqueAsync(dto);
        }
        public async Task<bool> isNicUniqueAsync(UserAndCredentialDTO dto)
        {
            return await _authenticationDAL.isNicUniqueAsync(dto);
        }
        public async Task<bool> isMobileNumUniqueAsync(UserAndCredentialDTO dto)
        {
            return await _authenticationDAL.isMobileNumUniqueAsync(dto);
        }
        public async Task<int> GetUserModelIDbyNICAsync(UserModel model)
        {
            return await _authenticationDAL.GetUserModelIDbyNICAsync(model);
        }
        public async Task<bool> InsertCredentialModelAsync(CredentialModel model)
        {
            return await _authenticationDAL.InsertCredentialModelAsync(model);
        }
        public async Task<UserModel> GetUserModelByIDAsync(byte id)
        {
            return await _authenticationDAL.GetUserModelByIDAsync(id);
        }
        public async Task<DataModelResult<CredentialModel>> GetCredentialModelByEmailAsync(CredentialModel model) 
        {
            DataModelResult<CredentialModel> result = await _authenticationDAL.GetCredentialModelByEmailAsync(model);
           return result;
        }
        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            return await _authenticationDAL.GetAllRolesAsync();
        }
        public async Task<bool> InsertUserModelCredentialModelAsync(UserModel userModel, CredentialModel credentialModel)
        {
            return await _authenticationDAL.InsertUserModelCredentialModelAsync(userModel,credentialModel);
        }
    }
}
