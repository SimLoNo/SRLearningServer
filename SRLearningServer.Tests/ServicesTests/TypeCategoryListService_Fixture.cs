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
    public class TypeCategoryListService_Fixture
    {
        private readonly Mock<ITypeCategoryListRepository> _mockTypeCategoryListRepository;
        private readonly Mock<IDomainToDtoConverter> _mockDomainToDtoConverter;
        private readonly Mock<IDtoToDomainConverter> _mockDtoToDomainConverter;
        private readonly ITypeCategoryListService _typeCategoryListService;
        private readonly TestDataGenerator _testDataGenerator;

        private TypeCategoryList _typeCategoryList1;
        private TypeCategoryList _typeCategoryList2;

        private TypeCategoryListDto _typeCategoryListDto1;
        private TypeCategoryListDto _typeCategoryListDto2;

        private Components.Models.Type _type1;
        private Components.Models.Type _type2;

        private TypeDto _typeDto1;
        private TypeDto _typeDto2;


        public TypeCategoryListService_Fixture()
        {
            _testDataGenerator = new TestDataGenerator();
            _mockTypeCategoryListRepository = new Mock<ITypeCategoryListRepository>();
            _mockDomainToDtoConverter = new Mock<IDomainToDtoConverter>();
            _mockDtoToDomainConverter = new Mock<IDtoToDomainConverter>();
            _typeCategoryListService = new TypeCategoryListService(_mockDtoToDomainConverter.Object, _mockDomainToDtoConverter.Object, _mockTypeCategoryListRepository.Object);
        }


        [TestInitialize]
        public void TestInitialize()
        {
            _typeCategoryListDto1 = _testDataGenerator.CreateTypeCategoryListDto(1, "typeCategoryList1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeCategoryListDto2 = _testDataGenerator.CreateTypeCategoryListDto(2, "typeCategoryList2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _typeCategoryList1 = _testDataGenerator.CreateTypeCategoryListFromDto(_typeCategoryListDto1);
            _typeCategoryList2 = _testDataGenerator.CreateTypeCategoryListFromDto(_typeCategoryListDto2);

            _typeDto1 = _testDataGenerator.CreateTypeDto(1, "type1", DateOnly.FromDateTime(DateTime.UtcNow), true);
            _typeDto2 = _testDataGenerator.CreateTypeDto(2, "type2", DateOnly.FromDateTime(DateTime.UtcNow), true);

            _type1 = _testDataGenerator.CreateTypeFromDto(_typeDto1);
            _type2 = _testDataGenerator.CreateTypeFromDto(_typeDto2);
        }

        [TestMethod]
        public async Task Create_TypeCategoryListDto_ReturnsTypeCategoryListDto()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeCategoryListDto1)).Returns(_typeCategoryList1);
            _mockTypeCategoryListRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync((TypeCategoryList)null);
            _mockTypeCategoryListRepository.Setup(x => x.Create(It.IsAny<TypeCategoryList>())).ReturnsAsync(_typeCategoryList1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Create(_typeCategoryListDto1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(_typeCategoryListDto1, result);
        }
        [TestMethod]
        public async Task Create_TypeCategoryListWithActiveNameAlreadyInDatabase_ReturnsNull()
        {
            //Arrange

            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeCategoryListDto1)).Returns(_typeCategoryList1);
            _mockTypeCategoryListRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync(_typeCategoryList1);
            _mockTypeCategoryListRepository.Setup(x => x.Create(It.IsAny<TypeCategoryList>())).ReturnsAsync(_typeCategoryList1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);
            //Act
            var result = await _typeCategoryListService.Create(_typeCategoryListDto1);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Create_TypeCategoryListDto_ReturnsNullWhenNotCreated()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeCategoryListDto1)).Returns(_typeCategoryList1);
            _mockTypeCategoryListRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync((TypeCategoryList)null);
            _mockTypeCategoryListRepository.Setup(x => x.Create(It.IsAny<TypeCategoryList>())).ReturnsAsync((TypeCategoryList)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Create(_typeCategoryListDto1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Create_TypeCategoryListDto_ReturnsNullWhenExceptionisThrown()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeCategoryListDto1)).Returns(_typeCategoryList1);
            _mockTypeCategoryListRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync((TypeCategoryList)null);
            _mockTypeCategoryListRepository.Setup(x => x.Create(It.IsAny<TypeCategoryList>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Create(_typeCategoryListDto1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_Int_ReturnsTypeCategoryListDto()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_typeCategoryList1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Deactivate(_typeCategoryListDto1.TypeCategoryListId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(_typeCategoryListDto1, result);
        }

        [TestMethod]
        public async Task Deactivate_Int_ReturnsNullWhenNotDeactivated()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync((TypeCategoryList)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Deactivate(_typeCategoryListDto1.TypeCategoryListId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_Int_ReturnsNullWhenExceptionIsThrown()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Deactivate(_typeCategoryListDto1.TypeCategoryListId);

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public async Task Deactivate_TypeCategoryList_ReturnsTypeCategoryListDto()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_typeCategoryList1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Deactivate(_typeCategoryListDto1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(_typeCategoryListDto1, result);
        }

        [TestMethod]
        public async Task Deactivate_TypeCategoryList_ReturnsNullWhenNotDeactivated()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync((TypeCategoryList)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Deactivate(_typeCategoryListDto1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_TypeCategoryList_ReturnsNullWhenExceptionIsThrown()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Deactivate(_typeCategoryListDto1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Delete_Int_ReturnsTypeCategoryListDtoWhenDeleted()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(_typeCategoryList1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Delete(_typeCategoryListDto1.TypeCategoryListId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(_typeCategoryListDto1, result);
        }

        [TestMethod]
        public async Task Delete_Int_ReturnsNullWhenNotDeleted()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync((TypeCategoryList)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Delete(_typeCategoryListDto1.TypeCategoryListId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Delete_Int_ReturnsNullWhenExceptionThrown()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Delete(_typeCategoryListDto1.TypeCategoryListId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Get_Int_ReturnsTypeCategoryListDto()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(_typeCategoryList1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Get(_typeCategoryListDto1.TypeCategoryListId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(_typeCategoryListDto1, result);
        }

        [TestMethod]
        public async Task Get_Int_ReturnsNullWhenNotFound()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((TypeCategoryList)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Get(_typeCategoryListDto1.TypeCategoryListId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Get_Int_ReturnsNullWhenExceptionThrown()
        {
            // Arrange
            _mockTypeCategoryListRepository.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Get(_typeCategoryListDto1.TypeCategoryListId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAll_ReturnsListOfTypeCategoryListDto()
        {
            // Arrange
            List<TypeCategoryList> typeCategoryLists = new List<TypeCategoryList> { _typeCategoryList1, _typeCategoryList2 };
            List<TypeCategoryListDto> typeCategoryListDtos = new List<TypeCategoryListDto> { _typeCategoryListDto1, _typeCategoryListDto2 };

            _mockTypeCategoryListRepository.Setup(x => x.GetAll()).ReturnsAsync(typeCategoryLists);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<TypeCategoryList>>(), It.IsAny<bool>())).Returns(typeCategoryListDtos);

            // Act
            var result = await _typeCategoryListService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<TypeCategoryListDto>));
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetAll_ReturnsEmptyListOfTypeCategoryListDtoWhenNoneExist()
        {
            // Arrange
            List<TypeCategoryList> typeCategoryLists = new List<TypeCategoryList> { _typeCategoryList1, _typeCategoryList2 };
            List<TypeCategoryListDto> typeCategoryListDtos = new List<TypeCategoryListDto> { _typeCategoryListDto1, _typeCategoryListDto2 };

            _mockTypeCategoryListRepository.Setup(x => x.GetAll()).ReturnsAsync((List<TypeCategoryList>)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<TypeCategoryList>>(), It.IsAny<bool>())).Returns(typeCategoryListDtos);

            // Act
            var result = await _typeCategoryListService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<TypeCategoryListDto>));
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetAll_ReturnsNullWhenExceptionThrown()
        {
            // Arrange
            List<TypeCategoryList> typeCategoryLists = new List<TypeCategoryList> { _typeCategoryList1, _typeCategoryList2 };
            List<TypeCategoryListDto> typeCategoryListDtos = new List<TypeCategoryListDto> { _typeCategoryListDto1, _typeCategoryListDto2 };

            _mockTypeCategoryListRepository.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<TypeCategoryList>>(), It.IsAny<bool>())).Returns(typeCategoryListDtos);

            // Act
            var result = await _typeCategoryListService.GetAll();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetByName_String_ReturnsTypeCategoryListDto()
        {
            //arrange
            _mockTypeCategoryListRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync(_typeCategoryList1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);
            //Act
            var result = await _typeCategoryListService.GetByName(_typeCategoryListDto1.TypeCategoryListName);
            //assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(_typeCategoryListDto1, result);
        }

        [TestMethod]
        public async Task GetByName_String_ReturnsNullWhenTypeCategoryListDoesNotExist()
        {
            //arrange
            _mockTypeCategoryListRepository.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync((TypeCategoryList)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);
            //Act
            var result = await _typeCategoryListService.GetByName(_typeCategoryListDto1.TypeCategoryListName);
            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetByName_String_ReturnsNullWhenExceptionThrown()
        {
            //arrange
            _mockTypeCategoryListRepository.Setup(x => x.GetByName(It.IsAny<string>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);
            //Act
            var result = await _typeCategoryListService.GetByName(_typeCategoryListDto1.TypeCategoryListName);
            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_TypeCategoryListDto_ReturnsTypeCategoryListDto()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeCategoryListDto1)).Returns(_typeCategoryList1);
            _mockTypeCategoryListRepository.Setup(x => x.Update(It.IsAny<TypeCategoryList>())).ReturnsAsync(_typeCategoryList1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Update(_typeCategoryListDto1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeCategoryListDto));
            Assert.AreEqual(_typeCategoryListDto1, result);
        }

        [TestMethod]
        public async Task Update_TypeCategoryListDto_ReturnsNullWhenNotFound()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeCategoryListDto1)).Returns(_typeCategoryList1);
            _mockTypeCategoryListRepository.Setup(x => x.Update(It.IsAny<TypeCategoryList>())).ReturnsAsync((TypeCategoryList)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Update(_typeCategoryListDto1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_TypeCategoryListDto_ReturnsNullWhenExceptionIsThrown()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeCategoryListDto1)).Returns(_typeCategoryList1);
            _mockTypeCategoryListRepository.Setup(x => x.Update(It.IsAny<TypeCategoryList>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<TypeCategoryList>(), It.IsAny<bool>())).Returns(_typeCategoryListDto1);

            // Act
            var result = await _typeCategoryListService.Update(_typeCategoryListDto1);

            // Assert
            Assert.IsNull(result);
        }

    }
}
