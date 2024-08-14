using Microsoft.EntityFrameworkCore;
using SRLearningServer.Components.Context;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Repositories;
using SRLearningServer.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLearningServer.Tests.RepositoriesTests
{
    [TestClass]
    [TestCategory("UnitTest")]
    public class TypeCategoryListRepository_Fixture
    {
        private readonly DbContextOptions<SRContext> _options;
        private readonly SRContext _context;
        private readonly TypeCategoryListRepository _repository;
        private readonly TestDataGenerator _testDataGenerator = new();

        private TypeCategoryList _typeCategoryList1;
        private TypeCategoryList _typeCategoryList2;
        private TypeCategoryList _typeCategoryList3;

        private Components.Models.Type _type1;
        private Components.Models.Type _type2;
        private Components.Models.Type _type3;
        private Components.Models.Type _type4;
        private Components.Models.Type _type5;

        private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Now);
        private DateOnly _oldDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        public TypeCategoryListRepository_Fixture()
        {
            _options = new DbContextOptionsBuilder<SRContext>()
                .UseInMemoryDatabase(databaseName: "SRContext")
                .Options;
            _context = new SRContext(_options);
            _repository = new TypeCategoryListRepository(_context);
        }

        [TestInitialize]
        public void WireUp()
        {
            _typeCategoryList1 = _testDataGenerator.CreateTypeCategoryList(1, "TypeCategoryList1", _oldDate, true);
            _typeCategoryList2 = _testDataGenerator.CreateTypeCategoryList(2, "TypeCategoryList2", _oldDate, true);
            _typeCategoryList3 = _testDataGenerator.CreateTypeCategoryList(3, "TypeCategoryList3", _oldDate, true);

            _type1 = _testDataGenerator.CreateType(1, "Type1", _oldDate, true);
            _type2 = _testDataGenerator.CreateType(2, "Type2", _oldDate, true);
            _type3 = _testDataGenerator.CreateType(3, "Type3", _oldDate, true);

        }

        [TestMethod]
        public async Task GetByName_TypeCategoryListInDatabase_ReturnsTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.GetByName(entityUsed.TypeCategoryListName);
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(TypeCategoryList));

            Assert.AreEqual(entityUsed.TypeCategoryListName, typeCategoryList.TypeCategoryListName);
            Assert.AreEqual(entityUsed.Active, typeCategoryList.Active);
            Assert.AreEqual(entityUsed.LastUpdated, typeCategoryList.LastUpdated);
            Assert.AreEqual(entityUsed.TypeCategoryListId, typeCategoryList.TypeCategoryListId);
        }
        [TestMethod]
        public async Task GetByName_TypeCategoryListNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.GetByName(entityUsed.TypeCategoryListName);
            //Assert
            Assert.IsNull(typeCategoryList);
        }
        [TestMethod]
        public async Task Update_TypeCategoryListInDatabase_ReturnsUpdatedTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            var oldEntityUsed = _testDataGenerator.CreateTypeCategoryList(entityUsed.TypeCategoryListId, "RandomName", _oldDate, false);
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(oldEntityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryList));

            Assert.AreEqual(entityUsed.TypeCategoryListName, result.TypeCategoryListName);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);
        }
        [TestMethod]
        public async Task Update_TypeCategoryListNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task Update_TypeCategoryListWithNewType_ReturnsUpdatedTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            var oldEntityUsed = _testDataGenerator.CreateTypeCategoryList(entityUsed.TypeCategoryListId, entityUsed.TypeCategoryListName, entityUsed.LastUpdated, entityUsed.Active);
            var type = _type1;
            var type2 = _type2;
            entityUsed.Types.Add(type);
            entityUsed.Types.Add(type2);
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(oldEntityUsed);
            _context.Types.Add(type);
            _context.Types.Add(type2);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryList));
            Assert.AreEqual(2, result.Types.Count);

            Assert.AreEqual(entityUsed.TypeCategoryListName, result.TypeCategoryListName);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);

            //First Type
            var typeChecked = result.Types.ToList().ElementAt(0);
            Assert.AreEqual(type.TypeId, typeChecked.TypeId);
            Assert.AreEqual(type.CardTypeName, typeChecked.CardTypeName);
            Assert.AreEqual(type.Active, typeChecked.Active);
            Assert.AreEqual(type.LastUpdated, typeChecked.LastUpdated);

            //Second Type
            var typeChecked2 = result.Types.ToList().ElementAt(1);
            Assert.AreEqual(type2.TypeId, typeChecked2.TypeId);
            Assert.AreEqual(type2.CardTypeName, typeChecked2.CardTypeName);
            Assert.AreEqual(type2.Active, typeChecked2.Active);
            Assert.AreEqual(type2.LastUpdated, typeChecked2.LastUpdated);
        }

        [TestMethod]
        public async Task Update_TypeCategoryListRemovesTypes_ReturnsUpdatedTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            var oldEntityUsed = _testDataGenerator.CreateTypeCategoryList(entityUsed.TypeCategoryListId, entityUsed.TypeCategoryListName, entityUsed.LastUpdated, entityUsed.Active);
            var type = _type1;
            var type2 = _type2;

            oldEntityUsed.Types.Add(type);
            oldEntityUsed.Types.Add(type2);
            entityUsed.Types.Add(type);
            await _context.Database.EnsureDeletedAsync();
            _context.Types.Add(type);
            _context.Types.Add(type2);
            _context.TypeCategoryLists.Add(oldEntityUsed);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryList));
            Assert.AreEqual(1, result.Types.Count);

            Assert.AreEqual(entityUsed.TypeCategoryListName, result.TypeCategoryListName);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);

            //First Type
            var typeChecked = result.Types.ToList().ElementAt(0);
            Assert.AreEqual(type.TypeId, typeChecked.TypeId);
            Assert.AreEqual(type.CardTypeName, typeChecked.CardTypeName);
            Assert.AreEqual(type.Active, typeChecked.Active);
            Assert.AreEqual(type.LastUpdated, typeChecked.LastUpdated);

        }

        [TestMethod]
        public async Task Update_TypeCategoryChangesType_ReturnsUpdatedTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            var oldEntityUsed = _testDataGenerator.CreateTypeCategoryList(entityUsed.TypeCategoryListId, entityUsed.TypeCategoryListName, entityUsed.LastUpdated, entityUsed.Active);
            var type = _type1;
            var type2 = _type2;
            var type3 = _type3;
            oldEntityUsed.Types.Add(type);
            oldEntityUsed.Types.Add(type3);
            entityUsed.Types.Add(type);
            entityUsed.Types.Add(type2);
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(oldEntityUsed);
            _context.Types.Add(type);
            _context.Types.Add(type2);
            _context.Types.Add(type3);
            await _context.SaveChangesAsync();
            //Act
            var result = await _repository.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryList));
            Assert.AreEqual(2, result.Types.Count);

            Assert.AreEqual(entityUsed.TypeCategoryListName, result.TypeCategoryListName);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(_currentDate, result.LastUpdated);
            Assert.AreEqual(entityUsed.TypeCategoryListId, result.TypeCategoryListId);

            //First Type
            var typeChecked = result.Types.ToList().ElementAt(0);
            Assert.AreEqual(type.TypeId, typeChecked.TypeId);
            Assert.AreEqual(type.CardTypeName, typeChecked.CardTypeName);
            Assert.AreEqual(type.Active, typeChecked.Active);
            Assert.AreEqual(type.LastUpdated, typeChecked.LastUpdated);

            //Second Type
            var typeChecked2 = result.Types.ToList().ElementAt(1);
            Assert.AreEqual(type2.TypeId, typeChecked2.TypeId);
            Assert.AreEqual(type2.CardTypeName, typeChecked2.CardTypeName);
            Assert.AreEqual(type2.Active, typeChecked2.Active);
            Assert.AreEqual(type2.LastUpdated, typeChecked2.LastUpdated);
        }





        #region BaseRepositoryTests

        [TestMethod]
        public async Task Create_NewTypeCategoryListWithId_ReturnsAddedTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(TypeCategoryList));
            Assert.AreEqual(entityUsed.TypeCategoryListName, typeCategoryList.TypeCategoryListName);
            Assert.AreEqual(entityUsed.Active, typeCategoryList.Active);
            Assert.AreEqual(_currentDate, typeCategoryList.LastUpdated);
            Assert.AreEqual(entityUsed.TypeCategoryListId, typeCategoryList.TypeCategoryListId);
        }

        [TestMethod]
        public async Task Create_NewTypeCategoryList_ReturnsAddedTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            entityUsed.TypeCategoryListId = new();
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(TypeCategoryList));
            Assert.AreEqual(entityUsed.TypeCategoryListName, typeCategoryList.TypeCategoryListName);
            Assert.AreEqual(entityUsed.Active, typeCategoryList.Active);
            Assert.AreEqual(_currentDate, typeCategoryList.LastUpdated);
            Assert.AreEqual(1, typeCategoryList.TypeCategoryListId);
        }

        [TestMethod]
        public async Task Create_TypeCategoryListAlreadyInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Create(entityUsed);
            //Assert
            Assert.IsNull(typeCategoryList);
        }

        [TestMethod]
        public async Task Deactivate_TypeCategoryListInDatabase_ReturnsDeactivatedTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Deactivate(entityUsed.TypeCategoryListId);
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(TypeCategoryList));
            Assert.IsFalse(typeCategoryList.Active);
            Assert.AreEqual(entityUsed.TypeCategoryListId, typeCategoryList.TypeCategoryListId);
            Assert.AreEqual(entityUsed.TypeCategoryListName, typeCategoryList.TypeCategoryListName);
            Assert.AreEqual(_currentDate, typeCategoryList.LastUpdated);

        }
        [TestMethod]
        public async Task Deactivate_TypeCategoryListNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Deactivate(entityUsed.TypeCategoryListId);
            //Assert
            Assert.IsNull(typeCategoryList);
        }
        [TestMethod]
        public async Task Deactivate_TypeCategoryListAlreadyInactiveInDatabase_ReturnsDeactivatedTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            entityUsed.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Deactivate(entityUsed.TypeCategoryListId);
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(TypeCategoryList));
            Assert.IsFalse(typeCategoryList.Active);
            Assert.AreEqual(entityUsed.TypeCategoryListId, typeCategoryList.TypeCategoryListId);

            Assert.AreEqual(entityUsed.TypeCategoryListName, typeCategoryList.TypeCategoryListName);
            Assert.AreEqual(_currentDate, typeCategoryList.LastUpdated);
        }
        [TestMethod]
        public async Task Delete_TypeCategoryListInDatabase_ReturnsTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Delete(entityUsed.TypeCategoryListId);
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(TypeCategoryList));

            Assert.AreEqual(entityUsed.TypeCategoryListName, typeCategoryList.TypeCategoryListName);
            Assert.AreEqual(entityUsed.Active, typeCategoryList.Active);
            Assert.AreEqual(entityUsed.LastUpdated, typeCategoryList.LastUpdated);
            Assert.AreEqual(entityUsed.TypeCategoryListId, typeCategoryList.TypeCategoryListId);
        }
        [TestMethod]
        public async Task Delete_TypeCategoryListNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Delete(entityUsed.TypeCategoryListId);
            //Assert
            Assert.IsNull(typeCategoryList);
        }
        [TestMethod]
        public async Task Get_TypeCategoryListInDatabase_ReturnsTypeCategoryList()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(entityUsed);
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Get(entityUsed.TypeCategoryListId);
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(TypeCategoryList));

            Assert.AreEqual(entityUsed.TypeCategoryListName, typeCategoryList.TypeCategoryListName);
            Assert.AreEqual(entityUsed.Active, typeCategoryList.Active);
            Assert.AreEqual(entityUsed.LastUpdated, typeCategoryList.LastUpdated);
            Assert.AreEqual(entityUsed.TypeCategoryListId, typeCategoryList.TypeCategoryListId);
        }
        [TestMethod]
        public async Task Get_TypeCategoryListNotInDatabase_ReturnsNull()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.Get(entityUsed.TypeCategoryListId);
            //Assert
            Assert.IsNull(typeCategoryList);
        }
        [TestMethod]
        public async Task GetAll_TypeCategoryListsInDatabase_ReturnsAllTypeCategoryLists()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            var entityUsed2 = _typeCategoryList2;
            var entityUsed3 = _typeCategoryList3;
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(entityUsed);
            _context.TypeCategoryLists.Add(entityUsed2);
            _context.TypeCategoryLists.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(IEnumerable<TypeCategoryList>));
            Assert.AreEqual(3, typeCategoryList.Count());
        }
        [TestMethod]
        public async Task GetAll_NoTypeCategoryListsInDatabase_ReturnsEmptyList()
        {
            //Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(IEnumerable<TypeCategoryList>));
            Assert.AreEqual(0, typeCategoryList.Count());
        }
        [TestMethod]
        public async Task GetAll_DeactivatedTypeCategoryListsInDatabase_ReturnsAllTypeCategoryLists()
        {
            //Arrange
            var entityUsed = _typeCategoryList1;
            var entityUsed2 = _typeCategoryList2;
            var entityUsed3 = _typeCategoryList3;
            entityUsed.Active = false;
            entityUsed2.Active = false;
            await _context.Database.EnsureDeletedAsync();
            _context.TypeCategoryLists.Add(entityUsed);
            _context.TypeCategoryLists.Add(entityUsed2);
            _context.TypeCategoryLists.Add(entityUsed3);
            await _context.SaveChangesAsync();
            //Act
            var typeCategoryList = await _repository.GetAll();
            //Assert
            Assert.IsNotNull(typeCategoryList);
            Assert.IsInstanceOfType(typeCategoryList, typeof(IEnumerable<TypeCategoryList>));
            Assert.AreEqual(3, typeCategoryList.Count());
        }

        #endregion
    }


}
