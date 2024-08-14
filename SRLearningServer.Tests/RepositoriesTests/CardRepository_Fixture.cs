using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SRLearningServer.Components.Context;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLearningServer.Tests.RepositoriesTests
{
    [TestCategory("UnitTest")]
    [TestClass]
    public class CardRepository_Fixture
    {
        private readonly DbContextOptions<SRContext> _options;
        private readonly SRContext _context;
        private readonly CardRepository _repository;

        private Components.Models.Type _type1;
        private Components.Models.Type _type2;
        private Components.Models.Type _type3;
        private Components.Models.Type _type4;
        private Components.Models.Type _type5;
        private Card _card1;
        private Card _card2;
        private Card _card3;
        private Card _card4;
        private Card _card5;
        private Attachment _attachment1;
        private Attachment _attachment2;
        private Attachment _attachment3;
        private Result _result1;
        private Result _result2;
        private Result _result3;
        private Result _result4;

        private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Now);
        private DateOnly _oldDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        private List<Components.Models.Type> _typeList1 = new();
        private List<Components.Models.Type> _typeList2 = new();
        private List<Components.Models.Type> _typeList3 = new();


        private List<Components.Models.Type> _typeList1Plus = new();
        private List<Components.Models.Type> _typeList2Plus = new();
        private List<Components.Models.Type> _typeList3Plus = new();
        public CardRepository_Fixture()
        {
            _options = new DbContextOptionsBuilder<SRContext>()
                .UseInMemoryDatabase(databaseName: "TypeRepository")
                .Options;
            _context = new(_options);

            _repository = new(_context);
        }

        private Components.Models.Type CreateType(int id, string name, bool active, DateOnly date)
        {
            return new()
            {
                TypeId = id,
                CardTypeName = name,
                Active = active,
                LastUpdated = date,
            };
        }
        private Card CreateCard(int id, string name, string text, bool active, DateOnly date)
        {
            return new()
            {
                CardId = id,
                CardName = name,
                CardText = text,
                //AttachmentId = attachmentId,
                Active = active,
                LastUpdated = date,
            };
        }
        private Attachment CreateAttachment(int id, string name, string url, bool active, DateOnly date)
        {
            return new()
            {
                AttachmentId = id,
                AttachmentName = name,
                AttachmentUrl = url,
                Active = active,
                LastUpdated = date,
            };
        }

        private Result CreateResult(int id, string text, bool active, DateOnly date)
        {
            return new()
            {
                ResultId = id,
                ResultText = text,
                Active = active,
                LastUpdated = date,
            };
        }

        [TestInitialize]
        public void WireUp()
        {
            _type1 = CreateType(1, "Signal", true, _oldDate);
            _type2 = CreateType(2, "Stop", true, _oldDate);
            _type3 = CreateType(3, "Warning", true, _oldDate);
            _type4 = CreateType(4, "TU", true, _oldDate);
            _type5 = CreateType(5, "Uordenssignal", false, _oldDate);



            _typeList1.AddRange(new[] { _type1, _type2 });
            _typeList1Plus.AddRange( new[] { _type1, _type2, _type3});
            _typeList2.AddRange(new[] { _type2, _type3 });
            _typeList2Plus.AddRange(new[] { _type2, _type3, _type4 });
            _typeList3.AddRange(new[] { _type3, _type4 });
            _typeList3Plus.AddRange(new[] { _type3, _type4, _type5 });


            _card1 = CreateCard(1, "Card1", "Card1 Text", true, _oldDate);
            _card2 = CreateCard(2, "Card2", "Card2 Text", true, _oldDate);
            _card3 = CreateCard(3, "Card3", "Card3 Text", false, _oldDate);
            _card4 = CreateCard(4, "Card4", "Card4 Text", true, _oldDate);
            _card5 = CreateCard(5, "Card5", "Card5 Text", true, _oldDate);
            _attachment1 = CreateAttachment(1, "Attachment1", "Icon1234.png", true, _oldDate);
            _attachment2 = CreateAttachment(2, "Attachment2", "Icon1234.png", true, _oldDate);
            _attachment3 = CreateAttachment(3, "Attachment3", "Icon1234.png", false, _oldDate);
            _result1 = CreateResult(1, "stands foran signalet", true, _oldDate);
            _result2 = CreateResult(2, "stands foran stop", true, _oldDate);
            _result3 = CreateResult(3, "stands foran warning", false, _oldDate);
            _result4 = CreateResult(4, "stands foran TU", true, _oldDate);
        }

        [TestMethod]
        public async Task GetByType_NoCriteriaSent_ReturnsEmptyList()
        {
            //Arrange
            List<List<Components.Models.Type>> selectionTypes = new();
            //Act
            var result = await _repository.GetByType(selectionTypes);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Card>));
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetByType_OneCriteriaListSent_ReturnsListOfCardsThatSatisfyTheList()
        {
            //Arrange
            List<List<Components.Models.Type>> selectionList = new() { new List<Components.Models.Type>() { _type1, _type2 } };
            var entityUsed1 = _card1;
            var entityUsed2 = _card2;
            entityUsed1.Types.Add(_type1);
            entityUsed1.Types.Add(_type2);
            entityUsed2.Types.Add(_type1);
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(_attachment1);
            _context.Attachments.Add(_attachment2);
            _context.Attachments.Add(_attachment3);
            _context.Types.Add(_type1);
            _context.Types.Add(_type2);
            _context.Types.Add(_type3);
            _context.Cards.Add(entityUsed1);
            _context.Cards.Add(entityUsed2);

            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.GetByType(selectionList);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Card>));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(entityUsed1.CardId, result[0].CardId);
            Assert.AreEqual(entityUsed1.CardName, result[0].CardName);
            Assert.AreEqual(entityUsed1.CardText, result[0].CardText);
            Assert.AreEqual(entityUsed1.Active, result[0].Active);
            Assert.AreEqual(entityUsed1.LastUpdated, result[0].LastUpdated);
        }

        [TestMethod]
        public async Task GetByType_OneCriteriaListSent_ReturnsEmptyListOfCardsWhenAllSatisfyingCardsAreInactive()
        {
            //Arrange
            List<List<Components.Models.Type>> selectionList = new() { new List<Components.Models.Type>() { _type1, _type2 } };
            var entityUsed1 = _card3;
            entityUsed1.Types.Add(_type1);
            entityUsed1.Types.Add(_type2);
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(_type1);
            _context.Types.Add(_type2);
            _context.Types.Add(_type3);
            _context.Cards.Add(entityUsed1);

            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.GetByType(selectionList);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Card>));
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetByType_TwoCriteriaListSent_ReturnsListOfCardsThatSatisfyAnyList()
        {
            //Arrange
            List<List<Components.Models.Type>> selectionList = new() { new List<Components.Models.Type>() { _type1, _type2 }, new List<Components.Models.Type>() { _type1, _type3 } };
            var entityUsed1 = _card1;
            var entityUsed2 = _card2;
            entityUsed1.Types.Add(_type1);
            entityUsed1.Types.Add(_type2);
            entityUsed2.Types.Add(_type1);
            entityUsed2.Types.Add(_type3);
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(_attachment1);
            _context.Attachments.Add(_attachment2);
            _context.Attachments.Add(_attachment3);
            _context.Types.Add(_type1);
            _context.Types.Add(_type2);
            _context.Types.Add(_type3);
            _context.Cards.Add(entityUsed1);
            _context.Cards.Add(entityUsed2);

            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.GetByType(selectionList);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Card>));
            Assert.AreEqual(2, result.Count);
            //First list
            Assert.AreEqual(entityUsed1.CardId, result[0].CardId);
            Assert.AreEqual(entityUsed1.CardName, result[0].CardName);
            Assert.AreEqual(entityUsed1.CardText, result[0].CardText);
            Assert.AreEqual(entityUsed1.Active, result[0].Active);
            Assert.AreEqual(entityUsed1.LastUpdated, result[0].LastUpdated);

            //Second list
            Assert.AreEqual(entityUsed2.CardId, result[1].CardId);
            Assert.AreEqual(entityUsed2.CardName, result[1].CardName);
            Assert.AreEqual(entityUsed2.CardText, result[1].CardText);
            Assert.AreEqual(entityUsed2.Active, result[1].Active);
            Assert.AreEqual(entityUsed2.LastUpdated, result[1].LastUpdated);
        }

        [TestMethod]
        public async Task GetByType_OneCriteriaListSent_ReturnsListOfAllCardsThatSatisfyTheList()
        {
            //Arrange
            List<List<Components.Models.Type>> selectionList = new() { new List<Components.Models.Type>() { _type1, _type2 } };
            var entityUsed1 = _card1;
            var entityUsed2 = _card2;
            entityUsed1.Types.Add(_type1);
            entityUsed1.Types.Add(_type2);
            entityUsed2.Types.Add(_type1);
            entityUsed2.Types.Add(_type2);
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(_attachment1);
            _context.Attachments.Add(_attachment2);
            _context.Attachments.Add(_attachment3);
            _context.Types.Add(_type1);
            _context.Types.Add(_type2);
            _context.Cards.Add(entityUsed1);
            _context.Cards.Add(entityUsed2);

            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.GetByType(selectionList);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Card>));
            Assert.AreEqual(2, result.Count);
            //First list
            Assert.AreEqual(entityUsed1.CardId, result[0].CardId);
            Assert.AreEqual(entityUsed1.CardName, result[0].CardName);
            Assert.AreEqual(entityUsed1.CardText, result[0].CardText);
            Assert.AreEqual(entityUsed1.Active, result[0].Active);
            Assert.AreEqual(entityUsed1.LastUpdated, result[0].LastUpdated);

            //Second list
            Assert.AreEqual(entityUsed2.CardId, result[1].CardId);
            Assert.AreEqual(entityUsed2.CardName, result[1].CardName);
            Assert.AreEqual(entityUsed2.CardText, result[1].CardText);
            Assert.AreEqual(entityUsed2.Active, result[1].Active);
            Assert.AreEqual(entityUsed2.LastUpdated, result[1].LastUpdated);
        }

        [TestMethod]
        public async Task GetByType_TwoCriteriaListSent_ReturnsListOfAllCardsThatSatisfyAnyList()
        {
            //Arrange
            List<List<Components.Models.Type>> selectionList = new() { new List<Components.Models.Type>() { _type1, _type2 }, new List<Components.Models.Type>() { _type1, _type3 } };
            var entityUsed1 = _card1;
            var entityUsed2 = _card2;
            var entityUsed3 = _card4;
            var entityUsed4 = _card5;
            entityUsed1.Types.Add(_type1);
            entityUsed1.Types.Add(_type2);
            entityUsed2.Types.Add(_type1);
            entityUsed2.Types.Add(_type2);


            entityUsed3.Types.Add(_type1);
            entityUsed3.Types.Add(_type3);
            entityUsed4.Types.Add(_type1);
            entityUsed4.Types.Add(_type3);
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(_attachment1);
            _context.Attachments.Add(_attachment2);
            _context.Attachments.Add(_attachment3);
            _context.Types.Add(_type1);
            _context.Types.Add(_type2);
            _context.Types.Add(_type3);
            _context.Cards.Add(entityUsed1);
            _context.Cards.Add(entityUsed2);
            _context.Cards.Add(entityUsed3);
            _context.Cards.Add(entityUsed4);

            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.GetByType(selectionList);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Card>));
            Assert.AreEqual(4, result.Count);

            //First list
            Assert.AreEqual(entityUsed1.CardId, result[0].CardId);
            Assert.AreEqual(entityUsed1.CardName, result[0].CardName);
            Assert.AreEqual(entityUsed1.CardText, result[0].CardText);
            Assert.AreEqual(entityUsed1.Active, result[0].Active);
            Assert.AreEqual(entityUsed1.LastUpdated, result[0].LastUpdated);

            //Second list
            Assert.AreEqual(entityUsed2.CardId, result[1].CardId);
            Assert.AreEqual(entityUsed2.CardName, result[1].CardName);
            Assert.AreEqual(entityUsed2.CardText, result[1].CardText);
            Assert.AreEqual(entityUsed2.Active, result[1].Active);
            Assert.AreEqual(entityUsed2.LastUpdated, result[1].LastUpdated);

            //Third list
            Assert.AreEqual(entityUsed3.CardId, result[2].CardId);
            Assert.AreEqual(entityUsed3.CardName, result[2].CardName);
            Assert.AreEqual(entityUsed3.CardText, result[2].CardText);
            Assert.AreEqual(entityUsed3.Active, result[2].Active);
            Assert.AreEqual(entityUsed3.LastUpdated, result[2].LastUpdated);

            //Fourth list
            Assert.AreEqual(entityUsed4.CardId, result[3].CardId);
            Assert.AreEqual(entityUsed4.CardName, result[3].CardName);
            Assert.AreEqual(entityUsed4.CardText, result[3].CardText);
            Assert.AreEqual(entityUsed4.Active, result[3].Active);
            Assert.AreEqual(entityUsed4.LastUpdated, result[3].LastUpdated);

        }

        [TestMethod]
        public async Task GetByType_OneCriteriaListSent_ReturnsListOfCardsThatSatisfyTheListEvenIfTheyHaveMoreTypes()
        {
            //Arrange
            List<List<Components.Models.Type>> selectionList = new() { new List<Components.Models.Type>() { _type1, _type2 } };
            var entityUsed1 = _card1;
            entityUsed1.Types.Add(_type1);
            entityUsed1.Types.Add(_type2);
            entityUsed1.Types.Add(_type3);
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(_attachment1);
            _context.Attachments.Add(_attachment2);
            _context.Attachments.Add(_attachment3);
            _context.Types.Add(_type1);
            _context.Types.Add(_type2);
            _context.Types.Add(_type3);
            _context.Cards.Add(entityUsed1);

            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.GetByType(selectionList);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Card>));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(entityUsed1.CardId, result[0].CardId);
            Assert.AreEqual(entityUsed1.CardName, result[0].CardName);
            Assert.AreEqual(entityUsed1.CardText, result[0].CardText);
            Assert.AreEqual(entityUsed1.Active, result[0].Active);
            Assert.AreEqual(entityUsed1.LastUpdated, result[0].LastUpdated);
        }


        [TestMethod]
        public async Task Update_CardDoesNotExistInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _card1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_CardExistsInDatabase_ReturnsUpdatedCard()
        {
            //Arrange
            var entityUsed = _card1;
            var oldCard = CreateCard(entityUsed.CardId, "RandomCardName", "RandomCardText", !entityUsed.Active, _oldDate);
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(oldCard);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(entityUsed.CardName, result.CardName);
            Assert.AreEqual(entityUsed.CardText, result.CardText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
        }

        [TestMethod]
        public async Task Update_CardThatAddsAttachment_ReturnsUpdatedCard()
        {
            //Arrange
            var entityUsed = _card1;
            var oldEntityUsed = CreateCard(entityUsed.CardId, entityUsed.CardName, entityUsed.CardText, entityUsed.Active, entityUsed.LastUpdated);
            var attachment = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(attachment);
            _context.Cards.Add(oldEntityUsed);
            await _context.SaveChangesAsync();
            entityUsed.Attachment = attachment;

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(entityUsed.Attachment, result.Attachment);
        }
        [TestMethod]
        public async Task Update_CardThatRemovesAttachment_ReturnsUpdatedCard()
        {
            //Arrange
            var entityUsed = _card1;
            var oldEntityUsed = CreateCard(entityUsed.CardId, entityUsed.CardName, entityUsed.CardText, entityUsed.Active, entityUsed.LastUpdated);
            var attachment = _attachment1;
            oldEntityUsed.Attachment = attachment;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(attachment);
            _context.Cards.Add(oldEntityUsed);
            await _context.SaveChangesAsync();
            entityUsed.Attachment = null;

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.IsNull(result.Attachment);
        }

        [TestMethod]
        public async Task Update_CardThatChangesAttachment_ReturnsUpdatedCard()
        {
            //Arrange
            var entityUsed = _card1;
            var oldEntityUsed = CreateCard(entityUsed.CardId, entityUsed.CardName, entityUsed.CardText, entityUsed.Active, entityUsed.LastUpdated);
            var attachment = _attachment1;
            var newAttachment = _attachment2;
            oldEntityUsed.Attachment = attachment;
            entityUsed.Attachment = newAttachment;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(attachment);
            _context.Attachments.Add(newAttachment);
            _context.Cards.Add(oldEntityUsed);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(newAttachment, result.Attachment);
        }

        [TestMethod]
        public async Task Update_CardThatAddsResults_ReturnsUpdatedCard()
        {
            //Arrange
            var entityUsed = _card1;
            var oldEntityUsed = CreateCard(entityUsed.CardId, entityUsed.CardName, entityUsed.CardText, entityUsed.Active, entityUsed.LastUpdated);
            var result1 = _result1;
            var result2 = _result2;
            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(result1);
            _context.Results.Add(result2);
            _context.Cards.Add(oldEntityUsed);
            await _context.SaveChangesAsync();
            entityUsed.Results.Add(result1);
            entityUsed.Results.Add(result2);

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(2, result.Results.Count);

            //First result
            var checkResult = result.Results.ToList().ElementAt(0);
            Assert.AreEqual(result1.ResultId, checkResult.ResultId);
            Assert.AreEqual(result1.ResultText, checkResult.ResultText);
            Assert.AreEqual(result1.Active, checkResult.Active);
            Assert.AreEqual(result1.LastUpdated, checkResult.LastUpdated);

            //Second result
            checkResult = result.Results.ToList().ElementAt(1);
            Assert.AreEqual(result2.ResultId, checkResult.ResultId);
            Assert.AreEqual(result2.ResultText, checkResult.ResultText);
            Assert.AreEqual(result2.Active, checkResult.Active);
            Assert.AreEqual(result2.LastUpdated, checkResult.LastUpdated);
        }

        [TestMethod]
        public async Task Update_CardThatRemovesResults_ReturnsUpdatedCard()
        {
            //Arrange
            var entityUsed = _card1;
            var oldEntityUsed = CreateCard(entityUsed.CardId, entityUsed.CardName, entityUsed.CardText, entityUsed.Active, entityUsed.LastUpdated);
            var result1 = _result1;
            var result2 = _result2;

            oldEntityUsed.Results.Add(result1);
            oldEntityUsed.Results.Add(result2);
            entityUsed.Results.Add(result1);

            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(result1);
            _context.Results.Add(result2);
            _context.Cards.Add(oldEntityUsed);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(1, result.Results.Count);

            //First result
            var checkResult = result.Results.ToList().ElementAt(0);
            Assert.AreEqual(result1.ResultId, checkResult.ResultId);
            Assert.AreEqual(result1.ResultText, checkResult.ResultText);
            Assert.AreEqual(result1.Active, checkResult.Active);
            Assert.AreEqual(result1.LastUpdated, checkResult.LastUpdated);

        }

        [TestMethod]
        public async Task Update_CardThatChangesResults_ReturnsUpdatedCard()
        {
            //Arrange
            var entityUsed = _card1;
            var oldEntityUsed = CreateCard(entityUsed.CardId, entityUsed.CardName, entityUsed.CardText, entityUsed.Active, entityUsed.LastUpdated);
            var result1 = _result1;
            var result2 = _result2;
            var result3 = _result4;

            oldEntityUsed.Results.Add(result1);
            oldEntityUsed.Results.Add(result2);

            entityUsed.Results.Add(result1);
            entityUsed.Results.Add(result3);

            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(result1);
            _context.Results.Add(result2);
            _context.Results.Add(result3);
            _context.Cards.Add(oldEntityUsed);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(2, result.Results.Count);

            //First result
            var checkResult = result.Results.ToList().ElementAt(0);
            Assert.AreEqual(result1.ResultId, checkResult.ResultId);
            Assert.AreEqual(result1.ResultText, checkResult.ResultText);
            Assert.AreEqual(result1.Active, checkResult.Active);
            Assert.AreEqual(result1.LastUpdated, checkResult.LastUpdated);

            //Second result
            checkResult = result.Results.ToList().ElementAt(1);
            Assert.AreEqual(result3.ResultId, checkResult.ResultId);
            Assert.AreEqual(result3.ResultText, checkResult.ResultText);
            Assert.AreEqual(result3.Active, checkResult.Active);
            Assert.AreEqual(result3.LastUpdated, checkResult.LastUpdated);
        }
        #region BaseRepositoryTests

        [TestMethod]
        public async Task Create_NewCardWithId_ReturnsAddedCard()
        {
            //Arrange
            var entityUsed = _card1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(entityUsed.CardName, result.CardName);
            Assert.AreEqual(entityUsed.CardText, result.CardText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
            Assert.AreEqual(entityUsed.CardId, result.CardId);
        }

        [TestMethod]
        public async Task Create_NewCard_ReturnsAddedCard()
        {
            //Arrange
            var entityUsed = _card1;
            entityUsed.CardId = new();
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(entityUsed.CardName, result.CardName);
            Assert.AreEqual(entityUsed.CardText, result.CardText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
            Assert.AreEqual(1, result.CardId);
        }

        [TestMethod]
        public async Task Create_CardAlreadyInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _card1;
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_CardInDatabase_ReturnsDeactivatedCard()
        {
            //Arrange
            var entityUsed = _card1;
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Deactivate(entityUsed.CardId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.IsFalse(result.Active);
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(entityUsed.CardName, result.CardName);
            Assert.AreEqual(entityUsed.CardText, result.CardText);
            Assert.AreEqual(_currentDate, result.LastUpdated);

        }
        [TestMethod]
        public async Task Deactivate_CardNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _card1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Deactivate(entityUsed.CardId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task Deactivate_CardAlreadyInactiveInDatabase_ReturnsDeactivatedCard()
        {
            //Arrange
            var entityUsed = _card1;
            entityUsed.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Deactivate(entityUsed.CardId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.IsFalse(result.Active);
            Assert.AreEqual(entityUsed.CardId, result.CardId);
            Assert.AreEqual(entityUsed.CardName, result.CardName);
            Assert.AreEqual(entityUsed.CardText, result.CardText);
            Assert.AreEqual(_currentDate, result.LastUpdated);
        }
        [TestMethod]
        public async Task Delete_CardInDatabase_ReturnsCard()
        {
            //Arrange
            var entityUsed = _card1;
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Delete(entityUsed.CardId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(entityUsed.CardName, result.CardName);
            Assert.AreEqual(entityUsed.CardText, result.CardText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.CardId, result.CardId);
        }
        [TestMethod]
        public async Task Delete_CardNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _card1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Delete(entityUsed.CardId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task Get_CardInDatabase_ReturnsCard()
        {
            //Arrange
            var entityUsed = _card1;
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(entityUsed.CardId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Card));
            Assert.AreEqual(entityUsed.CardName, result.CardName);
            Assert.AreEqual(entityUsed.CardText, result.CardText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.CardId, result.CardId);
        }
        [TestMethod]
        public async Task Get_CardNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _card1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(entityUsed.CardId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetAll_CardsInDatabase_ReturnsAllCards()
        {
            //Arrange
            var entityUsed = _card1;
            var entityUsed2 = _card2;
            var entityUsed3 = _card3;
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(entityUsed);
            _context.Cards.Add(entityUsed2);
            _context.Cards.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Card>));
            Assert.AreEqual(3, result.Count());
        }
        [TestMethod]
        public async Task GetAll_NoCardsInDatabase_ReturnsEmptyList()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Card>));
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public async Task GetAll_DeactivatedCardsInDatabase_ReturnsAllCards()
        {
            //Arrange
            var entityUsed = _card1;
            var entityUsed2 = _card2;
            var entityUsed3 = _card3;
            entityUsed.Active = false;
            entityUsed2.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(entityUsed);
            _context.Cards.Add(entityUsed2);
            _context.Cards.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Card>));
            Assert.AreEqual(3, result.Count());
        }

        #endregion
    }
}
