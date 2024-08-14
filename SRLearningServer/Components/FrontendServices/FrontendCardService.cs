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
    }
}
