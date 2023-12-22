using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public interface IAuthenticationDAL
    {
        bool IsCredentialsExists(CredentialModel model);
        UserModel GetUserModelByID(int id);
        bool IsUserModelUnique(UserModel model);
        bool InsertUserModelCredentialModel(UserModel userModel,CredentialModel credentialModel);
        bool InsertCredentialModel(CredentialModel model);
        int GetUserModelIDbyNIC(UserModel model);
        bool InsertUserModel(UserModel model);
        bool isEmailUnique(UserAndCredentialDTO dto);
        bool isNicUnique(UserAndCredentialDTO dto);
        bool isMobileNumUnique(UserAndCredentialDTO dto);
        int GetUserIdByCredentials(CredentialModel model);
        UserModel GetUserModelByCredentials(CredentialModel model);
        DataModelResult<CredentialModel> GetCredentialModelByEmail(CredentialModel model);
        List<UserRolesModel> GetUserRolesByUserId(int UserId);
        List<RoleModel> GetAllRoles();
    }
}