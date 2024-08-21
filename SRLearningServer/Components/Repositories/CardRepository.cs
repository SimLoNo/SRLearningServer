using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Context;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace SRLearningServer.Components.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        
        public CardRepository(SRContext context) : base(context)
        {
            
        }

        public async Task<Card> Create(Card entity)
        {
            try
            {
                entity.LastUpdated = DateOnly.FromDateTime(DateTime.Now);
                foreach (var result in entity.Results.ToList())
                {
                    var newResult = await _context.Results.FirstOrDefaultAsync(r => r.ResultId == result.ResultId);
                    entity.Results.Remove(result);
                    if (newResult != null)
                    {
                        entity.Results.Add(newResult);
                    }
                }

                foreach (var type in entity.Types.ToList())
                {
                    var newType = await _context.Types.FirstOrDefaultAsync(t => t.TypeId == type.TypeId);
                    entity.Types.Remove(type);
                    if (newType != null)
                    {
                        entity.Types.Add(newType);

                    }
                }
                _context.Cards.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// Returns a Card with it's relations with the given id. If no Card is found, returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Card> Get(int id)
        {
            try
            {
                var foundEntity = await _context.Cards
                    .Include(c => c.Results)
                    .Include(c => c.Types)
                    .Include(c => c.Attachment)
                    .FirstOrDefaultAsync(c => c.CardId == id);
                if (foundEntity == null)
                {
                    return null;
                }
                return foundEntity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<Card>> GetByType(List<List<Models.Type>> typeSelectors)
        {
            if (typeSelectors == null || typeSelectors.Count == 0)
            {
                return new List<Card>();
            }
            try
            {
                // Flatten the typeSelectors to get a list of all TypeIds
                //var typeIds = typeSelectors.SelectMany(ts => ts.Select(t => t.TypeId)).Distinct().ToList();

                // Fetch all active cards that have any of the specified TypeIds
                var activeCards = new List<Card>();
                foreach (var typeSelector in typeSelectors)
                {
                    var typeIds = typeSelector.Select(t => t.TypeId).ToList();

                    var tempCards = await _context.Cards
                        .Where(c => c.Active && typeIds.All(id => c.Types.Any(t => t.TypeId == id)))
                        .Include(c => c.Types)
                        .Include(c => c.Results)
                        .Include(c => c.Attachment)
                        .ToListAsync();

                    activeCards.AddRange(tempCards);
                }

                // Filter the active cards to match the typeSelectors
                /*var filteredCards = new List<Card>();
                foreach (var typeSelector in typeSelectors)
                {
                    var result = activeCards
                        .Where(c => typeSelector.All(ts => c.Types.Any(t => t.TypeId == ts.TypeId)))
                        .ToList();

                    filteredCards.AddRange(result);
                filteredCards = filteredCards.Distinct().ToList();
                }*/
                activeCards = activeCards.Distinct().ToList();
                foreach (var card in activeCards)
                {
                    card.Types.ToList().RemoveAll(t => t.Active == false);
                    card.Results.ToList().RemoveAll(r => r.Active == false);
                    if (card.Attachment is not null && card.Attachment.Active == false)
                    {
                        card.Attachment = null;
                    }
                }
                return activeCards;
            }
            catch (Exception ex)
            {

                return null;
            }

            
        }

        /*public async Task<IEnumerable<Card>> GetMultiple(List<Models.Card> cards)
        {
            List<Models.Card> cardList = new();
            if (cards.IsNullOrEmpty())
            {
                try
                {

                    cardList = await _context.Set<Models.Card>().Where(t => t.Active == true).ToListAsync();
                    return cardList;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            try
            {
                var cardIds = cards.Select(t => t.CardId).ToList();
                var cardNames = cards.Select(t => t.CardName).ToList();
                cardList = await _context.Set<Models.Card>().Where(t => t.Active == true && (cardIds.Contains(t.CardId) || cardNames.Contains(t.CardName))).ToListAsync();
                return cardList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }*/

        public async Task<Card> Update(Card card)
        {
            try
            {
                Models.Card trackedCard = await _context.Set<Models.Card>()
                    .Include(c => c.Types)
                    .Include(c => c.Results)
                    .FirstOrDefaultAsync(t => t.CardId == card.CardId);
                if (trackedCard == null)
                {
                    return null;
                }
                trackedCard.CardName = card.CardName;
                trackedCard.CardText = card.CardText;
                trackedCard.Active = card.Active;
                trackedCard.AttachmentId = card.AttachmentId;
                var trackedResultIds = new HashSet<int>(trackedCard.Results.Select(r => r.ResultId));
                var cardResultIds = new HashSet<int>(card.Results.Select(r => r.ResultId));

                // Add new results
                foreach (var result in card.Results)
                {
                    if (!trackedResultIds.Contains(result.ResultId))
                    {
                        trackedCard.Results.Add(result);
                    }
                }

                // Remove old results
                foreach (var result in trackedCard.Results.ToList())
                {
                    if (!card.Results.Any(r => r.ResultId == result.ResultId))
                    {
                        trackedCard.Results.Remove(result);
                    }
                }
                //trackedCard.Results.ToList().RemoveAll(r => !cardResultIds.Contains(r.ResultId));

                trackedCard.LastUpdated = DateOnly.FromDateTime(DateTime.Now);

                await _context.SaveChangesAsync();
                return trackedCard;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
