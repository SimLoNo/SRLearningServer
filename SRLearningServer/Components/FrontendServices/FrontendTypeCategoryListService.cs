using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class FrontendTypeCategoryListService : IFrontendTypeCategoryListService
    {
        private readonly HttpClient httpClient;

        public FrontendTypeCategoryListService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<TypeCategoryListDto>> GetAll()
        {
            return await httpClient.GetFromJsonAsync<List<TypeCategoryListDto>>("api/TypeCategoryList");
        }

        public async Task<TypeCategoryListDto> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<TypeCategoryListDto>($"api/TypeCategoryList/byid/{id}");
        }

        public async Task<TypeCategoryListDto> Create(TypeCategoryListDto typeCategoryListDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/TypeCategoryList", typeCategoryListDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeCategoryListDto>();
        }

        public async Task<TypeCategoryListDto> Update(TypeCategoryListDto typeCategoryListDto)
        {
            var response = await httpClient.PutAsJsonAsync("api/TypeCategoryList", typeCategoryListDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeCategoryListDto>();
        }

        public async Task<TypeCategoryListDto> Deactivate(int id)
        {
            var response = await httpClient.PutAsync($"api/TypeCategoryList/{id}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeCategoryListDto>();
        }

        public async Task<TypeCategoryListDto> Delete(int id)
        {
            var response = await httpClient.DeleteAsync($"api/TypeCategoryList/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeCategoryListDto>();
        }

        public async Task<TypeCategoryListDto> GetByName(string name)
        {
            var result = await httpClient.GetFromJsonAsync<TypeCategoryListDto>($"api/TypeCategoryList/{name}");
            return result;
        }


    }
}
