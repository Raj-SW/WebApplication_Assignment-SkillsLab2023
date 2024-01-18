using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer;
using WebApplication_Assignment_SkillsLab2023.BusinessLayer.Interface;
using WebApplication_Assignment_SkillsLab2023.DAL.Interface;
using WebApplication_Assignment_SkillsLab2023.DataTransferObjects;
using WebApplication_Assignment_SkillsLab2023.Services.Interfaces;

namespace YourNamespace.Tests
{
    [TestFixture]
    public class EnrolmentBLTests
    {
        private IEnrolmentBL _enrolmentBL;
        private Mock<IEnrolmentDAL> _mockEnrolmentDAL;
        private Mock<IFileHandlerService> _mockFileHandlerService;
        private Mock<IUserBL> _mockUserBL;
        private Mock<ITrainingBL> _mockTrainingBL;

        [SetUp]
        public void SetUp()
        {
            _mockEnrolmentDAL = new Mock<IEnrolmentDAL>();
            _mockFileHandlerService = new Mock<IFileHandlerService>();
            _mockUserBL = new Mock<IUserBL>();
            _mockTrainingBL = new Mock<ITrainingBL>();

            _enrolmentBL = new EnrolmentBL(_mockEnrolmentDAL.Object, _mockFileHandlerService.Object, _mockUserBL.Object, _mockTrainingBL.Object);
        }

        [Test]
        public async Task GetEmployeesPendingEnrolmentByManagerIdAsync_ReturnsListOfPendingEnrolments()
        {
            // Arrange
            byte managerId = 1;
            var expectedEnrolments = new List<GetPendingEmployeesEnrolmentOfAMangerDTO> { /* Your test data */ };
            _mockEnrolmentDAL.Setup(e => e.GetEmployeesPendingEnrolmentByManagerIdAsync(managerId))
                             .ReturnsAsync(expectedEnrolments);

            // Act
            var result = await _enrolmentBL.GetEmployeesPendingEnrolmentByManagerIdAsync(managerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<GetPendingEmployeesEnrolmentOfAMangerDTO>>(result);
            Assert.AreEqual(expectedEnrolments, result);
        }

    }
}
