using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendAttachmentService
    {
        /// <summary>
        /// Finds all the Attachments in the database.
        /// </summary>
        /// <returns>List of AttachmentDto</returns>
        public Task<List<AttachmentDto>> GetAll();
        /// <summary>
        /// Creates a new Attachment in the database.
        /// </summary>
        /// <param name="attachment">Attachment</param>
        /// <returns>AttachmentDto</returns>
        public Task<AttachmentDto> Create(AttachmentDto attachment);
        /// <summary>
        /// Deactivates an Attachment in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AttachmentDto</returns>
        public Task<AttachmentDto> Deactivate(int id);
        /// <summary>
        /// Deletes an Attachment from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AttachmentDto</returns>
        public Task<AttachmentDto> Delete(int id);
        /// <summary>
        /// Gets the attachment by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AttachmentDto if found, otherwise null</returns>
        public Task<AttachmentDto> GetById(int id);
        /// <summary>
        /// Update an Attachment in the database.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns>AttachmentDto</returns>
        public Task<AttachmentDto> Update(AttachmentDto attachment);
    }
}
