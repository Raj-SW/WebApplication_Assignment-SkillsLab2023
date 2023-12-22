using System.Collections.Generic;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public interface IUserBL
    {
        List<UserRolesModel> GetAllUserRolesModelByUserId(int UserId);
    }
}