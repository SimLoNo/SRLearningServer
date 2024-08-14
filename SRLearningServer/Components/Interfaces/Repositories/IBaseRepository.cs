using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        //Task<TEntity> Create(TEntity entity);
        Task<TEntity> Get(int id);
        Task<TEntity> Deactivate(int id);
        Task<TEntity> Delete(int entity);
        Task<List<TEntity>> GetAll();

    }
}
