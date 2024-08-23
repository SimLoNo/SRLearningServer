using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendAttachmentService : IFrontendAttachmentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _endpoint;
        public FrontendAttachmentService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["Api:Endpoints:Base"];
            _endpoint = config["Api:Endpoints:Attachment"];
        }

        public async Task<AttachmentDto> Create(AttachmentDto attachment)
        {
            var response = await _httpClient.PostAsJsonAsync<AttachmentDto>($"{_baseUrl}/{_endpoint}", attachment);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AttachmentDto>();
        }
        public async Task<List<AttachmentDto>> GetAll()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{_endpoint}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<AttachmentDto>>();
            }
            return new();
        }

        public async Task<AttachmentDto> Deactivate(int id)
        {
            var response = await _httpClient.PutAsync($"{_baseUrl}/{_endpoint}/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AttachmentDto>();
        }

        public async Task<AttachmentDto> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{_endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AttachmentDto>();
        }

        public async Task<AttachmentDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<AttachmentDto>($"{_baseUrl}/{_endpoint}/{id}");
        }

        public async Task<AttachmentDto> Update(AttachmentDto attachment)
        {
            int id = attachment.AttachmentId;
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{_endpoint}", attachment);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AttachmentDto>();
        }


    }
}
