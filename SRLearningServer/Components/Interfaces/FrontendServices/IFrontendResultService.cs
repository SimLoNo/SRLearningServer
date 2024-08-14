using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendResultService
    {
        public Task<List<ResultDto>> GetAll();
    }
}
