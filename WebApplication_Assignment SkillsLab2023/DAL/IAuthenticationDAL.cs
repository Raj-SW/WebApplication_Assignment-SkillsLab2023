using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public interface IAuthenticationDAL
    {
        bool CheckCredentials(CredentialModel model);
        UserModel GetUserModel(CredentialModel model);
        bool CheckUniqueness(UserModel model);
    }
}