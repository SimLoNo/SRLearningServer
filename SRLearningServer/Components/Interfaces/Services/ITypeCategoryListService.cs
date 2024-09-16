using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.Services
{
    public interface ITypeCategoryListService : IBaseDataService<TypeCategoryListDto>
    {
        /// <summary>
        /// Gets a TypeCategoryList by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<TypeCategoryListDto> GetByName(string name);
    }
}
