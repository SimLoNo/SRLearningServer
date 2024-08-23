using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Interfaces.Utilities;
using SRLearningServer.Components.Models.DTO;
using System.Net.Http.Json;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendCardService : IFrontendCardService
    {
        private readonly HttpClient _httpClient;
        private readonly INotificationUtility _notificationUtility;
        private readonly string _baseUrl;
        private readonly string _endpoint;
        public FrontendCardService(HttpClient httpClient, INotificationUtility notificationUtility, IConfiguration config)
        {
            _httpClient = httpClient;
            _notificationUtility = notificationUtility;
            _baseUrl = config["Api:Endpoints:Base"];
            _endpoint = config["Api:Endpoints:Card"];
        }
        public async Task<CardDto> Create(CardDto cardDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{_endpoint}", cardDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CardDto>();
        }

        public async Task<CardDto> Deactivate(int id)
        {
            var response = await _httpClient.PutAsync($"{_baseUrl}/{_endpoint}/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CardDto>();
        }

        public async Task<CardDto> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{_endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CardDto>();
        }
        public async Task<CardDto> GetByid(int id)
        {
            return await _httpClient.GetFromJsonAsync<CardDto>($"{_baseUrl}/{_endpoint}/{id}");
        }

        public async Task<List<CardDto>> GetAll()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{_endpoint}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<CardDto>>();
            }
            return new();
        }

        public async Task<List<CardDto>> GetByType(List<List<TypeDto>> typeList)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{_endpoint}/GetByType", typeList);
            response.EnsureSuccessStatusCode();
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                _notificationUtility.SendNotification("Der blev ikke fundet nogle kort med de valgte kriterier.");
                return new List<CardDto>();
            }
            return await response.Content.ReadFromJsonAsync<List<CardDto>>();
        }

        public async Task<CardDto> Update(CardDto cardDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{_endpoint}", cardDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CardDto>();
        }
    }
}
