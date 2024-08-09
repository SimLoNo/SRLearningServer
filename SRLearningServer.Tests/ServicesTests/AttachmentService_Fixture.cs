using Moq;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
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
    public class AttachmentService_Fixture
    {
        private readonly Mock<IDtoToDomainConverter> _dtoToDomainConverter = new();
        private readonly Mock<IDomainToDtoConverter> _domainToDtoConverter = new();
        private readonly Mock<IAttachmentRepository> _attachmentRepository = new();
        private readonly AttachmentService _attachmentService;
        private readonly TestDataGenerator _testDataGenerator = new();

        private AttachmentDto _attachmentDto1;
        private AttachmentDto _attachmentDto2;
        private ResultDto _resultDto1;
        private CardDto _cardDto1;
        private TypeDto _typeDto1;

        private Attachment _attachment1;
        private Attachment _attachment2;
        private Result _result1;
        private Card _card1;
        private Components.Models.Type _type1;

        private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Now);
        private DateOnly _oldDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        public AttachmentService_Fixture()
        {

            _attachmentService = new AttachmentService(_dtoToDomainConverter.Object, _domainToDtoConverter.Object, _attachmentRepository.Object);
        }
        [TestInitialize]
        public void TestInitialize()
        {
            _attachmentDto1 = _testDataGenerator.CreateAttachmentDto(1, "Attachment1", "Url1", _currentDate, true);
            _attachment1 = _testDataGenerator.CreateAttachmentFromDto(_attachmentDto1);

            _attachmentDto2 = _testDataGenerator.CreateAttachmentDto(2, "Attachment2", "Url2", _oldDate, false);
            _attachment2 = _testDataGenerator.CreateAttachmentFromDto(_attachmentDto2);
        }

        

        [TestMethod]
        public async Task Create_AttachmentDto_ReturnsAttachmentDto()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<AttachmentDto>())).Returns(returnedAttachment);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Create(It.IsAny<Attachment>())).Returns(Task.FromResult(returnedAttachment));
            //Act
            var result = await _attachmentService.Create(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(returnedAttachment.AttachmentId, result.AttachmentId);

        }

        [TestMethod]
        public async Task Create_AttachmentDtoThatisNotCreated_ReturnsNull()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<AttachmentDto>())).Returns(returnedAttachment);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Create(It.IsAny<Attachment>())).Returns(() => null);
            //Act
            var result = await _attachmentService.Create(entityUsed);
            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task Deactivate_AttachmentDto_ReturnsAttachmentDto()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            returnedAttachment.Active = false;
            entityUsed.Active = false;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult(returnedAttachment));
            //Act
            var result = await _attachmentService.Deactivate(entityUsed.AttachmentId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(false, result.Active);
            Assert.AreEqual(returnedAttachment.AttachmentId, result.AttachmentId);

        }

        [TestMethod]
        public async Task Deactivate_int_ReturnsAttachmentDto()
        {
            //Arrange
            Attachment returnedAttachment = _attachment1;
            AttachmentDto entityUsed = _attachmentDto1;
            int idUsed = returnedAttachment.AttachmentId;
            returnedAttachment.Active = false;
            entityUsed.Active = false;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult(returnedAttachment));
            //Act
            var result = await _attachmentService.Deactivate(idUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(returnedAttachment.AttachmentName, result.AttachmentName);
            Assert.AreEqual(returnedAttachment.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(returnedAttachment.LastUpdated, result.LastUpdated);
            Assert.AreEqual(false, result.Active);
            Assert.AreEqual(idUsed, result.AttachmentId);

        }

        [TestMethod]
        public async Task Deactivate_AttachmentDto_ReturnsNull()
        {
            //Arrange
            int idUsed = _attachment1.AttachmentId;
            AttachmentDto entityUsed = _attachmentDto1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult<Attachment>(null));
            //Act
            var result = await _attachmentService.Deactivate(idUsed);
            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task Deactivate_int_ReturnsNull()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Deactivate(It.IsAny<int>())).Returns(Task.FromResult<Attachment>(null));
            //Act
            var result = await _attachmentService.Deactivate(entityUsed.AttachmentId);
            //Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task Delete_AttachmentDto_ReturnsAttachmentDto()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<AttachmentDto>())).Returns(returnedAttachment);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Delete(It.IsAny<Attachment>())).Returns(Task.FromResult(returnedAttachment));
            //Act
            var result = await _attachmentService.Delete(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(returnedAttachment.AttachmentId, result.AttachmentId);
        }

        [TestMethod]
        public async Task Delete_AttachmentDto_ReturnsNull()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<AttachmentDto>())).Returns(returnedAttachment);
            _attachmentRepository.Setup(x => x.Delete(It.IsAny<Attachment>())).Returns(Task.FromResult<Attachment>(null));
            //Act
            var result = await _attachmentService.Delete(entityUsed);
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Get_int_ReturnsAttachmentDto()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            Attachment returnedAttachment = _attachment1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(returnedAttachment));
            //Act
            var result = await _attachmentService.Get(entityUsed.AttachmentId);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(entityUsed.AttachmentName, result.AttachmentName);
            Assert.AreEqual(entityUsed.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(entityUsed.LastUpdated, result.LastUpdated);
            Assert.AreEqual(entityUsed.Active, result.Active);
            Assert.AreEqual(returnedAttachment.AttachmentId, result.AttachmentId);
        }
        [TestMethod]
        public async Task Get_int_ReturnsNull()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult<Attachment>(null));
            //Act
            var result = await _attachmentService.Get(entityUsed.AttachmentId);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task Get_int_ReturnsNullWhenIdIsZero()
        {
            //Arrange
            AttachmentDto entityUsed = _attachmentDto1;
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult<Attachment>(null));
            //Act
            var result = await _attachmentService.Get(0);
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetAll_ReturnsListOfAttachments()
        {
            //Arrange
            List<AttachmentDto> entityUsed = new List<AttachmentDto> { _attachmentDto1, _attachmentDto2 };
            List<Attachment> returnedAttachments = new List<Attachment> { _attachment1, _attachment2 };
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Attachment>>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(returnedAttachments));
            //Act
            var result = await _attachmentService.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<AttachmentDto>));
            Assert.AreEqual(entityUsed.Count, result.Count);
            Assert.AreEqual(entityUsed[0].AttachmentName, result[0].AttachmentName);
            Assert.AreEqual(entityUsed[0].AttachmentUrl, result[0].AttachmentUrl);
            Assert.AreEqual(entityUsed[0].LastUpdated, result[0].LastUpdated);
            Assert.AreEqual(entityUsed[0].Active, result[0].Active);
            Assert.AreEqual(returnedAttachments[0].AttachmentId, result[0].AttachmentId);
        }

        [TestMethod]
        public async Task GetAll_ReturnsNull_WhenNoAttachmentsFound()
        {
            //Arrange
            List<AttachmentDto> entityUsed = new List<AttachmentDto> { _attachmentDto1, _attachmentDto2 };
            List<Attachment> returnedAttachments = new List<Attachment> { _attachment1, _attachment2 };
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<List<Attachment>>(), It.IsAny<bool>())).Returns(entityUsed);
            _attachmentRepository.Setup(x => x.GetAll()).Returns(() => null);
            //Act
            var result = await _attachmentService.GetAll();
            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_AttachmentToUpdate_ReturnsUpdatedAttachment()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            var returnedAttachment = _attachment2;
            var updatedAttachment = _attachmentDto2;

            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<AttachmentDto>())).Returns(returnedAttachment);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(updatedAttachment);
            _attachmentRepository.Setup(x => x.Update(It.IsAny<Attachment>())).Returns(Task.FromResult(returnedAttachment));

            //Act
            var result = await _attachmentService.Update(entityUsed);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AttachmentDto));
            Assert.AreEqual(updatedAttachment.AttachmentName, result.AttachmentName);
            Assert.AreEqual(updatedAttachment.AttachmentUrl, result.AttachmentUrl);
            Assert.AreEqual(updatedAttachment.LastUpdated, result.LastUpdated);
            Assert.AreEqual(updatedAttachment.Active, result.Active);
        }

        [TestMethod]
        public async Task Update_AttachmentNotUpdated_ReturnsNull()
        {
            //Arrange
            var entityUsed = _attachmentDto1;
            var returnedAttachment = _attachment2;
            var updatedAttachment = _attachmentDto2;

            _dtoToDomainConverter.Setup(x => x.ConvertToDomainFromDto(It.IsAny<AttachmentDto>())).Returns(returnedAttachment);
            _domainToDtoConverter.Setup(x => x.ConvertToDtoFromDomain(It.IsAny<Attachment>(), It.IsAny<bool>())).Returns(updatedAttachment);
            _attachmentRepository.Setup(x => x.Update(It.IsAny<Attachment>())).Returns(() => null);

            //Act
            var result = await _attachmentService.Update(entityUsed);
            //Assert
            Assert.IsNull(result);
        }



    }
}
