using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.Common;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.DAL.Interface;
using WebApplication_Assignment_SkillsLab2023.Services;
using WebApplication_Assignment_SkillsLab2023.Services.Interfaces;

namespace WebApplication_Assignment_SkillsLab2023
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IDataAccessLayer, DataAccessLayer>();
            container.RegisterType<IDBCommand, DBCommand>();
            container.RegisterType<IAuthenticationDAL, AuthenticationDAL>();
            container.RegisterType<IAuthenticationBL, AuthenticationBL>();
            container.RegisterType<ITrainingBL, TrainingBL>();
            container.RegisterType<ITrainingDAL, TrainingDAL>();
            container.RegisterType<IFileHandlerService, LocalFileHandlerService>();
            container.RegisterType<IUserDAL, UserDAL>();
            container.RegisterType<IUserBL, UserBL>();
            container.RegisterType<IEnrolmentBL, EnrolmentBL>();
            container.RegisterType<IEnrolmentDAL, EnrolmentDAL>();
            container.RegisterType<IDepartmentBL, DepartmentBL>();
            container.RegisterType<IDepartmentDAL, DepartmentDAL>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}