using Moq;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Interfaces.Services;
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
    public class TypeService_Fixture
    {
        private readonly Mock<IDomainToDtoConverter> _mockDomainToDtoConverter;
        private readonly Mock<IDtoToDomainConverter> _mockDtoToDomainConverter;
        private readonly Mock<ITypeRepository> _mockTypeRepository;
        private readonly ITypeService _typeService;
        private readonly TestDataGenerator _testDataGenerator;

        private TypeDto _typeDto1;
        private TypeDto _typeDto2;

        private Components.Models.Type _type1;
        private Components.Models.Type _type2;

        public TypeService_Fixture()
        {
            _mockDomainToDtoConverter = new Mock<IDomainToDtoConverter>();
            _mockDtoToDomainConverter = new Mock<IDtoToDomainConverter>();
            _mockTypeRepository = new Mock<ITypeRepository>();
            _typeService = new TypeService(_mockDtoToDomainConverter.Object, _mockDomainToDtoConverter.Object, _mockTypeRepository.Object);
            _testDataGenerator = new TestDataGenerator();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _typeDto1 = _testDataGenerator.CreateTypeDto(1, "type1", DateOnly.FromDateTime(DateTime.UtcNow),true);
            _typeDto2 = _testDataGenerator.CreateTypeDto(2,"type2", DateOnly.FromDateTime(DateTime.UtcNow),true);

            _type1 = _testDataGenerator.CreateTypeFromDto(_typeDto1);
            _type2 = _testDataGenerator.CreateTypeFromDto(_typeDto2);
        }

        [TestMethod]
        public async Task Create_TypeDto_ReturnsTypeDto()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeDto1)).Returns(_type1);
            _mockTypeRepository.Setup(x => x.Create(_type1)).ReturnsAsync(_type1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(_type1, true)).Returns(_typeDto1);

            // Act
            var result = await _typeService.Create(_typeDto1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(_typeDto1, result);
        }

        [TestMethod]
        public async Task Create_TypeDto_ReturnsNullWhenNotCreated()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeDto1)).Returns(_type1);
            _mockTypeRepository.Setup(x => x.Create(_type1)).ReturnsAsync((Components.Models.Type)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(_type1, true)).Returns(_typeDto1);

            // Act
            var result = await _typeService.Create(_typeDto1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Create_TypeDto_ReturnsNullWhenExceptionThrown()
        {
            // Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(_typeDto1)).Returns(_type1);
            _mockTypeRepository.Setup(x => x.Create(_type1)).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(_type1, true)).Returns(_typeDto1);

            // Act
            var result = await _typeService.Create(_typeDto1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_TypeDto_ReturnsTypeDto()
        {
            //arrange
            _mockTypeRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_type1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Deactivate(_typeDto1);
            //assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(_typeDto1, result);
        }

        [TestMethod]
        public async Task Deactivate_TypeDto_ReturnsNullWhenNotFound()
        {
            //arrange
            _mockTypeRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync((Components.Models.Type)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Deactivate(_typeDto1);
            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_TypeDto_ReturnsNullWhenExceptionThrown()
        {
            //arrange
            _mockTypeRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Deactivate(_typeDto1);
            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_Int_ReturnsTypeDto()
        {
            //arrange
            _mockTypeRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync(_type1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Deactivate(_typeDto1.TypeId);
            //assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(_typeDto1, result);
        }

        [TestMethod]
        public async Task Deactivate_Int_ReturnsNullWhenNotFound()
        {
            //arrange
            _mockTypeRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ReturnsAsync((Components.Models.Type)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Deactivate(_typeDto1.TypeId);
            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Deactivate_Int_ReturnsNullWhenExceptionThrown()
        {
            //arrange
            _mockTypeRepository.Setup(x => x.Deactivate(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Deactivate(_typeDto1.TypeId);
            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Delete_Int_ReturnsTypeDtoWhenDeleted()
        {
            //Arrange
            _mockTypeRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(_type1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Delete(_typeDto1.TypeId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(_typeDto1, result);
        }

        [TestMethod]
        public async Task Delete_Int_ReturnsNullWhenNotDeleted()
        {
            //Arrange
            _mockTypeRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync((Components.Models.Type)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Delete(_typeDto1.TypeId);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Delete_Int_ReturnsNullWhenExceptionThrown()
        {
            //Arrange
            _mockTypeRepository.Setup(x => x.Delete(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Delete(_typeDto1.TypeId);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Get_Int_ReturnsTypeDtoWhenFound()
        {
            //Arrange
            _mockTypeRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(_type1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Get(_typeDto1.TypeId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(_typeDto1, result);
        }

        [TestMethod]
        public async Task Get_Int_ReturnsNullWhenNotFound()
        {
            //Arrange
            _mockTypeRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((Components.Models.Type)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Get(_typeDto1.TypeId);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Get_Int_ReturnsNullWhenExceptionThrown()
        {
            //Arrange
            _mockTypeRepository.Setup(x => x.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Get(_typeDto1.TypeId);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAll_ReturnsListOfTypeDto()
        {
            //Arrange
            List<Components.Models.Type> types = new List<Components.Models.Type> { _type1, _type2 };
            List<TypeDto> typeDtos = new List<TypeDto> { _typeDto1, _typeDto2 };
            _mockTypeRepository.Setup(x => x.GetAll()).ReturnsAsync(types);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Components.Models.Type>>(), It.IsAny<bool>())).Returns(typeDtos);
            //Act
            var result = await _typeService.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<TypeDto>));
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetAll_ReturnsEmptyListOfTypeDtoWhenNoneFound()
        {
            //Arrange
            List<Components.Models.Type> types = new List<Components.Models.Type> {};
            List<TypeDto> typeDtos = new List<TypeDto> { _typeDto1, _typeDto2 };
            _mockTypeRepository.Setup(x => x.GetAll()).ReturnsAsync(types);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Components.Models.Type>>(), It.IsAny<bool>())).Returns(typeDtos);
            //Act
            var result = await _typeService.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<TypeDto>));
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetAll_ReturnsNullWhenExceptionThrown()
        {
            //Arrange
            List<Components.Models.Type> types = new List<Components.Models.Type> { };
            List<TypeDto> typeDtos = new List<TypeDto> { _typeDto1, _typeDto2 };
            _mockTypeRepository.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Components.Models.Type>>(), It.IsAny<bool>())).Returns(typeDtos);
            //Act
            var result = await _typeService.GetAll();
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_TypeDto_ReturnsTypeDtoWhenUpdated()
        {
            //Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<TypeDto>())).Returns(_type1);
            _mockTypeRepository.Setup(x => x.Update(_type1)).ReturnsAsync(_type1);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Update(_typeDto1);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeDto));
            Assert.AreEqual(_typeDto1, result);
        }

        [TestMethod]
        public async Task Update_TypeDto_ReturnsNullWhenNotUpdated()
        {
            //Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<TypeDto>())).Returns(_type1);
            _mockTypeRepository.Setup(x => x.Update(_type1)).ReturnsAsync((Components.Models.Type)null);
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Update(_typeDto1);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_TypeDto_ReturnsNullWhenExceptionThrown()
        {
            //Arrange
            _mockDtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<TypeDto>())).Returns(_type1);
            _mockTypeRepository.Setup(x => x.Update(_type1)).ThrowsAsync(new Exception());
            _mockDomainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Components.Models.Type>(), It.IsAny<bool>())).Returns(_typeDto1);
            //Act
            var result = await _typeService.Update(_typeDto1);
            //Assert
            Assert.IsNull(result);
        }
    }
}
