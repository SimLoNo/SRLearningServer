using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public async Task<AttachmentDto> Create(AttachmentDto entity)
        {
            try
            {
                Attachment newAttachment = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                newAttachment = await _attachmentRepository.Create(newAttachment);
                if (newAttachment is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(newAttachment, true);

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<AttachmentDto> Deactivate(AttachmentDto entity)
        {
            return await Deactivate(entity.AttachmentId);
        }

        public async Task<AttachmentDto> Deactivate(int id)
        {
            try
            {
                Attachment attachment = await _attachmentRepository.Deactivate(id);
                if (attachment is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(attachment, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<AttachmentDto> Delete(int entity)
        {
            try
            {
                //Attachment attachment = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                Attachment attachment = await _attachmentRepository.Delete(entity);
                if (attachment is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(attachment, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<AttachmentDto> Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    Attachment attachment = await _attachmentRepository.Get(id);
                    if (attachment is null)
                    {
                        return null;
                    }
                    return _domainToDtoConverter.ConvertToDtoFromDomain(attachment, true);
                }
                return null;

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<AttachmentDto>> GetAll()
        {
            try
            {
                List<Attachment> attachments = await _attachmentRepository.GetAll();
                if (attachments.IsNullOrEmpty())
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(attachments, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<AttachmentDto> Update(AttachmentDto entity)
        {
            try
            {
                Attachment attachment = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                attachment = await _attachmentRepository.Update(attachment);
                if (attachment is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(attachment);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
