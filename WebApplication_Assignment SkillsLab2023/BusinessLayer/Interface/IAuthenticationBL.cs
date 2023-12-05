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
        UserModel LoginUser(CredentialModel model);
        bool RegisterUser(RegistrationModel model);
        void Logout();
        bool IsUserModelUnique(UserModel model);
        int IsCredentialsExists(CredentialModel model);
        bool InsertUserModel(UserModel model);
        bool InsertCredentialModel(CredentialModel model);
        bool IsUserActivated(CredentialModel model);
        UserModel GetUserModelbyNIC(UserModel model);

    }
}
