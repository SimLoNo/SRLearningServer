using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Components.Utilities;
using SRLearningServer.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLearningServer.Tests.UtilitiesTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class ActivitySortingUtility_Fixture
    {
        private ActivitySortingUtility _activitySortingUtility;
        private TestDataGenerator _testDataGenerator;

        private AttachmentDto _attachmentDto1;
        private AttachmentDto _attachmentDto2;

        private CardDto _cardDto1;
        private CardDto _cardDto2;

        private ResultDto _resultDto1;
        private ResultDto _resultDto2;

        private TypeCategoryListDto _typeCategoryListDto1;
        private TypeCategoryListDto _typeCategoryListDto2;

        private TypeDto _typeDto1;
        private TypeDto _typeDto2;

        public ActivitySortingUtility_Fixture()
        {
            _activitySortingUtility = new ActivitySortingUtility();
            _testDataGenerator = new TestDataGenerator();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _attachmentDto1 = _testDataGenerator.CreateAttachmentDto(1, "attachment1", "url1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _attachmentDto2 = _testDataGenerator.CreateAttachmentDto(2, "attachment2", "url2", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _cardDto1 = _testDataGenerator.CreateCardDto(1, "card1", "text1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _cardDto2 = _testDataGenerator.CreateCardDto(2, "card2", "text2", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _resultDto1 = _testDataGenerator.CreateResultDto(1, "result1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _resultDto2 = _testDataGenerator.CreateResultDto(2, "result2", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeCategoryListDto1 = _testDataGenerator.CreateTypeCategoryListDto(1, "typeCategoryList1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeCategoryListDto2 = _testDataGenerator.CreateTypeCategoryListDto(2, "typeCategoryList2", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeDto1 = _testDataGenerator.CreateTypeDto(1, "type1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeDto2 = _testDataGenerator.CreateTypeDto(2, "type2", DateOnly.FromDateTime(DateTime.UtcNow), true);
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoListWithDifferingActiveOnRemoveValueFalse_ShouldReturnSortedAttachmentDtoList()
        {
            // Arrange
            _attachmentDto2.Active = false;
            List<AttachmentDto> attachmentDtos = new List<AttachmentDto> { _attachmentDto1, _attachmentDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(attachmentDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoListWithDifferingActiveOnRemoveValueTrue_ShouldReturnSortedAttachmentDtoList()
        {
            // Arrange
            _attachmentDto2.Active = false;
            List<AttachmentDto> attachmentDtos = new List<AttachmentDto> { _attachmentDto1, _attachmentDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(attachmentDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoListWithAllActiveOnRemoveValueFalse_ShouldReturnSortedAttachmentDtoList()
        {
            // Arrange
            List<AttachmentDto> attachmentDtos = new List<AttachmentDto> { _attachmentDto1, _attachmentDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(attachmentDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoListWithAllActiveOnRemoveValueTrue_ShouldReturnEmptyAttachmentDtoList()
        {
            // Arrange
            List<AttachmentDto> attachmentDtos = new List<AttachmentDto> { _attachmentDto1, _attachmentDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(attachmentDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoListWithNoActiveOnRemoveValueFalse_ShouldReturnEmptyAttachmentDtoList()
        {
            // Arrange
            _attachmentDto1.Active = false;
            _attachmentDto2.Active = false;
            List<AttachmentDto> attachmentDtos = new List<AttachmentDto> { _attachmentDto1, _attachmentDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(attachmentDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoListWithNoActiveOnRemoveValueTrue_ShouldReturnFullAttachmentDtoList()
        {
            // Arrange
            _attachmentDto1.Active = false;
            _attachmentDto2.Active = false;
            List<AttachmentDto> attachmentDtos = new List<AttachmentDto> { _attachmentDto1, _attachmentDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(attachmentDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAnEmptyAttachmentDtoList_ShouldReturnEmptyAttachmentDtoList()
        {
            // Arrange
            List<AttachmentDto> attachmentDtos = new List<AttachmentDto> { };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(attachmentDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoInactiveOnRemoveValueFalse_ShouldReturnNull()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            entityUsed.Active = false;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoActiveOnRemoveValueTrue_ShouldReturnNull()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoInactiveOnRemoveValueTrue_ShouldReturnAttachmentDto()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            entityUsed.Active = false;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed, result);
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoActiveOnRemoveValueFalse_ShouldReturnAttachmentDto()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed, result);
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoWithListOfResultDtosWithDifferingActiveOnRemoveValueFalse_ShouldReturnAttachmentDtoWithSortedResultDtos()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            var relatedEntity1 = _resultDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _resultDto2;
            entityUsed.Results = new List<ResultDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
            Assert.AreEqual(1, result.Results.Count());
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoWithListOfResultDtosWithDifferingActiveOnRemoveValueTrue_ShouldReturnAttachmentDtoWithSortedResultDtos()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            var relatedEntity1 = _resultDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _resultDto2;
            entityUsed.Results = new List<ResultDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
            Assert.AreEqual(1, result.Results.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoWithListOfResultDtosWithAllActiveOnRemoveValueFalse_ShouldReturnAttachmentDtoWithAllResultDtos()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            var relatedEntity1 = _resultDto1;
            var relatedEntity2 = _resultDto2;
            entityUsed.Results = new List<ResultDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
            Assert.AreEqual(2, result.Results.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoWithListOfResultDtosWithAllActiveOnRemoveValueTrue_ShouldReturnAttachmentDtoWitEmptyResultDtos()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _resultDto1;
            var relatedEntity2 = _resultDto2;
            entityUsed.Results = new List<ResultDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
            Assert.AreEqual(0, result.Results.Count());
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoWithListOfCardDtosWithAllActiveOnRemoveValueTrue_ShouldReturnAttachmentDtoWitEmptyCardDtos()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _cardDto1;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
            Assert.AreEqual(0, result.Cards.Count());
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoWithListOfCardDtosWithNoActiveOnRemoveValueFalse_ShouldReturnAttachmentDtoWitEmptyCardDtos()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            var relatedEntity1 = _cardDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _cardDto2;
            relatedEntity2.Active = false;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
            Assert.AreEqual(0, result.Cards.Count());
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoWithListOfCardDtosWithDifferingActiveOnRemoveValueTrue_ShouldReturnAttachmentDtoWitSortedCardDtos()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _cardDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
            Assert.AreEqual(1, result.Cards.Count());
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithAttachmentDtoWithListOfCardDtosWithDifferingActiveOnRemoveValueFalse_ShouldReturnAttachmentDtoWitSortedCardDtos()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            var relatedEntity1 = _cardDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
            Assert.AreEqual(1, result.Cards.Count());
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithListOfCardDtoWithAllActiveOnRemoveValueFalse_ShouldReturnFullCardDtoList()
        {
            // Arrange
            List<CardDto> cardDtos = new List<CardDto> { _cardDto1, _cardDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(cardDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithListOfCardDtoWithAllActiveOnRemoveValueTrue_ShouldReturnEmptyCardDtoList()
        {
            // Arrange
            List<CardDto> cardDtos = new List<CardDto> { _cardDto1, _cardDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(cardDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithListOfCardDtoWithNoActiveOnRemoveValueFalse_ShouldReturnEmptyCardDtoList()
        {
            // Arrange
            _cardDto1.Active = false;
            _cardDto2.Active = false;
            List<CardDto> cardDtos = new List<CardDto> { _cardDto1, _cardDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(cardDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithListOfCardDtoWithNoActiveOnRemoveValueTrue_ShouldReturnFullCardDtoList()
        {
            // Arrange
            _cardDto1.Active = false;
            _cardDto2.Active = false;
            List<CardDto> cardDtos = new List<CardDto> { _cardDto1, _cardDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(cardDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithEmptyListOfCardDto_ShouldReturnEmptyCardDtoList()
        {
            // Arrange
            List<CardDto> cardDtos = new List<CardDto> {};

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(cardDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithCardDtoActiveOnRemoveValueTrue_ShouldReturnNull()
        {
            //Arrange
            var entityUsed = _cardDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithCardDtoInactiveOnRemoveValueFalse_ShouldReturnNull()
        {
            //Arrange
            var entityUsed = _cardDto1;
            entityUsed.Active = false;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithCardDtoInactiveOnRemoveValueTrue_ShouldReturnCardDto()
        {
            //Arrange
            var entityUsed = _cardDto1;
            entityUsed.Active = false;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed, result);
        }
        [TestMethod]
        public void SortActiveByStatus_WhenCalledWithCardDtoActiveOnRemoveValueFalse_ShouldReturnCardDto()
        {
            //Arrange
            var entityUsed = _cardDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed, result);
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfTypesAllActiveOnRemoveValueFalse_ShouldReturnCardDtoWithFullListOfTypes()
        {
            //Arrange
            var entityUsed = _cardDto1;
            var relatedEntity1 = _typeDto1;
            var relatedEntity2 = _typeDto2;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(2, result.Types.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfTypesAllActiveOnRemoveValueTrue_ShouldReturnCardDtoWithEmptyListOfTypes()
        {
            //Arrange
            var entityUsed = _cardDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _typeDto1;
            var relatedEntity2 = _typeDto2;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(0, result.Types.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfTypesNoActiveOnRemoveValueFalse_ShouldReturnCardDtoWithEmptyListOfTypes()
        {
            //Arrange
            var entityUsed = _cardDto1;
            var relatedEntity1 = _typeDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeDto2;
            relatedEntity2.Active = false;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(0, result.Types.Count());
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfTypesDifferingActiveOnRemoveValueFalse_ShouldReturnCardDtoWithSortedListOfTypes()
        {
            //Arrange
            var entityUsed = _cardDto1;
            var relatedEntity1 = _typeDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeDto2;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(1, result.Types.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfTypesDifferingActiveOnRemoveValueTrue_ShouldReturnCardDtoWithSortedListOfTypes()
        {
            //Arrange
            var entityUsed = _cardDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _typeDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeDto2;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(1, result.Types.Count());
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfResultDtoWithAllActiveOnRemoveValueFalse_ShouldReturnCardDtoWithFullListOfResultDtos()
        {
            //Arrange
            var entityUsed = _cardDto1;
            var relatedEntity1 = _resultDto1;
            var relatedEntity2 = _resultDto2;
            entityUsed.Results = new List<ResultDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(2, result.Results.Count());
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfResultDtoWithAllActiveOnRemoveValueTrue_ShouldReturnCardDtoWithEmptyListOfResultDtos()
        {
            //Arrange
            var entityUsed = _cardDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _resultDto1;
            var relatedEntity2 = _resultDto2;
            entityUsed.Results = new List<ResultDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(0, result.Results.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfResultDtoWithNoActiveOnRemoveValueFalse_ShouldReturnCardDtoWithEmptyListOfResultDtos()
        {
            //Arrange
            var entityUsed = _cardDto1;
            var relatedEntity1 = _resultDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _resultDto2;
            relatedEntity2.Active = false;
            entityUsed.Results = new List<ResultDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(0, result.Results.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfResultDtoWithDifferingActiveOnRemoveValueTrue_ShouldReturnCardDtoWithSortedListOfResultDtos()
        {
            //Arrange
            var entityUsed = _cardDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _resultDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _resultDto2;
            entityUsed.Results = new List<ResultDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(1, result.Results.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithListOfResultDtoWithDifferingActiveOnRemoveValueFalse_ShouldReturnCardDtoWithSortedListOfResultDtos()
        {
            //Arrange
            var entityUsed = _cardDto1;
            var relatedEntity1 = _resultDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _resultDto2;
            entityUsed.Results = new List<ResultDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(1, result.Results.Count());
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithAttachmentDtoActiveOnRemoveValueFalse_ShouldReturnCardDtoWithAttachmentDto()
        {
            //Arrange
            var entityUsed = _cardDto1;
            var relatedEntity1 = _attachmentDto1;
            entityUsed.Attachment = relatedEntity1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.IsNotNull(result.Attachment);
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithAttachmentDtoActiveOnRemoveValueTrue_ShouldReturnCardDtoAttachmentNull()
        {
            //Arrange
            var entityUsed = _cardDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _attachmentDto1;
            entityUsed.Attachment = relatedEntity1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.IsNull(result.Attachment);
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithCardDtoWithAttachmentDtoInactiveOnRemoveValueFalse_ShouldReturnCardDtoAttachmentNull()
        {
            //Arrange
            var entityUsed = _cardDto1;
            var relatedEntity1 = _attachmentDto1;
            relatedEntity1.Active = false;
            entityUsed.Attachment = relatedEntity1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.IsNull(result.Attachment);
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfResultDtoWithAllActiveOnRemoveValueFalse_ShouldReturnFullResultDtoList()
        {
            // Arrange
            List<ResultDto> resultDtos = new List<ResultDto> { _resultDto1, _resultDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(resultDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfResultDtoWithAllActiveOnRemoveValueTrue_ShouldReturnEmptyResultDtoList()
        {
            // Arrange
            List<ResultDto> resultDtos = new List<ResultDto> { _resultDto1, _resultDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(resultDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfResultDtoWithNoActiveOnRemoveValueFalse_ShouldReturnEmptyResultDtoList()
        {
            // Arrange
            _resultDto1.Active = false;
            _resultDto2.Active = false;
            List<ResultDto> resultDtos = new List<ResultDto> { _resultDto1, _resultDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(resultDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfResultDtoWithNoActiveOnRemoveValueTrue_ShouldReturnFullResultDtoList()
        {
            // Arrange
            _resultDto1.Active = false;
            _resultDto2.Active = false;
            List<ResultDto> resultDtos = new List<ResultDto> { _resultDto1, _resultDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(resultDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithEmptyListOfResultDto_ShouldReturnEmptyResultDtoList()
        {
            // Arrange
            List<ResultDto> resultDtos = new List<ResultDto> { };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(resultDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoActiveOnRemoveValueTrue_ShouldReturnNull()
        {
            //Arrange
            var entityUsed = _resultDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoInactiveOnRemoveValueFalse_ShouldReturnNull()
        {
            //Arrange
            var entityUsed = _resultDto1;
            entityUsed.Active = false;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoInactiveOnRemoveValueTrue_ShouldReturnResultDto()
        {
            //Arrange
            var entityUsed = _resultDto1;
            entityUsed.Active = false;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed, result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoActiveOnRemoveValueFalse_ShouldReturnResultDto()
        {
            //Arrange
            var entityUsed = _resultDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed, result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoWithAttachmentDtoActiveOnRemoveValueFalse_ShouldReturnResultDtoWithAttachmentDto()
        {
            //Arrange
            var entityUsed = _resultDto1;
            var relatedEntity1 = _attachmentDto1;
            entityUsed.Attachment = relatedEntity1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
            Assert.IsNotNull(result.Attachment);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoWithAttachmentDtoActiveOnRemoveValueTrue_ShouldReturnResultDtoWithAttachmentNull()
        {
            //Arrange
            var entityUsed = _resultDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _attachmentDto1;
            entityUsed.Attachment = relatedEntity1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
            Assert.IsNull(result.Attachment);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoWithAttachmentDtoInactiveOnRemoveValueFalse_ShouldReturnResultDtoWithAttachmentNull()
        {
            //Arrange
            var entityUsed = _resultDto1;
            var relatedEntity1 = _attachmentDto1;
            relatedEntity1.Active = false;
            entityUsed.Attachment = relatedEntity1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
            Assert.IsNull(result.Attachment);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoWithListOfCardsAllActiveOnRemoveValueFalse_ShouldReturnResultDtoWithFullListOfCards()
        {
            //Arrange
            var entityUsed = _resultDto1;
            var relatedEntity1 = _cardDto1;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
            Assert.AreEqual(2, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoWithListOfCardsAllActiveOnRemoveValueTrue_ShouldReturnResultDtoWithEmptyListOfCards()
        {
            //Arrange
            var entityUsed = _resultDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _cardDto1;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
            Assert.AreEqual(0, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoWithListOfCardsDifferingActiveOnRemoveValueTrue_ShouldReturnResultDtoWithSortedListOfCards()
        {
            //Arrange
            var entityUsed = _resultDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _cardDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
            Assert.AreEqual(1, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithResultDtoWithListOfCardsDifferingActiveOnRemoveValueFalse_ShouldReturnResultDtoWithSortedListOfCards()
        {
            //Arrange
            var entityUsed = _resultDto1;
            var relatedEntity1 = _cardDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
            Assert.AreEqual(1, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeCategoryListDtoWithAllActiveOnRemoveValueFalse_ShouldReturnFullListOfTypeCategoryListDto()
        {
            //Arrange
            var entityUseed = new List<TypeCategoryListDto> { _typeCategoryListDto1, _typeCategoryListDto2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUseed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeCategoryListDtoWithAllActiveOnRemoveValueTrue_ShouldReturnEmptyListOfTypeCategoryListDto()
        {
            //Arrange
            var entityUseed = new List<TypeCategoryListDto> { _typeCategoryListDto1, _typeCategoryListDto2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUseed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeCategoryListDtoWithNoActiveOnRemoveValueFalse_ShouldReturnEmptyListOfTypeCategoryListDto()
        {
            //Arrange
            _typeCategoryListDto1.Active = false;
            _typeCategoryListDto2.Active = false;
            var entityUseed = new List<TypeCategoryListDto> { _typeCategoryListDto1, _typeCategoryListDto2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUseed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeCategoryListDtoWithNoActiveOnRemoveValueTrue_ShouldReturnFullListOfTypeCategoryListDto()
        {
            //Arrange
            _typeCategoryListDto1.Active = false;
            _typeCategoryListDto2.Active = false;
            var entityUseed = new List<TypeCategoryListDto> { _typeCategoryListDto1, _typeCategoryListDto2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUseed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeCategoryListDtoWithDifferingActiveOnRemoveValueFalse_ShouldReturnSortedListOfTypeCategoryListDto()
        {
            //Arrange
            _typeCategoryListDto1.Active = false;
            var entityUseed = new List<TypeCategoryListDto> { _typeCategoryListDto1, _typeCategoryListDto2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUseed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeCategoryListDtoWithDifferingActiveOnRemoveValueTrue_ShouldReturnSortedListOfTypeCategoryListDto()
        {
            //Arrange
            _typeCategoryListDto1.Active = false;
            var entityUseed = new List<TypeCategoryListDto> { _typeCategoryListDto1, _typeCategoryListDto2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUseed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithEmptyListOfTypeCategoryListDtoWithDifferingActiveOnRemoveValueTrue_ShouldReturnEmptyListOfTypeCategoryListDto()
        {
            //Arrange
            var entityUseed = new List<TypeCategoryListDto> {};
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUseed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoActiveOnRemoveValueTrue_ShouldReturnNull()
        {
            //Arrange
            var entityUsed = _typeCategoryListDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoActiveOnRemoveValueFalse_ShouldReturnTypeCategoryListDto()
        {
            //Arrange
            var entityUsed = _typeCategoryListDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entityUsed, result);
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoInactiveOnRemoveValueFalse_ShouldReturnNull()
        {
            //Arrange
            _typeCategoryListDto1.Active = false;
            var entityUsed = _typeCategoryListDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoInactiveOnRemoveValueTrue_ShouldReturnTypeCategoryListDto()
        {
            //Arrange
            _typeCategoryListDto1.Active = false;
            var entityUsed = _typeCategoryListDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entityUsed, result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoWithListOfTypesAllActiveOnRemoveValueFalse_ShouldReturnTypeCategoryListDtoWithFullListOfTypes()
        {
            //Arrange
            var entityUsed = _typeCategoryListDto1;
            var relatedEntity1 = _typeDto1;
            var relatedEntity2 = _typeDto2;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);
            Assert.AreEqual(2, result.Types.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoWithListOfTypesAllActiveOnRemoveValueTrue_ShouldReturnTypeCategoryListDtoWithEmptyistOfTypes()
        {
            //Arrange
            var entityUsed = _typeCategoryListDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _typeDto1;
            var relatedEntity2 = _typeDto2;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);
            Assert.AreEqual(0, result.Types.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoWithListOfTypesDifferingActiveOnRemoveValueFalse_ShouldReturnTypeCategoryListDtoWithSortedListOfTypes()
        {
            //Arrange
            var entityUsed = _typeCategoryListDto1;
            var relatedEntity1 = _typeDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeDto2;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);
            Assert.AreEqual(1, result.Types.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoWithListOfTypesDifferingActiveOnRemoveValueTrue_ShouldReturnTypeCategoryListDtoWithSortedListOfTypes()
        {
            //Arrange
            var entityUsed = _typeCategoryListDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _typeDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeDto2;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);
            Assert.AreEqual(1, result.Types.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoWithListOfTypesNoActiveOnRemoveValueFalse_ShouldReturnTypeCategoryListDtoWithEmptyListOfTypes()
        {
            //Arrange
            var entityUsed = _typeCategoryListDto1;
            var relatedEntity1 = _typeDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeDto2;
            relatedEntity2.Active = false;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);
            Assert.AreEqual(0, result.Types.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeCategoryListDtoWithListOfTypesNoActiveOnRemoveValueTrue_ShouldReturnTypeCategoryListDtoWithFullListOfTypes()
        {
            //Arrange
            var entityUsed = _typeCategoryListDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _typeDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeDto2;
            relatedEntity2.Active = false;
            entityUsed.Types = new List<TypeDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);
            Assert.AreEqual(2, result.Types.Count());
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeDtoWithAllActiveOnRemoveValueFalse_ShouldReturnFullTypeDtoList()
        {
            // Arrange
            List<TypeDto> typeDtos = new List<TypeDto> { _typeDto1, _typeDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(typeDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeDtoWithAllActiveOnRemoveValueTrue_ShouldReturnEmptyTypeDtoList()
        {
            // Arrange
            List<TypeDto> typeDtos = new List<TypeDto> { _typeDto1, _typeDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(typeDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeDtoWithDifferingActiveOnRemoveValueFalse_ShouldReturnSortedTypeDtoList()
        {
            // Arrange
            _typeDto1.Active = false;
            List<TypeDto> typeDtos = new List<TypeDto> { _typeDto1, _typeDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(typeDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithListOfTypeDtoWithDifferingActiveOnRemoveValueTrue_ShouldReturnSortedTypeDtoList()
        {
            // Arrange
            _typeDto1.Active = false;
            List<TypeDto> typeDtos = new List<TypeDto> { _typeDto1, _typeDto2 };

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(typeDtos, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithEmptyListOfTypeDto_ShouldReturnÉmptyTypeDtoList()
        {
            // Arrange
            List<TypeDto> typeDtos = new List<TypeDto> {};

            // Act
            var result = _activitySortingUtility.SortActiveByStatus(typeDtos, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoActiveOnRemoveValueTrue_ShouldReturnNull()
        {
            //Arrange
            var entityUsed = _typeDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoActiveOnRemoveValueFalse_ShouldReturnTypeDto()
        {
            //Arrange
            var entityUsed = _typeDto1;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed, result);
        }

        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoInactiveOnRemoveValuefalse_ShouldReturnNull()
        {
            //Arrange
            var entityUsed = _typeDto1;
            entityUsed.Active = false;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoInactiveOnRemoveValueTrue_ShouldReturnTypeDto()
        {
            //Arrange
            var entityUsed = _typeDto1;
            entityUsed.Active = false;
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed, result);
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfCardsAllActiveOnRemoveValueFalse_ShouldReturnTypeDtoWithFullListOfCards()
        {
            //Arrange
            var entityUsed = _typeDto1;
            var relatedEntity1 = _cardDto1;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(2, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfCardsAllActiveOnRemoveValueTrue_ShouldReturnTypeDtoWithEmptyListOfCards()
        {
            //Arrange
            var entityUsed = _typeDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _cardDto1;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(0, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfCardsDifferingActiveOnRemoveValueFalse_ShouldReturnTypeDtoWithSortedListOfCards()
        {
            //Arrange
            var entityUsed = _typeDto1;
            var relatedEntity1 = _cardDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(1, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfCardsDifferingActiveOnRemoveValueTrue_ShouldReturnTypeDtoWithSortedListOfCards()
        {
            //Arrange
            var entityUsed = _typeDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _cardDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _cardDto2;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(1, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfCardsNoActiveOnRemoveValueFalse_ShouldReturnTypeDtoWithEmptyListOfCards()
        {
            //Arrange
            var entityUsed = _typeDto1;
            var relatedEntity1 = _cardDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _cardDto2;
            relatedEntity2.Active = false;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(0, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfCardsNoActiveOnRemoveValueTrue_ShouldReturnTypeDtoWithFullListOfCards()
        {
            //Arrange
            var entityUsed = _typeDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _cardDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _cardDto2;
            relatedEntity2.Active = false;
            entityUsed.Cards = new List<CardDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(2, result.Cards.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfTypeCategoryListDtoAllActiveOnRemoveValueFalse_ShouldReturnTypeDtoWithFullListOfTypeCategoryListDto()
        {
            //Arrange
            var entityUsed = _typeDto1;
            var relatedEntity1 = _typeCategoryListDto1;
            var relatedEntity2 = _typeCategoryListDto2;
            entityUsed.TypeCategoryLists = new List<TypeCategoryListDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(2, result.TypeCategoryLists.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfTypeCategoryListDtoAllActiveOnRemoveValueTrue_ShouldReturnTypeDtoWithEmptyListOfTypeCategoryListDto()
        {
            //Arrange
            var entityUsed = _typeDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _typeCategoryListDto1;
            var relatedEntity2 = _typeCategoryListDto2;
            entityUsed.TypeCategoryLists = new List<TypeCategoryListDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(0, result.TypeCategoryLists.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfTypeCategoryListDtoDifferingActiveOnRemoveValueFalse_ShouldReturnTypeDtoWithSortedListOfTypeCategoryListDto()
        {
            //Arrange
            var entityUsed = _typeDto1;
            var relatedEntity1 = _typeCategoryListDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeCategoryListDto2;
            entityUsed.TypeCategoryLists = new List<TypeCategoryListDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(1, result.TypeCategoryLists.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfTypeCategoryListDtoDifferingActiveOnRemoveValueTrue_ShouldReturnTypeDtoWithSortedListOfTypeCategoryListDto()
        {
            //Arrange
            var entityUsed = _typeDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _typeCategoryListDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeCategoryListDto2;
            entityUsed.TypeCategoryLists = new List<TypeCategoryListDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(1, result.TypeCategoryLists.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfTypeCategoryListDtoNoActiveOnRemoveValueFalse_ShouldReturnTypeDtoWithEmptyListOfTypeCategoryListDto()
        {
            //Arrange
            var entityUsed = _typeDto1;
            var relatedEntity1 = _typeCategoryListDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeCategoryListDto2;
            relatedEntity2.Active = false;
            entityUsed.TypeCategoryLists = new List<TypeCategoryListDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, false);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(0, result.TypeCategoryLists.Count());
        }
        [TestMethod]
        public void SortByActiveStatus_WhenCalledWithTypeDtoWithListOfTypeCategoryListDtoNoActiveOnRemoveValueTrue_ShouldReturnTypeDtoWithFullListOfTypeCategoryListDto()
        {
            //Arrange
            var entityUsed = _typeDto1;
            entityUsed.Active = false;
            var relatedEntity1 = _typeCategoryListDto1;
            relatedEntity1.Active = false;
            var relatedEntity2 = _typeCategoryListDto2;
            relatedEntity2.Active = false;
            entityUsed.TypeCategoryLists = new List<TypeCategoryListDto> { relatedEntity1, relatedEntity2 };
            //Act
            var result = _activitySortingUtility.SortActiveByStatus(entityUsed, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(2, result.TypeCategoryLists.Count());
        }







    }
}
