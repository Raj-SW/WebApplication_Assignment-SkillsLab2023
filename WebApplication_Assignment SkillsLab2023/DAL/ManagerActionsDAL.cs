using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.DAL.Common;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;

namespace WebApplication_Assignment_SkillsLab2023.DAL
{
    public class ManagerActionsDAL : IManagerActionsDAL
    {
        private readonly IDBCommand _dBCommand;
        public ManagerActionsDAL(IDBCommand dBCommand) 
        {
            _dBCommand = dBCommand;
        }
        public void ApproveEmployeeEnrolment()
        {
            throw new NotImplementedException();
        }

        public List<GetPendingEmployeesEnrolmentOfAMangerDTO> GetEmployeesPendingEnrolmentByManagerId(byte managerId)
        {
            const string GET_EMPLOYEES_PENDING_ENROLMENT_BY_MANAGER_ID_QUERY = @"
            SELECT e.EnrolmentId, e.TrainingId, e.UserId, u.UserFirstName, u.UserLastName, e.EnrolmentDateTime, t.TrainingName,t.TrainingRegistrationDeadline
            FROM Enrolment e 
            INNER JOIN [User] u ON u.UserId = e.UserId
            INNER JOIN Training t ON t.TrainingId = e.TrainingId
            WHERE e.ManagerApproval = 'Pending' AND u.ManagerId = @ManagerId";

            List<GetPendingEmployeesEnrolmentOfAMangerDTO> employeesEnrolmentList =new List<GetPendingEmployeesEnrolmentOfAMangerDTO>();
            GetPendingEmployeesEnrolmentOfAMangerDTO employeesEnrolment;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ManagerId",managerId));
            var dt = _dBCommand.GetDataWithConditions(GET_EMPLOYEES_PENDING_ENROLMENT_BY_MANAGER_ID_QUERY, parameters);
            foreach (DataRow row in dt.Rows) 
            {
                employeesEnrolment = new GetPendingEmployeesEnrolmentOfAMangerDTO();
                employeesEnrolment.EnrolmentId = (byte)row["EnrolmentId"];
                employeesEnrolment.TrainingId = (byte)row["TrainingId"];
                employeesEnrolment.UserId = (byte)row["UserId"];
                employeesEnrolment.UserFirstName = (string)row["UserFirstName"];
                employeesEnrolment.UserLastName = (string)row["UserLastName"];
                employeesEnrolment.EnrolmentDateTime = (DateTime)row["EnrolmentDateTime"];
                employeesEnrolment.TrainingName = (string)row["TrainingName"];
                employeesEnrolment.TrainingRegistrationDeadline = (DateTime)row["TrainingRegistrationDeadline"];
                employeesEnrolmentList.Add(employeesEnrolment);
            }
            return employeesEnrolmentList;
        }

        public void RejectEmployeeEnrolment()
        {
            throw new NotImplementedException();
        }
    }
}