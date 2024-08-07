using Microsoft.EntityFrameworkCore;
using SRLearningServer.Components.Context;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Repositories
{
    public class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(SRContext context) : base(context)
        {
        }

        /*/// <summary>
        /// Gets all active Attachments from the database.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Attachment>> GetMultiple()
        {
            try
            {
                return await _context.Set<Attachment>().Where(t => t.Active == true).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }*/

        /// <summary>
        /// Takes an Attachment and updates the database entry with the same AttachmentId. If the Attachment does not exist, null is returned.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Attachment> Update(Attachment attachment)
        {
            try
            {
                Attachment trackedAttachment = await _context.Set<Attachment>().FirstOrDefaultAsync(t => t.AttachmentId == attachment.AttachmentId);
                if (trackedAttachment == null)
                {
                    return null;
                }
                trackedAttachment.AttachmentName = attachment.AttachmentName;
                trackedAttachment.AttachmentUrl = attachment.AttachmentUrl;
                trackedAttachment.Active = attachment.Active;
                trackedAttachment.LastUpdated = DateOnly.FromDateTime(DateTime.Now);

                // Update results
                var trackedResultIds = new HashSet<int>(trackedAttachment.Results.Select(r => r.ResultId));
                var attachmentResultIds = new HashSet<int>(attachment.Results.Select(r => r.ResultId));

                // Add new results
                foreach (var result in attachment.Results)
                {
                    if (!trackedResultIds.Contains(result.ResultId))
                    {
                        trackedAttachment.Results.Add(result);
                    }
                }

                // Remove old results
                foreach (Result result in trackedAttachment.Results.ToList())
                {
                    if (!attachment.Results.Any(r => r.ResultId == result.ResultId))
                    {
                        trackedAttachment.Results.Remove(result);
                    }
                }
                //trackedAttachment.Results.ToList().RemoveAll(r => !attachmentResultIds.Contains(r.ResultId));

                // Update Cards
                var trackedCardIds = new HashSet<int>(trackedAttachment.Cards.Select(r => r.CardId));
                var attachmentCardIds = new HashSet<int>(attachment.Cards.Select(r => r.CardId));

                // Add new cards
                foreach (var card in attachment.Cards)
                {
                    if (!trackedCardIds.Contains(card.CardId))
                    {
                        trackedAttachment.Cards.Add(card);
                    }
                }

                // Remove old cards
                foreach (Card card in trackedAttachment.Cards.ToList())
                {
                    if (!attachment.Cards.Any(c => c.CardId == card.CardId))
                    {
                        trackedAttachment.Cards.Remove(card);
                    }
                }
                //trackedAttachment.Cards.ToList().RemoveAll(r => !attachmentCardIds.Contains(r.CardId));

                await _context.SaveChangesAsync();
                return trackedAttachment;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
