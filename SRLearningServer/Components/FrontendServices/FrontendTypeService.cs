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
        public async Task<List<TypeDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<TypeDto>>("api/type");
        }
    }
}
