using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface ITypeCategoryListRepository : IBaseRepository<TypeCategoryList>
    {
        Task<TypeCategoryList> Create(TypeCategoryList entity);
        public Task<TypeCategoryList> GetByName(string name);
        public Task<TypeCategoryList> Update(TypeCategoryList typeCategoryList);
        Task<TypeCategoryList> Get(int id);
    }
}
