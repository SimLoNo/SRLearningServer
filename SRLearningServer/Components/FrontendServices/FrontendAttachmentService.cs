using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendAttachmentService : IFrontendAttachmentService
    {
        private readonly HttpClient _httpClient;
        public FrontendAttachmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<AttachmentDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<AttachmentDto>>("api/Attachment");
        }
    }
}
