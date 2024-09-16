using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface IAttachmentRepository : IBaseRepository<Attachment>
    {
        /// <summary>
        /// Creates a new Attachment in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Attachment> Create(Attachment entity);
        /// <summary>
        /// Updates an Attachment in the database.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        Task<Attachment> Update(Attachment attachment);
        /// <summary>
        /// Gets an Attachment in the database by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Attachment> Get(int id);
    }
}
