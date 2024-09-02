using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Converters
{
    public class DomainToDtoConverter : IDomainToDtoConverter
    {
        public List<AttachmentDto> ConvertToDtoFromDomain(IEnumerable<Attachment> entities, bool convertRelations = false)
        {
            if (entities.IsNullOrEmpty())
            {
                return null;
            }
            List<AttachmentDto> attachmentDtos = new();
            try
            {
                foreach (Attachment attachment in entities)
                {
                    AttachmentDto newAttachment = new()
                    {
                        AttachmentId = attachment.AttachmentId,
                        AttachmentName = attachment.AttachmentName,
                        AttachmentUrl = attachment.AttachmentUrl,
                        LastUpdated = attachment.LastUpdated,
                        Active = attachment.Active,
                        Cards = new List<CardDto>(),
                        Results = new List<ResultDto>()
                    };
                    if (convertRelations == true)
                    {
                        if (!attachment.Cards.IsNullOrEmpty())
                        {
                            newAttachment.Cards =ConvertToDtoFromDomain(attachment.Cards);
                        }
                        if (!attachment.Results.IsNullOrEmpty())
                        {
                            newAttachment.Results = ConvertToDtoFromDomain(attachment.Results);
                        }
                    }
                    attachmentDtos.Add(newAttachment);
                }
                return attachmentDtos;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public AttachmentDto ConvertToDtoFromDomain(Attachment entity, bool convertRelations = false)
        {
            if (entity is null)
            {
                return null;
            }
            try
            {
                List<Attachment> attachments = new() { entity };
                List<AttachmentDto> attachmentDtos = ConvertToDtoFromDomain(attachments, convertRelations).ToList();
                return attachmentDtos[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<CardDto> ConvertToDtoFromDomain(IEnumerable<Card> entities, bool convertRelations = false)
        {
            if (entities.IsNullOrEmpty())
            {
                return null;
            }
            List<CardDto> cardDtos = new();
            try
            {
                foreach (var card in entities)
                {
                    CardDto newDto = new()
                    {
                        CardId = card.CardId,
                        CardName = card.CardName,
                        CardText = card.CardText,
                        AttachmentId = card.AttachmentId,
                        LastUpdated = card.LastUpdated,
                        Active = card.Active,
                        Types = new List<TypeDto>(),
                        Results = new List<ResultDto>(),


                    };
                    if (convertRelations == true)
                    {
                        if (!card.Types.IsNullOrEmpty())
                        {
                            newDto.Types = ConvertToDtoFromDomain(card.Types, false);
                        }
                        if (!card.Results.IsNullOrEmpty())
                        {
                            newDto.Results = ConvertToDtoFromDomain(card.Results, false);
                        }
                        if (card.Attachment is not null)
                        {
                            newDto.Attachment = ConvertToDtoFromDomain(card.Attachment, false);
                        }

                    }
                    cardDtos.Add(newDto);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return cardDtos;
        }

        public CardDto ConvertToDtoFromDomain(Card entity, bool convertRelations = false)
        {
            if (entity is null)
            {
                return null;
            }
            try
            {
                List<Card> entities = new() { entity };
                List<CardDto> cardDtos = ConvertToDtoFromDomain(entities, convertRelations).ToList();
                return cardDtos[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<ResultDto> ConvertToDtoFromDomain(IEnumerable<Result> entities, bool convertRelations = false)
        {
            if (entities.IsNullOrEmpty())
            {
                return null;
            }
            List<ResultDto> dtos = new();
            try
            {
                foreach (Result result in entities)
                {
                    ResultDto dto = new()
                    {
                        ResultId = result.ResultId,
                        AttachmentId = result.AttachmentId,
                        ResultText = result.ResultText,
                        LastUpdated = result.LastUpdated,
                        Active = result.Active,
                        Cards = new List<CardDto>(),

                    };
                    if (convertRelations == true)
                    {
                        if (result.Attachment is not null)
                        {
                            dto.Attachment = ConvertToDtoFromDomain(result.Attachment);
                        }
                        if (!result.Cards.IsNullOrEmpty())
                        {
                            dto.Cards = ConvertToDtoFromDomain(result.Cards);
                        }
                    }

                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public ResultDto ConvertToDtoFromDomain(Result entity, bool convertRelations = false)
        {
            if (entity is null)
            {
                return null;
            }
            try
            {
                List<Result> entities = new() { entity };
                List<ResultDto> dtos = ConvertToDtoFromDomain(entities, convertRelations).ToList();
                return dtos[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<TypeDto> ConvertToDtoFromDomain(IEnumerable<Models.Type> entities, bool convertRelations = false)
        {
            if (entities.IsNullOrEmpty())
            {
                return null;
            }
            List<TypeDto> typeDtos = new();
            try
            {
                foreach (Models.Type type in entities)
                {
                    TypeDto dto = new()
                    {
                        TypeId = type.TypeId,
                        CardTypeName = type.CardTypeName,
                        LastUpdated = type.LastUpdated,
                        Active = type.Active,
                        Cards = new List<CardDto>(),
                        TypeCategoryLists = new List<TypeCategoryListDto>()
                    };
                    if (convertRelations == true)
                    {
                        if (!type.Cards.IsNullOrEmpty())
                        {
                            dto.Cards = ConvertToDtoFromDomain(type.Cards);
                        }
                        if (!type.TypeCategoryLists.IsNullOrEmpty())
                        {
                            dto.TypeCategoryLists = ConvertToDtoFromDomain(type.TypeCategoryLists);
                        }
                    }
                    typeDtos.Add(dto);
                }
                return typeDtos;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public TypeDto ConvertToDtoFromDomain(Models.Type entity, bool convertRelations = false)
        {
            if (entity is null)
            {
                return null;
            }
            try
            {
                List<Models.Type> entities = new() { entity };
                List<TypeDto> typeDtos = ConvertToDtoFromDomain(entities, convertRelations).ToList();
                return typeDtos[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<TypeCategoryListDto> ConvertToDtoFromDomain(IEnumerable<TypeCategoryList> entities, bool convertRelations = false)
        {
            if (entities.IsNullOrEmpty())
            {
                return null;
            }
            List<TypeCategoryListDto> typeDtos = new();
            try
            {
                foreach (TypeCategoryList typeCategoryList in entities)
                {
                    TypeCategoryListDto dto = new()
                    {
                        TypeCategoryListId = typeCategoryList.TypeCategoryListId,
                        TypeCategoryListName = typeCategoryList.TypeCategoryListName,
                        LastUpdated = typeCategoryList.LastUpdated,
                        Active = typeCategoryList.Active,
                        Types = new List<TypeDto>()
                    };
                    if (convertRelations == true)
                    {
                        if (!typeCategoryList.Types.IsNullOrEmpty())
                        {
                            dto.Types = ConvertToDtoFromDomain(typeCategoryList.Types);
                        }
                    }
                    typeDtos.Add(dto);
                }
                return typeDtos;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public TypeCategoryListDto ConvertToDtoFromDomain(TypeCategoryList entity, bool convertRelations = false)
        {
            if (entity is null)
            {
                return null;
            }
            try
            {
                List<TypeCategoryList> entities = new() { entity };
                List<TypeCategoryListDto> typeCategoryListDtos = ConvertToDtoFromDomain(entities, convertRelations).ToList();
                return typeCategoryListDtos[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
