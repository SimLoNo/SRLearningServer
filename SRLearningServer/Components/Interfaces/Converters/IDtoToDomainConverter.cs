using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Converters
{
    public interface IDtoToDomainConverter
    {
        /// <summary>
         /// Takes an List of AttachmentDto and converts them to an List of Attachment. If convertRelations is true, it will also convert the AttachmentDto's relations
         /// </summary>
         /// <param name="entities"></param>
         /// <param name="convertRelations"></param>
         /// <returns></returns>
        public List<Attachment> ConvertToDomainFromDto(List<AttachmentDto> entities);


        /// <summary>
        /// Takes a single AttachmentDto and converts it to an Attachment. If convertRelations is true, it will also convert the AttachmentDto's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public Attachment ConvertToDomainFromDto(AttachmentDto entity);


        /// <summary>
        /// Takes an List of CardDto and converts them to an List of Card. If convertRelations is true, it will also convert the CardDto's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public List<Card> ConvertToDomainFromDto(List<CardDto> entities);


        /// <summary>
        /// Takes a single CardDto and converts it to an Card. If convertRelations is true, it will also convert the CardDto's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public Card ConvertToDomainFromDto(CardDto entity);


        /// <summary>
        /// Takes an List of ResultDto and converts them to an List of Result. If convertRelations is true, it will also convert the ResultDto's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public List<Result> ConvertToDomainFromDto(List<ResultDto> entities);


        /// <summary>
        /// Takes a single ResultDto and converts it to an Result. If convertRelations is true, it will also convert the ResultDto's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public Result ConvertToDomainFromDto(ResultDto entity);


        /// <summary>
        /// Takes an List of TypeDto and converts them to an List of Type. If convertRelations is true, it will also convert the TypeDto's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public List<Models.Type> ConvertToDomainFromDto(List<TypeDto> entities);


        /// <summary>
        /// Takes a single TypeDto and converts it to an Type. If convertRelations is true, it will also convert the TypeDto's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public Models.Type ConvertToDomainFromDto(TypeDto entity);
    }
}
