using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL;
using WebApplication_Assignment_SkillsLab2023.Models;
using WebApplication_Assignment_SkillsLab2023.Services;

public class AuthenticationBLTests
{
    private IAuthenticationBL _authenticationBL;
    private Mock<IAuthenticationDAL> _mockAuthenticationDAL;
    private static List<CredentialModel> ListOfCredentialModel;
    private static List<UserModel> ListOfUserModel;

    [SetUp]
    public void SetUp()
    {
        var salt = PasswordHashing.GenerateTimestampSalt();
        _mockAuthenticationDAL = new Mock<IAuthenticationDAL>();

        #region Mock Database

        ListOfCredentialModel = new List<CredentialModel>()
        {
            new CredentialModel(){AccessId=1,UserId=1,RawPassword ="11111111", Password=PasswordHashing.HashPassword("userLoginSuccess",salt), Salt= salt , Activated= true , Email ="userLoginSuccess@email.com" },
            new CredentialModel(){AccessId=2,UserId=2,RawPassword ="11111111", Password=PasswordHashing.HashPassword("11111111",salt), Salt= salt , Activated= true , Email ="userWrongCredential@email.com" },
            new CredentialModel(){AccessId=3,UserId=3,RawPassword ="11111111", Password=PasswordHashing.HashPassword("11111111",salt), Salt= salt , Activated= false , Email ="userNotActivated@email.com" },
            new CredentialModel(){AccessId=4,UserId=4,RawPassword ="11111111", Password=PasswordHashing.HashPassword("11111111",salt), Salt= salt , Activated= true , Email ="user3@ceridian.com" },
        };
        ListOfUserModel = new List<UserModel>()
        {
            new UserModel(){UserId=7,NIC = "S180500110199G",UserFirstName = "Raj", UserLastName = "Seetohul", MobileNum = "58342636", DepartmentId = 2}
        };

        #endregion

        #region Mock AuthenticationDAL

        _mockAuthenticationDAL.Setup(mockAuthenticationDAL => mockAuthenticationDAL.GetCredentialModelByEmailAsync(It.IsAny<CredentialModel>())).ReturnsAsync((CredentialModel credentialModel) => {
            if (ListOfCredentialModel.Any(credmodel => credmodel.Email == credentialModel.Email))
            {
                return
                new DataModelResult<CredentialModel>
                {
                    ResultTask = new TaskResult { isSuccess = true },
                    ResultObject = ListOfCredentialModel.Where(model => model.Email == credentialModel.Email).FirstOrDefault(),
                };
            }
            else
            {
                return
                new DataModelResult<CredentialModel>
                {
                    ResultTask = new TaskResult { isSuccess = false },
                    ResultObject = new CredentialModel(),
                };
            }
        });
        _mockAuthenticationDAL.Setup(_mockAuthenticationDAL => _mockAuthenticationDAL.GetUserModelByIDAsync(It.IsAny<byte>())).ReturnsAsync((byte id) => {
            return ListOfUserModel.Where(user => user.UserId == id).FirstOrDefault();
        });

        #endregion

        _authenticationBL = new AuthenticationBL(_mockAuthenticationDAL.Object);

    }
    [Test]
    [TestCase("userNotFound@gmail.com","1234")]
    public async Task LoginUserAsync_UserNotFound_ReturnsErrorMessage(string email,string password)
    {
        // Arrange
        CredentialModel model = new CredentialModel{
            Email = email,
            RawPassword = password
        };
        // Act
        var result = await _authenticationBL.LoginUserAsync(model);
        // Assert
        Assert.IsFalse(result.ResultTask.isSuccess);
        Assert.AreEqual("User not Found!", result.ResultTask.GetAllResultMessageAsString());
    }
    [Test]
    [TestCase("userNotActivated@email.com", "11111111")]
    public async Task LoginUserAsync_UserNotActivatedYet_ReturnsErrorMessage(string email, string password)
    {
        // Arrange
        CredentialModel model = new CredentialModel
        {
            Email = email,
            RawPassword = password
        };
        // Act
        var result = await _authenticationBL.LoginUserAsync(model);
        // Assert
        Assert.IsFalse(result.ResultTask.isSuccess);
        Assert.AreEqual("User not Activated Yet! Please contact Admin.", result.ResultTask.GetAllResultMessageAsString());
    }
    [Test]
    [TestCase("userWrongCredential@email.com", "11111112")]
    public async Task LoginUserAsync_UserWrongCredentials_ReturnsErrorMessage(string email, string password)
    {
        // Arrange
        CredentialModel model = new CredentialModel
        {
            Email = email,
            RawPassword = password
        };
        // Act
        var result = await _authenticationBL.LoginUserAsync(model);
        // Assert
        Assert.IsFalse(result.ResultTask.isSuccess);
        Assert.AreEqual("Wrong Credentials", result.ResultTask.GetAllResultMessageAsString());
    }
    [Test]
    [TestCase("userLoginSuccess@email.com", "userLoginSuccess")]
    public async Task LoginUserAsync_UserLoggedInSuccessfully_ReturnsSuccessMessage(string email, string password)
    {
        // Arrange
        CredentialModel model = new CredentialModel
        {
            Email = email,
            RawPassword = password
        };
        // Act
        var result = await _authenticationBL.LoginUserAsync(model);
        // Assert
        Assert.IsTrue(result.ResultTask.isSuccess);
        Assert.AreEqual("Logged In Successfully", result.ResultTask.GetAllResultMessageAsString());
    }
}
