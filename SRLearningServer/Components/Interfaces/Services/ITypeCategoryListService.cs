using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.Services
{
    public interface ITypeCategoryListService : IBaseDataService<TypeCategoryListDto>
    {

        public Task<TypeCategoryListDto> GetByName(string name);
    }
}
