using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface IResultRepository : IBaseRepository<Result>
    {
        /// <summary>
        /// Creates a new Result in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Result> Create(Result entity);
        /// <summary>
        /// Updates a result in the database.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        Task<Result> Update(Result result);
        /// <summary>
        /// Gets a result in the database by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Get(int id);
    }
}
