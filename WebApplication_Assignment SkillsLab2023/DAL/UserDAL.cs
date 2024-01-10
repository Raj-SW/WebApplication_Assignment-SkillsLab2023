using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using System.Threading.Tasks;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class UserDAL:IUserDAL
    {
        private readonly IDBCommand _command;
        public UserDAL(IDBCommand command)
        {
            _command = command;
        }

        #region Get Model
        public async Task<string> GetEmployeeEmailbyUserIdAsync(byte UserId)
        {
            const string GET_EMPLOYEE_EMAIL_BY_USER_ID = @"
                            SELECT Email
                            FROM [Credential]
                            WHERE UserId = @UserId";
            List<SqlParameter>parameters = new List<SqlParameter>() { new SqlParameter("@UserId",UserId)};
            var dt =await  _command.GetDataWithConditionsAsync<CredentialModel>(GET_EMPLOYEE_EMAIL_BY_USER_ID, parameters);
            return dt.FirstOrDefault().Email;
        }
        public async Task<string> GetManagerEmailThroughEmployeeUserIdAsync(byte UserId)
        {
            const string GET_EMPLOYEE_EMAL_BY_USER_ID = @"
                            SELECT c.Email FROM [Credential] c 
                            INNER JOIN [User] u ON c.UserId= u.ManagerId
                            WHERE u.UserId = @UserId";
            List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@UserId", UserId) };
            var dt = await _command.GetDataWithConditionsAsync<CredentialModel>(GET_EMPLOYEE_EMAL_BY_USER_ID, parameters);
            return dt.FirstOrDefault().Email;
        }
        public async Task<string> GetUserNamebyUserIdAsync(byte userId) {
            const string GET_USER_NAME_BY_USER_ID = @"SELECT UserFirstName, UserLastName FROM [User] WHERE UserId = @UserId";
            List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@UserId",userId) };
            var dt = await _command.GetDataWithConditionsAsync<UserModel>(GET_USER_NAME_BY_USER_ID,parameters);
            return dt.FirstOrDefault().UserFirstName + ' ' + dt.FirstOrDefault().UserLastName;
        }
        #endregion

        #region User Model Manipulations
        public async Task<List<UserAndRolesDTO>> GetAllUsersAndTheirRolesAsync()
        {
            const string GET_ALL_USERS_QUERY = @"SELECT * FROM [User] u INNER JOIN [Credential] c ON u.UserId = c.UserId WHERE c.Activated = 1";
            const string GET_ALL_ROLES_OF_A_USER_BY_USER_ID = @"SELECT * FROM User_Roles WHERE UserId = @UserId";
            UserAndRolesDTO dto;
            List<UserAndRolesDTO> dtoList = new List<UserAndRolesDTO>();
            var dt = await _command.GetDataAsync(GET_ALL_USERS_QUERY);
            foreach(DataRow row in dt.Rows)
            {
                dto= new UserAndRolesDTO();
                dto.UserId = (byte)row["UserId"];
                dto.NIC = (string)row["NIC"];
                dto.UserFirstName = (string)row["UserFirstName"];
                dto.UserLastName = (string)row["UserLastName"];
                dto.DepartmentId = (byte)row["DepartmentId"];
                dto.ManagerId = (byte)row["ManagerId"];
                dto.MobileNum = (string)row["MobileNum"];
                dtoList.Add(dto);
            }
            foreach(var User in dtoList)
            {
                List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@UserId",User.UserId)};
                var dt2 = await _command.GetDataWithConditionsAsync<byte>(GET_ALL_ROLES_OF_A_USER_BY_USER_ID, parameters);
                User.Roles = new List<byte>();
                User.Roles=dt2;
            }
            return dtoList;
        }
        public async Task<bool> ActivatePendingUserAsync(ActivationDTO activationDTO)
        {
            const string ACTIVATE_PENDING_ACCOUNT_QUERY = @"UPDATE [User] SET ManagerId = @ManagerId, DepartmentId = @DepartmentId WHERE UserId = @UserId;
                                                            UPDATE [Credential] SET Activated = 1 WHERE UserId = @UserId;
                                                            INSERT INTO User_Roles(UserId,RoleId) VALUES(@UserId,@RoleId);";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ManagerId", activationDTO.ManagerId));
            parameters.Add(new SqlParameter("@DepartmentId", activationDTO.DepartmentId));
            parameters.Add(new SqlParameter("@UserId", activationDTO.UserId));
            parameters.Add(new SqlParameter("@RoleId", activationDTO.RoleId));
            await _command.InsertUpdateDataAsync(ACTIVATE_PENDING_ACCOUNT_QUERY, parameters);
            return true;
        }
        public async Task<List<UserModel>> GetAllPendingUserModelsAsync()
        {
            const string GET_ALL_PENDING_USER_MODELS_QUERY = @"SELECT u.* FROM [User] u INNER JOIN [Credential] c ON u.UserId = c.UserId WHERE c.Activated = 0 ";
            List<UserModel> ListOfPendingUserAccounts = new List<UserModel>();
            UserModel userModel;
            var dt = await _command.GetDataAsync(GET_ALL_PENDING_USER_MODELS_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                userModel = new UserModel();
                userModel.UserId = (byte)row["UserId"];
                userModel.UserFirstName = (string)row["UserFirstName"];
                userModel.UserLastName = (string)row["UserLastName"];
                userModel.MobileNum = (string)row["MobileNum"];
                userModel.NIC = (string)row["NIC"];
                ListOfPendingUserAccounts.Add(userModel);
            }
            return ListOfPendingUserAccounts;
        }
        public void DeactivatePendingUser(byte UserID)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateUserAndRolesAsync(UserAndRolesDTO userAndRolesDTO)
        {
            string UpdateUserAndRolesQuery = @"UPDATE [User] SET DepartmentId = @DepartmentId, ManagerId = @ManagerId WHERE UserId = @UserId;";
            List<SqlParameter> parameters = new List<SqlParameter>() 
            {
                new SqlParameter("@DepartmentId",userAndRolesDTO.DepartmentId),
                new SqlParameter("@ManagerId", userAndRolesDTO.ManagerId),
                new SqlParameter("@UserId", userAndRolesDTO.UserId)
            };
            if (userAndRolesDTO.Roles.Count>0)
            {
                UpdateUserAndRolesQuery += "DELETE FROM User_Roles WHERE UserId = @UserId;";
                var index = 0;
                foreach (var role in userAndRolesDTO.Roles)
                {
                    UpdateUserAndRolesQuery += $"INSERT INTO User_Roles(UserId,RoleId) VALUES(@UserId{index},@RoleId{index});";
                    parameters.Add(new SqlParameter($"@UserId{index}",userAndRolesDTO.UserId));
                    parameters.Add(new SqlParameter($"@RoleId{index}",role));
                    index++;
                }
            }
            await _command.InsertUpdateDataAsync(UpdateUserAndRolesQuery, parameters); 
            return true;
        }
        #endregion

        #region Manager Manipulations
        public async Task<List<ManagerDTO>> GetAllManagersAsync()
        {
            const string GET_ALL_MANAGERS_QUERY = @"SELECT u.UserId, u.UserFirstName,u.UserLastName,u.DepartmentId,r.RoleId,r.RoleName
                                                    FROM [User] u 
                                                    INNER JOIN [User_Roles] ur ON u.UserId = ur.UserId
                                                    INNER JOIN [Roles] r ON r.RoleId = ur.RoleId
                                                    WHERE r.RoleName='Manager';";
            List<ManagerDTO> listOfManagers = new List<ManagerDTO>();
            ManagerDTO managerModel;
            var dt = await _command.GetDataAsync(GET_ALL_MANAGERS_QUERY);
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
        public async Task<List<ManagerDTO>> GetAllManagersByDepartmentIdAsync(byte DepartmentId)
        {
            const string GET_ALL_MANAGERS_FROM_A_DEPARTMENT = @"SELECT u.* FROM [User] u INNER JOIN [User_Roles] ur ON u.UserId=ur.UserId WHERE ur.RoleId = 2 AND u.DepartmentId = @DepartmentId ";
            List<ManagerDTO> listOfManagers = new List<ManagerDTO>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
            var dt = await _command.GetDataWithConditionsAsync<ManagerDTO>(GET_ALL_MANAGERS_FROM_A_DEPARTMENT, parameters);
            return dt;
        }
        #endregion

        #region User Roles Manipulations
        public async Task<List<RoleModel>> GetAllUserRolesAsync()
        {
            const string GET_ALL_USER_ROLES_QUERY = @"SELECT * FROM [Roles]";
            List<RoleModel> listOfUserRoles = new List<RoleModel>();
            RoleModel roleModel;
            var dt = await _command.GetDataAsync(GET_ALL_USER_ROLES_QUERY);
            foreach (DataRow row in dt.Rows)
            {
                roleModel = new RoleModel();
                roleModel.RoleId = (byte)row["RoleId"];
                roleModel.RoleName = (string)row["RoleName"];
                listOfUserRoles.Add(roleModel);
            }
            return listOfUserRoles;
        }
        public async Task<List<UserRolesModel>> GetAllUserRolesModelByUserIdAsync(int UserId)
        {
            const string RETRIEVE_USER_ROLES_BY_USER_ID_QUERY = @"SELECT * FROM Roles r INNER JOIN User_Roles ur ON ur.RoleId=r.RoleId WHERE ur.UserId=@UserId";
            List<UserRolesModel> UserRolesList = new List<UserRolesModel>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserId", UserId));
            var dt = await _command.GetDataWithConditionsAsync<UserRolesModel>(RETRIEVE_USER_ROLES_BY_USER_ID_QUERY, parameters);
            return dt;
        }
        #endregion

        #region EmployeeTraining
        public void AssignTrainingToEmployee(byte EmployeeId, byte TrainingId)
        {
            throw new NotImplementedException();
        }

        public Task DeactivatePendingUserAsync(byte UserID)
        {
            throw new NotImplementedException();
        }

        public Task AssignTrainingToEmployeeAsync(byte EmployeeId, byte TrainingId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}