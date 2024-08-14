using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;
using System.Net.Http.Json;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendCardService : IFrontendCardService
    {
        private readonly HttpClient _httpClient;
        public FrontendCardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CardDto> Create(CardDto cardDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Card", cardDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CardDto>();
        }

        public async Task<CardDto> Deactivate(int id)
        {
            var response = await _httpClient.PutAsync($"api/Card/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CardDto>();
        }

        public async Task<CardDto> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Card/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CardDto>();
        }
        public async Task<CardDto> GetByid(int id)
        {
            return await _httpClient.GetFromJsonAsync<CardDto>($"api/Card/{id}");
        }

        public async Task<List<CardDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<CardDto>>("api/Card");
        }

        public async Task<List<CardDto>> GetByType(List<List<TypeDto>> typeList)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Card/GetByType", typeList);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CardDto>>();
        }

        public async Task<CardDto> Update(CardDto cardDto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Card", cardDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CardDto>();
        }
    }
}
