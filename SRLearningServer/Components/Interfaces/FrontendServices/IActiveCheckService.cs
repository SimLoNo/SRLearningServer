using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IActiveCheckService
    {
        /// <summary>
        /// Takes a list of CardDto objects and checks if they are active.
        /// </summary>
        /// <param name="entities">List of CardDto to go through and check for any inactive.</param>
        /// <returns>Returns a list of strings indicating inactive entities.</returns>
        public List<string> CheckActive(List<CardDto> entities);
        /// <summary>
        /// Takes a CardDto object and checks if it is active.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(CardDto entity);

        /// <summary>
        /// Takes a list of TypeCategoryListDto objects and checks if they are active.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Returns a list of strings indicating inactive entities.</returns>
        public List<string> CheckActive(List<TypeCategoryListDto> entities);
        /// <summary>
        /// Takes a TypeCategoryListDto object and checks if it is active.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(TypeCategoryListDto entity);

        /// <summary>
        /// Takes a list of AttachmentDto objects and checks if they are active.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Returns a list of strings indicating inactive entities.</returns>
        public List<string> CheckActive(List<AttachmentDto> entities);
        /// <summary>
        /// Takes an AttachmentDto object and check if it is inactive.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(AttachmentDto entity);

        /// <summary>
        /// Takes a list of ResultDto and checks if they are active.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Returns a list of strings indicating inactive entities</returns>
        public List<string> CheckActive(List<ResultDto> entities);
        /// <summary>
        /// Takes a ResultDto object and checks if it is active.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(ResultDto entity);

        /// <summary>
        /// Takes a list of TypeDto objects and checks if they are active.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Returns a list of strings indicating inactive entities</returns>
        public List<string> CheckActive(List<TypeDto> entities);
        /// <summary>
        /// Takes a TypeDto object and checks if it is active.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(TypeDto entity);
    }
}
