using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendTypeService : IFrontendTypeService
    {
        private readonly HttpClient _httpClient;
        public FrontendTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TypeDto> Create(TypeDto type)
        {
            var response = await _httpClient.PostAsJsonAsync("api/type", type);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeDto>();
        }

        public async Task<TypeDto> Deactivate(int id)
        {
            var response = await _httpClient.PutAsync($"api/type/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeDto>();
        }

        public async Task<TypeDto> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/type/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeDto>();
        }

        public async Task<TypeDto> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<TypeDto>($"api/type/{id}");
        }
        public async Task<List<TypeDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<TypeDto>>("api/type");
        }

        public async Task<TypeDto> Update(TypeDto type)
        {
            var response = await _httpClient.PutAsJsonAsync("api/type", type);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeDto>();
        }
    }
}
