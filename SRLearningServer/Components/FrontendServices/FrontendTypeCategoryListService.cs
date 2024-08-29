using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendTypeCategoryListService : IFrontendTypeCategoryListService
    {
        private readonly HttpClient httpClient;
        private readonly string _baseUrl;
        private readonly string _endpoint;

        public FrontendTypeCategoryListService(HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;
            _baseUrl = config["Api:Endpoints:Base"];
            _endpoint = config["Api:Endpoints:TypeCategoryList"];
        }

        public async Task<List<TypeCategoryListDto>> GetAll()
        {
            var response = await httpClient.GetAsync($"{_baseUrl}/{_endpoint}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<List<TypeCategoryListDto>>();
            }
            return new();
        }

        public async Task<TypeCategoryListDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<TypeCategoryListDto>($"{_baseUrl}/{_endpoint}/byid/{id}");
        }

        public async Task<TypeCategoryListDto> Create(TypeCategoryListDto typeCategoryListDto)
        {
            var response = await httpClient.PostAsJsonAsync($"{_baseUrl}/{_endpoint}", typeCategoryListDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeCategoryListDto>();
        }

        public async Task<TypeCategoryListDto> Update(TypeCategoryListDto typeCategoryListDto)
        {
            var response = await httpClient.PutAsJsonAsync($"{_baseUrl}/{_endpoint}", typeCategoryListDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeCategoryListDto>();
        }

        public async Task<TypeCategoryListDto> Deactivate(int id)
        {
            var response = await httpClient.PutAsync($"{_baseUrl}/{_endpoint}/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeCategoryListDto>();
        }

        public async Task<TypeCategoryListDto> Delete(int id)
        {
            var response = await httpClient.DeleteAsync($"{_baseUrl}/{_endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeCategoryListDto>();
        }

        public async Task<TypeCategoryListDto?> GetByName(string name)
        {
            var response = await httpClient.GetAsync($"{_baseUrl}/{_endpoint}/{name}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TypeCategoryListDto>();
            }
            return null;
        }


    }
}
