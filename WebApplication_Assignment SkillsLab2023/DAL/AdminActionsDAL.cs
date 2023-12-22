using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace ConsoleApp5.DAL
{
    public class AdminActionsDAL : IAdminActionsDAL
    {
        private readonly IDBCommand _command;
        public AdminActionsDAL(IDBCommand command) 
        {
            _command = command;
        }
        public void ActivatePendingUser(byte UserID)
        {
            throw new NotImplementedException();
        }
        public void AssignTrainingToEmployee(byte EmployeeId, byte TrainingId)
        {
            throw new NotImplementedException();
        }
        public void DeactivatePendingUser(byte UserID)
        {
            throw new NotImplementedException();
        }
        public void DeleteEmployeeEnrolment(byte Employee, byte TrainingId)
        {
            throw new NotImplementedException();
        }
        public List<UserModel> GetAllPendingUserModels()
        {
            const string  GET_ALL_PENDING_USER_MODELS_QUERY = @"SELECT u.* FROM [User] u INNER JOIN [Credential] c ON u.UserId = c.UserId WHERE c.Activated = 0 ";
            List<UserModel> ListOfPendingUserAccounts = new List<UserModel>();
            UserModel userModel;
            var dt = _command.GetData(GET_ALL_PENDING_USER_MODELS_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                userModel = new UserModel();
                userModel.UserId=(byte)row["UserId"];
                userModel.UserFirstName = (string)row["UserFirstName"];
                userModel.UserLastName = (string)row["UserLastName"];
                userModel.MobileNum =(string)row["MobileNum"];
                userModel.NIC = (string)row["NIC"];
                ListOfPendingUserAccounts.Add(userModel);
            }
            return ListOfPendingUserAccounts;
        }
        public void PromoteUser(byte UserID)
        {
            throw new NotImplementedException();
        }
    }
}
