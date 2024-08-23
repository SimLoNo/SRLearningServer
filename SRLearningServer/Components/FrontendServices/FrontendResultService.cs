using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendResultService : IFrontendResultService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _endpoint;

        public FrontendResultService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["Api:Endpoints:Base"];
            _endpoint = config["Api:Endpoints:Result"];
        }

        public async Task<ResultDto> Create(ResultDto entity)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{_endpoint}", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResultDto>();
        }

        public async Task<ResultDto> Deactivate(int id)
        {
            var response = await _httpClient.PutAsync($"{_baseUrl}/{_endpoint}/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResultDto>();
        }

        public async Task<ResultDto> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{_endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResultDto>();
        }

        public async Task<ResultDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<ResultDto>($"{_baseUrl}/{_endpoint}/{id}");
        }

        public async Task<List<ResultDto>> GetAll()
        {
            var response =  await _httpClient.GetAsync($"{_baseUrl}/{_endpoint}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultDto>>();
            }
            return new();
        }

        public async Task<ResultDto> Update(ResultDto entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{_endpoint}", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResultDto>();
        }
    }
}
