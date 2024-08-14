using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendAttachmentService
    {
        public Task<List<AttachmentDto>> GetAll();
        public Task<AttachmentDto> Create(AttachmentDto attachment);
        public Task<AttachmentDto> Deactivate(int id);
        public Task<AttachmentDto> Delete(int id);
        public Task<AttachmentDto> GetById(int id);
        public Task<AttachmentDto> Update(AttachmentDto attachment);
    }
}
