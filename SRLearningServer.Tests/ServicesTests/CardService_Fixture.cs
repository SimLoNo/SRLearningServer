using Microsoft.Identity.Client;
using Moq;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Components.Services;
using SRLearningServer.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLearningServer.Tests.ServicesTests
{
    [TestClass]
    [TestCategory("UnitTest")]
    public class CardService_Fixture
    {
        private CardService _cardService;
        private Mock<ICardRepository> _mockCardRepository;
        private Mock<IDtoToDomainConverter> _mockDtoToDomainConverter;
        private Mock<IDomainToDtoConverter> _mockDomainToDtoConverter;
        private readonly TestDataGenerator _testDataGenerator;

        private Card _card1;
        private Card _card2;

        private CardDto _cardDto1;
        private CardDto _cardDto2;

        private Components.Models.Type _type1;
        private Components.Models.Type _type2;

        private TypeDto _typeDto1;
        private TypeDto _typeDto2;

        private Result _result1;
        private Result _result2;

        private ResultDto _resultDto1;
        private ResultDto _resultDto2;

        private Attachment _attachment1;
        private Attachment _attachment2;

        private AttachmentDto _attachmentDto1;
        private AttachmentDto _attachmentDto2;

        public CardService_Fixture()
        {
            _testDataGenerator = new TestDataGenerator();
            _mockCardRepository = new Mock<ICardRepository>();
            _mockDtoToDomainConverter = new Mock<IDtoToDomainConverter>();
            _mockDomainToDtoConverter = new Mock<IDomainToDtoConverter>();
            _cardService = new CardService(_mockDtoToDomainConverter.Object, _mockDomainToDtoConverter.Object, _mockCardRepository.Object);
        }



        [TestInitialize]
        public void TestInitialize()
        {
            _typeDto1 = _testDataGenerator.CreateTypeDto(1,"type1",DateOnly.FromDateTime(DateTime.UtcNow),true);
            _typeDto2 = _testDataGenerator.CreateTypeDto(2, "type2", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _type1 = _testDataGenerator.CreateTypeFromDto(_typeDto1);
            _type2 = _testDataGenerator.CreateTypeFromDto(_typeDto2);

            _resultDto1 = _testDataGenerator.CreateResultDto(1, "result1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _resultDto2 = _testDataGenerator.CreateResultDto(2, "result2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _result1 = _testDataGenerator.CreateResultFromDto(_resultDto1);
            _result2 = _testDataGenerator.CreateResultFromDto(_resultDto2);

            _attachmentDto1 = _testDataGenerator.CreateAttachmentDto(1, "attachment1", "url1", DateOnly.FromDateTime(DateTime.UtcNow),true);
            _attachmentDto2 = _testDataGenerator.CreateAttachmentDto(2, "attachment2", "url2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _attachment1 = _testDataGenerator.CreateAttachmentFromDto(_attachmentDto1);
            _attachment2 = _testDataGenerator.CreateAttachmentFromDto(_attachmentDto2);

            _cardDto1 = _testDataGenerator.CreateCardDto(1, "card1","text1", DateOnly.FromDateTime(DateTime.UtcNow),true);
            _cardDto2 = _testDataGenerator.CreateCardDto(2, "card2", "text2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _card1 = _testDataGenerator.CreateCardFromDto(_cardDto1);
            _card2 = _testDataGenerator.CreateCardFromDto(_cardDto2);
        }

        [TestMethod]
        public async Task Create_CardDto_ReturnsCardDto()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<CardDto>())).Returns(_card1);
            _mockCardRepository.Setup(x => x.Create(It.IsAny<Card>())).ReturnsAsync(_card1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);

            // Act
            var result = await _cardService.Create(_cardDto1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(_cardDto1, result);
        }

        [TestMethod]
        public async Task Create_ExceptionThrown_ReturnsNull()
        {
            //Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<CardDto>())).Returns(_card1);
            _mockCardRepository.Setup(x => x.Create(It.IsAny<Card>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);
            //Act
            var result = await _cardService.Create(_cardDto1);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_CardDto_ReturnsCardDtoWithActiveFalse()
        {
            // Arrange
            _card1.Active = false;
            _cardDto1.Active = false;
            _mockCardRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_card1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);

            // Act
            var result = await _cardService.Deactivate(_cardDto1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(_cardDto1, result);
        }

        [TestMethod]
        public async Task Deactivate_Int_ReturnsCardDto()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_card1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);

            //Act
            var result = await _cardService.Deactivate(1);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(_cardDto1, result);
        }

        [TestMethod]
        public async Task Deactivate_Int_ReturnsNull()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);
            //Act
            var result = await _cardService.Deactivate(1);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Delete_Int_ReturnsCardDto()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(_card1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);
            //Act
            var result = await _cardService.Delete(1);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(_cardDto1, result);
        }

        [TestMethod]
        public async Task Delete_Int_ReturnsNull()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);
            //Act
            var result = await _cardService.Delete(1);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Get_Int_ReturnsCardDto()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(_card1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);
            //Act
            var result = await _cardService.Get(1);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(_cardDto1, result);
        }

        [TestMethod]
        public async Task Get_Int_ReturnsNull()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);
            //Act
            var result = await _cardService.Get(1);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAll_ReturnsListOfCardDto()
        {
            //Arrange
            List<Card> cards = new List<Card> { _card1, _card2 };
            List<CardDto> cardDtos = new List<CardDto> { _cardDto1, _cardDto2 };
            _mockCardRepository.Setup(x => x.GetAll()).ReturnsAsync(cards);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Card>>(), It.IsAny<bool>())).Returns(cardDtos);
            //Act
            var result = await _cardService.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<CardDto>));
            Assert.AreEqual(cardDtos, result);
        }

        [TestMethod]
        public async Task GetAll_ReturnsEmptyListWhenNoCardsFound()
        {
            //Arrange
            List<CardDto> cardsControlList = new List<CardDto>() { _cardDto1, _cardDto2 };
            _mockCardRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Card>());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Card>>(), It.IsAny<bool>())).Returns(cardsControlList);
            //Act
            var result = await _cardService.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<CardDto>));
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetAll_ReturnsNull()
        {
            //Arrange
            _mockCardRepository.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Card>>(), It.IsAny<bool>())).Returns(new List<CardDto>());
            //Act
            var result = await _cardService.GetAll();
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetByType_ListOfListOfType_ReturnsListOfCardDto()
        {
            //Arrange
            List<List<TypeDto>> typeDtoList = new List<List<TypeDto>> { new List<TypeDto> { _typeDto1, _typeDto2 } };
            List<TypeDto> typeDtos = new List<TypeDto>  { _typeDto1, _typeDto2 };

            List<Components.Models.Type> typeList = new List<Components.Models.Type> { _type1, _type2 };
            List<Card> cards = new List<Card> { _card1, _card2 };
            List<CardDto> cardDtos = new List<CardDto> { _cardDto1, _cardDto2 };
            _mockCardRepository.Setup(x => x.GetByType(It.IsAny<List<List<Components.Models.Type>>>())).ReturnsAsync(cards);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Card>>(), It.IsAny<bool>())).Returns(cardDtos);
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<List<TypeDto>>())).Returns(typeList);
            //Act
            var result = await _cardService.GetByType(typeDtoList);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<CardDto>));
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetByType_ListOfListOfType_ReturnsNullWhenException()
        {
            //arrange
            List<List<TypeDto>> typeDtoList = new List<List<TypeDto>> { new List<TypeDto> { _typeDto1, _typeDto2 } };
            List<TypeDto> typeDtos = new List<TypeDto> { _typeDto1, _typeDto2 };

            List<Components.Models.Type> typeList = new List<Components.Models.Type> { _type1, _type2 };
            List<Card> cards = new List<Card> { _card1, _card2 };
            List<CardDto> cardDtos = new List<CardDto> { _cardDto1, _cardDto2 };
            _mockCardRepository.Setup(x => x.GetByType(It.IsAny<List<List<Components.Models.Type>>>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Card>>(), It.IsAny<bool>())).Returns(cardDtos);
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<List<TypeDto>>())).Returns(typeList);
            //Act
            var result = await _cardService.GetByType(typeDtoList);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_CardDto_ReturnsCardDto()
        {
            //Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<CardDto>())).Returns(_card1);
            _mockCardRepository.Setup(x => x.Update(It.IsAny<Card>())).ReturnsAsync(_card1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);
            //Act
            var result = await _cardService.Update(_cardDto1);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(_cardDto1, result);
        }

        [TestMethod]
        public async Task Update_CardDto_ReturnsNull()
        {
            //Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<CardDto>())).Returns(_card1);
            _mockCardRepository.Setup(x => x.Update(It.IsAny<Card>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);
            //Act
            var result = await _cardService.Update(_cardDto1);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_CardDto_ReturnsNullWhenCardIsNull()
        {
            //Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<CardDto>())).Returns(_card1);
            _mockCardRepository.Setup(x => x.Update(It.IsAny<Card>())).ReturnsAsync((Card)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Card>(), It.IsAny<bool>())).Returns(_cardDto1);
            //Act
            var result = await _cardService.Update(_cardDto1);
            //Assert
            Assert.IsNull(result);
        }


    }
}
