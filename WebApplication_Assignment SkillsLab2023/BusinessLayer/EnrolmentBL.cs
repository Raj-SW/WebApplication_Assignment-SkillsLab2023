﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL.Interface;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Services.Interfaces;

namespace WebApplication_Assignment_SkillsLab2023.BusinessLayer
{
    public class EnrolmentBL : IEnrolmentBL
    {
        private readonly IFileHandlerService _iFileHandlerService;
        private readonly IEnrolmentDAL _enrolmentDAL;
        public EnrolmentBL(IEnrolmentDAL  enrolmentDAL, IFileHandlerService fileHandlerService) 
        {
            _enrolmentDAL = enrolmentDAL;
            _iFileHandlerService = fileHandlerService;
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
        #endregion

        #region Insert Models
        public bool EnrolEmployeeIntoTraining(int userId, int trainingId, HttpFileCollectionBase FileCollection)
        {
            TaskResult uploadTaskResult = new TaskResult();
            //TODO:
            //Security protocols here
            //Notify user of successfull enrolment
            uploadTaskResult = _iFileHandlerService.FileUpload(userId, trainingId, FileCollection);
            if (uploadTaskResult.isSuccess)
            {
                uploadTaskResult.isSuccess = _enrolmentDAL.EnrolEmployeeIntoTraining(userId, trainingId, uploadTaskResult.ResultMessageList);
                return uploadTaskResult.isSuccess;
            }
            return false;
        }
        #endregion

        #region Update Model
        public bool ApproveEnrolment(byte enrolmentId)
        {
            return _enrolmentDAL.ApproveEnrolment(enrolmentId);
        }
        public bool RejectEnrolment(byte enrolmentId, string remarks)
        {
            return _enrolmentDAL.RejectEnrolment(enrolmentId, remarks);
        }

       
        #endregion
    }
}