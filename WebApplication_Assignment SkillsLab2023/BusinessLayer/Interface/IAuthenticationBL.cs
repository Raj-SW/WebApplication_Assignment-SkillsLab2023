using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface
{
    public interface IAuthenticationBL
    {
        Task<DataModelResult<UserModel>> LoginUserAsync(CredentialModel model);
        Task<TaskResult> RegisterUserAsync(UserAndCredentialDTO dto);
        void Logout();
        Task<TaskResult> IsUserModelUniqueAsync(UserAndCredentialDTO dto);
        Task<bool> IsCredentialsExistsAsync(CredentialModel model);
        Task<bool> InsertCredentialModelAsync(CredentialModel model);
        Task<int> GetUserModelIDbyNICAsync(UserModel model);
        Task<bool> isEmailUniqueAsync(UserAndCredentialDTO dto);
        Task<bool> isNicUniqueAsync(UserAndCredentialDTO dto);
        Task<bool> isMobileNumUniqueAsync(UserAndCredentialDTO dto);
        Task<DataModelResult<CredentialModel>> GetCredentialModelByEmailAsync(CredentialModel model);
        byte[] HashPassword(string password, byte[] salt);
        byte[] GenerateTimestampSalt();
        Task<List<RoleModel>> GetAllRolesAsync();
    }
}
