using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.Services
{
    public interface ICardService : IBaseDataService<Card, CardDto>
    {
        List<CardDto> GetByType(List<List<TypeDto>> typeId);
    }
}
