using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface ITypeCategoryListRepository : IBaseRepository<TypeCategoryList>
    {
        /// <summary>
        /// Creates a new TypeCategoryList in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TypeCategoryList> Create(TypeCategoryList entity);
        /// <summary>
        /// Gets a TypeCategoryList in the database by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<TypeCategoryList> GetByName(string name);
        /// <summary>
        /// Updates a TypeCategoryList in the database.
        /// </summary>
        /// <param name="typeCategoryList"></param>
        /// <returns></returns>
        public Task<TypeCategoryList> Update(TypeCategoryList typeCategoryList);
        /// <summary>
        /// Gets a TypeCategoryList in the database by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TypeCategoryList> Get(int id);
    }
}
