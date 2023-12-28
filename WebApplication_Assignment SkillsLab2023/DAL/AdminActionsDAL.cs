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
        public bool AddTrainingPrerequisite(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            throw new NotImplementedException();
        }
        public void AssignTrainingToEmployee(byte EmployeeId, byte TrainingId)
        {
            throw new NotImplementedException();
        }
        public bool CreateTraining(CreateTrainingDTO createTrainingDTO)
        {
            string CREATE_TRAINING_QUERY = @"INSERT INTO [Training] (TrainingName,TrainingStatus,DepartmentPriority,TrainingDescription,TrainingRegistrationDeadline,SeatsTotal,CoachId)
                                            VALUES (@TrainingName,@TrainingStatus,@DepartmentPriority,@TrainingDescription,CONVERT(DATE, @TrainingRegistrationDeadline),@TotalSeats,@CoachId);";
            var index = 0;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@TrainingName",createTrainingDTO.TrainingName));
            parameters.Add(new SqlParameter("@TrainingStatus",createTrainingDTO.TrainingStatus));
            parameters.Add(new SqlParameter("@DepartmentPriority",createTrainingDTO.DepartmentPriority));
            parameters.Add(new SqlParameter("@TrainingDescription",createTrainingDTO.TrainingDescription));
            parameters.Add(new SqlParameter("@TrainingRegistrationDeadline", createTrainingDTO.RegistrationDeadline));
            parameters.Add(new SqlParameter("@TotalSeats", createTrainingDTO.TotalSeats));
            parameters.Add(new SqlParameter("@CoachId", createTrainingDTO.Coach));
            if (createTrainingDTO.Prerequisites.Count > 0)
            {
                CREATE_TRAINING_QUERY +=
                    "DECLARE @generateTrainingId INT; SET @generateTrainingId = SCOPE_IDENTITY();";
                    foreach (var prerequisite in createTrainingDTO.Prerequisites)
                    {
                        CREATE_TRAINING_QUERY += $"INSERT INTO [TrainingPrerequisite](PrerequisiteId, TrainingId) VALUES(@PrerequisiteId{index},@generateTrainingId);";
                        parameters.Add(new SqlParameter($"@PrerequisiteID{index}",prerequisite));
                        index++;
                    }
            }
            _command.InsertUpdateData(CREATE_TRAINING_QUERY,parameters);
            return true;
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
        public bool UpdateTraining(TrainingModel trainingmodel)
        {
            const string UPDATE_TRAINING_QUERY = @"
                UPDATE Training
                SET
                    TrainingName = @UpdatedTrainingName,
                    TrainingStatus = @UpdatedStatus,
                    DepartmentPriority = @UpdatedPriority,
                    TrainingDescription = @UpdatedDescription,
                    SeatsTotal = @UpdatedSeatsTotal,
                    CoachId = @UpdatedCoachId,
                    TrainingRegistrationDeadline = @UpdatedDeadline
                WHERE
                    TrainingId = @TrainingId; ";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UpdatedTrainingName",trainingmodel.TrainingName));
            parameters.Add(new SqlParameter("@UpdatedTrainingName",trainingmodel.TrainingStatus));
            parameters.Add(new SqlParameter("@UpdatedTrainingName",trainingmodel.DepartmentPriority));
            parameters.Add(new SqlParameter("@UpdatedTrainingName",trainingmodel.SeatsTotal));
            parameters.Add(new SqlParameter("@UpdatedTrainingName",trainingmodel.CoachId));
            parameters.Add(new SqlParameter("@UpdatedTrainingName",trainingmodel.TrainingRegistrationDeadline));
            parameters.Add(new SqlParameter("@UpdatedTrainingName",trainingmodel.TrainingDescription));
            parameters.Add(new SqlParameter("@TrainingId", trainingmodel.TrainingId));
            _command.InsertUpdateData(UPDATE_TRAINING_QUERY,parameters);
            return true; 
        }
        public bool UpdateTrainingPrerequisite(TrainingPrerequisiteModel trainingPrerequisiteModel)
        {
            throw new NotImplementedException();
        }
    }
}
