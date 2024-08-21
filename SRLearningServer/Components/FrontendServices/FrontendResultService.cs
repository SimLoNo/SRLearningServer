using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendResultService : IFrontendResultService
    {
        private readonly HttpClient _httpClient;

        public FrontendResultService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResultDto> Create(ResultDto entity)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Result", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResultDto>();
        }

        public async Task<ResultDto> Deactivate(int id)
        {
            var response = await _httpClient.PutAsync($"api/Result/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResultDto>();
        }

        public async Task<ResultDto> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Result/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResultDto>();
        }

        public async Task<ResultDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<ResultDto>($"api/Result/{id}");
        }

        public async Task<List<ResultDto>> GetAll()
        {
            var response =  await _httpClient.GetAsync("api/Result");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultDto>>();
            }
            return new();
        }

        public async Task<ResultDto> Update(ResultDto entity)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Result", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResultDto>();
        }
    }
}
