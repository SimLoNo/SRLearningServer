using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendTypeService
    {
        public Task<List<TypeDto>> GetAll();
    }
}
