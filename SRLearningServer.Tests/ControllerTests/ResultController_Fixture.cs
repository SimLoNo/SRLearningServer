using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using SRLearningServer.Components.Controllers;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLearningServer.Tests.ControllerTests
{
    [TestClass]
    [TestCategory("UnitTest")]
    public class ResultController_Fixture
    {
        private readonly Mock<IResultService> _mockService;
        private readonly ResultController _controller;
        private readonly TestDataGenerator _testDataGenerator;

        private ResultDto _resultDto1;
        private ResultDto _resultDto2;

        public ResultController_Fixture()
        {
            _testDataGenerator = new TestDataGenerator();
            _mockService = new Mock<IResultService>();
            _controller = new ResultController(_mockService.Object);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _resultDto1 = _testDataGenerator.CreateResultDto(1,"result1", DateOnly.FromDateTime(DateTime.UtcNow),true);
            _resultDto2 = _testDataGenerator.CreateResultDto(2, "result2", DateOnly.FromDateTime(DateTime.UtcNow), true);
        }

        [TestMethod]
        public async Task Create_ReturnsStatusCode400WhenResultsIsNotCreated()
        {
            //Arrange
            _mockService.Setup(x => x.Create(It.IsAny<ResultDto>())).ReturnsAsync((ResultDto)null);
            //Act
            var result = await _controller.Create(_resultDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Create_ReturnsStatusCode200WhenResultsIsCreated()
        {
            //Arrange
            _mockService.Setup(x => x.Create(It.IsAny<ResultDto>())).ReturnsAsync(_resultDto1);
            //Act
            var result = await _controller.Create(_resultDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Create_ReturnsStatusCode500WhenExceptionThronw()
        {
            //Arrange
            _mockService.Setup(x => x.Create(It.IsAny<ResultDto>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Create(_resultDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode200WhenResultIsDeactivated()
        {
            //Arrange
            _mockService.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_resultDto1);
            //Act
            var result = await _controller.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode400WhenResultIsNotDeactivated()
        {
            //Arrange
            _mockService.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync((ResultDto)null);
            //Act
            var result = await _controller.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _mockService.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode200WhenResultIsDeleted()
        {
            //Arrange
            _mockService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(_resultDto1);
            //Act
            var result = await _controller.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode400WhenResultIsNotDeleted()
        {
            //Arrange
            _mockService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync((ResultDto)null);
            //Act
            var result = await _controller.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _mockService.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_ReturnsStatusCode200WhenResultIsFound()
        {
            //Arrange
            _mockService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(_resultDto1);
            //Act
            var result = await _controller.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_ReturnsStatusCode404WhenResultIsNotFound()
        {
            //Arrange
            _mockService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((ResultDto)null);
            //Act
            var result = await _controller.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _mockService.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode200WhenResultsExist()
        {
            //arrange
            _mockService.Setup(x => x.GetAll()).ReturnsAsync(new List<ResultDto> { _resultDto1, _resultDto2 });
            //Act
            var result = await _controller.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode204WhenNoResultsExist()
        {
            //arrange
            _mockService.Setup(x => x.GetAll()).ReturnsAsync(new List<ResultDto> {});
            //Act
            var result = await _controller.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(204, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode500WhenExceptionThrown()
        {
            //arrange
            _mockService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode200WhenResultIsUpdated()
        {
            //Arrange
            _mockService.Setup(x => x.Update(It.IsAny<ResultDto>())).ReturnsAsync(_resultDto1);
            //Act
            var result = await _controller.Update(_resultDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode400WhenResultIsNotUpdated()
        {
            //Arrange
            _mockService.Setup(x => x.Update(It.IsAny<ResultDto>())).ReturnsAsync((ResultDto)null);
            //Act
            var result = await _controller.Update(_resultDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _mockService.Setup(x => x.Update(It.IsAny<ResultDto>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Update(_resultDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
    }   
}
