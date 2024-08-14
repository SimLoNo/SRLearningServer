using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
    public class AttachmentRepository_Fixture
    {

        private readonly DbContextOptions<SRContext> _options;
        private readonly SRContext _context;
        private readonly AttachmentRepository _repository;

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
        public AttachmentRepository_Fixture()
        {
            _options = new DbContextOptionsBuilder<SRContext>()
                .UseInMemoryDatabase(databaseName: "AttachmentRepository")
                .Options;
            _context = new SRContext(_options);
            _repository = new AttachmentRepository(_context);

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
        private Result CreateResult(int id, string name, string text, DateOnly lastUpdated, bool active)
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
            _attachment1 = CreateAttachment(1, "Attachment1", "url1", DateOnly.FromDateTime(DateTime.Now), true);
            _attachment2 = CreateAttachment(2, "Attachment2", "url2", DateOnly.FromDateTime(DateTime.Now), true);
            _attachment3 = CreateAttachment(3, "Attachment3", "url3", DateOnly.FromDateTime(DateTime.Now), true);
            _result1 = CreateResult(1, "Result1", "text1", DateOnly.FromDateTime(DateTime.Now), true);
            _result2 = CreateResult(2, "Result2", "text2", DateOnly.FromDateTime(DateTime.Now), true);
            _result3 = CreateResult(3, "Result3", "text3", DateOnly.FromDateTime(DateTime.Now), true);
            _card1 = CreateCard(1, "Card1", "text1", DateOnly.FromDateTime(DateTime.Now), true);
            _card2 = CreateCard(2, "Card2", "text2", DateOnly.FromDateTime(DateTime.Now), true);
            _card3 = CreateCard(3, "Card3", "text3", DateOnly.FromDateTime(DateTime.Now), true);
        }

        [TestMethod]
        public async Task Update_AttachmentWithNoRelations_ReturnsUpdatedAttachment()
        {
            //Arrange
            var entityUsed = _attachment1;
            Attachment oldEntity = CreateAttachment(entityUsed.AttachmentId, "Tom Builder", "KingsbridgeForLife.png", DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5)), !entityUsed.Active);

            //Confirm that oldEntity values is not the same as entityUsed
            Assert.AreNotEqual(entityUsed.AttachmentName, oldEntity.AttachmentName);
            Assert.AreNotEqual(entityUsed.AttachmentUrl, oldEntity.AttachmentUrl);
            Assert.AreNotEqual(entityUsed.Active, oldEntity.Active);
            Assert.AreNotEqual(entityUsed.LastUpdated, oldEntity.LastUpdated);
            // Confirm that oldEntity has the same AttachmentId as entityUsed
            Assert.AreEqual(entityUsed.AttachmentId, oldEntity.AttachmentId);

            //Setup the database
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(oldEntity);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
        }

        [TestMethod]
        public async Task Update_AttachmentNotFoundInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(entityUsed);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_AttachmentWithResults_ReturnsUpdatedAttachmentWithResults()
        {
            //Arrange
            var entityUsed = _attachment1;
            var oldEntityUsed = CreateAttachment(entityUsed.AttachmentId, entityUsed.AttachmentName, entityUsed.AttachmentUrl, entityUsed.LastUpdated, entityUsed.Active);
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(oldEntityUsed);
            _context.Results.AddRange(_result1, _result2);
            await _context.SaveChangesAsync();
            entityUsed.Results.Add(_result1);
            entityUsed.Results.Add(_result2);
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(2, result.Results.Count);
            // Verify the first result
            Result verifyResult = result.Results.ToList().ElementAt(0);
            Assert.AreEqual(_result1.ResultId, verifyResult.ResultId);
            Assert.AreEqual(_result1.ResultText, verifyResult.ResultText);
            Assert.AreEqual(_result1.Active, verifyResult.Active);
            Assert.AreEqual(_result1.LastUpdated, verifyResult.LastUpdated);
            // Verify the second result
            verifyResult = result.Results.ToList().ElementAt(1);
            Assert.AreEqual(_result2.ResultId, verifyResult.ResultId);
            Assert.AreEqual(_result2.ResultText, verifyResult.ResultText);
            Assert.AreEqual(_result2.Active, verifyResult.Active);
            Assert.AreEqual(_result2.LastUpdated, verifyResult.LastUpdated);
        }

        [TestMethod]
        public async Task Update_AttachmentWithUpdatedResults_ReturnsUpdatedAttachmentWithTheUpdatedResults()
        {
            //Arrange
            Attachment entityUsed = _attachment1;

            Attachment entityUsedInDatabase = CreateAttachment(entityUsed.AttachmentId, entityUsed.AttachmentName, entityUsed.AttachmentUrl, entityUsed.LastUpdated, entityUsed.Active);

            entityUsedInDatabase.Results.Add(_result1);
            entityUsedInDatabase.Results.Add(_result2);

            entityUsed.Results.Add(_result2);
            entityUsed.Results.Add(_result3);

            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsedInDatabase);
            _context.Results.AddRange(_result1, _result2, _result3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(2, result.Results.Count);

            // Verify the first result
            Result verifyResult = result.Results.ToList().ElementAt(0);
            Assert.AreEqual(_result2.ResultId, verifyResult.ResultId);
            Assert.AreEqual(_result2.ResultText, verifyResult.ResultText);
            Assert.AreEqual(_result2.Active, verifyResult.Active);
            Assert.AreEqual(_result2.LastUpdated, verifyResult.LastUpdated);
            // Verify the second result
            verifyResult = result.Results.ToList().ElementAt(1);
            Assert.AreEqual(_result3.ResultId, verifyResult.ResultId);
            Assert.AreEqual(_result3.ResultText, verifyResult.ResultText);
            Assert.AreEqual(_result3.Active, verifyResult.Active);
            Assert.AreEqual(_result3.LastUpdated, verifyResult.LastUpdated);
        }

        [TestMethod]
        public async Task Update_AttachmentWithCards_ReturnsUpdatedAttachmentWithCards()
        {
            //Arrange
            var entityUsed = _attachment1;
            var oldEntityUsed = CreateAttachment(entityUsed.AttachmentId, entityUsed.AttachmentName, entityUsed.AttachmentUrl, entityUsed.LastUpdated, entityUsed.Active);
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(oldEntityUsed);
            _context.Cards.AddRange(_card1, _card2);
            await _context.SaveChangesAsync();
            entityUsed.Cards.Add(_card1);
            entityUsed.Cards.Add(_card2);
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(2, result.Cards.Count);
            // Verify the first result
            Card verifyCard = result.Cards.ToList().ElementAt(0);
            Assert.AreEqual(_card1.CardId, verifyCard.CardId);
            Assert.AreEqual(_card1.CardName, verifyCard.CardName);
            Assert.AreEqual(_card1.CardText, verifyCard.CardText);
            Assert.AreEqual(_card1.Active, verifyCard.Active);
            Assert.AreEqual(_card1.LastUpdated, verifyCard.LastUpdated);
            // Verify the second result
            verifyCard = result.Cards.ToList().ElementAt(1);
            Assert.AreEqual(_card2.CardId, verifyCard.CardId);
            Assert.AreEqual(_card2.CardName, verifyCard.CardName);
            Assert.AreEqual(_card2.CardText, verifyCard.CardText);
            Assert.AreEqual(_card2.Active, verifyCard.Active);
            Assert.AreEqual(_card2.LastUpdated, verifyCard.LastUpdated);
        }

        [TestMethod]
        public async Task Update_AttachmentWithUpdateCards_ReturnsUpdatedAttachmentWithTheUpdatedCards()
        {
            //Arrange
            Attachment entityUsed = _attachment1;

            Attachment entityUsedInDatabase = CreateAttachment(entityUsed.AttachmentId, entityUsed.AttachmentName, entityUsed.AttachmentUrl, entityUsed.LastUpdated, entityUsed.Active);

            entityUsedInDatabase.Cards.Add(_card1);
            entityUsedInDatabase.Cards.Add(_card2);

            entityUsed.Cards.Add(_card2);
            entityUsed.Cards.Add(_card3);

            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsedInDatabase);
            _context.Cards.AddRange(_card1, _card2, _card3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(2, result.Cards.Count);

            // Verify the first result
            Card verifyCard = result.Cards.ToList().ElementAt(0);
            Assert.AreEqual(_card2.CardId, verifyCard.CardId);
            Assert.AreEqual(_card2.CardName, verifyCard.CardName);
            Assert.AreEqual(_card2.CardText, verifyCard.CardText);
            Assert.AreEqual(_card2.Active, verifyCard.Active);
            Assert.AreEqual(_card2.LastUpdated, verifyCard.LastUpdated);
            // Verify the second result
            verifyCard = result.Cards.ToList().ElementAt(1);
            Assert.AreEqual(_card3.CardId, verifyCard.CardId);
            Assert.AreEqual(_card3.CardName, verifyCard.CardName);
            Assert.AreEqual(_card3.CardText, verifyCard.CardText);
            Assert.AreEqual(_card3.Active, verifyCard.Active);
            Assert.AreEqual(_card3.LastUpdated, verifyCard.LastUpdated);

        }

        [TestMethod]
        public async Task Update_AttachmentWithUpdateCardsAndResults_ReturnsUpdatedAttachmentWithTheUpdatedCardsAndResults()
        {
            //Arrange
            Attachment entityUsed = _attachment1;

            Attachment entityUsedInDatabase = CreateAttachment(entityUsed.AttachmentId, entityUsed.AttachmentName, entityUsed.AttachmentUrl, entityUsed.LastUpdated, entityUsed.Active);

            entityUsedInDatabase.Cards.Add(_card1);
            entityUsedInDatabase.Cards.Add(_card2);
            entityUsedInDatabase.Results.Add(_result1);
            entityUsedInDatabase.Results.Add(_result2);


            entityUsed.Cards.Add(_card2);
            entityUsed.Cards.Add(_card3);
            entityUsed.Results.Add(_result2);
            entityUsed.Results.Add(_result3);

            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsedInDatabase);
            _context.Cards.AddRange(_card1, _card2, _card3);
            _context.Results.AddRange(_result1, _result2, _result3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(2, result.Cards.Count);
            Assert.AreEqual(2, result.Results.Count);

            // Verify the first result
            Card verifyCard = result.Cards.ToList().ElementAt(0);
            Assert.AreEqual(_card2.CardId, verifyCard.CardId);
            Assert.AreEqual(_card2.CardName, verifyCard.CardName);
            Assert.AreEqual(_card2.CardText, verifyCard.CardText);
            Assert.AreEqual(_card2.Active, verifyCard.Active);
            Assert.AreEqual(_card2.LastUpdated, verifyCard.LastUpdated);
            // Verify the second result
            verifyCard = result.Cards.ToList().ElementAt(1);
            Assert.AreEqual(_card3.CardId, verifyCard.CardId);
            Assert.AreEqual(_card3.CardName, verifyCard.CardName);
            Assert.AreEqual(_card3.CardText, verifyCard.CardText);
            Assert.AreEqual(_card3.Active, verifyCard.Active);
            Assert.AreEqual(_card3.LastUpdated, verifyCard.LastUpdated);


            // Verify the first result
            Result verifyResult = result.Results.ToList().ElementAt(0);
            Assert.AreEqual(_result2.ResultId, verifyResult.ResultId);
            Assert.AreEqual(_result2.ResultText, verifyResult.ResultText);
            Assert.AreEqual(_result2.Active, verifyResult.Active);
            Assert.AreEqual(_result2.LastUpdated, verifyResult.LastUpdated);
            // Verify the second result
            verifyResult = result.Results.ToList().ElementAt(1);
            Assert.AreEqual(_result3.ResultId, verifyResult.ResultId);
            Assert.AreEqual(_result3.ResultText, verifyResult.ResultText);
            Assert.AreEqual(_result3.Active, verifyResult.Active);
            Assert.AreEqual(_result3.LastUpdated, verifyResult.LastUpdated);

        }

        [TestMethod]
        public async Task Update_AttachmentWithCardsAndResults_ReturnsUpdatedAttachmentWithTheUpdatedCardsAndResults()
        {
            //Arrange
            Attachment entityUsed = _attachment1;

            Attachment entityUsedInDatabase = CreateAttachment(entityUsed.AttachmentId, entityUsed.AttachmentName, entityUsed.AttachmentUrl, entityUsed.LastUpdated, entityUsed.Active);

            


            entityUsed.Cards.Add(_card2);
            entityUsed.Cards.Add(_card3);
            entityUsed.Results.Add(_result2);
            entityUsed.Results.Add(_result3);

            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsedInDatabase);
            _context.Cards.AddRange(_card2, _card3);
            _context.Results.AddRange(_result2, _result3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(2, result.Cards.Count);
            Assert.AreEqual(2, result.Results.Count);

            // Verify the first result
            Card verifyCard = result.Cards.ToList().ElementAt(0);
            Assert.AreEqual(_card2.CardId, verifyCard.CardId);
            Assert.AreEqual(_card2.CardName, verifyCard.CardName);
            Assert.AreEqual(_card2.CardText, verifyCard.CardText);
            Assert.AreEqual(_card2.Active, verifyCard.Active);
            Assert.AreEqual(_card2.LastUpdated, verifyCard.LastUpdated);
            // Verify the second result
            verifyCard = result.Cards.ToList().ElementAt(1);
            Assert.AreEqual(_card3.CardId, verifyCard.CardId);
            Assert.AreEqual(_card3.CardName, verifyCard.CardName);
            Assert.AreEqual(_card3.CardText, verifyCard.CardText);
            Assert.AreEqual(_card3.Active, verifyCard.Active);
            Assert.AreEqual(_card3.LastUpdated, verifyCard.LastUpdated);


            // Verify the first result
            Result verifyResult = result.Results.ToList().ElementAt(0);
            Assert.AreEqual(_result2.ResultId, verifyResult.ResultId);
            Assert.AreEqual(_result2.ResultText, verifyResult.ResultText);
            Assert.AreEqual(_result2.Active, verifyResult.Active);
            Assert.AreEqual(_result2.LastUpdated, verifyResult.LastUpdated);
            // Verify the second result
            verifyResult = result.Results.ToList().ElementAt(1);
            Assert.AreEqual(_result3.ResultId, verifyResult.ResultId);
            Assert.AreEqual(_result3.ResultText, verifyResult.ResultText);
            Assert.AreEqual(_result3.Active, verifyResult.Active);
            Assert.AreEqual(_result3.LastUpdated, verifyResult.LastUpdated);

        }

        [TestMethod]
        public async Task Update_AttachmentWithoutCardsAndResults_ReturnsUpdatedAttachmentWithoutCardsAndResults()
        {
            //Arrange
            Attachment entityUsed = _attachment1;

            Attachment entityUsedInDatabase = CreateAttachment(entityUsed.AttachmentId, entityUsed.AttachmentName, entityUsed.AttachmentUrl, entityUsed.LastUpdated, entityUsed.Active);

            entityUsedInDatabase.Cards.Add(_card1);
            entityUsedInDatabase.Cards.Add(_card2);
            entityUsedInDatabase.Results.Add(_result1);
            entityUsedInDatabase.Results.Add(_result2);



            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsedInDatabase);
            _context.Cards.AddRange(_card1, _card2);
            _context.Results.AddRange(_result1, _result2);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(0, result.Cards.Count);
            Assert.AreEqual(0, result.Results.Count);
        }

        #region BaseRepositoryTests

        [TestMethod]
        public async Task Create_NewAttachmentWithId_ReturnsAddedAttachment()
        {
            //Arrange
            var entityUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
        }

        [TestMethod]
        public async Task Create_NewAttachment_ReturnsAddedAttachment()
        {
            //Arrange
            var entityUsed = _attachment1;
            entityUsed.AttachmentId = new();
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(1, result.AttachmentId);
        }

        [TestMethod]
        public async Task Create_AttachmentAlreadyInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_AttachmentInDatabase_ReturnsDeactivatedAttachment()
        {
            //Arrange
            var entityUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Deactivate(entityUsed.AttachmentId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.IsFalse(result.Active);
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
        }
        [TestMethod]
        public async Task Deactivate_AttachmentNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Deactivate(entityUsed.AttachmentId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task Deactivate_AttachmentAlreadyInactiveInDatabase_ReturnsDeactivatedAttachment()
        {
            //Arrange
            var entityUsed = _attachment1;
            entityUsed.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Deactivate(entityUsed.AttachmentId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.IsFalse(result.Active);
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
        }
        [TestMethod]
        public async Task Delete_AttachmentInDatabase_ReturnsAttachment()
        {
            //Arrange
            var entityUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Delete(entityUsed.AttachmentId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
        }
        [TestMethod]
        public async Task Delete_AttachmentNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Delete(entityUsed.AttachmentId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task Get_AttachmentInDatabase_ReturnsAttachment()
        {
            //Arrange
            var entityUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(entityUsed.AttachmentId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Attachment));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.AttachmentId, result.AttachmentId);
        }
        [TestMethod]
        public async Task Get_AttachmentNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _attachment1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(entityUsed.AttachmentId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetAll_AttachmentsInDatabase_ReturnsAllAttachments()
        {
            //Arrange
            var entityUsed = _attachment1;
            var entityUsed2 = _attachment2;
            var entityUsed3 = _attachment3;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsed);
            _context.Attachments.Add(entityUsed2);
            _context.Attachments.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Attachment>));
            Assert.AreEqual(3, result.Count());
        }
        [TestMethod]
        public async Task GetAll_NoAttachmentsInDatabase_ReturnsEmptyList()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Attachment>));
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public async Task GetAll_DeactivatedAttachmentsInDatabase_ReturnsAllAttachments()
        {
            //Arrange
            var entityUsed = _attachment1;
            var entityUsed2 = _attachment2;
            var entityUsed3 = _attachment3;
            entityUsed.Active = false;
            entityUsed2.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.Attachments.Add(entityUsed);
            _context.Attachments.Add(entityUsed2);
            _context.Attachments.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Attachment>));
            Assert.AreEqual(3, result.Count());
        }

        #endregion
    }
}
