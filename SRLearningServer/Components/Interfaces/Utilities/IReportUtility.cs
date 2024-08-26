using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Interfaces.Utilities
{
    public interface IReportUtility
    {
        /// <summary>
        /// Formats a list of TypeDto objects into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(IEnumerable<TypeDto> entity, string seperator);
        /// <summary>
        /// Formats a TypeDto object into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(TypeDto entity, string seperator);
        /// <summary>
        /// Formats a list of AttachmentDto objects into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(IEnumerable<AttachmentDto> entity, string seperator);
        /// <summary>
        /// Formats an AttachmentDto object into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(AttachmentDto entity, string seperator);
        /// <summary>
        /// Formats a list of CardDto objects into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(IEnumerable<CardDto> entity, string seperator);
        /// <summary>
        /// Formats a CardDto object into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(CardDto entity, string seperator);
        /// <summary>
        /// Formats a list of ResultDto objects into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(IEnumerable<ResultDto> entity, string seperator);
        /// <summary>
        /// Formats a ResultDto object into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(ResultDto entity, string seperator);
        /// <summary>
        /// Formats a list of TypeCategoryListDto objects into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(IEnumerable<TypeCategoryListDto> entity, string seperator);
        /// <summary>
        /// Formats a TypeCategoryListDto object into a string for a report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Format(TypeCategoryListDto entity, string seperator);

        public Task GenerateReport(string report);

    }
}
