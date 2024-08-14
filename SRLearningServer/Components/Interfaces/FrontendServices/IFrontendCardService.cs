using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendCardService
    {
        public Task<CardDto> Create(CardDto cardDto);
    }
}
