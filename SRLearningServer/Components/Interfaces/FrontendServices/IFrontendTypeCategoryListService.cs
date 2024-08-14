using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendTypeCategoryListService
    {
        public Task<List<TypeCategoryListDto>> GetAll();
        public Task<TypeCategoryListDto> GetById(int id);
        public Task<TypeCategoryListDto> Create(TypeCategoryListDto typeCategoryListDto);

        public Task<TypeCategoryListDto> Update(TypeCategoryListDto typeCategoryListDto);
        public Task<TypeCategoryListDto> Deactivate(int id);
        public Task<TypeCategoryListDto> Delete(int id);
        public Task<TypeCategoryListDto> GetByName(string name);
    }
}
