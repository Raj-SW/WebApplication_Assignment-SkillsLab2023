using System.Collections.Generic;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface
{
    public interface IAuthenticationBL
    {
        DataModelResult<UserModel> LoginUser(CredentialModel model);
        TaskResult RegisterUser(UserAndCredentialDTO dto);
        void Logout();
        TaskResult IsUserModelUnique(UserAndCredentialDTO dto);
        bool IsCredentialsExists(CredentialModel model);
        bool InsertUserModel(UserModel model);
        bool InsertCredentialModel(CredentialModel model);
        bool IsUserActivated(CredentialModel model);
        int GetUserModelIDbyNIC(UserModel model);
        bool isEmailUnique(UserAndCredentialDTO dto);
        bool isNicUnique(UserAndCredentialDTO dto);
        bool isMobileNumUnique(UserAndCredentialDTO dto);
        DataModelResult<CredentialModel> GetCredentialModelByEmail(CredentialModel model);
        byte[] HashPassword(string password, byte[] salt);
        byte[] GenerateTimestampSalt();
        List<RoleModel> GetAllRoles();
    }
}
