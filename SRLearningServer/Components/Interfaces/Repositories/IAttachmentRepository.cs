using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface IAttachmentRepository : IBaseRepository<Attachment>
    {

        Task<Attachment> Update(Attachment attachment);
    }
}
