using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
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
    public class TypeCategoryListController_Fixture
    {
        private readonly Mock<ITypeCategoryListService> _serviceMock;
        private readonly TypeCategoryListController _controller;
        private readonly TestDataGenerator _testDataGenerator;

        private TypeCategoryListDto _typeCategoryListDto1;
        private TypeCategoryListDto _typeCategoryListDto2;

        public TypeCategoryListController_Fixture()
        {
            _testDataGenerator = new TestDataGenerator();
            _serviceMock = new Mock<ITypeCategoryListService>();
            _controller = new TypeCategoryListController(_serviceMock.Object);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _typeCategoryListDto1 = _testDataGenerator.CreateTypeCategoryListDto(1, "typeCategoryList1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeCategoryListDto2 = _testDataGenerator.CreateTypeCategoryListDto(2, "typeCategoryList2", DateOnly.FromDateTime(DateTime.UtcNow), true);
        }

        [TestMethod]
        public async Task Create_ReturnsStatusCode400WhenTypeCategoryListIsNotCreated()
        {
            //Arrange
            _serviceMock.Setup(x => x.Create(It.IsAny<TypeCategoryListDto>())).ReturnsAsync((TypeCategoryListDto)null);
            //Act
            var result = await _controller.Create(_typeCategoryListDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Create_ReturnsStatusCode200WhenTypeCategoryListIsCreated()
        {
            //Arrange
            _serviceMock.Setup(x => x.Create(It.IsAny<TypeCategoryListDto>())).ReturnsAsync(_typeCategoryListDto1);
            //Act
            var result = await _controller.Create(_typeCategoryListDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }


        [TestMethod]
        public async Task Create_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _serviceMock.Setup(x => x.Create(It.IsAny<TypeCategoryListDto>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Create(_typeCategoryListDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode404WhenTypeCategoryListIsNotDeactivated()
        {
            //Arrange
            _serviceMock.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync((TypeCategoryListDto)null);
            //Act
            var result = await _controller.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode200WhenTypeCategoryListIsDeactivated()
        {
            //Arrange
            _serviceMock.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_typeCategoryListDto1);
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
            _serviceMock.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode404WhenTypeCategoryListIsNotDeleted()
        {
            //Arrange
            _serviceMock.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync((TypeCategoryListDto)null);
            //Act
            var result = await _controller.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Delete_ReturnsStatusCode200WhenTypeCategoryListIsDeleted()
        {
            //Arrange
            _serviceMock.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(_typeCategoryListDto1);
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
            _serviceMock.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Get_ReturnsStatusCode404WhenTypeCategoryListIsNotFound()
        {
            //Arrange
            _serviceMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((TypeCategoryListDto)null);
            //Act
            var result = await _controller.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Get_ReturnsStatusCode200WhenTypeCategoryListIsFound()
        {
            //Arrange
            _serviceMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(_typeCategoryListDto1);
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
            _serviceMock.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task GetAll_ReturnsStatusCode200WhenTypeCategoryListExist()
        {
            //Arrange
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(new List<TypeCategoryListDto>() { _typeCategoryListDto1, _typeCategoryListDto2});
            //Act
            var result = await _controller.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode204WhenTypeCategoryListDoesNotExist()
        {
            //Arrange
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync((List<TypeCategoryListDto>)null);
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
            _serviceMock.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetByName_ReturnsStatusCode404WhenTypeCategoryListIsNotFound()
        {
            //Arrange
            _serviceMock.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync((TypeCategoryListDto)null);
            //Act
            var result = await _controller.GetByName("typeCategoryList1");
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task GetByName_ReturnsStatusCode200WhenTypeCategoryListIsFound()
        {
            //Arrange
            _serviceMock.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync(_typeCategoryListDto1);
            //Act
            var result = await _controller.GetByName("typeCategoryList1");
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task GetByName_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _serviceMock.Setup(x => x.GetByName(It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.GetByName("typeCategoryList1");
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Update_ReturnsStatusCode404WhenTypeCategoryListIsNotUpdated()
        {
            //Arrange
            _serviceMock.Setup(x => x.Update(It.IsAny<TypeCategoryListDto>())).ReturnsAsync((TypeCategoryListDto)null);
            //Act
            var result = await _controller.Update(_typeCategoryListDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Update_ReturnsStatusCode200WhenTypeCategoryListIsUpdated()
        {
            //Arrange
            _serviceMock.Setup(x => x.Update(It.IsAny<TypeCategoryListDto>())).ReturnsAsync(_typeCategoryListDto1);
            //Act
            var result = await _controller.Update(_typeCategoryListDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }
        [TestMethod]
        public async Task Update_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _serviceMock.Setup(x => x.Update(It.IsAny<TypeCategoryListDto>())).ThrowsAsync(new Exception());
            //Act
            var result = await _controller.Update(_typeCategoryListDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }


    }
}
