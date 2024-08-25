using Microsoft.EntityFrameworkCore;
using SRLearningServer.Components.Context;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Models;
using System.Net.Mail;

namespace SRLearningServer.Components.Repositories
{
    public class ResultRepository : BaseRepository<Models.Result>, IResultRepository
    {
        public ResultRepository(SRContext context) : base(context)
        {

        }

        public async Task<Result> Create(Result entity)
        {
            try
            {
                entity.LastUpdated = DateOnly.FromDateTime(DateTime.UtcNow);
                foreach (var card in entity.Cards.ToList())
                {
                    var newCard = await _context.Cards.FirstOrDefaultAsync(c => c.CardId == card.CardId);
                    entity.Cards.Remove(card);
                    if (newCard != null)
                    {
                        entity.Cards.Add(newCard);
                    }
                }
                _context.Results.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /*public async Task<IEnumerable<Result>> GetMultiple() // TODO need to figure out how I want this to work, should it simply return all active results in which case it will need a rename, or should there be a selection process, and if so what should I select on?
        {
            try
            {
                return await _context.Set<Result>().Where(t => t.Active == true).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }*/

        /// <summary>
        /// Returns a Result with it's relations with the given id. If no Result is found, returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Get(int id)
        {
            try
            {
                var foundEntity = await _context.Results
                    .Include(r => r.Cards)
                    .Include(r => r.Attachment)
                    .FirstOrDefaultAsync(r => r.ResultId == id);
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
        /// <summary>
        /// Updates a result in the database with the new result data. If the result does not exist in the database, returns null.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result> Update(Result result)
        {
            try
            {
                Result trackedResult = await _context.Set<Result>()
                    .Include(r => r.Cards)
                    .Include(r => r.Attachment)
                    .FirstOrDefaultAsync(t => t.ResultId == result.ResultId);
                if (trackedResult == null)
                {
                    return null;
                }
                trackedResult.ResultText = result.ResultText;
                trackedResult.Active = result.Active;
                trackedResult.AttachmentId = result.AttachmentId;
                trackedResult.Attachment = result.Attachment;
                var trackedCardIds = new HashSet<int>(trackedResult.Cards.Select(r => r.CardId));
                var resultCardIds = new HashSet<int>(result.Cards.Select(r => r.CardId));

                // Add new results
                foreach (var card in result.Cards)
                {
                    if (!trackedCardIds.Contains(card.CardId))
                    {
                        trackedResult.Cards.Add(card);
                    }
                }

                // Remove old results
                foreach (Card card in trackedResult.Cards.ToList())
                {
                    if (!result.Cards.Any(c => c.CardId == card.CardId))
                    {
                        trackedResult.Cards.Remove(card);
                    }
                }
                //trackedResult.Cards.ToList().RemoveAll(r => !resultCardIds.Contains(r.CardId));

                trackedResult.LastUpdated = DateOnly.FromDateTime(DateTime.Now);

                await _context.SaveChangesAsync();
                return trackedResult;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
