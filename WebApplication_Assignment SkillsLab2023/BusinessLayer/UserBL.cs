using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class UserBL : IUserBL
    {
        private readonly IUserDAL _userDAL;
        public UserBL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }
        public List<UserRolesModel> GetAllUserRolesModelByUserId(int UserId)
        {
            return _userDAL.GetAllUserRolesModelByUserId(UserId);
        }
    }
}