using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        //Task<IEnumerable<Card>> GetMultiple(List<Models.Card> cards);
        Task<Card> Create(Card entity);

        /// <summary>
        /// Gets all the active cards that has a relation to all the Types in any of the provided typeSelectors
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        Task<List<Card>> GetByType(List<List<Models.Type>> typeId);
        /// <summary>
        /// Updates a Card in the database
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        Task<Card> Update(Card card);
        /// <summary>
        /// Gets a Card in the database by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Card> Get(int id);

    }
}
