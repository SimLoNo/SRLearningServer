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
    public class CardController_Fixture
    {

        private readonly Mock<ICardService> _cardService;
        private readonly CardController _cardController;
        private readonly TestDataGenerator _testDataGenerator;

        private CardDto _cardDto1;
        private CardDto _cardDto2;

        private TypeDto _typeDto1;
        private TypeDto _typeDto2;

        public CardController_Fixture()
        {
            _cardService = new Mock<ICardService>();
            _cardController = new CardController(_cardService.Object);
            _testDataGenerator = new TestDataGenerator();

        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cardDto1 = _testDataGenerator.CreateCardDto(1, "card1","text1", DateOnly.FromDateTime(DateTime.UtcNow),true);
            _cardDto2 = _testDataGenerator.CreateCardDto(2, "card2", "text2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _typeDto1 = _testDataGenerator.CreateTypeDto(1, "type1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeDto2 = _testDataGenerator.CreateTypeDto(2, "type2", DateOnly.FromDateTime(DateTime.UtcNow), true);

        }

        [TestMethod]
        public async Task Create_ReturnsStatusCode200WhenCardIsCreated()
        {
            // Arrange
            _cardService.Setup(x => x.Create(It.IsAny<CardDto>())).ReturnsAsync(_cardDto1);

            // Act
            var result = await _cardController.Create(_cardDto1);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }


        [TestMethod]
        public async Task Create_ReturnsStatusCode400WhenSentCardIsNull()
        {
            // Arrange
            _cardService.Setup(x => x.Create(It.IsAny<CardDto>())).ReturnsAsync(_cardDto1);

            // Act
            var result = await _cardController.Create((CardDto)null);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Create_ReturnsStatusCode400WhenCardIsNotCreated()
        {
            // Arrange
            _cardService.Setup(x => x.Create(It.IsAny<CardDto>())).ReturnsAsync((CardDto)null);

            // Act
            var result = await _cardController.Create(_cardDto1);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Create_ReturnsStatusCode500WhenExceptionThrown()
        {
            // Arrange
            _cardService.Setup(x => x.Create(It.IsAny<CardDto>())).ThrowsAsync(new Exception());

            // Act
            var result = await _cardController.Create(_cardDto1);
            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_returnsStatusCode200WhenCardIsDeactivated()
        {
            //Arrange
            _cardService.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_cardDto1);
            //Act
            var result = await _cardController.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_returnsStatusCode400WhenCardIsNotDeactivated()
        {
            //Arrange
            _cardService.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync((CardDto)null);
            //Act
            var result = await _cardController.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Deactivate_returnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _cardService.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _cardController.Deactivate(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode200WhenCardIsDeleted()
        {
            //Arrange
            _cardService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(_cardDto1);
            //Act
            var result = await _cardController.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode400WhenCardIsNotDeleted()
        {
            //Arrange
            _cardService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync((CardDto)null);
            //Act
            var result = await _cardController.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _cardService.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _cardController.Delete(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_ReturnsStatusCode200WhenCardIsFoundd()
        {
            //Arrange
            _cardService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(_cardDto1);
            //Act
            var result = await _cardController.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_ReturnsStatusCode404WhenCardIsNotFound()
        {
            //Arrange
            _cardService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((CardDto)null);
            //Act
            var result = await _cardController.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _cardService.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            //Act
            var result = await _cardController.Get(1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode200WhenCardsExist()
        {
            //Arrange
            _cardService.Setup(x => x.GetAll()).ReturnsAsync(new List<CardDto> { _cardDto1, _cardDto2 });
            //Act
            var result = await _cardController.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);

        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode204WhenNoCardsExist()
        {
            //Arrange
            _cardService.Setup(x => x.GetAll()).ReturnsAsync(new List<CardDto> {});
            //Act
            var result = await _cardController.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(204, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _cardService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
            //Act
            var result = await _cardController.GetAll();
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);

        }

        [TestMethod]
        public async Task GetByType_ReturnsStatusCode200WhenCardsIsFound()
        {
            //arrange
            List<List<TypeDto>> types = new List<List<TypeDto>> { new List<TypeDto> { _typeDto1, _typeDto2 } };
            List<CardDto> cards = new List<CardDto> { _cardDto1, _cardDto2 };
            _cardService.Setup(x => x.GetByType(It.IsAny<List<List<TypeDto>>>())).ReturnsAsync(cards);
            //Act
            var result = await _cardController.GetByType(types);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetByType_ReturnsStatusCode204WhenNoMatchingCardsIsFound()
        {
            //arrange
            List<List<TypeDto>> types = new List<List<TypeDto>> { new List<TypeDto> { _typeDto1, _typeDto2 } };
            List<CardDto> cards = new List<CardDto> {};
            _cardService.Setup(x => x.GetByType(It.IsAny<List<List<TypeDto>>>())).ReturnsAsync(cards);
            //Act
            var result = await _cardController.GetByType(types);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(204, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetByType_ReturnsStatusCode500WhenExceptionThrown()
        {
            //arrange
            List<List<TypeDto>> types = new List<List<TypeDto>> { new List<TypeDto> { _typeDto1, _typeDto2 } };
            _cardService.Setup(x => x.GetByType(It.IsAny<List<List<TypeDto>>>())).ThrowsAsync(new Exception());
            //Act
            var result = await _cardController.GetByType(types);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode200WhenCardIsUpdated()
        {
            //Arrange
            _cardService.Setup(x => x.Update(It.IsAny<CardDto>())).ReturnsAsync(_cardDto1);
            //Act
            var result = await _cardController.Update(_cardDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(200, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode400WhenSentCardIsNull()
        {
            //Arrange
            _cardService.Setup(x => x.Update(It.IsAny<CardDto>())).ReturnsAsync(_cardDto1);
            //Act
            var result = await _cardController.Update((CardDto)null);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode400WhenCardIsNotUpdated()
        {
            //Arrange
            _cardService.Setup(x => x.Update(It.IsAny<CardDto>())).ReturnsAsync((CardDto)null);
            //Act
            var result = await _cardController.Update(_cardDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsStatusCode500WhenExceptionThrown()
        {
            //Arrange
            _cardService.Setup(x => x.Update(It.IsAny<CardDto>())).ThrowsAsync(new Exception());
            //Act
            var result = await _cardController.Update(_cardDto1);
            //Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
        }
    }
}
