using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.Services
{
    public interface IAttachmentService : IBaseDataService<Attachment, AttachmentDto>
    {
        /*/// <summary>
        /// Takes a single attachment and converts it to a DTO. If convertRelations is true, it will also convert the attachment's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public AttachmentDto ConvertToDto(Attachment entity, bool convertRelations = false);

        /// <summary>
        /// Takes a single DTO and converts it to an attachment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Attachment ConvertFromDto(AttachmentDto entity);*/
    }
}
