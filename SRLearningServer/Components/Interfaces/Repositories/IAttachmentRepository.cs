using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface IAttachmentRepository : IBaseRepository<Attachment>
    {
        Task<Attachment> Create(Attachment entity);
        Task<Attachment> Update(Attachment attachment);
        Task<Attachment> Get(int id);
    }
}
