using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public interface IAuthenticationDAL
    {
        bool IsCredentialsExists(CredentialModel model);
        UserModel GetUserModelByID(CredentialModel model);
        bool IsUserModelUnique(UserModel model);
        bool InsertUserModel(UserModel model);

    }
}