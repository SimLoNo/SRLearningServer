using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Converters
{
    public class DtoToDomainConverter : IDtoToDomainConverter
    {
        public IEnumerable<Attachment> ConvertToAttachmentFromAttachmentDto(IEnumerable<AttachmentDto> entities)
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
                        foreach (Result result in ConvertToResultFromResultDto(dto.Results))
                        {
                            attachment.Results.Add(result);
                        }
                    }
                    if (!dto.Cards.IsNullOrEmpty())
                    {
                        foreach (Card card in ConvertToCardFromCardDto(dto.Cards))
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

        public Attachment ConvertToAttachmentFromAttachmentDto(AttachmentDto entity)
        {
            if (entity is null)
            {
                return null;
            }
            try
            {
                List<AttachmentDto> attachmentDtos = new() { entity };
                List<Attachment> attachments = ConvertToAttachmentFromAttachmentDto(attachmentDtos).ToList();
                return attachments[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Card> ConvertToCardFromCardDto(IEnumerable<CardDto> entities)
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
                        foreach (Components.Models.Type type in ConvertToTypeFromTypeDto(cardDto.Types))
                        {
                            newCard.Types.Add(type);
                        }
                    }
                    if (!cardDto.Results.IsNullOrEmpty())
                    {
                        foreach (Result result in ConvertToResultFromResultDto(cardDto.Results))
                        {
                            newCard.Results.Add(result);
                        }
                    }
                    if (cardDto.Attachment is not null)
                    {
                        newCard.Attachment = ConvertToAttachmentFromAttachmentDto(cardDto.Attachment);
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

        public Card ConvertToCardFromCardDto(CardDto entity)
        {
            try
            {
                List<CardDto> entities = new() { entity };
                List<Card> cards = ConvertToCardFromCardDto(entities).ToList();
                return cards[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Result> ConvertToResultFromResultDto(IEnumerable<ResultDto> entities)
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
                        result.Attachment = ConvertToAttachmentFromAttachmentDto(dto.Attachment);
                    }
                    if (!dto.Cards.IsNullOrEmpty())
                    {
                        foreach (Card card in ConvertToCardFromCardDto(dto.Cards))
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

        public Result ConvertToResultFromResultDto(ResultDto entity)
        {
            try
            {
                List<ResultDto> entities = new() { entity };
                List<Result> results = ConvertToResultFromResultDto(entities).ToList();
                return results[0];
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Models.Type> ConvertToTypeFromTypeDto(IEnumerable<TypeDto> entities)
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
                        foreach (Card card in ConvertToCardFromCardDto(dto.Cards))
                        {
                            type.Cards.Add(card);
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

        public Models.Type ConvertToTypeFromTypeDto(TypeDto entity)
        {
            List<TypeDto> entities = new() { entity };
            List<Models.Type> types = ConvertToTypeFromTypeDto(entities).ToList();
            return types[0];
        }
    }
}
