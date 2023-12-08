using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public interface IAuthenticationDAL
    {
        int IsCredentialsExists(CredentialModel model);
        UserModel GetUserModelByID(CredentialModel model);
        bool IsUserModelUnique(UserModel model);
        bool InsertUserModelCredentialModel(UserModel userModel,CredentialModel credentialModel);
        bool InsertCredentialModel(CredentialModel model);
        int GetUserModelIDbyNIC(UserModel model);
        bool InsertUserModel(UserModel model);
    }
}