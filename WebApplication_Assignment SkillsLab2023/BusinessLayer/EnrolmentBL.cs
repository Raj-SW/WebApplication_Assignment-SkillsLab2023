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

        public EnrolmentBL(IEnrolmentDAL enrolmentDAL, IFileHandlerService fileHandlerService, IUserBL userBL, ITrainingBL trainingBl)
        {
            _enrolmentDAL = enrolmentDAL;
            _iFileHandlerService = fileHandlerService;
            _userBL = userBL;
            _trainingBL = trainingBl;
        }

        #region Get Model
        public async Task<List<GetPendingEmployeesEnrolmentOfAMangerDTO>> GetEmployeesPendingEnrolmentByManagerIdAsync(byte managerId)
        {
            return await _enrolmentDAL.GetEmployeesPendingEnrolmentByManagerIdAsync(managerId);
        }
        public async Task<List<UserPrerequisiteModel>> GetEnrolmentPrerequisitesOfAUserByEnrolmentIdAsync(byte enrolmentId)
        {
            return await _enrolmentDAL.GetEnrolmentPrerequisitesOfAUserByEnrolmentIdAsync(enrolmentId);
        }
        public async Task<List<UserPrerequisiteModel>> GetAllEnrolmentsManagerWiseAsync(byte ManagerId)
        {
            return await _enrolmentDAL.GetAllEnrolmentsManagerWiseAsync(ManagerId);
        }
        public async Task<bool> isUserAlreadyRegisteredInTrainingAsync(byte trainingId, byte UserId)
        {
            return await _enrolmentDAL.isUserAlreadyRegisteredInTrainingAsync(trainingId, UserId);
        }
        public async Task<int> GetPrerequisiteCountOfATraining(byte trainingId) {
            return await _trainingBL.GetPrerequisiteCountOfATraining(trainingId);
        }
        #endregion

        #region Insert Models
        public async Task<bool> EnrolEmployeeIntoTrainingAsync(byte userId, byte trainingId, HttpFileCollectionBase FileCollection)
        {
            TaskResult uploadTaskResult = new TaskResult();
            //check if training requires prerequisite and if user uploaded
            if (FileCollection != null && FileCollection.Count > 0)
            {
                uploadTaskResult = _iFileHandlerService.FileUpload(userId, trainingId, FileCollection);
            }
            uploadTaskResult.isSuccess = await _enrolmentDAL.EnrolEmployeeIntoTrainingAsync(userId, trainingId, uploadTaskResult.ResultMessageList);
            string userEmail = await _userBL.GetEmployeeEmailbyUserIdAsync(userId);
            string employeeName = await _userBL.GetUserNamebyUserIdAsync(userId);
            string managerEmail = await _userBL.GetManagerEmailThroughEmployeeUserIdAsync(userId);
            string trainingName = await _trainingBL.GetTrainingNameByTrainingIdAsync(trainingId);
            uploadTaskResult.isSuccess = await EmailSender.SendEmailAsync($"Enrolment for {trainingName} - DF Learning Hub", $"Your enrolment for {trainingName} has been successfully received.\r\nPlease wait for your manager's approval.\r\n\r\nThis is an auto-generated email. Please do not reply to this email\r\n\r\nKind regards,\r\nDF Learning Hub", userEmail);
            uploadTaskResult.isSuccess = await EmailSender.SendEmailAsync($"Employee Enrolment for {trainingName}", $"Your employee {employeeName} has registered for {trainingName} successfully.\r\nPlease review his documents.\r\n\r\nThis is an auto-generated email. Please do not reply to this email\r\n\r\nKind regards,\r\nDF Learning Hub", managerEmail);

            return uploadTaskResult.isSuccess;
        }
        #endregion

        #region Update Model
        public async Task<bool> ManagerApproveEnrolmentAsync(byte enrolmentId, byte userId, byte trainingId)
        {
            var isSentEmployee = false;
            var isSentManager = false;
            var isSuccess = await _enrolmentDAL.ManagerApproveEnrolmentAsync(enrolmentId);

            if (isSuccess)
            {
                string employeeEmail = await _userBL.GetEmployeeEmailbyUserIdAsync(userId);
                string employeeName = await _userBL.GetUserNamebyUserIdAsync(userId);
                string managerEmail = await _userBL.GetManagerEmailThroughEmployeeUserIdAsync(userId);
                string trainingName = await _trainingBL.GetTrainingNameByTrainingIdAsync(trainingId);
                isSentEmployee = await EmailSender.SendEmailAsync($"Enrolment for {trainingName} - DF Learning Hub", $"Your enrolment for {trainingName} has been approved by your manager ", employeeEmail);
                isSentManager = await EmailSender.SendEmailAsync($"Employee Enrolment for {trainingName}", $"You have approved training for employee: {employeeName}", managerEmail);
            }

            return (isSuccess);
        }
        public async Task<bool> ManagerRejectEnrolmentAsync(byte enrolmentId, byte userId, byte trainingId, string remarks)
        {
            var isSentEmployee = false;
            var isSentManager = false;
            var isSuccess = await _enrolmentDAL.ManagerRejectEnrolmentAsync(enrolmentId, remarks);
            string employeeEmail = await _userBL.GetEmployeeEmailbyUserIdAsync(userId);
            string employeeName = await _userBL.GetUserNamebyUserIdAsync(userId);
            string managerEmail = await _userBL.GetManagerEmailThroughEmployeeUserIdAsync(userId);
            string trainingName = await _trainingBL.GetTrainingNameByTrainingIdAsync(trainingId);
            isSentEmployee = await EmailSender.SendEmailAsync($"Enrolment for {trainingName} - DF Learning Hub", $"Unfortunately, your enrolment for {trainingName} has been rejected by your manager ", employeeEmail);
            isSentManager = await EmailSender.SendEmailAsync($"Employee Enrolment for {trainingName}", $"You have rejected training for employee: {employeeName}", managerEmail);

            return isSuccess;
        }
        public async Task AutomaticEnrolmentProcessingForTrainingByTrainingIdAsync(byte trainingId)
        {
            await _enrolmentDAL.AutomaticEnrolmentProcessingForTrainingByTrainingIdAsync(trainingId);
        }
        public async Task AutomaticEnrolmentProcessingForAllTrainingAsync()
        {
            var automaticProcessingDTO = await _enrolmentDAL.AutomaticEnrolmentProcessingForAllTrainingAsync();

            foreach (var approvedEnrolment in automaticProcessingDTO)
            {
                var managerEmail = await _userBL.GetManagerEmailThroughEmployeeUserIdAsync(approvedEnrolment.UserId);
                var employeeResult = await EmailSender.SendEmailAsync($"Enrolment for {approvedEnrolment.TrainingName} - DF Learning Hub", $"You have been successfully enrolled for the training {approvedEnrolment.TrainingName}. Good Luck! ", approvedEnrolment.Email);
                var managerResult = await EmailSender.SendEmailAsync($"Employee Enrolment for {approvedEnrolment.TrainingName}", $"Your employee {approvedEnrolment.UserFirstName} {approvedEnrolment.UserLastName} has been successfully enrolled in the training {approvedEnrolment.TrainingName}", managerEmail);
            }
        }
        #endregion
    }
}
