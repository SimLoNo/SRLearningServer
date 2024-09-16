using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.Services
{
    public interface ICardService : IBaseDataService<CardDto>
    {
        /// <summary>
        /// Gets a list of Cards from the database that match one of the given type lists.
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public Task<List<CardDto>> GetByType(List<List<TypeDto>> typeId);
    }
}
