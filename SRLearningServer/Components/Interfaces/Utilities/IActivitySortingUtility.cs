using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.Utilities
{
    public interface IActivitySortingUtility
    {
        /// <summary>
        /// Sorts the Attachments by their status
        /// </summary>
        /// <param name="entities">The collection that will be ran through.</param>
        /// <param name="removeValue">The value of the Active property that will be removed.</param>
        /// <returns>Returns a list of AttachmentDto where entities with the Active property identical to the removeValue parameter is removed.</returns>
        public IEnumerable<AttachmentDto> SortActiveByStatus(IEnumerable<AttachmentDto> entities, bool removeValue = false);

        /// <summary>
        /// Sorts the activity by its status
        /// </summary>
        /// <param name="entity">The object being checked.</param>
        /// <param name="removeValue">the value being removed.</param>
        /// <returns>Returns an AttachmentDto if the Active property is different than the removeValue parameter, othervwise returns null.</returns>
        public AttachmentDto? SortActiveByStatus(AttachmentDto entity, bool removeValue = false);

        /// <summary>
        /// Sorts the Cards by their status
        /// </summary>
        /// <param name="entities">The collection that will be ran through.</param>
        /// <param name="removeValue">The value of the Active property that will be removed.</param>
        /// <returns>Returns a list of CardDto where entities with the Active property identical to the removeValue parameter is removed.</returns>
        public IEnumerable<CardDto> SortActiveByStatus(IEnumerable<CardDto> entities, bool removeValue = false);

        /// <summary>
        /// Sorts the CardDto by its status
        /// </summary>
        /// <param name="entity">The object being checked.</param>
        /// <param name="removeValue">The value of the Active property that will be removed.</param>
        /// <returns>Returns a CardDto if the Active property is different than the removeValue parameter, othervwise returns null.</returns>
        public CardDto? SortActiveByStatus(CardDto entity, bool removeValue = false);

        /// <summary>
        /// Sorts the Results by their status
        /// </summary>
        /// <param name="entities">The collection that will be ran through.</param>
        /// <param name="removeValue">The value of the Active property that will be removed.</param>
        /// <returns>Returns a list of ResultDto where entities with the Active property identical to the removeValue parameter is removed.</returns>
        public IEnumerable<ResultDto> SortActiveByStatus(IEnumerable<ResultDto> entities, bool removeValue = false);

        /// <summary>
        /// Sorts the Result by its status.
        /// </summary>
        /// <param name="entity">The object being checked.</param>
        /// <param name="removeValue">The value of the Active property that will be removed.</param>
        /// <returns>Returns a ResultDto if the Active property is different than the removeValue parameter, othervwise returns null.</returns>
        public ResultDto? SortActiveByStatus(ResultDto entity, bool removeValue = false);

        /// <summary>
        /// Sorts the TypeCategoryListDtos by its status.
        /// </summary>
        /// <param name="entities">The collection that will be ran through.</param>
        /// <param name="removeValue">The value of the Active property that will be removed.</param>
        /// <returns>Returns a list of TypeCategoryListDto where entities with the Active property identical to the removeValue parameter is removed</returns>
        public IEnumerable<TypeCategoryListDto> SortActiveByStatus(IEnumerable<TypeCategoryListDto> entities, bool removeValue = false);

        /// <summary>
        /// Sorts the TypeCategoryListDto by its status.
        /// </summary>
        /// <param name="entity">The object being checked.</param>
        /// <param name="removeValue">The value of the Active property that will be removed.</param>
        /// <returns>Returns a TypeCategoryListDto if the Active property is different than the removeValue parameter, othervwise returns null.</returns>
        public TypeCategoryListDto? SortActiveByStatus(TypeCategoryListDto entity, bool removeValue = false);

        /// <summary>
        /// Sorts the TypeDtos by their status.
        /// </summary>
        /// <param name="entities">The collection that will be ran through.</param>
        /// <param name="removeValue">The value of the Active property that will be removed.</param>
        /// <returns>Returns a list of TypeDto where entities with the Active property identical to the removeValue parameter is removed.</returns>
        public IEnumerable<TypeDto> SortActiveByStatus(IEnumerable<TypeDto> entities, bool removeValue = false);

        /// <summary>
        /// Sorts the TypeDto by its status.
        /// </summary>
        /// <param name="entity">The object being checked.</param>
        /// <param name="removeValue">The value of the Active property that will be removed.</param>
        /// <returns>Returns a TypeDto if the Active property is different than the removeValue parameter, othervwise returns null.</returns>
        public TypeDto? SortActiveByStatus(TypeDto entity, bool removeValue = false);
    }
}
