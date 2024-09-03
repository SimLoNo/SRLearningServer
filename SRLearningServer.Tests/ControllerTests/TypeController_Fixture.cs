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
    public class TypeController_Fixture
    {

        private readonly Mock<ITypeService> _service;
        private readonly TypeController _controller;
        private readonly TestDataGenerator _testDataGenerator;

        private TypeDto _typeDto1;
        private TypeDto _typeDto2;

        public TypeController_Fixture()
        {
            _service = new Mock<ITypeService>();
            _controller = new TypeController(_service.Object);
            _testDataGenerator = new TestDataGenerator();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _typeDto1 = _testDataGenerator.CreateTypeDto(1, "type1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeDto2 = _testDataGenerator.CreateTypeDto(2, "type2", DateOnly.FromDateTime(DateTime.UtcNow), true);
        }

        [TestMethod]
        public async Task Create_ReturnsStatusCode400WhenTypeIsNotCreated()
        {
            //Arrange
            _service.Setup(x => x.Create(It.IsAny<TypeDto>())).ReturnsAsync((TypeDto)null);
            //Act
            var result = await _controller.Create(_typeDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Create_ReturnsStatusCode200WhenTypeIsCreated()
        {
            //Arrange
            _service.Setup(x => x.Create(It.IsAny<TypeDto>())).ReturnsAsync(_typeDto1);
            //Act
            var result = await _controller.Create(_typeDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Create_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _service.Setup(x => x.Create(It.IsAny<TypeDto>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Create(_typeDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode400WhenTypeIsNotDeactivated()
        {
            //Arrange
            _service.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync((TypeDto)null);
            //Act
            var result = await _controller.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode200WhenTypeIsDeactivated()
        {
            //Arrange
            _service.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_typeDto1);
            //Act
            var result = await _controller.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _service.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Delete_ReturnsStatusCode400WhenTypeIsNotDeleted()
        {
            //Arrange
            _service.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync((TypeDto)null);
            //Act
            var result = await _controller.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Delete_ReturnsStatusCode200WhenTypeIsDeleted()
        {
            //Arrange
            _service.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(_typeDto1);
            //Act
            var result = await _controller.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Delete_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _service.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_ReturnsStatusCode404WhenTypeIsNotFound()
        {
            //Arrange
            _service.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((TypeDto)null);
            //Act
            var result = await _controller.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Get_ReturnsStatusCode200WhenTypeIsFound()
        {
            //Arrange
            _service.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(_typeDto1);
            //Act
            var result = await _controller.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Get_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _service.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task GetAll_ReturnsStatusCode200WhenTypesExist()
        {
            //Arrange
            _service.Setup(x => x.GetAll()).ReturnsAsync(new List<TypeDto> { _typeDto1, _typeDto2 });
            //Act
            var result = await _controller.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task GetAll_ReturnsStatusCode204WhenTypesDoNotExist()
        {
            //Arrange
            _service.Setup(x => x.GetAll()).ReturnsAsync(new List<TypeDto>());
            //Act
            var result = await _controller.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(204, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task GetAll_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _service.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Update_ReturnsStatusCode400WhenTypeIsNotUpdated()
        {
            //Arrange
            _service.Setup(x => x.Update(It.IsAny<TypeDto>())).ReturnsAsync((TypeDto)null);
            //Act
            var result = await _controller.Update(_typeDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Update_ReturnsStatusCode200WhenTypeIsUpdated()
        {
            //Arrange
            _service.Setup(x => x.Update(It.IsAny<TypeDto>())).ReturnsAsync(_typeDto1);
            //Act
            var result = await _controller.Update(_typeDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Update_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _service.Setup(x => x.Update(It.IsAny<TypeDto>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Update(_typeDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

    }
}
