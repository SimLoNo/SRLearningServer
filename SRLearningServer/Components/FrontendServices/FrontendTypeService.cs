using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendTypeService : IFrontendTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _endpoint;
        public FrontendTypeService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["Api:Endpoints:Base"];
            _endpoint = config["Api:Endpoints:Type"];
        }

        public async Task<TypeDto> Create(TypeDto type)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{_endpoint}", type);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeDto>();
        }

        public async Task<TypeDto> Deactivate(int id)
        {
            var response = await _httpClient.PutAsync($"{_baseUrl}/{_endpoint}/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeDto>();
        }

        public async Task<TypeDto> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{_endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeDto>();
        }

        public async Task<TypeDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<TypeDto>($"{_baseUrl}/{_endpoint}/{id}");
        }
        public async Task<List<TypeDto>> GetAll()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{_endpoint}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<TypeDto>>();
            }
            return new();
        }

        public async Task<TypeDto> Update(TypeDto type)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{_endpoint}", type);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeDto>();
        }
    }
}
