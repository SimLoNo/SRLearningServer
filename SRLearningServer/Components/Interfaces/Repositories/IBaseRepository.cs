using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        //Task<TEntity> Create(TEntity entity);
        //Task<TEntity> Get(int id);
        /// <summary>
        /// Updates an entitys Active property in the database to false..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> Deactivate(int id);
        /// <summary>
        /// Deletes an entity from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Delete(int entity);
        /// <summary>
        /// Gets all the entities of the implementation type in the database
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAll();

    }
}
