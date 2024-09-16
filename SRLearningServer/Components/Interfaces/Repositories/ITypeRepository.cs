using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface ITypeRepository : IBaseRepository<Models.Type>
    {
        //Task<IEnumerable<Models.Type>> GetMultiple(List<Models.Type> types);
        /// <summary>
        /// Creates a new Type in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Models.Type> Create(Models.Type entity);
        /// <summary>
        /// Updates a Type in the database
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<Models.Type> Update(Models.Type type);
        /// <summary>
        /// Gets a Type in the database by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Models.Type> Get(int id);
    }
}
