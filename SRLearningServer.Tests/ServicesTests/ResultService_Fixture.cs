using Moq;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Components.Models;
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
    public class ResultService_Fixture
    {
        private readonly Mock<IDtoToDomainConverter> _dtoToDomainConverter = new();
        private readonly Mock<IDomainToDtoConverter> _domainToDtoConverter = new();
        private readonly Mock<IResultRepository> _resultRepository = new();
        private readonly ResultService _resultService;
        private readonly TestDataGenerator _testDataGenerator = new();

        private ResultDto _resultDto1;
        private ResultDto _resultDto2;

        private Result _result1;
        private Result _result2;
        private Card _card1;
        private Components.Models.Type _type1;

        private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Now);
        private DateOnly _oldDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        public ResultService_Fixture()
        {

            _resultService = new ResultService(_dtoToDomainConverter.Object, _domainToDtoConverter.Object, _resultRepository.Object);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _resultDto1 = _testDataGenerator.CreateResultDto(1, "Result1", _currentDate, true);
            _result1 = _testDataGenerator.CreateResultFromDto(_resultDto1);

            _resultDto2 = _testDataGenerator.CreateResultDto(2, "Result2", _oldDate, true);
            _result2 = _testDataGenerator.CreateResultFromDto(_resultDto2);
        }



        [TestMethod]
        public async Task Create_ResultDto_ReturnsResultDto()
        {
            //Arrange
            ResultDto entityUsed = _resultDto1;
            Result returnedResult = _result1;
            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<ResultDto>())).Returns(returnedResult);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Create(It.IsAny<Result>())).Returns(Task.FromResult(returnedResult));
            //Act
            var result = await _resultService.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(returnedResult.ResultId, result.ResultId);

        }

        [TestMethod]
        public async Task Create_ResultDtoThatisNotCreated_ReturnsNull()
        {
            //Arrange
            ResultDto entityUsed = _resultDto1;
            Result returnedResult = _result1;
            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<ResultDto>())).Returns(returnedResult);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Create(It.IsAny<Result>())).Returns(() => null);
            //Act
            var result = await _resultService.Create(entityUsed);
            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task Deactivate_ResultDto_ReturnsResultDto()
        {
            //Arrange
            ResultDto entityUsed = _resultDto1;
            Result returnedResult = _result1;
            returnedResult.Active = false;
            entityUsed.Active = false;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult(returnedResult));
            //Act
            var result = await _resultService.Deactivate(entityUsed.ResultId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(false, result.Active);
            Assert.AreEqual(returnedResult.ResultId, result.ResultId);

        }

        [TestMethod]
        public async Task Deactivate_int_ReturnsResultDto()
        {
            //Arrange
            Result returnedResult = _result1;
            ResultDto entityUsed = _resultDto1;
            int idUsed = returnedResult.ResultId;
            returnedResult.Active = false;
            entityUsed.Active = false;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult(returnedResult));
            //Act
            var result = await _resultService.Deactivate(idUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(returnedResult.LastUpdated, result.LastUpdated);
            Assert.AreEqual(false, result.Active);
            Assert.AreEqual(idUsed, result.ResultId);

        }

        [TestMethod]
        public async Task Deactivate_ResultDto_ReturnsNull()
        {
            //Arrange
            int idUsed = _result1.ResultId;
            ResultDto entityUsed = _resultDto1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult<Result>(null));
            //Act
            var result = await _resultService.Deactivate(idUsed);
            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task Deactivate_int_ReturnsNull()
        {
            //Arrange
            ResultDto entityUsed = _resultDto1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult<Result>(null));
            //Act
            var result = await _resultService.Deactivate(entityUsed.ResultId);
            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task Delete_ResultDto_ReturnsResultDto()
        {
            //Arrange
            ResultDto entityUsed = _resultDto1;
            Result returnedResult = _result1;
            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<ResultDto>())).Returns(returnedResult);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.FromResult(returnedResult));
            //Act
            var result = await _resultService.Delete(entityUsed.ResultId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(returnedResult.ResultId, result.ResultId);
        }

        [TestMethod]
        public async Task Delete_ResultDto_ReturnsNull()
        {
            //Arrange
            ResultDto entityUsed = _resultDto1;
            Result returnedResult = _result1;
            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<ResultDto>())).Returns(returnedResult);
            _resultRepository.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.FromResult<Result>(null));
            //Act
            var result = await _resultService.Delete(entityUsed.ResultId);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Get_int_ReturnsResultDto()
        {
            //Arrange
            ResultDto entityUsed = _resultDto1;
            Result returnedResult = _result1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(returnedResult));
            //Act
            var result = await _resultService.Get(entityUsed.ResultId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(entityUsed.ResultText, result.ResultText);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(returnedResult.ResultId, result.ResultId);
        }
        [TestMethod]
        public async Task Get_int_ReturnsNull()
        {
            //Arrange
            ResultDto entityUsed = _resultDto1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult<Result>(null));
            //Act
            var result = await _resultService.Get(entityUsed.ResultId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task Get_int_ReturnsNullWhenIdIsZero()
        {
            //Arrange
            ResultDto entityUsed = _resultDto1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult<Result>(null));
            //Act
            var result = await _resultService.Get(0);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetAll_ReturnsListOfResults()
        {
            //Arrange
            List<ResultDto> entityUsed = new List<ResultDto> { _resultDto1, _resultDto2 };
            List<Result> returnedResults = new List<Result> { _result1, _result2 };
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Result>>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(returnedResults));
            //Act
            var result = await _resultService.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ResultDto>));
            Assert.AreEqual(entityUsed.Count, result.Count);
            Assert.AreEqual(entityUsed[0].ResultText, result[0].ResultText);
            Assert.AreEqual(entityUsed[0].LastUpdated, result[0].LastUpdated);
            Assert.AreEqual(entityUsed[0].Active, result[0].Active);
            Assert.AreEqual(returnedResults[0].ResultId, result[0].ResultId);
        }

        [TestMethod]
        public async Task GetAll_ReturnsNull_WhenNoResultsFound()
        {
            //Arrange
            List<ResultDto> entityUsed = new List<ResultDto> { _resultDto1, _resultDto2 };
            List<Result> returnedResults = new List<Result> { _result1, _result2 };
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Result>>(), It.IsAny<bool>())).Returns(entityUsed);
            _resultRepository.Setup(x => x.GetAll()).Returns(() => null);
            //Act
            var result = await _resultService.GetAll();
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_ResultToUpdate_ReturnsUpdatedResult()
        {
            //Arrange
            var entityUsed = _resultDto1;
            var returnedResult = _result2;
            var updatedResult = _resultDto2;

            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<ResultDto>())).Returns(returnedResult);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(updatedResult);
            _resultRepository.Setup(x => x.Update(It.IsAny<Result>())).Returns(Task.FromResult(returnedResult));

            //Act
            var result = await _resultService.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ResultDto));
            Assert.AreEqual(updatedResult.ResultText, result.ResultText);
            Assert.AreEqual(updatedResult.LastUpdated, result.LastUpdated);
            Assert.AreEqual(updatedResult.Active, result.Active);
        }

        [TestMethod]
        public async Task Update_ResultNotUpdated_ReturnsNull()
        {
            //Arrange
            var entityUsed = _resultDto1;
            var returnedResult = _result2;
            var updatedResult = _resultDto2;

            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<ResultDto>())).Returns(returnedResult);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Result>(), It.IsAny<bool>())).Returns(updatedResult);
            _resultRepository.Setup(x => x.Update(It.IsAny<Result>())).Returns(() => null);

            //Act
            var result = await _resultService.Update(entityUsed);
            //Assert
            Assert.IsNull(result);
        }
    }
}
