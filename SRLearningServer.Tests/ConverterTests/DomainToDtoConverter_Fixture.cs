using SRLearningServer.Components.Converters;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLearningServer.Tests.ConverterTests
{
    [TestClass]
    [TestCategory("UnitTest")]
    public class DomainToDtoConverter_Fixture
    {
        private readonly DomainToDtoConverter _domainToDtoConverter;
        private readonly TestDataGenerator _testDataGenerator;


        private Attachment _attachment1;
        private Attachment _attachment2;

        private Card _card1;
        private Card _card2;

        private Result _result1;
        private Result _result2;

        private Components.Models.Type _type1;
        private Components.Models.Type _type2;

        private TypeCategoryList _typeCategoryList1;
        private TypeCategoryList _typeCategoryList2;



        public DomainToDtoConverter_Fixture()
        {
            _domainToDtoConverter = new();
            _testDataGenerator = new TestDataGenerator();
        }

        [TestInitialize]
        public void Initialize()
        {
            _attachment1 = _testDataGenerator.CreateAttachment(1, "Attachment1", "url1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _attachment2 = _testDataGenerator.CreateAttachment(2, "Attachment2", "url2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _card1 = _testDataGenerator.CreateCard(1, "Card1", "text1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _card2 = _testDataGenerator.CreateCard(2, "Card2", "text2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _result1 = _testDataGenerator.CreateResult(1, "Result1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _result2 = _testDataGenerator.CreateResult(2, "Result2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _type1 = _testDataGenerator.CreateType(1, "Type1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _type2 = _testDataGenerator.CreateType(2, "Type2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _typeCategoryList1 = _testDataGenerator.CreateTypeCategoryList(1, "TypeCategoryList1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeCategoryList2 = _testDataGenerator.CreateTypeCategoryList(2, "TypeCategoryList2", DateOnly.FromDateTime(DateTime.UtcNow), true);

        }

        [TestMethod]
        public void DomainToDtoConverter_EmptyIEnumerableOfAttachment_ReturnsNull()
        {
            //Arrange
            var entity = new List<Attachment>();

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_IEnumerableOfAttachmentWithRelations_ReturnsIEnumerableOfAttachmentDtoWithEmptyRelations()
        {
            //Arrange
            var attachment1 = _attachment1;
            attachment1.Cards.Add(_card1);
            attachment1.Results.Add(_result1);

            var attachment2 = _attachment2;
            attachment2.Cards.Add(_card2);
            attachment2.Results.Add(_result2);
            var entity = new List<Attachment> { attachment1, attachment2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entity.Count, result.Count());
            Assert.AreEqual(entity[0].AttachmentId, result.ElementAt(0).AttachmentId);
            Assert.AreEqual(entity[1].AttachmentId, result.ElementAt(1).AttachmentId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Results);
            Assert.AreEqual(0, result.ElementAt(0).Results.Count());
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(0, result.ElementAt(0).Cards.Count());

            Assert.IsNotNull(result.ElementAt(1).Results);
            Assert.AreEqual(0, result.ElementAt(1).Results.Count());
            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(0, result.ElementAt(1).Cards.Count());

        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfAttachmentWithRelations_ReturnsIEnumerableOfAttachmentDtoWithRelations()
        {
            //Arrange
            var attachment1 = _attachment1;
            attachment1.Cards.Add(_card1);
            attachment1.Results.Add(_result1);

            var attachment2 = _attachment2;
            attachment2.Cards.Add(_card2);
            attachment2.Results.Add(_result2);
            var entity = new List<Attachment> { attachment1, attachment2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entity.Count, result.Count());
            Assert.AreEqual(entity[0].AttachmentId, result.ElementAt(0).AttachmentId);
            Assert.AreEqual(entity[1].AttachmentId, result.ElementAt(1).AttachmentId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Results);
            Assert.AreEqual(1, result.ElementAt(0).Results.Count());
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(1, result.ElementAt(0).Cards.Count());

            Assert.IsNotNull(result.ElementAt(1).Results);
            Assert.AreEqual(1, result.ElementAt(1).Results.Count());
            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(1, result.ElementAt(1).Cards.Count());

        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfAttachmentWithoutRelations_ReturnsIEnumerableOfAttachmentDtoWithoutRelations()
        {
            //Arrange
            var attachment1 = _attachment1;

            var attachment2 = _attachment2;
            var entity = new List<Attachment> { attachment1, attachment2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entity.Count, result.Count());
            Assert.AreEqual(entity[0].AttachmentId, result.ElementAt(0).AttachmentId);
            Assert.AreEqual(entity[1].AttachmentId, result.ElementAt(1).AttachmentId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Results);
            Assert.AreEqual(0, result.ElementAt(0).Results.Count());
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(0, result.ElementAt(0).Cards.Count());

            Assert.IsNotNull(result.ElementAt(1).Results);
            Assert.AreEqual(0, result.ElementAt(1).Results.Count());
            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(0, result.ElementAt(1).Cards.Count());

        }

        [TestMethod]
        public void DomainToDtoConverter_IEnumerableOfAttachment_ReturnsIEnumerableOfAttachmentDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _attachment1;

            var entity2 = _attachment2;
            var entities = new List<Attachment> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            var checkingResult = result.ElementAt(0);
            var checkingEntity = entities[0];
            Assert.AreEqual(checkingEntity.AttachmentId, checkingResult.AttachmentId);
            Assert.AreEqual(checkingEntity.AttachmentName, checkingResult.AttachmentName);
            Assert.AreEqual(checkingEntity.AttachmentUrl, checkingResult.AttachmentUrl);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);


            checkingResult = result.ElementAt(1);
            checkingEntity = entities[1];
            Assert.AreEqual(checkingEntity.AttachmentId, checkingResult.AttachmentId);
            Assert.AreEqual(checkingEntity.AttachmentName, checkingResult.AttachmentName);
            Assert.AreEqual(checkingEntity.AttachmentUrl, checkingResult.AttachmentUrl);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);


        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DomainToDtoConverter_NullAttachment_ReturnsNull()
        {
            //Arrange
            Attachment entity = null;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_AttachmentWithRelations_ReturnsAttachmentDtoWithEmptyRelations()
        {
            //Arrange
            var entity = _attachment1;
            entity.Cards.Add(_card1);
            entity.Results.Add(_result1);


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(0, result.Results.Count());
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(0, result.Cards.Count());


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_AttachmentWithRelations_ReturnsAttachmentDtoWithRelations()
        {
            //Arrange
            var entity = _attachment1;
            entity.Cards.Add(_card1);
            entity.Results.Add(_result1);


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(1, result.Results.Count());
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(1, result.Cards.Count());


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_AttachmentWithoutRelations_ReturnsAttachmentDtoWithoutRelations()
        {
            //Arrange
            var entity = _attachment1;


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(0, result.Results.Count());
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(0, result.Cards.Count());

        }

        [TestMethod]
        public void DomainToDtoConverter_Attachment_ReturnsAttachmentDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _attachment1;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);
            Assert.AreEqual(entity.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entity.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DomainToDtoConverter_EmptyIEnumerableOfCard_ReturnsNull()
        {
            //Arrange
            var entity = new List<Card>();

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_IEnumerableOfCardWithRelations_ReturnsIEnumerableOfCardDtoWithEmptyRelations()
        {
            //Arrange
            var card1 = _card1;
            card1.Types.Add(_type1);
            card1.Results.Add(_result1);
            card1.Attachment = _attachment1;

            var card2 = _card2;
            card2.Types.Add(_type1);
            card2.Results.Add(_result2);
            card2.Attachment = _attachment2;
            var entity = new List<Card> { card1, card2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entity.Count, result.Count());
            Assert.AreEqual(entity[0].CardId, result.ElementAt(0).CardId);
            Assert.AreEqual(entity[1].CardId, result.ElementAt(1).CardId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Results);
            Assert.AreEqual(0, result.ElementAt(0).Results.Count());
            Assert.IsNotNull(result.ElementAt(0).Types);
            Assert.AreEqual(0, result.ElementAt(0).Types.Count());
            Assert.IsNull(result.ElementAt(0).Attachment);

            Assert.IsNotNull(result.ElementAt(1).Results);
            Assert.AreEqual(0, result.ElementAt(1).Results.Count());
            Assert.IsNotNull(result.ElementAt(1).Types);
            Assert.AreEqual(0, result.ElementAt(1).Types.Count());
            Assert.IsNull(result.ElementAt(1).Attachment);


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfCardWithRelations_ReturnsIEnumerableOfCardDtoWithRelations()
        {
            //Arrange
            var card1 = _card1;
            card1.Types.Add(_type1);
            card1.Results.Add(_result1);
            card1.Attachment = _attachment1;

            var card2 = _card2;
            card2.Types.Add(_type2);
            card2.Results.Add(_result2);
            card2.Attachment = _attachment2;
            var entity = new List<Card> { card1, card2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entity.Count, result.Count());
            Assert.AreEqual(entity[0].CardId, result.ElementAt(0).CardId);
            Assert.AreEqual(entity[1].CardId, result.ElementAt(1).CardId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Results);
            Assert.AreEqual(1, result.ElementAt(0).Results.Count());
            Assert.IsNotNull(result.ElementAt(0).Types);
            Assert.AreEqual(1, result.ElementAt(0).Types.Count());
            Assert.IsNotNull(result.ElementAt(0).Attachment);

            Assert.IsNotNull(result.ElementAt(1).Results);
            Assert.AreEqual(1, result.ElementAt(1).Results.Count());
            Assert.IsNotNull(result.ElementAt(1).Types);
            Assert.AreEqual(1, result.ElementAt(1).Types.Count());
            Assert.IsNotNull(result.ElementAt(1).Attachment);

        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfCardWithoutRelations_ReturnsIEnumerableOfCardDtoWithoutRelations()
        {
            //Arrange
            var card1 = _card1;

            var card2 = _card2;
            var entity = new List<Card> { card1, card2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entity.Count, result.Count());
            Assert.AreEqual(entity[0].CardId, result.ElementAt(0).CardId);
            Assert.AreEqual(entity[1].CardId, result.ElementAt(1).CardId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Results);
            Assert.AreEqual(0, result.ElementAt(0).Results.Count());
            Assert.IsNotNull(result.ElementAt(0).Types);
            Assert.AreEqual(0, result.ElementAt(0).Types.Count());
            Assert.IsNull(result.ElementAt(0).Attachment);

            Assert.IsNotNull(result.ElementAt(1).Results);
            Assert.AreEqual(0, result.ElementAt(1).Results.Count());
            Assert.IsNotNull(result.ElementAt(1).Types);
            Assert.AreEqual(0, result.ElementAt(1).Types.Count());
            Assert.IsNull(result.ElementAt(1).Attachment);

        }

        [TestMethod]
        public void DomainToDtoConverter_IEnumerableOfCard_ReturnsIEnumerableOfCardDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _card1;

            var entity2 = _card2;
            var entities = new List<Card> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            var checkingResult = result.ElementAt(0);
            var checkingEntity = entities[0];
            Assert.AreEqual(checkingEntity.CardId, checkingResult.CardId);
            Assert.AreEqual(checkingEntity.CardName, checkingResult.CardName);
            Assert.AreEqual(checkingEntity.CardText, checkingResult.CardText);
            Assert.AreEqual(checkingEntity.AttachmentId, checkingResult.AttachmentId);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);


            checkingResult = result.ElementAt(1);
            checkingEntity = entities[1];
            Assert.AreEqual(checkingEntity.CardId, checkingResult.CardId);
            Assert.AreEqual(checkingEntity.CardName, checkingResult.CardName);
            Assert.AreEqual(checkingEntity.CardText, checkingResult.CardText);
            Assert.AreEqual(checkingEntity.AttachmentId, checkingResult.AttachmentId);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);


        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DomainToDtoConverter_NullCard_ReturnsNull()
        {
            //Arrange
            Card entity = null;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_CardWithRelations_ReturnsCardDtoWithEmptyRelations()
        {
            //Arrange
            var entity = _card1;
            entity.Types.Add(_type1);
            entity.Results.Add(_result1);
            entity.Attachment = _attachment1;



            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(0, result.Results.Count());
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(0, result.Types.Count());
            Assert.IsNull(result.Attachment);


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_CardWithRelations_ReturnsCardDtoWithRelations()
        {
            //Arrange
            var entity = _card1;
            entity.Types.Add(_type1);
            entity.Results.Add(_result1);
            entity.Attachment = _attachment1;


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entity.CardId, result.CardId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(1, result.Results.Count());
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(1, result.Types.Count());
            Assert.IsNotNull(result.Attachment);


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_CardWithoutRelations_ReturnsCardDtoWithoutRelations()
        {
            //Arrange
            var entity = _card1;


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entity.CardId, result.CardId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(0, result.Results.Count());
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(0, result.Types.Count());
            Assert.IsNull(result.Attachment);

        }
        [TestMethod]
        public void DomainToDtoConverter_Card_ReturnsCardDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _card1;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CardDto));
            Assert.AreEqual(entity.CardId, result.CardId);
            Assert.AreEqual(entity.CardName, result.CardName);
            Assert.AreEqual(entity.CardText, result.CardText);
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DomainToDtoConverter_EmptyIEnumerableOfResult_ReturnsNull()
        {
            //Arrange
            var entity = new List<Result>();

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_IEnumerableOfResultWithRelations_ReturnsIEnumerableOfResultDtoWithEmptyRelations()
        {
            //Arrange
            var entity1 = _result1;
            entity1.Cards.Add(_card1);
            entity1.Attachment = _attachment1;

            var entity2 = _result2;
            entity2.Cards.Add(_card1);
            entity2.Attachment = _attachment2;
            var entities = new List<Result> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].ResultId, result.ElementAt(0).ResultId);
            Assert.AreEqual(entities[1].ResultId, result.ElementAt(1).ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(0, result.ElementAt(0).Cards.Count());
            Assert.IsNull(result.ElementAt(0).Attachment);

            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(0, result.ElementAt(1).Cards.Count());
            Assert.IsNull(result.ElementAt(1).Attachment);


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfResultWithRelations_ReturnsIEnumerableOfResultDtoWithRelations()
        {
            //Arrange
            var entity1 = _result1;
            entity1.Cards.Add(_card1);
            entity1.Attachment = _attachment1;

            var entity2 = _result2;
            entity2.Cards.Add(_card1);
            entity2.Attachment = _attachment2;
            var entities = new List<Result> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].ResultId, result.ElementAt(0).ResultId);
            Assert.AreEqual(entities[1].ResultId, result.ElementAt(1).ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(1, result.ElementAt(0).Cards.Count());
            Assert.IsNotNull(result.ElementAt(0).Attachment);

            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(1, result.ElementAt(1).Cards.Count());
            Assert.IsNotNull(result.ElementAt(1).Attachment);

        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfResultWithoutRelations_ReturnsIEnumerableOfResultDtoWithoutRelations()
        {
            //Arrange
            var entity1 = _result1;

            var entity2 = _result2;
            var entities = new List<Result> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].ResultId, result.ElementAt(0).ResultId);
            Assert.AreEqual(entities[1].ResultId, result.ElementAt(1).ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(0, result.ElementAt(0).Cards.Count());
            Assert.IsNull(result.ElementAt(0).Attachment);

            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(0, result.ElementAt(1).Cards.Count());
            Assert.IsNull(result.ElementAt(1).Attachment);

        }

        [TestMethod]
        public void DomainToDtoConverter_IEnumerableOfResult_ReturnsIEnumerableOfResultDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _result1;

            var entity2 = _result2;
            var entities = new List<Result> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            var checkingResult = result.ElementAt(0);
            var checkingEntity = entities[0];
            Assert.AreEqual(checkingEntity.ResultId, checkingResult.ResultId);
            Assert.AreEqual(checkingEntity.ResultText, checkingResult.ResultText);
            Assert.AreEqual(checkingEntity.AttachmentId, checkingResult.AttachmentId);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);


            checkingResult = result.ElementAt(1);
            checkingEntity = entities[1];
            Assert.AreEqual(checkingEntity.ResultId, checkingResult.ResultId);
            Assert.AreEqual(checkingEntity.ResultText, checkingResult.ResultText);
            Assert.AreEqual(checkingEntity.AttachmentId, checkingResult.AttachmentId);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);
        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DomainToDtoConverter_NullResult_ReturnsNull()
        {
            //Arrange
            Result entity = null;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_ResultWithRelations_ReturnsResultDtoWithEmptyRelations()
        {
            //Arrange
            var entity = _result1;
            entity.Cards.Add(_card1);
            entity.Attachment = _attachment1;



            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entity.ResultId, result.ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(0, result.Cards.Count());
            Assert.IsNull(result.Attachment);


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_ResultWithRelations_ReturnsResultDtoWithRelations()
        {
            //Arrange
            var entity = _result1;
            entity.Cards.Add(_card1);
            entity.Attachment = _attachment1;


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entity.ResultId, result.ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(1, result.Cards.Count());
            Assert.IsNotNull(result.Attachment);


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_ResultWithoutRelations_ReturnsResultDtoWithoutRelations()
        {
            //Arrange
            var entity = _result1;


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entity.ResultId, result.ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(0, result.Cards.Count());
            Assert.IsNull(result.Attachment);

        }

        [TestMethod]
        public void DomainToDtoConverter_Result_ReturnsResultDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _result1;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entity.ResultId, result.ResultId);
            Assert.AreEqual(entity.ResultText, result.ResultText);
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }
        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DomainToDtoConverter_EmptyIEnumerableOfType_ReturnsNull()
        {
            //Arrange
            var entity = new List<Components.Models.Type>();

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_IEnumerableOfTypeWithRelations_ReturnsIEnumerableOfTypeDtoWithEmptyRelations()
        {
            //Arrange
            var entity1 = _type1;
            entity1.Cards.Add(_card1);
            entity1.TypeCategoryLists.Add(_typeCategoryList1);

            var entity2 = _type2;
            entity2.Cards.Add(_card1);
            entity2.TypeCategoryLists.Add(_typeCategoryList2);
            var entities = new List<Components.Models.Type> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].TypeId, result.ElementAt(0).TypeId);
            Assert.AreEqual(entities[1].TypeId, result.ElementAt(1).TypeId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(0, result.ElementAt(0).Cards.Count());
            Assert.IsNotNull(result.ElementAt(0).TypeCategoryLists);
            Assert.AreEqual(0, result.ElementAt(0).TypeCategoryLists.Count());

            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(0, result.ElementAt(1).Cards.Count());
            Assert.IsNotNull(result.ElementAt(1).TypeCategoryLists);
            Assert.AreEqual(0, result.ElementAt(1).TypeCategoryLists.Count());


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfTypeWithRelations_ReturnsIEnumerableOfTypeDtoWithRelations()
        {
            //Arrange
            var entity1 = _type1;
            entity1.Cards.Add(_card1);
            entity1.TypeCategoryLists.Add(_typeCategoryList1);

            var entity2 = _type2;
            entity2.Cards.Add(_card1);
            entity2.TypeCategoryLists.Add(_typeCategoryList1);
            var entities = new List<Components.Models.Type> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].TypeId, result.ElementAt(0).TypeId);
            Assert.AreEqual(entities[1].TypeId, result.ElementAt(1).TypeId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(1, result.ElementAt(0).Cards.Count());
            Assert.IsNotNull(result.ElementAt(0).TypeCategoryLists);
            Assert.AreEqual(1, result.ElementAt(0).TypeCategoryLists.Count());

            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(1, result.ElementAt(1).Cards.Count());
            Assert.IsNotNull(result.ElementAt(1).TypeCategoryLists);
            Assert.AreEqual(1, result.ElementAt(1).TypeCategoryLists.Count());

        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfTypeWithoutRelations_ReturnsIEnumerableOfTypeDtoWithoutRelations()
        {
            //Arrange
            var entity1 = _type1;

            var entity2 = _type2;
            var entities = new List<Components.Models.Type> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].TypeId, result.ElementAt(0).TypeId);
            Assert.AreEqual(entities[1].TypeId, result.ElementAt(1).TypeId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(0, result.ElementAt(0).Cards.Count());
            Assert.IsNotNull(result.ElementAt(0).TypeCategoryLists);
            Assert.AreEqual(0, result.ElementAt(0).TypeCategoryLists.Count());

            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(0, result.ElementAt(1).Cards.Count());
            Assert.IsNotNull(result.ElementAt(1).TypeCategoryLists);
            Assert.AreEqual(0, result.ElementAt(1).TypeCategoryLists.Count());

        }

        [TestMethod]
        public void DomainToDtoConverter_IEnumerableOfType_ReturnsIEnumerableOfTypeDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _type1;

            var entity2 = _type2;
            var entities = new List<Components.Models.Type> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            var checkingResult = result.ElementAt(0);
            var checkingEntity = entities[0];
            Assert.AreEqual(checkingEntity.TypeId, checkingResult.TypeId);
            Assert.AreEqual(checkingEntity.CardTypeName, checkingResult.CardTypeName);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);


            checkingResult = result.ElementAt(1);
            checkingEntity = entities[1];
            Assert.AreEqual(checkingEntity.TypeId, checkingResult.TypeId);
            Assert.AreEqual(checkingEntity.CardTypeName, checkingResult.CardTypeName);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);
        }


        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DomainToDtoConverter_NullType_ReturnsNull()
        {
            //Arrange
            Components.Models.Type entity = null;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_TypeWithRelations_ReturnsTypeDtoWithEmptyRelations()
        {
            //Arrange
            var entity = _type1;
            entity.Cards.Add(_card1);
            entity.TypeCategoryLists.Add(_typeCategoryList1);



            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entity.TypeId, result.TypeId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(0, result.Cards.Count());
            Assert.IsNotNull(result.TypeCategoryLists);
            Assert.AreEqual(0, result.TypeCategoryLists.Count());


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_TypeWithRelations_ReturnsTypeDtoWithRelations()
        {
            //Arrange
            var entity = _type1;
            entity.Cards.Add(_card1);
            entity.TypeCategoryLists.Add(_typeCategoryList1);


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entity.TypeId, result.TypeId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(1, result.Cards.Count());
            Assert.IsNotNull(result.TypeCategoryLists);
            Assert.AreEqual(1, result.TypeCategoryLists.Count());


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_TypeWithoutRelations_ReturnsTypeDtoWithoutRelations()
        {
            //Arrange
            var entity = _type1;


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entity.TypeId, result.TypeId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(0, result.Cards.Count());
            Assert.IsNotNull(result.TypeCategoryLists);
            Assert.AreEqual(0, result.TypeCategoryLists.Count());

        }

        [TestMethod]
        public void DomainToDtoConverter_Type_ReturnsTypeDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _type1;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(entity.TypeId, result.TypeId);
            Assert.AreEqual(entity.CardTypeName, result.CardTypeName);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DomainToDtoConverter_EmptyIEnumerableOfTypeCategoryList_ReturnsNull()
        {
            //Arrange
            var entity = new List<TypeCategoryList>();

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_IEnumerableOfTypeCategoryListWithRelations_ReturnsIEnumerableOfTypeCategoryListDtoWithEmptyRelations()
        {
            //Arrange
            var entity1 = _typeCategoryList1;
            entity1.Types.Add(_type1);

            var entity2 = _typeCategoryList2;
            entity2.Types.Add(_type1);
            var entities = new List<TypeCategoryList> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].TypeCategoryListId, result.ElementAt(0).TypeCategoryListId);
            Assert.AreEqual(entities[1].TypeCategoryListId, result.ElementAt(1).TypeCategoryListId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Types);
            Assert.AreEqual(0, result.ElementAt(0).Types.Count());

            Assert.IsNotNull(result.ElementAt(1).Types);
            Assert.AreEqual(0, result.ElementAt(1).Types.Count());

        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfTypeCategoryListWithRelations_ReturnsIEnumerableOfTypeCategoryListDtoWithRelations()
        {
            //Arrange
            var entity1 = _typeCategoryList1;
            entity1.Types.Add(_type1);

            var entity2 = _typeCategoryList2;
            entity2.Types.Add(_type1);
            var entities = new List<TypeCategoryList> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].TypeCategoryListId, result.ElementAt(0).TypeCategoryListId);
            Assert.AreEqual(entities[1].TypeCategoryListId, result.ElementAt(1).TypeCategoryListId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Types);
            Assert.AreEqual(1, result.ElementAt(0).Types.Count());

            Assert.IsNotNull(result.ElementAt(1).Types);
            Assert.AreEqual(1, result.ElementAt(1).Types.Count());

        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_IEnumerableOfTypeCategoryListWithoutRelations_ReturnsIEnumerableOfTypeCategoryListDtoWithoutRelations()
        {
            //Arrange
            var entity1 = _typeCategoryList1;

            var entity2 = _typeCategoryList2;
            var entities = new List<TypeCategoryList> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].TypeCategoryListId, result.ElementAt(0).TypeCategoryListId);
            Assert.AreEqual(entities[1].TypeCategoryListId, result.ElementAt(1).TypeCategoryListId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Types);
            Assert.AreEqual(0, result.ElementAt(0).Types.Count());

            Assert.IsNotNull(result.ElementAt(1).Types);
            Assert.AreEqual(0, result.ElementAt(1).Types.Count());

        }

        [TestMethod]
        public void DomainToDtoConverter_IEnumerableOfTypeCategoryList_ReturnsIEnumerableOfTypeCategoryListDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _typeCategoryList1;

            var entity2 = _typeCategoryList2;
            var entities = new List<TypeCategoryList> { entity1, entity2 };

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            var checkingResult = result.ElementAt(0);
            var checkingEntity = entities[0];
            Assert.AreEqual(checkingEntity.TypeCategoryListId, checkingResult.TypeCategoryListId);
            Assert.AreEqual(checkingEntity.TypeCategoryListName, checkingResult.TypeCategoryListName);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);


            checkingResult = result.ElementAt(1);
            checkingEntity = entities[1];
            Assert.AreEqual(checkingEntity.TypeCategoryListId, checkingResult.TypeCategoryListId);
            Assert.AreEqual(checkingEntity.TypeCategoryListName, checkingResult.TypeCategoryListName);
            Assert.AreEqual(checkingEntity.LastUpdated, checkingResult.LastUpdated);
            Assert.AreEqual(checkingEntity.Active, checkingResult.Active);
        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DomainToDtoConverter_NullTypeCategoryList_ReturnsNull()
        {
            //Arrange
            TypeCategoryList entity = null;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsDefault_TypeCategoryListWithRelations_ReturnsTypeCategoryListDtoWithEmptyRelations()
        {
            //Arrange
            var entity = _typeCategoryList1;
            entity.Types.Add(_type1);



            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entity.TypeCategoryListId, result.TypeCategoryListId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(0, result.Types.Count());


        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_TypeCategoryListWithRelations_ReturnsTypeCategoryListDtoWithRelations()
        {
            //Arrange
            var entity = _typeCategoryList1;
            entity.Types.Add(_type1);


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entity.TypeCategoryListId, result.TypeCategoryListId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(1, result.Types.Count());

        }

        [TestMethod]
        public void DomainToDtoConverterWithConvertRelationsTrue_TypeCategoryListWithoutRelations_ReturnsTypeCategoryListDtoWithoutRelations()
        {
            //Arrange
            var entity = _typeCategoryList1;


            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity, true);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entity.TypeCategoryListId, result.TypeCategoryListId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(0, result.Types.Count());

        }



        [TestMethod]
        public void DomainToDtoConverter_TypeCategoryList_ReturnsTypeCategoryListDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _typeCategoryList1;

            //Act
            var result = _domainToDtoConverter.ConvertToDtoFromDomain(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(entity.TypeCategoryListId, result.TypeCategoryListId);
            Assert.AreEqual(entity.TypeCategoryListName, result.TypeCategoryListName);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }
    }
}
