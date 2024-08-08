using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Converters
{
    public class DtoToDomainConverter : IDtoToDomainConverter
    {
        public List<Attachment> ConvertToDomainFromDto(List<AttachmentDto> entities)
        {
            if (entities.IsNullOrEmpty())
            {
                return null;
            }
            List<Attachment> attachments = new();
            try
            {
                foreach (AttachmentDto dto in entities)
                {
                    Attachment attachment = new()
                    {
                        AttachmentId = dto.AttachmentId,
                        AttachmentName = dto.AttachmentName,
                        AttachmentUrl = dto.AttachmentUrl,
                        LastUpdated = dto.LastUpdated,
                        Active = dto.Active
                    };
                    if (!dto.Results.IsNullOrEmpty())
                    {
                        foreach (Result result in ConvertToDomainFromDto(dto.Results))
                        {
                            attachment.Results.Add(result);
                        }
                    }
                    if (!dto.Cards.IsNullOrEmpty())
                    {
                        foreach (Card card in ConvertToDomainFromDto(dto.Cards))
                        {
                            attachment.Cards.Add(card);
                        }
                    }
                    attachments.Add(attachment);
                }
                return attachments;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Attachment ConvertToDomainFromDto(AttachmentDto entity)
        {
            if (entity is null)
            {
                return null;
            }
            try
            {
                List<AttachmentDto> attachmentDtos = new() { entity };
                List<Attachment> attachments = ConvertToDomainFromDto(attachmentDtos).ToList();
                return attachments[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Card> ConvertToDomainFromDto(List<CardDto> entities)
        {
            List<Card> cards = new();
            try
            {
                foreach (CardDto cardDto in entities)
                {
                    Card newCard = new()
                    {
                        CardId = cardDto.CardId,
                        CardName = cardDto.CardName,
                        CardText = cardDto.CardText,
                        //AttachmentId = cardDto.AttachmentId,
                        LastUpdated = cardDto.LastUpdated,
                        Active = cardDto.Active
                    };
                    if (!cardDto.Types.IsNullOrEmpty())
                    {
                        foreach (Components.Models.Type type in ConvertToDomainFromDto(cardDto.Types))
                        {
                            newCard.Types.Add(type);
                        }
                    }
                    if (!cardDto.Results.IsNullOrEmpty())
                    {
                        foreach (Result result in ConvertToDomainFromDto(cardDto.Results))
                        {
                            newCard.Results.Add(result);
                        }
                    }
                    if (cardDto.Attachment is not null)
                    {
                        newCard.Attachment = ConvertToDomainFromDto(cardDto.Attachment);
                    }
                    cards.Add(newCard);
                }
                return cards;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Card ConvertToDomainFromDto(CardDto entity)
        {
            try
            {
                List<CardDto> entities = new() { entity };
                List<Card> cards = ConvertToDomainFromDto(entities).ToList();
                return cards[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Result> ConvertToDomainFromDto(List<ResultDto> entities)
        {
            List<Result> results = new();
            try
            {
                foreach (ResultDto dto in entities)
                {
                    Result result = new()
                    {
                        ResultId = dto.ResultId,
                        //AttachmentId = dto.AttachmentId,
                        ResultText = dto.ResultText,
                        LastUpdated = dto.LastUpdated,
                        Active = dto.Active
                    };
                    if (dto.Attachment is not null)
                    {
                        result.Attachment = ConvertToDomainFromDto(dto.Attachment);
                    }
                    if (!dto.Cards.IsNullOrEmpty())
                    {
                        foreach (Card card in ConvertToDomainFromDto(dto.Cards))
                        {
                            result.Cards.Add(card);
                        }
                    }
                    results.Add(result);
                }
                return results;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Result ConvertToDomainFromDto(ResultDto entity)
        {
            try
            {
                List<ResultDto> entities = new() { entity };
                List<Result> results = ConvertToDomainFromDto(entities).ToList();
                return results[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Models.Type> ConvertToDomainFromDto(List<TypeDto> entities)
        {
            List<Models.Type> types = new();
            try
            {
                foreach (TypeDto dto in entities)
                {
                    Models.Type type = new()
                    {
                        TypeId = dto.TypeId,
                        CardTypeName = dto.CardTypeName,
                        LastUpdated = dto.LastUpdated,
                        Active = dto.Active
                    };
                    if (!dto.Cards.IsNullOrEmpty())
                    {
                        foreach (Card card in ConvertToDomainFromDto(dto.Cards))
                        {
                            type.Cards.Add(card);
                        }
                    }
                    if (!dto.TypeCategoryLists.IsNullOrEmpty())
                    {
                        foreach (TypeCategoryList tcl in ConvertToDomainFromDto(dto.TypeCategoryLists))
                        {
                            type.TypeCategoryLists.Add(tcl);
                        }
                    }
                    types.Add(type);
                }
                return types;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Models.Type ConvertToDomainFromDto(TypeDto entity)
        {
            List<TypeDto> entities = new() { entity };
            List<Models.Type> types = ConvertToDomainFromDto(entities).ToList();
            return types[0];
        }

        public List<TypeCategoryList> ConvertToDomainFromDto(List<TypeCategoryListDto> entities)
        {
            List<TypeCategoryList> typeCategoryLists = new();
            try
            {
                foreach (TypeCategoryListDto dto in entities)
                {
                    TypeCategoryList typeCategoryList = new()
                    {
                        TypeCategoryListId = dto.TypeCategoryListId,
                        TypeCategoryListName = dto.TypeCategoryListName,
                        LastUpdated = dto.LastUpdated,
                        Active = dto.Active
                    };
                    if (!dto.Types.IsNullOrEmpty())
                    {
                        foreach (Models.Type tcl in ConvertToDomainFromDto(dto.Types))
                        {
                            typeCategoryList.Types.Add(tcl);
                        }
                    }
                    typeCategoryLists.Add(typeCategoryList);
                }
                return typeCategoryLists;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public TypeCategoryList ConvertToDomainFromDto(TypeCategoryListDto entity)
        {
            List<TypeCategoryListDto> entities = new() { entity };
            List<TypeCategoryList> typeCategoryLists = ConvertToDomainFromDto(entities).ToList();
            return typeCategoryLists[0];
        }
    }
}
