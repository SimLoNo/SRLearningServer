using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface IResultRepository : IBaseRepository<Result>
    {
        Task<Result> Update(Result result);
    }
}
