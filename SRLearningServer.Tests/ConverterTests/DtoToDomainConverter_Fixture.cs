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
    public class DtoToDomainConverter_Fixture
    {
        private readonly DtoToDomainConverter _dtoToDomainConverter;
        private readonly TestDataGenerator _testDataGenerator;

        private AttachmentDto _attachment1;
        private AttachmentDto _attachment2;

        private CardDto _card1;
        private CardDto _card2;

        private ResultDto _result1;
        private ResultDto _result2;

        private TypeDto _type1;
        private TypeDto _type2;

        private TypeCategoryListDto _typeCategoryList1;
        private TypeCategoryListDto _typeCategoryList2;

        public DtoToDomainConverter_Fixture()
        {
            _dtoToDomainConverter = new DtoToDomainConverter();
            _testDataGenerator = new TestDataGenerator();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _attachment1 = _testDataGenerator.CreateAttachmentDto(1, "Attachment1","url1", DateOnly.FromDateTime(DateTime.UtcNow),true);
            _attachment2 = _testDataGenerator.CreateAttachmentDto(2, "Attachment2", "url2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _card1 = _testDataGenerator.CreateCardDto(1, "Card1", "text1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _card2 = _testDataGenerator.CreateCardDto(2, "Card2", "text2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _result1 = _testDataGenerator.CreateResultDto(1, "Result1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _result2 = _testDataGenerator.CreateResultDto(2, "Result2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _type1 = _testDataGenerator.CreateTypeDto(1, "Type1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _type2 = _testDataGenerator.CreateTypeDto(2, "Type2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _typeCategoryList1 = _testDataGenerator.CreateTypeCategoryListDto(1, "TypeCategoryList1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeCategoryList2 = _testDataGenerator.CreateTypeCategoryListDto(2, "TypeCategoryList2", DateOnly.FromDateTime(DateTime.UtcNow), true);
        }

        [TestMethod]
        public void DtoToDomainConverter_EmptyIEnumerableOfAttachment_ReturnsNull()
        {
            //Arrange
            var entity = new List<AttachmentDto>();

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

        

        [TestMethod]
        public void DtoToDomainConverter_IEnumerableOfAttachmentWithRelations_ReturnsIEnumerableOfAttachmentDtoWithRelations()
        {
            //Arrange
            var attachment1 = _attachment1;
            attachment1.Cards = new List<CardDto>() { _card1 } ;
            attachment1.Results = new List<ResultDto>() { _result1 } ;

            var attachment2 = _attachment2;
            attachment2.Cards = new List<CardDto>() { _card2 } ;
            attachment2.Results = new List<ResultDto>() { _result2 } ;
            var entity = new List<AttachmentDto> { attachment1, attachment2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
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
        public void DtoToDomainConverter_IEnumerableOfAttachmentWithoutRelations_ReturnsIEnumerableOfAttachmentDtoWithoutRelations()
        {
            //Arrange
            var attachment1 = _attachment1;

            var attachment2 = _attachment2;
            var entity = new List<AttachmentDto> { attachment1, attachment2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
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
        public void DtoToDomainConverter_IEnumerableOfAttachment_ReturnsIEnumerableOfAttachmentDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _attachment1;

            var entity2 = _attachment2;
            var entities = new List<AttachmentDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
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
        public void DtoToDomainConverter_NullAttachment_ReturnsNull()
        {
            //Arrange
            AttachmentDto entity = null;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

        

        [TestMethod]
        public void DtoToDomainConverter_AttachmentWithRelations_ReturnsAttachmentDtoWithRelations()
        {
            //Arrange
            var entity = _attachment1;
            entity.Cards = new List<CardDto>() { _card1 } ;
            entity.Results = new List<ResultDto>() { _result1 } ;


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(1, result.Results.Count());
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(1, result.Cards.Count());


        }

        [TestMethod]
        public void DtoToDomainConverter_AttachmentWithoutRelations_ReturnsAttachmentDtoWithoutRelations()
        {
            //Arrange
            var entity = _attachment1;


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(0, result.Results.Count());
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(0, result.Cards.Count());

        }

        [TestMethod]
        public void DtoToDomainConverter_Attachment_ReturnsAttachmentDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _attachment1;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);
            Assert.AreEqual(entity.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entity.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DtoToDomainConverter_EmptyIEnumerableOfCard_ReturnsNull()
        {
            //Arrange
            var entity = new List<CardDto>();

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

        

        [TestMethod]
        public void DtoToDomainConverter_IEnumerableOfCardWithRelations_ReturnsIEnumerableOfCardDtoWithRelations()
        {
            //Arrange
            var card1 = _card1;
            card1.Types = new List<TypeDto>() { _type1 } ;
            card1.Results = new List<ResultDto>() { _result1 } ;
            card1.Attachment = _attachment1;

            var card2 = _card2;
            card2.Types = new List<TypeDto>() { _type2 } ;
            card2.Results = new List<ResultDto>() { _result2 } ;
            card2.Attachment = _attachment2;
            var entity = new List<CardDto> { card1, card2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
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
            Assert.IsNotNull(result.ElementAt(0).AttachmentId);

            Assert.IsNotNull(result.ElementAt(1).Results);
            Assert.AreEqual(1, result.ElementAt(1).Results.Count());
            Assert.IsNotNull(result.ElementAt(1).Types);
            Assert.AreEqual(1, result.ElementAt(1).Types.Count());
            Assert.IsNotNull(result.ElementAt(1).AttachmentId);

        }

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_IEnumerableOfCardWithoutRelations_ReturnsIEnumerableOfCardDtoWithoutRelations()
        {
            //Arrange
            var card1 = _card1;

            var card2 = _card2;
            var entity = new List<CardDto> { card1, card2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
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
            Assert.IsNull(result.ElementAt(0).AttachmentId);

            Assert.IsNotNull(result.ElementAt(1).Results);
            Assert.AreEqual(0, result.ElementAt(1).Results.Count());
            Assert.IsNotNull(result.ElementAt(1).Types);
            Assert.AreEqual(0, result.ElementAt(1).Types.Count());
            Assert.IsNull(result.ElementAt(1).AttachmentId);

        }

        [TestMethod]
        public void DtoToDomainConverter_IEnumerableOfCard_ReturnsIEnumerableOfCardDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _card1;

            var entity2 = _card2;
            var entities = new List<CardDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
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
        public void DtoToDomainConverter_NullCard_ReturnsNull()
        {
            //Arrange
            CardDto entity = null;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

        

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_CardWithRelations_ReturnsCardDtoWithRelations()
        {
            //Arrange
            var entity = _card1;
            entity.Types = new List<TypeDto>() { _type1 } ;
            entity.Results = new List<ResultDto>() { _result1 } ;
            entity.Attachment = _attachment1;


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(entity.CardId, result.CardId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(1, result.Results.Count());
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(1, result.Types.Count());
            Assert.IsNotNull(result.AttachmentId);


        }

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_CardWithoutRelations_ReturnsCardDtoWithoutRelations()
        {
            //Arrange
            var entity = _card1;


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(entity.CardId, result.CardId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(0, result.Results.Count());
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(0, result.Types.Count());
            Assert.IsNull(result.AttachmentId);

        }
        [TestMethod]
        public void DtoToDomainConverter_Card_ReturnsCardDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _card1;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(entity.CardId, result.CardId);
            Assert.AreEqual(entity.CardName, result.CardName);
            Assert.AreEqual(entity.CardText, result.CardText);
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DtoToDomainConverter_EmptyIEnumerableOfResult_ReturnsNull()
        {
            //Arrange
            var entity = new List<ResultDto>();

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

        

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_IEnumerableOfResultWithRelations_ReturnsIEnumerableOfResultDtoWithRelations()
        {
            //Arrange
            var entity1 = _result1;
            entity1.Cards = new List<CardDto>() { _card1 } ;
            entity1.Attachment = _attachment1;

            var entity2 = _result2;
            entity2.Cards = new List<CardDto>() { _card1 } ;
            entity2.Attachment = _attachment2;
            var entities = new List<ResultDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].ResultId, result.ElementAt(0).ResultId);
            Assert.AreEqual(entities[1].ResultId, result.ElementAt(1).ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(1, result.ElementAt(0).Cards.Count());
            Assert.IsNotNull(result.ElementAt(0).AttachmentId);

            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(1, result.ElementAt(1).Cards.Count());
            Assert.IsNotNull(result.ElementAt(1).AttachmentId);

        }

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_IEnumerableOfResultWithoutRelations_ReturnsIEnumerableOfResultDtoWithoutRelations()
        {
            //Arrange
            var entity1 = _result1;

            var entity2 = _result2;
            var entities = new List<ResultDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entities.Count, result.Count());
            Assert.AreEqual(entities[0].ResultId, result.ElementAt(0).ResultId);
            Assert.AreEqual(entities[1].ResultId, result.ElementAt(1).ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.ElementAt(0).Cards);
            Assert.AreEqual(0, result.ElementAt(0).Cards.Count());
            Assert.IsNull(result.ElementAt(0).AttachmentId);

            Assert.IsNotNull(result.ElementAt(1).Cards);
            Assert.AreEqual(0, result.ElementAt(1).Cards.Count());
            Assert.IsNull(result.ElementAt(1).AttachmentId);

        }

        [TestMethod]
        public void DtoToDomainConverter_IEnumerableOfResult_ReturnsIEnumerableOfResultDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _result1;

            var entity2 = _result2;
            var entities = new List<ResultDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
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
        public void DtoToDomainConverter_NullResult_ReturnsNull()
        {
            //Arrange
            ResultDto entity = null;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

        
        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_ResultWithRelations_ReturnsResultDtoWithRelations()
        {
            //Arrange
            var entity = _result1;
            entity.Cards = new List<CardDto>() { _card1 } ;
            entity.Attachment = _attachment1;


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(entity.ResultId, result.ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(1, result.Cards.Count());
            Assert.IsNotNull(result.AttachmentId);


        }

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_ResultWithoutRelations_ReturnsResultDtoWithoutRelations()
        {
            //Arrange
            var entity = _result1;


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(entity.ResultId, result.ResultId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(0, result.Cards.Count());
            Assert.IsNull(result.AttachmentId);

        }

        [TestMethod]
        public void DtoToDomainConverter_Result_ReturnsResultDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _result1;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(entity.ResultId, result.ResultId);
            Assert.AreEqual(entity.ResultText, result.ResultText);
            Assert.AreEqual(entity.AttachmentId, result.AttachmentId);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }
        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DtoToDomainConverter_EmptyIEnumerableOfType_ReturnsNull()
        {
            //Arrange
            var entity = new List<TypeDto>();

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

       

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_IEnumerableOfTypeWithRelations_ReturnsIEnumerableOfTypeDtoWithRelations()
        {
            //Arrange
            var entity1 = _type1;
            entity1.Cards = new List<CardDto>() { _card1 } ;
            entity1.TypeCategoryLists = new List<TypeCategoryListDto>() {_typeCategoryList1};

            var entity2 = _type2;
            entity2.Cards = new List<CardDto>() { _card1 } ;
            entity2.TypeCategoryLists = new List<TypeCategoryListDto>() {_typeCategoryList2};
            var entities = new List<TypeDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
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
        public void DtoToDomainConverterWithConvertRelationsTrue_IEnumerableOfTypeWithoutRelations_ReturnsIEnumerableOfTypeDtoWithoutRelations()
        {
            //Arrange
            var entity1 = _type1;

            var entity2 = _type2;
            var entities = new List<TypeDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
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
        public void DtoToDomainConverter_IEnumerableOfType_ReturnsIEnumerableOfTypeDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _type1;

            var entity2 = _type2;
            var entities = new List<TypeDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
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
        public void DtoToDomainConverter_NullType_ReturnsNull()
        {
            //Arrange
            TypeDto entity = null;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

        

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_TypeWithRelations_ReturnsTypeDtoWithRelations()
        {
            //Arrange
            var entity = _type1;
            entity.Cards = new List<CardDto>() { _card1 } ;
            entity.TypeCategoryLists = new List<TypeCategoryListDto>() {_typeCategoryList1};


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(entity.TypeId, result.TypeId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(1, result.Cards.Count());
            Assert.IsNotNull(result.TypeCategoryLists);
            Assert.AreEqual(1, result.TypeCategoryLists.Count());


        }

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_TypeWithoutRelations_ReturnsTypeDtoWithoutRelations()
        {
            //Arrange
            var entity = _type1;


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(entity.TypeId, result.TypeId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(0, result.Cards.Count());
            Assert.IsNotNull(result.TypeCategoryLists);
            Assert.AreEqual(0, result.TypeCategoryLists.Count());

        }

        [TestMethod]
        public void DtoToDomainConverter_Type_ReturnsTypeDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _type1;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(entity.TypeId, result.TypeId);
            Assert.AreEqual(entity.CardTypeName, result.CardTypeName);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }

        //--------------------------------------------------------------------------------

        [TestMethod]
        public void DtoToDomainConverter_EmptyIEnumerableOfTypeCategoryList_ReturnsNull()
        {
            //Arrange
            var entity = new List<TypeCategoryListDto>();

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

        

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_IEnumerableOfTypeCategoryListWithRelations_ReturnsIEnumerableOfTypeCategoryListDtoWithRelations()
        {
            //Arrange
            var entity1 = _typeCategoryList1;
            entity1.Types = new List<TypeDto>() { _type1 } ;

            var entity2 = _typeCategoryList2;
            entity2.Types = new List<TypeDto>() { _type1 } ;
            var entities = new List<TypeCategoryListDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
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
        public void DtoToDomainConverterWithConvertRelationsTrue_IEnumerableOfTypeCategoryListWithoutRelations_ReturnsIEnumerableOfTypeCategoryListDtoWithoutRelations()
        {
            //Arrange
            var entity1 = _typeCategoryList1;

            var entity2 = _typeCategoryList2;
            var entities = new List<TypeCategoryListDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
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
        public void DtoToDomainConverter_IEnumerableOfTypeCategoryList_ReturnsIEnumerableOfTypeCategoryListDtoWithCorrectProperties()
        {
            //Arrange
            var entity1 = _typeCategoryList1;

            var entity2 = _typeCategoryList2;
            var entities = new List<TypeCategoryListDto> { entity1, entity2 };

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entities);
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
        public void DtoToDomainConverter_NullTypeCategoryList_ReturnsNull()
        {
            //Arrange
            TypeCategoryListDto entity = null;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNull(result);
        }

        

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_TypeCategoryListWithRelations_ReturnsTypeCategoryListDtoWithRelations()
        {
            //Arrange
            var entity = _typeCategoryList1;
            entity.Types = new List<TypeDto>() { _type1 } ;


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryList));
            Assert.AreEqual(entity.TypeCategoryListId, result.TypeCategoryListId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(1, result.Types.Count());

        }

        [TestMethod]
        public void DtoToDomainConverterWithConvertRelationsTrue_TypeCategoryListWithoutRelations_ReturnsTypeCategoryListDtoWithoutRelations()
        {
            //Arrange
            var entity = _typeCategoryList1;


            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryList));
            Assert.AreEqual(entity.TypeCategoryListId, result.TypeCategoryListId);

            //Checks if the relations are empty
            Assert.IsNotNull(result.Types);
            Assert.AreEqual(0, result.Types.Count());

        }



        [TestMethod]
        public void DtoToDomainConverter_TypeCategoryList_ReturnsTypeCategoryListDtoWithCorrectProperties()
        {
            //Arrange
            var entity = _typeCategoryList1;

            //Act
            var result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryList));
            Assert.AreEqual(entity.TypeCategoryListId, result.TypeCategoryListId);
            Assert.AreEqual(entity.TypeCategoryListName, result.TypeCategoryListName);
            Assert.AreEqual(entity.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entity.Active, result.Active);

        }
    }
}
