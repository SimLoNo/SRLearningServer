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
    public class AttachmentController_Fixture
    {
        private readonly Mock<IAttachmentService> _attachmentServiceMock;
        private readonly AttachmentController attachmentController;
        private readonly TestDataGenerator _testDataGenerator;

        private AttachmentDto _attachmentDto1;
        private AttachmentDto _attachmentDto2;

        public AttachmentController_Fixture()
        {
            _attachmentServiceMock = new Mock<IAttachmentService>();
            attachmentController = new AttachmentController(_attachmentServiceMock.Object);
            _testDataGenerator = new TestDataGenerator();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _attachmentDto1 = _testDataGenerator.CreateAttachmentDto(1, "attachment1", "url1", DateOnly.FromDateTime(DateTime.UtcNow),true);
            _attachmentDto2 = _testDataGenerator.CreateAttachmentDto(2, "attachment2", "url2", DateOnly.FromDateTime(DateTime.UtcNow), true);
        }

        [TestMethod]
        public async Task Create_AttachmentDot_ReturnsStatusCode200WhenCreated()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Create(It.IsAny<AttachmentDto>())).ReturnsAsync(_attachmentDto1);
            //Act
            var result = await attachmentController.Create(_attachmentDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Create_AttachmentDot_ReturnsStatusCode400WhenNotCreated()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Create(It.IsAny<AttachmentDto>())).ReturnsAsync((AttachmentDto)null);
            //Act
            var result = await attachmentController.Create(_attachmentDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Create_AttachmentDot_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Create(It.IsAny<AttachmentDto>())).ThrowsAsync(new Exception());
            //Act
            var result = await attachmentController.Create(_attachmentDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode200WhenAttachmentsExist()
        {
            //Arrange
            List<AttachmentDto> attachments = new List<AttachmentDto> { _attachmentDto1, _attachmentDto2 };
            _attachmentServiceMock.Setup(x => x.GetAll()).ReturnsAsync(attachments);
            //Act
            var result = await attachmentController.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode204WhenNoAttachmentsExist()
        {
            //Arrange
            List<AttachmentDto> attachments = new List<AttachmentDto> {};
            _attachmentServiceMock.Setup(x => x.GetAll()).ReturnsAsync(attachments);
            //Act
            var result = await attachmentController.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(204, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            List<AttachmentDto> attachments = new List<AttachmentDto> { };
            _attachmentServiceMock.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
            //Act
            var result = await attachmentController.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode200WhenAttachmentIsUpdated()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_attachmentDto1);
            //act
            var result = await attachmentController.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode404WhenAttachmentIsNotFound()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync((AttachmentDto)null);
            //act
            var result = await attachmentController.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            //act
            var result = await attachmentController.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode200WhenAttachmentIsDeleted()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(_attachmentDto1);
            //act
            var result = await attachmentController.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode404WhenAttachmentIsNotDeleted()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync((AttachmentDto)null);
            //act
            var result = await attachmentController.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            //act
            var result = await attachmentController.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetById_Int_ReturnsStatusCode200WhenAttachmentFound()
        {
            //arrange
            _attachmentServiceMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(_attachmentDto1);
            //Act
            var result = await attachmentController.GetById(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }


        [TestMethod]
        public async Task GetById_Int_ReturnsStatusCode404WhenAttachmentnOTFound()
        {
            //arrange
            _attachmentServiceMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((AttachmentDto)null);
            //Act
            var result = await attachmentController.GetById(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }


        [TestMethod]
        public async Task GetById_Int_ReturnsStatusCode500WhenExceptionThrown()
        {
            //arrange
            _attachmentServiceMock.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await attachmentController.GetById(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode200WhenAttachmentIsUpdated()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Update(It.IsAny<AttachmentDto>())).ReturnsAsync(_attachmentDto1);
            //act
            var result = await attachmentController.Update(_attachmentDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode404WhenAttachmentIsNotUpdated()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Update(It.IsAny<AttachmentDto>())).ReturnsAsync((AttachmentDto)null);
            //act
            var result = await attachmentController.Update(_attachmentDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _attachmentServiceMock.Setup(x => x.Update(It.IsAny<AttachmentDto>())).ThrowsAsync(new Exception());
            //act
            var result = await attachmentController.Update(_attachmentDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }


    }
}
