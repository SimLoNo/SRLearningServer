using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendAttachmentService
    {
        public Task<List<AttachmentDto>> GetAll();
    }
}
