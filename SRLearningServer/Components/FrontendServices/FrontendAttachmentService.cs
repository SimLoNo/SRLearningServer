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

        public async Task<AttachmentDto> Create(AttachmentDto attachment)
        {
            var response = await _httpClient.PostAsJsonAsync<AttachmentDto>("api/Attachment", attachment);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AttachmentDto>();
        }
        public async Task<List<AttachmentDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<AttachmentDto>>("api/Attachment");
        }

        public async Task<AttachmentDto> Deactivate(int id)
        {
            var response = await _httpClient.PutAsync($"api/Attachment/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AttachmentDto>();
        }

        public async Task<AttachmentDto> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Attachment/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AttachmentDto>();
        }

        public async Task<AttachmentDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<AttachmentDto>($"api/Attachment/{id}");
        }

        public async Task<AttachmentDto> Update(AttachmentDto attachment)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Attachment", attachment);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AttachmentDto>();
        }


    }
}
