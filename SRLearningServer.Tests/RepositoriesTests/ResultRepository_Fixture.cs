using Microsoft.EntityFrameworkCore;
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
    [TestClass]
    [TestCategory("UnitTest")]
    public class ResultRepository_Fixture
    {

        private readonly DbContextOptions<SRContext> _options;
        private readonly SRContext _context;
        private readonly ResultRepository _repository;

        private Components.Models.Type _type1;
        private Components.Models.Type _type2;
        private Components.Models.Type _type3;
        private Card _card1;
        private Card _card2;
        private Card _card3;
        private Attachment _attachment1;
        private Attachment _attachment2;
        private Attachment _attachment3;
        private Result _result1;
        private Result _result2;
        private Result _result3;


        private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Now);
        private DateOnly _oldDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        public ResultRepository_Fixture()
        {
            _options = new DbContextOptionsBuilder<SRContext>()
                .UseInMemoryDatabase(databaseName: "SRContext")
                .Options;
            _context = new SRContext(_options);
            _repository = new ResultRepository(_context);
        }

        private Attachment CreateAttachment(int id, string name, string url, DateOnly lastUpdated, bool active)
        {
            return new()
            {
                AttachmentId = id,
                AttachmentName = name,
                AttachmentUrl = url,
                LastUpdated = lastUpdated,
                Active = active
            };
        }
        private Card CreateCard(int id, string name, string text, DateOnly lastUpdated, bool active)
        {
            return new()
            {
                CardId = id,
                CardName = name,
                CardText = text,
                Active = active,
                LastUpdated = lastUpdated
            };
        }
        private Result CreateResult(int id, string text, DateOnly lastUpdated, bool active)
        {
            return new()
            {
                ResultId = id,
                ResultText = text,
                Active = active,
                LastUpdated = lastUpdated
            };
        }

        [TestInitialize]
        public void WireUp()
        {
            _result1 = CreateResult(1, "Result1", _oldDate, true);
            _result2 = CreateResult(2, "Result2", _oldDate, true);
            _result3 = CreateResult(3, "Result3", _oldDate, true);

            _card1 = CreateCard(1, "Card1", "Card1Text", _oldDate, true);
            _card2 = CreateCard(2, "Card2", "Card2Text", _oldDate, true);
            _card3 = CreateCard(3, "Card3", "Card3Text", _oldDate, true);

            _attachment1 = CreateAttachment(1, "Attachment1", "url1", _oldDate, true);
            _attachment2 = CreateAttachment(2, "Attachment2", "url2", _oldDate, true);
            _attachment3 = CreateAttachment(3, "Attachment3", "url3", _oldDate, true);
        }

        [TestMethod]
        public async Task Update_ResultNotFoundInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _result1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_UpdatedResult_ReturnsUpdatedResult()
        {
            //Arrange
            var entityUsed = _result1;
            var oldResult = CreateResult(entityUsed.ResultId, "RandomText", _oldDate, !entityUsed.Active);
            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(oldResult);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
        }

        [TestMethod]
        public async Task Update_ResultAddsAttachment_ReturnsUpdatedResult()
        {
            //Arrange
            var entityUsed = _result1;
            var oldEntityUsed = CreateResult(entityUsed.ResultId, entityUsed.ResultText, entityUsed.LastUpdated, !entityUsed.Active);
            var attachmentUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(attachmentUsed);
            _context.Results.Add(oldEntityUsed);
            await _context.SaveChangesAsync();
            entityUsed.Attachment = attachmentUsed;

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsNotNull(result.Attachment);
            Assert.AreEqual(attachmentUsed.AttachmentId, result.Attachment.AttachmentId);
            Assert.AreEqual(attachmentUsed.AttachmentName, result.Attachment.AttachmentName);
            Assert.AreEqual(attachmentUsed.AttachmentUrl, result.Attachment.AttachmentUrl);
            Assert.AreEqual(attachmentUsed.Active, result.Attachment.Active);
            Assert.AreEqual(attachmentUsed.LastUpdated, result.Attachment.LastUpdated);
        }

        [TestMethod]
        public async Task Update_ResultRemovesAttachment_ReturnsUpdatedResult()
        {
            //Arrange
            var entityUsed = _result1;
            var oldEntityUsed = CreateResult(entityUsed.ResultId, entityUsed.ResultText, entityUsed.LastUpdated, !entityUsed.Active);
            var attachmentUsed = _attachment1;
            oldEntityUsed.Attachment = attachmentUsed;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(attachmentUsed);
            _context.Results.Add(oldEntityUsed);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsNull(result.Attachment);
        }

        [TestMethod]
        public async Task Update_ResultChangesAttachment_ReturnsUpdatedResult()
        {
            //Arrange
            var entityUsed = _result1;
            var oldEntityUsed = CreateResult(entityUsed.ResultId, entityUsed.ResultText, entityUsed.LastUpdated, !entityUsed.Active);
            var attachmentUsed = _attachment1;
            var attachmentUsed2 = _attachment2;
            oldEntityUsed.Attachment = attachmentUsed;
            entityUsed.Attachment = attachmentUsed2;

            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(attachmentUsed);
            _context.Attachments.Add(attachmentUsed2);
            _context.Results.Add(oldEntityUsed);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsNotNull(result.Attachment);
            Assert.AreEqual(attachmentUsed2.AttachmentId, result.Attachment.AttachmentId);
            Assert.AreEqual(attachmentUsed2.AttachmentName, result.Attachment.AttachmentName);
            Assert.AreEqual(attachmentUsed2.AttachmentUrl, result.Attachment.AttachmentUrl);
            Assert.AreEqual(attachmentUsed2.Active, result.Attachment.Active);
            Assert.AreEqual(attachmentUsed2.LastUpdated, result.Attachment.LastUpdated);
        }

        [TestMethod]
        public async Task Update_ResultAddsCard_ReturnsUpdatedResult()
        {
            //Arrange
            var entityUsed = _result1;
            var oldEntityUsed = CreateResult(entityUsed.ResultId, entityUsed.ResultText, entityUsed.LastUpdated, !entityUsed.Active);
            var cardUsed = _card1;
            var cardUsed2 = _card2;
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(cardUsed);
            _context.Cards.Add(cardUsed2);
            _context.Results.Add(oldEntityUsed);
            await _context.SaveChangesAsync();
            entityUsed.Cards.Add(cardUsed);
            entityUsed.Cards.Add(cardUsed2);

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(2, result.Cards.Count());

            //First Card
            var checkedCard = result.Cards.ToList().ElementAt(0);
            Assert.AreEqual(cardUsed.CardId, checkedCard.CardId);
            Assert.AreEqual(cardUsed.CardName, checkedCard.CardName);
            Assert.AreEqual(cardUsed.CardText, checkedCard.CardText);
            Assert.AreEqual(cardUsed.Active, checkedCard.Active);
            Assert.AreEqual(cardUsed.LastUpdated, checkedCard.LastUpdated);

            //Second Card
            checkedCard = result.Cards.ToList().ElementAt(1);
            Assert.AreEqual(cardUsed2.CardId, checkedCard.CardId);
            Assert.AreEqual(cardUsed2.CardName, checkedCard.CardName);
            Assert.AreEqual(cardUsed2.CardText, checkedCard.CardText);
            Assert.AreEqual(cardUsed2.Active, checkedCard.Active);
            Assert.AreEqual(cardUsed2.LastUpdated, checkedCard.LastUpdated);
        }

        [TestMethod]
        public async Task Update_ResultRemovesCard_ReturnsUpdatedResult()
        {
            //Arrange
            var entityUsed = _result1;
            var oldEntityUsed = CreateResult(entityUsed.ResultId, entityUsed.ResultText, entityUsed.LastUpdated, !entityUsed.Active);
            var cardUsed = _card1;
            var cardUsed2 = _card2;
            oldEntityUsed.Cards.Add(cardUsed);
            oldEntityUsed.Cards.Add(cardUsed2);
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(cardUsed);
            _context.Cards.Add(cardUsed2);
            _context.Results.Add(oldEntityUsed);
            await _context.SaveChangesAsync();
            entityUsed.Cards.Add(cardUsed2);

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(1, result.Cards.Count());
        }

        [TestMethod]
        public async Task Update_ResultChangesCard_ReturnsUpdatedResult()
        {
            //Arrange
            var entityUsed = _result1;
            var oldEntityUsed = CreateResult(entityUsed.ResultId, entityUsed.ResultText, entityUsed.LastUpdated, !entityUsed.Active);
            var cardUsed = _card1;
            var cardUsed2 = _card2;
            var cardUsed3 = _card3;
            oldEntityUsed.Cards.Add(cardUsed);
            oldEntityUsed.Cards.Add(cardUsed2);
            await _context.Database.EnsureDeletedAsync();
            _context.Cards.Add(cardUsed);
            _context.Cards.Add(cardUsed2);
            _context.Cards.Add(cardUsed3);
            _context.Results.Add(oldEntityUsed);
            await _context.SaveChangesAsync();
            entityUsed.Cards.Add(cardUsed);
            entityUsed.Cards.Add(cardUsed3);

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsNotNull(result.Cards);
            Assert.AreEqual(2, result.Cards.Count());

            //First Card
            var checkedCard = result.Cards.ToList().ElementAt(0);
            Assert.AreEqual(cardUsed.CardId, checkedCard.CardId);
            Assert.AreEqual(cardUsed.CardName, checkedCard.CardName);
            Assert.AreEqual(cardUsed.CardText, checkedCard.CardText);
            Assert.AreEqual(cardUsed.Active, checkedCard.Active);
            Assert.AreEqual(cardUsed.LastUpdated, checkedCard.LastUpdated);

            //Second Card
            checkedCard = result.Cards.ToList().ElementAt(1);
            Assert.AreEqual(cardUsed3.CardId, checkedCard.CardId);
            Assert.AreEqual(cardUsed3.CardName, checkedCard.CardName);
            Assert.AreEqual(cardUsed3.CardText, checkedCard.CardText);
            Assert.AreEqual(cardUsed3.Active, checkedCard.Active);
            Assert.AreEqual(cardUsed3.LastUpdated, checkedCard.LastUpdated);
        }


        #region BaseRepositoryTests

        [TestMethod]
        public async Task Create_NewResultWithId_ReturnsAddedResult()
        {
            //Arrange
            var entityUsed = _result1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
        }

        [TestMethod]
        public async Task Create_NewResult_ReturnsAddedResult()
        {
            //Arrange
            var entityUsed = _result1;
            entityUsed.ResultId = new();
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
            Assert.AreEqual(1, result.ResultId);
        }

        [TestMethod]
        public async Task Create_ResultAlreadyInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _result1;
            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_ResultInDatabase_ReturnsDeactivatedResult()
        {
            //Arrange
            var entityUsed = _result1;
            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Deactivate(entityUsed.ResultId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Active);
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(_currentDate, result.LastUpdated);

        }
        [TestMethod]
        public async Task Deactivate_ResultNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _result1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Deactivate(entityUsed.ResultId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task Deactivate_ResultAlreadyInactiveInDatabase_ReturnsDeactivatedResult()
        {
            //Arrange
            var entityUsed = _result1;
            entityUsed.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Deactivate(entityUsed.ResultId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsFalse(result.Active);
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
            
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(_currentDate, result.LastUpdated);
        }
        [TestMethod]
        public async Task Delete_ResultInDatabase_ReturnsResult()
        {
            //Arrange
            var entityUsed = _result1;
            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Delete(entityUsed.ResultId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
        }
        [TestMethod]
        public async Task Delete_ResultNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _result1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Delete(entityUsed.ResultId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task Get_ResultInDatabase_ReturnsResult()
        {
            //Arrange
            var entityUsed = _result1;
            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(entityUsed.ResultId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.ResultId, result.ResultId);
        }
        [TestMethod]
        public async Task Get_ResultNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _result1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(entityUsed.ResultId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetAll_ResultsInDatabase_ReturnsAllResults()
        {
            //Arrange
            var entityUsed = _result1;
            var entityUsed2 = _result2;
            var entityUsed3 = _result3;
            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(entityUsed);
            _context.Results.Add(entityUsed2);
            _context.Results.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Result>));
            Assert.AreEqual(3, result.Count());
        }
        [TestMethod]
        public async Task GetAll_NoResultsInDatabase_ReturnsEmptyList()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Result>));
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public async Task GetAll_DeactivatedResultsInDatabase_ReturnsAllResults()
        {
            //Arrange
            var entityUsed = _result1;
            var entityUsed2 = _result2;
            var entityUsed3 = _result3;
            entityUsed.Active = false;
            entityUsed2.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.Results.Add(entityUsed);
            _context.Results.Add(entityUsed2);
            _context.Results.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Result>));
            Assert.AreEqual(3, result.Count());
        }

        #endregion
    }
}
