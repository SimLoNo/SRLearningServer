using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;
using System.Net.Mail;

namespace SRLearningServer.Components.Interfaces.Converters
{
    public interface IDomainToDtoConverter
    {
        /// <summary>
        /// Takes an IEnumerable of Attachment and converts them to an IEnumerable of AttachmentDto. If convertRelations is true, it will also convert the Attachment's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public List<AttachmentDto> ConvertToDtoFromDomain(IEnumerable<Models.Attachment> entities, bool convertRelations = false);


        /// <summary>
        /// Takes a single Attachment and converts it to an AttachmentDto. If convertRelations is true, it will also convert the Attachment's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public AttachmentDto ConvertToDtoFromDomain(Models.Attachment entity, bool convertRelations = false);


        /// <summary>
        /// Takes an IEnumerable of Card and converts them to an IEnumerable of CardDto. If convertRelations is true, it will also convert the Card's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public List<CardDto> ConvertToDtoFromDomain(IEnumerable<Card> entities, bool convertRelations = false);


        /// <summary>
        /// Takes a single Card and converts it to an CardDto. If convertRelations is true, it will also convert the Card's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public CardDto ConvertToDtoFromDomain(Card entity, bool convertRelations = false);


        /// <summary>
        /// Takes an IEnumerable of Result and converts them to an IEnumerable of ResultDto. If convertRelations is true, it will also convert the Result's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public List<ResultDto> ConvertToDtoFromDomain(IEnumerable<Result> entities, bool convertRelations = false);


        /// <summary>
        /// Takes a single Result and converts it to an ResultDto. If convertRelations is true, it will also convert the Result's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public ResultDto ConvertToDtoFromDomain(Result entity, bool convertRelations = false);


        /// <summary>
        /// Takes an IEnumerable of Type and converts them to an IEnumerable of TypeDto. If convertRelations is true, it will also convert the Type's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public List<TypeDto> ConvertToDtoFromDomain(IEnumerable<Models.Type> entities, bool convertRelations = false);


        /// <summary>
        /// Takes a single Type and converts it to an TypeDto. If convertRelations is true, it will also convert the Type's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public TypeDto ConvertToDtoFromDomain(Models.Type entity, bool convertRelations = false);

        /// <summary>
        /// Takes an IEnumerable of TypeCategoryList and converts them to an IEnumerable of TypeCategoryListDto. If convertRelations is true, it will also convert the TypeCategoryList's relations
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public List<TypeCategoryListDto> ConvertToDtoFromDomain(IEnumerable<TypeCategoryList> entities, bool convertRelations = false);

        /// <summary>
        /// Takes a single TypeCategoryList and converts it to an TypeCategoryListDto. If convertRelations is true, it will also convert the TypeCategoryList's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public TypeCategoryListDto ConvertToDtoFromDomain(TypeCategoryList entity, bool convertRelations = false);
    }
}
