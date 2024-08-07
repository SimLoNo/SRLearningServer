using Microsoft.IdentityModel.Tokens;
using Moq;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Interfaces.Services;
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
    public class AttachmentService_Fixture
    {
        private readonly Mock<IDtoToDomainConverter> _dtoToDomainConverter = new();
        private readonly Mock<IDomainToDtoConverter> _domainToDtoConverter = new();
        private readonly Mock<IAttachmentRepository> _attachmentRepository = new();
        private readonly AttachmentService _attachmentService;
        private readonly TestDataGenerator _testDataGenerator = new();

        private AttachmentDto _attachmentDto1;
        private ResultDto _resultDto1;
        private CardDto _cardDto1;
        private TypeDto _typeDto1;

        private Attachment _attachment1;
        private Result _result1;
        private Card _card1;
        private Components.Models.Type _type1;

        public AttachmentService_Fixture()
        {

            _attachmentService = new AttachmentService(_dtoToDomainConverter.Object, _domainToDtoConverter.Object, _attachmentRepository.Object);
        }
        [TestInitialize]
        public void TestInitialize()
        {
            /*DateOnly date = DateOnly.FromDateTime(DateTime.UtcNow);
            _typeDto1 = _testDataGenerator.CreateTypeDto(1, "Signal", date, true, new());
            _type1 = _testDataGenerator.CreateType(1, "Signal", date, true, new());

            _resultDto1 = _testDataGenerator.CreateResultDto(1, "Result1", date, true, null, new List<CardDto>());
            _result1 = _testDataGenerator.CreateResult(1, "Result1", date, true, null, new List<Card>());

            _cardDto1 = _testDataGenerator.CreateCardDto(1, "Card1", "Card1 Text", date, true, new List<TypeDto> { _typeDto1 }, new());
            _card1 = _testDataGenerator.CreateCard(1, "Card1", "Card1 Text", date, true, new List<Components.Models.Type> { _type1 }, new());

            _attachmentDto1 = _testDataGenerator.CreateAttachmentDto(1, "Attachment1", "Attachment1 Url", date, true, new List<CardDto> { _cardDto1 }, new List<ResultDto> { _resultDto1 });
            _attachment1 = _testDataGenerator.CreateAttachment(1, "Attachment1", "Attachment1 Url", date, true, new List<Card> { _card1 }, new List<Result> { _result1 });*/
        }

        /*[TestMethod]
        public void ConvertFromDto_WhenCalled_ReturnsAttachment()
        {
            // Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            List<Card> cards = new();
            List<Result> results = new();
            results.Add(_result1);
            cards.Add(_card1);
            _domainToDtoConverter.Setup(x => x.ConvertFromDto(It.IsAny<List<ResultDto>>())).Returns( results );
            _dtoToDomainConverter.Setup(x => x.ConvertFromDto(It.IsAny<List<CardDto>>())).Returns( cards );

            // Act
            Attachment attachment = _attachmentService.ConvertFromDto(entityUsed);

            // Assert
            Assert.IsNotNull(attachment);
            Assert.AreEqual(entityUsed.AttachmentId, attachment.AttachmentId);
            Assert.AreEqual(entityUsed.AttachmentName, attachment.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, attachment.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, attachment.LastUpdated);
            Assert.AreEqual(entityUsed.Active, attachment.Active);

            Assert.AreEqual(entityUsed.Cards.Count, attachment.Cards.Count);
            Card card = attachment.Cards.FirstOrDefault();
            Assert.IsNotNull(card);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().CardId, card.CardId);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().CardName, card.CardName);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().CardText, card.CardText);
            //Assert.AreEqual(entityUsed.Cards.FirstOrDefault().AttachmentId, card.AttachmentId);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().LastUpdated, card.LastUpdated);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Active, card.Active);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().TypeId, card.Types.FirstOrDefault().TypeId);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().CardTypeName, card.Types.FirstOrDefault().CardTypeName);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().LastUpdated, card.Types.FirstOrDefault().LastUpdated);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().Active, card.Types.FirstOrDefault().Active);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().Cards.Count, card.Types.FirstOrDefault().Cards.Count);

            Assert.AreEqual(entityUsed.Results.Count, attachment.Results.Count);
            Result result = attachment.Results.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(entityUsed.Results.FirstOrDefault().ResultId, result.ResultId);
            Assert.AreEqual(entityUsed.Results.FirstOrDefault().ResultText, result.ResultText);
            //Assert.AreEqual(entityUsed.Results.FirstOrDefault().AttachmentId, result.AttachmentId);
            Assert.AreEqual(entityUsed.Results.FirstOrDefault().LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Results.FirstOrDefault().Active, result.Active);
            Assert.IsNull(result.Attachment);

        }

        [TestMethod]
        public void ConvertToDto_WhenCalledWithRelations_ReturnsAttachmentDtoWithDtoRelations()
        {
            // Arrange
            Attachment entityUsed = _attachment1;
            List<CardDto> cards = new();
            List<ResultDto> results = new();
            results.Add(_resultDto1);
            cards.Add(_cardDto1);
            _domainToDtoConverter.Setup(x => x.ConvertToDto(It.IsAny<List<Result>>(), It.IsAny<bool>())).Returns(results);
            _dtoToDomainConverter.Setup(x => x.ConvertToDto(It.IsAny<List<Card>>(), It.IsAny<bool>())).Returns(cards);

            // Act
            AttachmentDto attachment = _attachmentService.ConvertToDto(entityUsed, true);

            // Assert
            Assert.IsNotNull(attachment);
            Assert.AreEqual(entityUsed.AttachmentId, attachment.AttachmentId);
            Assert.AreEqual(entityUsed.AttachmentName, attachment.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, attachment.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, attachment.LastUpdated);
            Assert.AreEqual(entityUsed.Active, attachment.Active);

            Assert.AreEqual(entityUsed.Cards.Count, attachment.Cards.Count);
            CardDto card = attachment.Cards.FirstOrDefault();
            Assert.IsNotNull(card);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().CardId, card.CardId);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().CardName, card.CardName);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().CardText, card.CardText);
            //Assert.AreEqual(entityUsed.Cards.FirstOrDefault().AttachmentId, card.AttachmentId);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().LastUpdated, card.LastUpdated);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Active, card.Active);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().TypeId, card.Types.FirstOrDefault().TypeId);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().CardTypeName, card.Types.FirstOrDefault().CardTypeName);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().LastUpdated, card.Types.FirstOrDefault().LastUpdated);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().Active, card.Types.FirstOrDefault().Active);
            Assert.AreEqual(entityUsed.Cards.FirstOrDefault().Types.FirstOrDefault().Cards.Count, card.Types.FirstOrDefault().Cards.Count);

            Assert.AreEqual(entityUsed.Results.Count, attachment.Results.Count);
            ResultDto result = attachment.Results.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(entityUsed.Results.FirstOrDefault().ResultId, result.ResultId);
            Assert.AreEqual(entityUsed.Results.FirstOrDefault().ResultText, result.ResultText);
            //Assert.AreEqual(entityUsed.Results.FirstOrDefault().AttachmentId, result.AttachmentId);
            Assert.AreEqual(entityUsed.Results.FirstOrDefault().LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Results.FirstOrDefault().Active, result.Active);
            Assert.IsNull(result.Attachment);

        }

        [TestMethod]
        public void ConvertToDto_WhenCalledWithoutRelations_ReturnsAttachmentDtoOnly()
        {
            // Arrange
            Attachment entityUsed = _attachment1;
            List<CardDto> cards = new();
            List<ResultDto> results = new();
            results.Add(_resultDto1);
            cards.Add(_cardDto1);
            _domainToDtoConverter.Setup(x => x.ConvertToDto(It.IsAny<List<Result>>(), It.IsAny<bool>())).Returns(results);
            _dtoToDomainConverter.Setup(x => x.ConvertToDto(It.IsAny<List<Card>>(), It.IsAny<bool>())).Returns(cards);

            // Act
            AttachmentDto attachment = _attachmentService.ConvertToDto(entityUsed, false);

            // Assert
            Assert.IsNotNull(attachment);
            Assert.AreEqual(entityUsed.AttachmentId, attachment.AttachmentId);
            Assert.AreEqual(entityUsed.AttachmentName, attachment.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, attachment.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, attachment.LastUpdated);
            Assert.AreEqual(entityUsed.Active, attachment.Active);

            Assert.AreEqual(0, attachment.Cards.Count);

            Assert.AreEqual(0, attachment.Results.Count);

        }*/

        /*[TestMethod]
        public void Create_AttachmentDto_ReturnsAttachmentDto()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            entityUsed.AttachmentId = new();
            _dtoToDomainConverter.Setup(x => x.ConvertToAttachmentFromAttachmentDto(It.IsAny<AttachmentDto>())).Returns(returnedAttachment);
            _attachmentRepository.Setup(x => x.Create(It.IsAny<Attachment>())).Returns(Task.FromResult(returnedAttachment));
            //Act
            var result = _attachmentService.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(returnedAttachment.AttachmentId, result.AttachmentId);

        }

        [TestMethod]
        public void Deactivate_AttachmentDto_ReturnsAttachmentDto()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            returnedAttachment.Active = false;
            _attachmentRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult(returnedAttachment));
            //Act
            var result = _attachmentService.Deactivate(entityUsed.AttachmentId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(false, result.Active);
            Assert.AreEqual(returnedAttachment.AttachmentId, result.AttachmentId);

        }

        [TestMethod]
        public void Deactivate_int_ReturnsAttachmentDto()
        {
            //Arrange
            Attachment returnedAttachment = _attachment1;
            int idUsed = returnedAttachment.AttachmentId;
            returnedAttachment.Active = false;
            _attachmentRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult(returnedAttachment));
            //Act
            var result = _attachmentService.Deactivate(idUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(returnedAttachment.AttachmentName, result.AttachmentName);
            Assert.AreEqual(returnedAttachment.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(returnedAttachment.LastUpdated, result.LastUpdated);
            Assert.AreEqual(false, result.Active);
            Assert.AreEqual(idUsed, result.AttachmentId);

        }

        [TestMethod]
        public void Deactivate_AttachmentDto_ReturnsNull()
        {
            //Arrange
            int idUsed = _attachment1.AttachmentId;
            _attachmentRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult<Attachment>(null));
            //Act
            var result = _attachmentService.Deactivate(idUsed);
            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public void Deactivate_int_ReturnsNull()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            _attachmentRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult<Attachment>(null));
            //Act
            var result = _attachmentService.Deactivate(entityUsed.AttachmentId);
            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public void Delete_AttachmentDto_ReturnsAttachmentDto()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            _attachmentRepository.Setup(x => x.Delete(It.IsAny<Attachment>())).Returns(Task.FromResult(returnedAttachment));
            //Act
            var result = _attachmentService.Delete(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(returnedAttachment.AttachmentId, result.AttachmentId);
        }

        [TestMethod]
        public void Delete_AttachmentDto_ReturnsNull()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            _attachmentRepository.Setup(x => x.Delete(It.IsAny<Attachment>())).Returns(Task.FromResult<Attachment>(null));
            //Act
            var result = _attachmentService.Delete(entityUsed);
            //Assert
            Assert.IsNull(result);
        }*/



    }
}
