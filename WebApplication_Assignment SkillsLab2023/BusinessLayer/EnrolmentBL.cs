using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL.Interface;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Services;
using WebApplication_Assignment_SkillsLab2023.Services.Interfaces;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class EnrolmentBL : IEnrolmentBL
    {
        private readonly IFileHandlerService _iFileHandlerService;
        private readonly IEnrolmentDAL _enrolmentDAL;
        private readonly IUserBL _userBL;
        private readonly ITrainingBL _trainingBL;
        public EnrolmentBL(IEnrolmentDAL  enrolmentDAL, IFileHandlerService fileHandlerService,IUserBL userBL, ITrainingBL trainingBl) 
        {
            _enrolmentDAL = enrolmentDAL;
            _iFileHandlerService = fileHandlerService;
            _userBL= userBL;
            _trainingBL = trainingBl;
        }

        #region Get Model
        public List<GetPendingEmployeesEnrolmentOfAMangerDTO> GetEmployeesPendingEnrolmentByManagerId(byte managerId)
        {
            return _enrolmentDAL.GetEmployeesPendingEnrolmentByManagerId(managerId);
        }
        public List<UserPrerequisiteModel> GetEnrolmentPrerequisitesOfAUserByEnrolmentId(byte enrolmentId)
        {

            return _enrolmentDAL.GetEnrolmentPrerequisitesOfAUserByEnrolmentId(enrolmentId);
        }
        public List<UserPrerequisiteModel> GetAllEnrolmentsManagerWise(byte ManagerId)
        {
            throw new NotImplementedException();
        }
        public bool isUserAlreadyRegisteredInTraining(byte trainingId, byte UserId)
        {
            return _enrolmentDAL.isUserAlreadyRegisteredInTraining(trainingId,UserId);
        }
        #endregion

        #region Insert Models
        public async Task<bool> EnrolEmployeeIntoTrainingAsync(byte userId, byte trainingId, HttpFileCollectionBase FileCollection)
        {
                TaskResult uploadTaskResult = new TaskResult();
                //TODO:
                //Security protocols here
                if (FileCollection != null && FileCollection.Count > 0)
                {
                    uploadTaskResult = _iFileHandlerService.FileUpload(userId, trainingId, FileCollection);
                }
                uploadTaskResult.isSuccess = _enrolmentDAL.EnrolEmployeeIntoTraining(userId, trainingId, uploadTaskResult.ResultMessageList);
                string userEmail = _userBL.GetEmployeeEmailbyUserId(userId);
                string employeeName = _userBL.GetUserNamebyUserId(userId);
                string managerEmail = _userBL.GetManagerEmailThroughEmployeeUserId(userId);
                string trainingName = _trainingBL.GetTrainingNameByTrainingId(trainingId);
                uploadTaskResult.isSuccess = await EmailSender.SendEmailAsync($"Enrolment for {trainingName} - DF Learning Hub", $"Your enrolment for {trainingName} has been successfully received.\r\nPlease wait for your manager's approval.\r\n\r\nThis is an auto-generated email. Please do not reply to this email\r\n\r\nKind regards,\r\nDF Learning Hub", userEmail);
                uploadTaskResult.isSuccess = await EmailSender.SendEmailAsync($"Employee Enrolment for {trainingName}", $"Your employee {employeeName} has registered for {trainingName} successfully.\r\nPlease review his documents.\r\n\r\nThis is an auto-generated email. Please do not reply to this email\r\n\r\nKind regards,\r\nDF Learning Hub", managerEmail);
                return uploadTaskResult.isSuccess;
            

        }
        #endregion

        #region Update Model
        public async Task<bool> ManagerApproveEnrolmentAsync(byte enrolmentId, byte userId, byte trainingId)
        {
            var isSentEmployee=false;
            var isSentManager=false;
            var isSuccess = _enrolmentDAL.ManagerApproveEnrolmentAsync(enrolmentId);
            if (isSuccess)
            {
                string employeeEmail = _userBL.GetEmployeeEmailbyUserId(userId);
                string employeeName = _userBL.GetUserNamebyUserId(userId);
                string managerEmail = _userBL.GetManagerEmailThroughEmployeeUserId(userId);
                string trainingName = _trainingBL.GetTrainingNameByTrainingId(trainingId);
                isSentEmployee = await EmailSender.SendEmailAsync($"Enrolment for {trainingName} - DF Learning Hub", $"Your enrolment for {trainingName} has been aproved by your manager ", employeeEmail);
                isSentManager = await EmailSender.SendEmailAsync($"Employee Enrolment for {trainingName}", $"You have approved training for employee: {employeeName}",managerEmail);
            }
            return (isSuccess);
        }
        public async Task<bool> ManagerRejectEnrolmentAsync(byte enrolmentId, byte userId, byte trainingId, string remarks)
        {
            var isSentEmployee = false;
            var isSentManager = false;
            var isSuccess = _enrolmentDAL.ManagerRejectEnrolmentAsync(enrolmentId, remarks);
            string employeeEmail = _userBL.GetEmployeeEmailbyUserId(userId);
            string employeeName = _userBL.GetUserNamebyUserId(userId);
            string managerEmail = _userBL.GetManagerEmailThroughEmployeeUserId(userId);
            string trainingName = _trainingBL.GetTrainingNameByTrainingId(trainingId);
            isSentEmployee = await EmailSender.SendEmailAsync($"Enrolment for {trainingName} - DF Learning Hub", $"Unfortunately your enrolment for {trainingName} has been rejected by your manager ", employeeEmail);
            isSentManager = await EmailSender.SendEmailAsync($"Employee Enrolment for {trainingName}", $"You have rejected training for employee: {employeeName}", managerEmail);
            return isSuccess;
        }
        public void AutomaticEnrolmentProcessingForTrainingByTrainingId(byte trainingId)
        {
            throw new NotImplementedException();
        }
        public async Task AutomaticEnrolmentProcessingForAllTrainingAsync()
        {
            var automaticProcessingDTO = _enrolmentDAL.AutomaticEnrolmentProcessingForAllTraining();
            foreach (var approvedEnrolment in automaticProcessingDTO)
            {
                //get email of employees manager
                var managerEmail = _userBL.GetManagerEmailThroughEmployeeUserId(approvedEnrolment.UserId);
                //send email notifications to employee
                var employeeResult = await EmailSender.SendEmailAsync($"Enrolment for {approvedEnrolment.TrainingName} - DF Learning Hub", $"You have been successfully enrolled for the training {approvedEnrolment.TrainingName}. Good Luck! ",approvedEnrolment.Email);
                // send email notifications to manager
                var managerResult = await EmailSender.SendEmailAsync($"Employee Enrolment for {approvedEnrolment.TrainingName}",$"Your employee {approvedEnrolment.UserFirstName} {approvedEnrolment.UserLastName} has been successfully enrolled in the training {approvedEnrolment.TrainingName}", managerEmail);
            }
        }
        #endregion
    }
}