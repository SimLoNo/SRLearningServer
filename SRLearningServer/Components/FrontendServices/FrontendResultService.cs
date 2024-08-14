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
        public async Task<List<ResultDto>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<ResultDto>>("api/Result");
        }
    }
}
