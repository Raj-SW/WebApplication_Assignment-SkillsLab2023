using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public interface IAuthenticationDAL
    {
        Task<bool> IsCredentialsExistsAsync(CredentialModel model);
        Task<UserModel> GetUserModelByIDAsync(int id);
        Task<bool> IsUserModelUnique(UserModel model);
        Task<bool> InsertUserModelCredentialModelAsync(UserModel userModel, CredentialModel credentialModel);
        Task<bool> InsertCredentialModelAsync(CredentialModel model);
        Task<int> GetUserModelIDbyNICAsync(UserModel model);
        Task<bool> isEmailUniqueAsync(UserAndCredentialDTO dto);
        Task<bool> isNicUniqueAsync(UserAndCredentialDTO dto);
        Task<bool> isMobileNumUniqueAsync(UserAndCredentialDTO dto);
        Task<int> GetUserIdByCredentialsAsync(CredentialModel model);
        Task<UserModel> GetUserModelByCredentialsAsync(CredentialModel model);
        Task<DataModelResult<CredentialModel>> GetCredentialModelByEmailAsync(CredentialModel model);
        Task<List<UserRolesModel>> GetUserRolesByUserIdAsync(int UserId);
        Task<List<RoleModel>> GetAllRolesAsync();
    }
}
