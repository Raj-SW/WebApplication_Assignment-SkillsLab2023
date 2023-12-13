using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface
{
    public interface IAuthenticationBL
    {
        DataModelResult<UserModel> LoginUser(CredentialModel model);
        TaskResult RegisterUser(RegistrationDTO dto);
        void Logout();
        TaskResult IsUserModelUnique(RegistrationDTO dto);
        bool IsCredentialsExists(CredentialModel model);
        bool InsertUserModel(UserModel model);
        bool InsertCredentialModel(CredentialModel model);
        bool IsUserActivated(CredentialModel model);
        int GetUserModelIDbyNIC(UserModel model);
        bool isEmailUnique(RegistrationDTO dto);
        bool isNicUnique(RegistrationDTO dto);
        bool isMobileNumUnique(RegistrationDTO dto);
        DataModelResult<CredentialModel> GetCredentialModelByEmailAndPassword(CredentialModel model);
    }
}
