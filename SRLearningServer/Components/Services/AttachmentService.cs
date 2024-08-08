using Humanizer;
using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IDtoToDomainConverter _dtoToDomainConverter;
        private readonly IDomainToDtoConverter _domainToDtoConverter;
        private readonly IAttachmentRepository _attachmentRepository;
        public AttachmentService(IDtoToDomainConverter dtoToDomainConverter, IDomainToDtoConverter domainToDtoConverter, IAttachmentRepository attachmentRepository)
        {
            _dtoToDomainConverter = dtoToDomainConverter;

            _domainToDtoConverter = domainToDtoConverter;

            _attachmentRepository = attachmentRepository;

        }

        public AttachmentDto Create(AttachmentDto entity)
        {
            try
            {
                Attachment newAttachment = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                newAttachment = Task.Run(() => _attachmentRepository.Create(newAttachment)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(newAttachment, true);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public AttachmentDto Deactivate(AttachmentDto entity)
        {
            return Deactivate(entity.AttachmentId);
        }

        public AttachmentDto Deactivate(int id)
        {
            try
            {
                Attachment attachment = Task.Run(() => _attachmentRepository.Deactivate(id)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(attachment, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public AttachmentDto Delete(AttachmentDto entity)
        {
            try
            {
                Attachment attachment = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                attachment = Task.Run(() => _attachmentRepository.Delete(attachment)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(attachment, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public AttachmentDto Get(int id)
        {
            try
            {
                Attachment attachment = Task.Run(() => _attachmentRepository.Get(id)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(attachment, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<AttachmentDto> GetAll()
        {
            try
            {
                List<Attachment> attachments = Task.Run(() => _attachmentRepository.GetAll()).Result.ToList();
                if (attachments.IsNullOrEmpty())
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(attachments, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
