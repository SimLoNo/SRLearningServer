using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface IResultRepository : IBaseRepository<Result>
    {
        Task<Result> Create(Result entity);
        Task<Result> Update(Result result);
        Task<Result> Get(int id);
    }
}
