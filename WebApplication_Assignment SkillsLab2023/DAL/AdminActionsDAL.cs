using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
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
        public bool ActivatePendingUser(ActivationDTO activationDTO)
        {
            const string ACTIVATE_PENDING_ACCOUNT_QUERY = @"UPDATE [User] SET ManagerId = @ManagerId, DepartmentId = @DepartmentId WHERE UserId = @UserId;
                                                            UPDATE [Credential] SET Activated = 1 WHERE UserId = @UserId;
                                                            INSERT INTO User_Roles(UserId,RoleId) VALUES(@UserId,@RoleId);";
            List< SqlParameter > parameters = new List< SqlParameter >();
            parameters.Add( new SqlParameter("@ManagerId",activationDTO.ManagerId));
            parameters.Add( new SqlParameter("@DepartmentId", activationDTO.DepartmentId));
            parameters.Add( new SqlParameter("@UserId", activationDTO.UserId));
            parameters.Add( new SqlParameter("@RoleId", activationDTO.RoleId));
            _command.InsertUpdateData(ACTIVATE_PENDING_ACCOUNT_QUERY,parameters);
            return true;
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
        public List<DepartmentModel> GetAllDepartments()
        {
            const string GET_ALL_DEPARTMENTS_QUERY = @"SELECT * FROM [Department]";
            List<DepartmentModel> listOfDepartments = new List<DepartmentModel>();
            DepartmentModel departmentModel;
            var dt = _command.GetData(GET_ALL_DEPARTMENTS_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                departmentModel = new DepartmentModel();
                departmentModel.DepartmentId = (byte)row["DepartmentId"];
                departmentModel.DepartmentName = (string)row["DepartmentName"];
                departmentModel.NoOfEmployees = (byte)row["NoOfEmployees"];
                listOfDepartments.Add(departmentModel);
            }
            return listOfDepartments;
        }
        public List<ManagerDTO> GetAllManagers()
        {
            const string GET_ALL_MANAGERS_QUERY = @"SELECT u.UserId, u.UserFirstName,u.UserLastName,u.DepartmentId,r.RoleId,r.RoleName
                                                    FROM [User] u 
                                                    INNER JOIN [User_Roles] ur ON u.UserId = ur.UserId
                                                    INNER JOIN [Roles] r ON r.RoleId = ur.RoleId
                                                    WHERE r.RoleName='Manager';";
            List<ManagerDTO> listOfManagers = new List<ManagerDTO>();
            ManagerDTO managerModel;
            var dt = _command.GetData(GET_ALL_MANAGERS_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                managerModel = new ManagerDTO();
                managerModel.UserId = (byte)row["UserId"];
                managerModel.UserFirstName = (string)row["UserFirstName"];
                managerModel.UserLastName = (string)row["UserLastName"];
                managerModel.DepartmentId = (byte)row["DepartmentId"];
                managerModel.RoleId = (byte)row["RoleId"];
                managerModel.RoleName = (string)row["RoleName"];
                listOfManagers.Add(managerModel);
            }
            return listOfManagers;
        }
        public List<ManagerDTO> GetAllManagersByDepartmentId(byte DepartmentId)
        {
            const string GET_ALL_MANAGERS_FROM_A_DEPARTMENT = @"SELECT u.* FROM [User] u INNER JOIN [User_Roles] ur ON u.UserId=ur.UserId WHERE ur.RoleId = 2 AND u.DepartmentId = @DepartmentId ";
            List<ManagerDTO> listOfManagers = new List<ManagerDTO>();
            ManagerDTO managerDTO;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
            var dt = _command.GetDataWithConditions(GET_ALL_MANAGERS_FROM_A_DEPARTMENT,parameters);
            foreach (DataRow row in dt.Rows)
            {
                managerDTO = new ManagerDTO();
                managerDTO.UserId = (byte)row["UserId"];
                managerDTO.UserFirstName = (string)row["UserFirstName"];
                managerDTO.UserLastName = (string)row["UserLastName"];
                listOfManagers.Add(managerDTO);
            }
            return listOfManagers;
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
        public List<RoleModel> GetAllUserRoles()
        {
            const string GET_ALL_USER_ROLES_QUERY = @"SELECT * FROM [Roles]";
            List<RoleModel> listOfUserRoles = new List<RoleModel>();
            RoleModel roleModel;
            var dt = _command.GetData(GET_ALL_USER_ROLES_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                roleModel = new RoleModel();
                roleModel.RoleId = (byte)row["RoleId"];
                roleModel.RoleName = (string)row["RoleName"];
                listOfUserRoles.Add(roleModel);
            }
            return listOfUserRoles;
        }
        public void PromoteUser(byte UserID)
        {
            throw new NotImplementedException();
        }
    }
}
