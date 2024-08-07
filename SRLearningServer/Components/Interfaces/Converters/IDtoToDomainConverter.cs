using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Converters
{
    public interface IDtoToDomainConverter
    {
        /// <summary>
         /// Takes an IEnumerable of AttachmentDto and converts them to an IEnumerable of Attachment. If convertRelations is true, it will also convert the AttachmentDto's relations
         /// </summary>
         /// <param name="entities"></param>
         /// <param name="convertRelations"></param>
         /// <returns></returns>
        public IEnumerable<Attachment> ConvertToAttachmentFromAttachmentDto(IEnumerable<AttachmentDto> entities);


        /// <summary>
        /// Takes a single AttachmentDto and converts it to an Attachment. If convertRelations is true, it will also convert the AttachmentDto's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public Attachment ConvertToAttachmentFromAttachmentDto(AttachmentDto entity);


        /// <summary>
        /// Takes an IEnumerable of CardDto and converts them to an IEnumerable of Card. If convertRelations is true, it will also convert the CardDto's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public IEnumerable<Card> ConvertToCardFromCardDto(IEnumerable<CardDto> entities);


        /// <summary>
        /// Takes a single CardDto and converts it to an Card. If convertRelations is true, it will also convert the CardDto's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public Card ConvertToCardFromCardDto(CardDto entity);


        /// <summary>
        /// Takes an IEnumerable of ResultDto and converts them to an IEnumerable of Result. If convertRelations is true, it will also convert the ResultDto's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public IEnumerable<Result> ConvertToResultFromResultDto(IEnumerable<ResultDto> entities);


        /// <summary>
        /// Takes a single ResultDto and converts it to an Result. If convertRelations is true, it will also convert the ResultDto's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public Result ConvertToResultFromResultDto(ResultDto entity);


        /// <summary>
        /// Takes an IEnumerable of TypeDto and converts them to an IEnumerable of Type. If convertRelations is true, it will also convert the TypeDto's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public IEnumerable<Models.Type> ConvertToTypeFromTypeDto(IEnumerable<TypeDto> entities);


        /// <summary>
        /// Takes a single TypeDto and converts it to an Type. If convertRelations is true, it will also convert the TypeDto's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public Models.Type ConvertToTypeFromTypeDto(TypeDto entity);
    }
}
