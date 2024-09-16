using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendCardService
    {
        /// <summary>
        /// Creates a Card in the database
        /// </summary>
        /// <param name="cardDto"></param>
        /// <returns>CardDto</returns>
        public Task<CardDto> Create(CardDto cardDto);
        /// <summary>
        /// Deactivates a Card in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CardDto</returns>
        public Task<CardDto> Deactivate(int id);
        /// <summary>
        /// Deletes a Card from the database½
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CardDto</returns>
        public Task<CardDto> Delete(int id);
        /// <summary>
        /// Gets a card from the database by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CardDto</returns>
        public Task<CardDto> GetByid(int id);
        /// <summary>
        /// Gets all the cards in the database
        /// </summary>
        /// <returns>List of CardDto</returns>
        public Task<List<CardDto>> GetAll();
        /// <summary>
        /// Gets all the active cards in the database who have a specific list of type
        /// </summary>
        /// <param name="typeList"></param>
        /// <returns>List of CardDto</returns>
        public Task<List<CardDto>> GetByType(List<List<TypeDto>> typeList);
        /// <summary>
        /// Updates a Card in the database
        /// </summary>
        /// <param name="cardDto"></param>
        /// <returns>CardDto</returns>
        public Task<CardDto> Update(CardDto cardDto);
    }
}
