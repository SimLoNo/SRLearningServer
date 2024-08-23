using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IActiveCheckService
    {
        /// <summary>
        /// Takes a list of CardDto objects and checks if they are active.
        /// </summary>
        /// <param name="entities">List of CardDto to go through and check for any inactive.</param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a list of strings indicating inactive entities.</returns>
        public List<string> CheckActive(IEnumerable<CardDto> entities, bool active);
        /// <summary>
        /// Takes a CardDto object and checks if it is active.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(CardDto entity, bool active);

        /// <summary>
        /// Takes a list of TypeCategoryListDto objects and checks if they are active.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a list of strings indicating inactive entities.</returns>
        public List<string> CheckActive(IEnumerable<TypeCategoryListDto> entities, bool active);
        /// <summary>
        /// Takes a TypeCategoryListDto object and checks if it is active.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(TypeCategoryListDto entity, bool active);

        /// <summary>
        /// Takes a list of AttachmentDto objects and checks if they are active.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a list of strings indicating inactive entities.</returns>
        public List<string> CheckActive(IEnumerable<AttachmentDto> entities, bool active);
        /// <summary>
        /// Takes an AttachmentDto object and check if it is inactive.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(AttachmentDto entity, bool active);

        /// <summary>
        /// Takes a list of ResultDto and checks if they are active.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a list of strings indicating inactive entities</returns>
        public List<string> CheckActive(IEnumerable<ResultDto> entities, bool active);
        /// <summary>
        /// Takes a ResultDto object and checks if it is active.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(ResultDto entity, bool active);

        /// <summary>
        /// Takes a list of TypeDto objects and checks if they are active.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a list of strings indicating inactive entities</returns>
        public List<string> CheckActive(IEnumerable<TypeDto> entities, bool active);
        /// <summary>
        /// Takes a TypeDto object and checks if it is active.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="active">A boolean indicating whether to check for active or inactive status.</param>
        /// <returns>Returns a string to identify if the entity is inactive.</returns>
        public string CheckActive(TypeDto entity, bool active);
    }
}
