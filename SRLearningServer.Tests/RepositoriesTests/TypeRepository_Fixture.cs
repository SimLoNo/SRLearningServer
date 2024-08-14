using Microsoft.EntityFrameworkCore;
using SRLearningServer.Components.Context;
using SRLearningServer.Components.Repositories;
using SRLearningServer.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SRLearningServer.Tests.RepositoriesTests
{
    [TestCategory("UnitTest")]
    [TestClass]
    public class TypeRepository_Fixture
    {
        private readonly DbContextOptions<SRContext> _options;
        private readonly SRContext _context;
        private readonly TypeRepository _repository;

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

        public TypeRepository_Fixture()
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
            _type3 = CreateType(3, "Warning", false, _oldDate);
            _card1 = CreateCard(1, "Card1", "Card1 Text", true, _oldDate);
            _card2 = CreateCard(2, "Card2", "Card2 Text", true, _oldDate);
            _card3 = CreateCard(3, "Card3", "Card3 Text", false, _oldDate);
            _attachment1 = CreateAttachment(1, "Attachment1", "Icon1234.png", true, _oldDate);
            _attachment2 = CreateAttachment(2, "Attachment2", "Icon1234.png", true, _oldDate);
            _attachment3 = CreateAttachment(3, "Attachment3", "Icon1234.png", false, _oldDate);
            _result1 = CreateResult(1, "stands foran signalet", true, _oldDate);
            _result2 = CreateResult(2, "stands foran stop", true, _oldDate);
            _result3 = CreateResult(3, "stands foran warning", false, _oldDate);
        }

        [TestMethod]
        public async Task Create_TypeWithoutCard_TypeCreated()
        {
            //Arrange
            int expectedId = 1;
            var entityUsed = _type1;
            entityUsed.TypeId = new int();
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(expectedId, result.TypeId);
            List<Components.Models.Type> allTypes = await _context.Types.ToListAsync();
            Assert.AreEqual(1, allTypes.Count);
        }

        [TestMethod]
        public async Task Create_TypeWithoutCardTypesAlreadyExistInDatabase_TypeCreated()
        {
            //Arrange
            int expectedId = 2;
            var entityUsed = _type2;
            entityUsed.TypeId = new int();
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(_type1);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(expectedId, result.TypeId);
            List<Components.Models.Type> allTypes = await _context.Types.ToListAsync();
            Assert.AreEqual(2, allTypes.Count);
        }
        [TestMethod]
        public async Task Create_TypeWithCard_TypeCreated()
        {
            //Arrange
            int expectedId = 1;
            var entityUsed = _type1;
            entityUsed.TypeId = new int();
            entityUsed.Cards.Add(_card1);
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(expectedId, result.TypeId);
            List<Components.Models.Type> allTypes = await _context.Types.ToListAsync();
            Assert.AreEqual(1, allTypes.Count);
            List<Card> allCards = await _context.Cards.ToListAsync();
            Assert.AreEqual(1, allCards.Count);
        }
        [TestMethod]
        public async Task Create_TypeWithCardTypesAlreadyExistInDatabase()
        {
            //Arrange
            int expectedId = 2;
            var entityUsed = _type2;
            entityUsed.TypeId = new int();
            entityUsed.Cards.Add(_card1);
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(_type1);
            await _context.SaveChangesAsync();

            //act
            var result = await _repository.Create(entityUsed);
            //assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(expectedId, result.TypeId);
            List<Components.Models.Type> allTypes = await _context.Types.ToListAsync();
            Assert.AreEqual(2, allTypes.Count);
            List<Card> allCards = await _context.Cards.ToListAsync();
            Assert.AreEqual(1, allCards.Count);

        }

        [TestMethod]
        public async Task Create_TypeWithCardThatIsAlreadyInDatabase()
        {
            //Arrange
            int expectedId = 1;
            var entityUsed = _type1;
            entityUsed.TypeId = new int();
            entityUsed.Cards.Add(_card1);
            await _context.Database.EnsureDeletedAsync();
            await _context.Cards.AddAsync(_card1);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(expectedId, result.TypeId);
            List<Components.Models.Type> allTypes = await _context.Types.ToListAsync();
            Assert.AreEqual(1, allTypes.Count);
            List<Card> allCards = await _context.Cards.ToListAsync();
            Assert.AreEqual(1, allCards.Count);

        }

        [TestMethod]
        public async Task Create_TypeWithTwoCardsWithOneAlreadyInDatabase()
        {
            //Arrange
            int expectedId = 1;
            var entityUsed = _type1;
            entityUsed.TypeId = new int();
            entityUsed.Cards.Add(_card1);
            entityUsed.Cards.Add(_card2);
            await _context.Database.EnsureDeletedAsync();
            await _context.Cards.AddAsync(_card1);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(expectedId, result.TypeId);
            List<Components.Models.Type> allTypes = await _context.Types.ToListAsync();
            Assert.AreEqual(1, allTypes.Count);
            List<Card> allCards = await _context.Cards.ToListAsync();
            Assert.AreEqual(2, allCards.Count);

        }
        

        [TestMethod]
        public async Task Deactivate_IdThatIsNotInDatabase_ReturnsNull()
        {
            //Arrange
            int id = _type1.TypeId;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Deactivate(id);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_IdThatIsNotInDatabaseWithOtherTypes_ReturnsNull()
        {
            //Arrange
            int id = _type1.TypeId;
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(_type2);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Deactivate(id);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_IdThatIsInDatabase_ReturnsTypeWithPropertyActiveToFalse()
        {
            //Arrange
            int id = _type1.TypeId;
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(_type1);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Deactivate(id);
            //Assert
            // Asserts the return value.
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(id, result.TypeId);
            Assert.IsFalse(result.Active);

            //Asserts the database value.
            var TypeFromDb = await _context.Types.FindAsync(id);
            Assert.IsNotNull(TypeFromDb);
            Assert.IsInstanceOfType(TypeFromDb, typeof(Components.Models.Type));
            Assert.IsFalse(TypeFromDb.Active);
        }

        [TestMethod]
        public async Task Delete_IdThatExistsInDatabase_ReturnsDeletedType()
        {
            //Arrange
            int id = _type1.TypeId;
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(_type1);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Delete(_type1.TypeId);
            //Assert
            // Asserts the return value.
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(id, result.TypeId);

            //Asserts the database value.
            var TypeFromDb = await _context.Types.FindAsync(id);
            Assert.IsNull(TypeFromDb);
        }

        [TestMethod]
        public async Task Delete_TypeThatExistsAlongWithOtherInDatabase_ReturnsDeletedType()
        {
            //Arrange
            int id = _type1.TypeId;
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(_type1);
            await _context.Types.AddAsync(_type2);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Delete(_type1.TypeId);
            //Assert
            // Asserts the return value.
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(id, result.TypeId);

            //Asserts the database value.
            var TypeFromDb = await _context.Types.FindAsync(id);
            Assert.IsNull(TypeFromDb);

            //Asserts The other Type is still in the database.
            var TypeFromDb2 = await _context.Types.FindAsync(_type2.TypeId);
            Assert.IsNotNull(TypeFromDb2);
        }

        [TestMethod]
        public async Task Delete_TypeWithRelatedCards_ReturnsDeletedTypeWithCardsStillInDatabase()
        {
            //Arrange
            var cardUsed = _card1;
            var typeUsed = _type1;
            typeUsed.Cards.Add(cardUsed);
            await _context.Database.EnsureDeletedAsync();
            await _context.Cards.AddAsync(cardUsed);
            await _context.Types.AddAsync(typeUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Delete(typeUsed.TypeId);
            //Assert
            // Asserts the return value.
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(typeUsed.TypeId, result.TypeId);

            //Asserts the Type is not in the database.
            var TypeFromDb = await _context.Types.FindAsync(typeUsed.TypeId);
            Assert.IsNull(TypeFromDb);

            //Asserts the Card is still in the database.
            var cardFromDb = await _context.Cards.FindAsync(cardUsed.CardId);
            Assert.IsNotNull(cardFromDb);
        }

        

        [TestMethod]
        public async Task Get_TypeThatExistsInDatabase_ReturnsType()
        {
            //Arrange
            var typeUsed = _type1;
            int id = typeUsed.TypeId;
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(typeUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(id);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(id, result.TypeId);
        }

        [TestMethod]
        public async Task Get_TypeThatExistInDatabaseWithOthers_ReturnsType()
        {
            //Arrange
            var typeUsed = _type1;
            var typeUsed2 = _type2;
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(typeUsed);
            await _context.Types.AddAsync(typeUsed2);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(typeUsed.TypeId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(typeUsed.TypeId, result.TypeId);
        }

        [TestMethod]
        public async Task Get_TypeWithCards_ReturnsTypeWithCards()
        {
            //Arrange
            var typeUsed = _type1;
            var cardUsed = _card1;
            typeUsed.Cards.Add(cardUsed);
            await _context.Database.EnsureDeletedAsync();
            await _context.Cards.AddAsync(cardUsed);
            await _context.Types.AddAsync(typeUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(typeUsed.TypeId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(typeUsed.TypeId, result.TypeId);
            Assert.AreEqual(1, result.Cards.Count);

        }

        [TestMethod]
        public async Task Get_TypeThatDoesNotExistInDatabase_ReturnsNull()
        {
            //Arrange
            int id = _type1.TypeId;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Get(id);
            //Assert
            Assert.IsNull(result);
        }

        

        [TestMethod]
        public async Task Update_TypeThatDoesNotExistInDatabase_ReturnsNull()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(_type1);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_TypeWithUpdatedName_ReturnsUpdatedType()
        {
            //Arrange
            var typeUsed = _type1;
            var oldEntityUsed = CreateType(typeUsed.TypeId, typeUsed.CardTypeName, typeUsed.Active, typeUsed.LastUpdated);
            string newName = "TotallyNewName";
            typeUsed.CardTypeName = newName;
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(oldEntityUsed);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(typeUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(typeUsed.TypeId, result.TypeId);
            Assert.AreEqual(newName, result.CardTypeName);
        }

        [TestMethod]
        public async Task Update_TypeWithUpdatedActiveToInactive_ReturnsUpdatedType()
        {
            //Arrange
            var typeUsed = _type3;
            var oldEntityUsed = CreateType(typeUsed.TypeId, typeUsed.CardTypeName, true, typeUsed.LastUpdated);
            Assert.AreNotEqual(oldEntityUsed.Active, typeUsed.Active);
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(oldEntityUsed);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(typeUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(typeUsed.TypeId, result.TypeId);
            Assert.AreEqual(false, result.Active);
        }

        [TestMethod]
        public async Task Update_TypeWithUpdatedInactiveToActive_ReturnsUpdatedType()
        {
            //Arrange
            var typeUsed = _type1;
            var oldEntityUsed = CreateType(typeUsed.TypeId, typeUsed.CardTypeName, false, typeUsed.LastUpdated);
            Assert.AreNotEqual(oldEntityUsed.Active, typeUsed.Active);
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(oldEntityUsed);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Update(typeUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(typeUsed.TypeId, result.TypeId);
            Assert.AreEqual(true, result.Active);
        }

        [TestMethod]
        public async Task Update_TypeWithUpdatedNameAndActive_ReturnsUpdatedType()
        {
            //Arrange
            var typeUsed = _type1;
            var oldEntityUsed = CreateType(typeUsed.TypeId, typeUsed.CardTypeName, typeUsed.Active, typeUsed.LastUpdated);
            bool newActive = false;
            string newName = "TotallyNewName";

            Assert.AreNotEqual(newActive, oldEntityUsed.Active);
            Assert.AreNotEqual(newName, oldEntityUsed.CardTypeName);

            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(oldEntityUsed);
            await _context.SaveChangesAsync();

            typeUsed.Active = newActive;
            typeUsed.CardTypeName = newName;

            //Act
            var result = await _repository.Update(typeUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(typeUsed.TypeId, result.TypeId);
            Assert.AreEqual(newActive, result.Active);
            Assert.AreEqual(newName, result.CardTypeName);
        }

        [TestMethod]
        public async Task Update_Type_ReturnsUpdatedTypeWithLastUpdated()
        {
            //Arrange
            var typeUsed = _type1;
            var oldEntityUsed = CreateType(typeUsed.TypeId, typeUsed.CardTypeName, typeUsed.Active, typeUsed.LastUpdated);

            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(oldEntityUsed);
            await _context.SaveChangesAsync();


            //Act
            var result = await _repository.Update(typeUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(typeUsed.TypeId, result.TypeId);
            Assert.AreEqual(_currentDate, result.LastUpdated);
        }

        [TestMethod]
        public async Task Create_NewType_ReturnsNewlyCreatedType()
        {
            //Arrange
            var entityUsed = _type1;
            entityUsed.TypeId = new int();
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Create(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(1, result.TypeId);
            Assert.AreEqual(entityUsed.CardTypeName, result.CardTypeName);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.UtcNow), result.LastUpdated);

        }

        [TestMethod]
        public async Task Create_NewType_ReturnsNewlyCreatedTypeWithCorrectLastUpdated()
        {
            //Arrange
            var entityUsed = _type1;
            entityUsed.TypeId = new int();
            entityUsed.LastUpdated = DateOnly.FromDateTime(DateTime.Now.AddDays(-5));
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Create(entityUsed);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.UtcNow), result.LastUpdated);

        }

        [TestMethod]
        public async Task Create_NewTypeAlreadyExists_ThrowsException()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(entityUsed);
            await _context.SaveChangesAsync();

            //Act - Assert
            Assert.ThrowsExceptionAsync<Exception>(() => _repository.Create(entityUsed));
        }

        [TestMethod]
        public async Task Get_IdFoundInDatabase_ReturnsType()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddAsync(entityUsed);
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Get(entityUsed.TypeId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Components.Models.Type));
            Assert.AreEqual(entityUsed.TypeId, result.TypeId);
            Assert.AreEqual(entityUsed.CardTypeName, result.CardTypeName);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
        }

        [TestMethod]
        public async Task Get_IdNotFoundInDatabase_ReturnsNull()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            //Act
            var result = await _repository.Get(_type1.TypeId);

            //Assert
            Assert.IsNull(result);
        }

        #region BaseRepositoryTests

        [TestMethod]
        public async Task Create_NewTypeWithId_ReturnsAddedType()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(type);
            Assert.IsInstanceOfType(type, typeof(Components.Models.Type));
            Assert.AreEqual(entityUsed.CardTypeName, type.CardTypeName);
            Assert.AreEqual(entityUsed.Active, type.Active);
            Assert.AreEqual(_currentDate, type.LastUpdated);
            Assert.AreEqual(entityUsed.TypeId, type.TypeId);
        }

        [TestMethod]
        public async Task Create_NewType_ReturnsAddedType()
        {
            //Arrange
            var entityUsed = _type1;
            entityUsed.TypeId = new();
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(type);
            Assert.IsInstanceOfType(type, typeof(Components.Models.Type));
            Assert.AreEqual(entityUsed.CardTypeName, type.CardTypeName);
            Assert.AreEqual(entityUsed.Active, type.Active);
            Assert.AreEqual(_currentDate, type.LastUpdated);
            Assert.AreEqual(1, type.TypeId);
        }

        [TestMethod]
        public async Task Create_TypeAlreadyInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNull(type);
        }

        [TestMethod]
        public async Task Deactivate_TypeInDatabase_ReturnsDeactivatedType()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Deactivate(entityUsed.TypeId);
            //Assert
            Assert.IsNotNull(type);
            Assert.IsInstanceOfType(type, typeof(Components.Models.Type));
            Assert.IsFalse(type.Active);
            Assert.AreEqual(entityUsed.TypeId, type.TypeId);
            Assert.AreEqual(entityUsed.CardTypeName, type.CardTypeName);
            Assert.AreEqual(_currentDate, type.LastUpdated);

        }
        [TestMethod]
        public async Task Deactivate_TypeNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Deactivate(entityUsed.TypeId);
            //Assert
            Assert.IsNull(type);
        }
        [TestMethod]
        public async Task Deactivate_TypeAlreadyInactiveInDatabase_ReturnsDeactivatedType()
        {
            //Arrange
            var entityUsed = _type1;
            entityUsed.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Deactivate(entityUsed.TypeId);
            //Assert
            Assert.IsNotNull(type);
            Assert.IsInstanceOfType(type, typeof(Components.Models.Type));
            Assert.IsFalse(type.Active);
            Assert.AreEqual(entityUsed.TypeId, type.TypeId);
            Assert.AreEqual(entityUsed.CardTypeName, type.CardTypeName);
            Assert.AreEqual(_currentDate, type.LastUpdated);
        }
        [TestMethod]
        public async Task Delete_TypeInDatabase_ReturnsType()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Delete(entityUsed.TypeId);
            //Assert
            Assert.IsNotNull(type);
            Assert.IsInstanceOfType(type, typeof(Components.Models.Type));
            Assert.AreEqual(entityUsed.CardTypeName, type.CardTypeName);
            Assert.AreEqual(entityUsed.Active, type.Active);
            Assert.AreEqual(entityUsed.LastUpdated, type.LastUpdated);
            Assert.AreEqual(entityUsed.TypeId, type.TypeId);
        }
        [TestMethod]
        public async Task Delete_TypeNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Delete(entityUsed.TypeId);
            //Assert
            Assert.IsNull(type);
        }
        [TestMethod]
        public async Task Get_TypeInDatabase_ReturnsType()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Get(entityUsed.TypeId);
            //Assert
            Assert.IsNotNull(type);
            Assert.IsInstanceOfType(type, typeof(Components.Models.Type));
            Assert.AreEqual(entityUsed.CardTypeName, type.CardTypeName);
            Assert.AreEqual(entityUsed.Active, type.Active);
            Assert.AreEqual(entityUsed.LastUpdated, type.LastUpdated);
            Assert.AreEqual(entityUsed.TypeId, type.TypeId);
        }
        [TestMethod]
        public async Task Get_TypeNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _type1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.Get(entityUsed.TypeId);
            //Assert
            Assert.IsNull(type);
        }
        [TestMethod]
        public async Task GetAll_TypesInDatabase_ReturnsAllTypes()
        {
            //Arrange
            var entityUsed = _type1;
            var entityUsed2 = _type2;
            var entityUsed3 = _type3;
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(entityUsed);
            _context.Types.Add(entityUsed2);
            _context.Types.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(type);
            Assert.IsInstanceOfType(type, typeof(IEnumerable<Components.Models.Type>));
            Assert.AreEqual(3, type.Count());
        }
        [TestMethod]
        public async Task GetAll_NoTypesInDatabase_ReturnsEmptyList()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(type);
            Assert.IsInstanceOfType(type, typeof(IEnumerable<Components.Models.Type>));
            Assert.AreEqual(0, type.Count());
        }
        [TestMethod]
        public async Task GetAll_DeactivatedTypesInDatabase_ReturnsAllTypes()
        {
            //Arrange
            var entityUsed = _type1;
            var entityUsed2 = _type2;
            var entityUsed3 = _type3;
            entityUsed.Active = false;
            entityUsed2.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(entityUsed);
            _context.Types.Add(entityUsed2);
            _context.Types.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var type = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(type);
            Assert.IsInstanceOfType(type, typeof(IEnumerable<Components.Models.Type>));
            Assert.AreEqual(3, type.Count());
        }

        #endregion

        /*[TestMethod]
        public async Task GetMultiple_EmptyList_ReturnsAllActiveTypes()
        {
            //Arrange
            List<Components.Models.Type> types = new List<Components.Models.Type>();
            types.Add(_type1);
            types.Add(_type2);
            types.Add(_type3);
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddRangeAsync(types);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetMultiple(new List<Components.Models.Type>());
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Components.Models.Type>));
            Assert.AreEqual(2, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(item.Active);
            }
        }

        [TestMethod]
        public async Task GetMultiple_ListOfTypes_ReturnsAllActiveTypes()
        {
            //Arrange
            List<Components.Models.Type> types = new List<Components.Models.Type>();
            types.Add(_type1);
            types.Add(_type2);
            types.Add(_type3);
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddRangeAsync(types);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetMultiple(types);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Components.Models.Type>));
            Assert.AreEqual(2, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(item.Active);
            }
        }

        [TestMethod]
        public async Task GetMultiple_ListOfTypesWithOnlyId_ReturnsAllActiveTypes()
        {
            //Arrange
            List<Components.Models.Type> types = new List<Components.Models.Type>();
            types.Add(_type1);
            types.Add(_type2);
            types.Add(_type3);
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddRangeAsync(types);
            await _context.SaveChangesAsync();

            foreach (Components.Models.Type item in types)
            {
                item.CardTypeName = null;
            }
            //Act
            var result = await _repository.GetMultiple(types);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Components.Models.Type>));
            Assert.AreEqual(2, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(item.Active);
            }
        }

        [TestMethod]
        public async Task GetMultiple_ListOfTypesWithOnlyNames_ReturnsAllActiveTypes()
        {
            //Arrange
            List<Components.Models.Type> types = new List<Components.Models.Type>();
            types.Add(_type1);
            types.Add(_type2);
            types.Add(_type3);
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddRangeAsync(types);
            await _context.SaveChangesAsync();

            foreach (Components.Models.Type item in types)
            {
                item.TypeId = new();
            }
            //Act
            var result = await _repository.GetMultiple(types);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Components.Models.Type>));
            Assert.AreEqual(2, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(item.Active);
            }
        }

        [TestMethod]
        public async Task GetMultiple_ListOfTypesOneWithNameOneWithId_ReturnsAllActiveTypes()
        {
            //Arrange
            List<Components.Models.Type> types = new List<Components.Models.Type>();
            types.Add(_type1);
            types.Add(_type2);
            types.Add(_type3);
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddRangeAsync(types);
            await _context.SaveChangesAsync();

            types[0].TypeId = new();
            types[1].CardTypeName = null;
            //Act
            var result = await _repository.GetMultiple(types);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Components.Models.Type>));
            Assert.AreEqual(2, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(item.Active);
            }
        }

        [TestMethod]
        public async Task GetMultiple_ListOfInactiveTypes_ReturnsAllActiveTypes()
        {
            //Arrange
            List<Components.Models.Type> types = new List<Components.Models.Type>();
            types.Add(_type1);
            types.Add(_type2);
            types.Add(_type3);
            foreach (var item in types)
            {
                item.Active = false;
            }
            await _context.Database.EnsureDeletedAsync();
            await _context.Types.AddRangeAsync(types);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.GetMultiple(types);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Components.Models.Type>));
            Assert.AreEqual(0, result.Count());
        }*/
    }
}
