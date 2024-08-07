using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLearningServer.Tests.Utilities
{
    public class TestDataGenerator
    {
        public AttachmentDto CreateAttachmentDto(int id, string name, string url, DateOnly updated, bool active, List<CardDto> cards, List<ResultDto> results)
        {
            AttachmentDto dto = new()
            {
                AttachmentId = id,
                AttachmentName = name,
                AttachmentUrl = url,
                LastUpdated = updated,
                Active = active,
            };
            if (!cards.IsNullOrEmpty())
            {
                dto.Cards.AddRange(cards);
            }
            if (!results.IsNullOrEmpty())
            {
                dto.Results.AddRange(results);
            }
            return dto;
        }
        public CardDto CreateCardDto(int id, string name, string text, DateOnly updated, bool active, List<TypeDto> types, List<ResultDto> results)
        {
            CardDto dto = new()
            {
                CardId = id,
                CardName = name,
                CardText = text,
                LastUpdated = updated,
                Active = active,
            };
            if (!types.IsNullOrEmpty())
            {
                dto.Types.AddRange(types);
            }
            if (!results.IsNullOrEmpty())
            {
                dto.Results.AddRange(results);
            }
            return dto;
        }
        public ResultDto CreateResultDto(int id, string text, DateOnly updated, bool active, AttachmentDto attachment, List<CardDto> cards)
        {
            ResultDto dto = new()
            {
                ResultId = id,
                ResultText = text,
                LastUpdated = updated,
                Active = active,
            };
            if (attachment is not null)
            {
                dto.Attachment = attachment;
            }
            if (!cards.IsNullOrEmpty())
            {
                dto.Cards.AddRange(cards);
            }
            return dto;
        }

        public TypeDto CreateTypeDto(int id, string name, DateOnly updated, bool active, List<CardDto> cards)
        {
            TypeDto dto = new()
            {
                TypeId = id,
                CardTypeName = name,
                LastUpdated = updated,
                Active = active,
            };
            if (!cards.IsNullOrEmpty())
            {
                dto.Cards.AddRange(cards);
            }
            return dto;
        }

        public Attachment CreateAttachment(int id, string name, string url, DateOnly updated, bool active, List<Card> cards, List<Result> results)
        {
            Attachment entity = new()
            {
                AttachmentId = id,
                AttachmentName = name,
                AttachmentUrl = url,
                LastUpdated = updated,
                Active = active,
            };
            if (!cards.IsNullOrEmpty())
            {
                foreach (Card card in cards)
                {
                    entity.Cards.Add(card);
                }
            }
            if (!results.IsNullOrEmpty())
            {
                foreach (Result result in results)
                {
                    entity.Results.Add(result);
                }
            }
            return entity;
        }
        public Card CreateCard(int id, string name, string text, DateOnly updated, bool active, List<Components.Models.Type> types, List<Result> results)
        {
            Card entity = new()
            {
                CardId = id,
                CardName = name,
                CardText = text,
                LastUpdated = updated,
                Active = active,
            };
            if (!types.IsNullOrEmpty())
            {
                foreach (Components.Models.Type type in types)
                {
                    entity.Types.Add(type);
                }
            }
            if (!results.IsNullOrEmpty())
            {
                foreach (Result result in results)
                {
                    entity.Results.Add(result);
                }
            }
            return entity;
        }
        public Result CreateResult(int id, string text, DateOnly updated, bool active, Attachment attachment)
        {
            Result entity = new()
            {
                ResultId = id,
                ResultText = text,
                LastUpdated = updated,
                Active = active,
            };
            if (attachment is not null)
            {
                entity.Attachment = attachment;
            }
            return entity;
        }

        public Components.Models.Type CreateType(int id, string name, DateOnly updated, bool active)
        {
            Components.Models.Type entity = new()
            {
                TypeId = id,
                CardTypeName = name,
                LastUpdated = updated,
                Active = active,
            };
            return entity;
        }

        public Attachment CreateAttachmentFromDto(AttachmentDto dto)
        {
            Attachment entity = new()
            {
                AttachmentId = dto.AttachmentId,
                AttachmentName = dto.AttachmentName,
                AttachmentUrl = dto.AttachmentUrl,
                LastUpdated = dto.LastUpdated,
                Active = dto.Active,
            };
            if (!dto.Cards.IsNullOrEmpty())
            {
                foreach (CardDto cardDto in dto.Cards)
                {
                    entity.Cards.Add(CreateCardFromDto(cardDto));
                }
            }
            if (!dto.Results.IsNullOrEmpty())
            {
                foreach (ResultDto resultDto in dto.Results)
                {
                    entity.Results.Add(CreateResultFromDto(resultDto));
                }
            }
            return entity;
        }
        public Card CreateCardFromDto(CardDto dto)
        {
            Card entity = new()
            {
                CardId = dto.CardId,
                CardName = dto.CardName,
                CardText = dto.CardText,
                //AttachmentId = dto.AttachmentId,
                LastUpdated = dto.LastUpdated,
                Active = dto.Active,
            };
            if (!dto.Types.IsNullOrEmpty())
            {
                foreach (TypeDto typeDto in dto.Types) 
                {
                    entity.Types.Add(CreateTypeFromDto(typeDto));
                }
            }
            if (!dto.Results.IsNullOrEmpty())
            {
                foreach (ResultDto resultDto in dto.Results)
                {
                    entity.Results.Add(CreateResultFromDto(resultDto));
                }
            }
            if(dto.Attachment is not null)
            {
                entity.Attachment = CreateAttachmentFromDto(dto.Attachment);
            }
            return entity;
        }
        public Result CreateResultFromDto(ResultDto dto)
        {
            Result entity = new()
            {
                ResultId = dto.ResultId,
                ResultText = dto.ResultText,
                //AttachmentId = dto.AttachmentId,
                LastUpdated = dto.LastUpdated,
                Active = dto.Active,
            };
            if (!dto.Cards.IsNullOrEmpty())
            {
                foreach (CardDto cardDto in dto.Cards)
                {
                    entity.Cards.Add(CreateCardFromDto(cardDto));
                }
            }
            if (dto.Attachment is not null)
            {
                entity.Attachment = CreateAttachmentFromDto(dto.Attachment);
            }
            return entity;
        }
        public Components.Models.Type CreateTypeFromDto(TypeDto dto)
        {
            Components.Models.Type entity = new()
            {
                TypeId = dto.TypeId,
                CardTypeName = dto.CardTypeName,
                LastUpdated = dto.LastUpdated,
                Active = dto.Active,
            };
            if (!dto.Cards.IsNullOrEmpty())
            {
                foreach (CardDto cardDto in dto.Cards)
                {
                    entity.Cards.Add(CreateCardFromDto(cardDto));
                }
            }
            return entity;
        }

        public TypeCategoryList CreateTypeCategoryList(int id, string name, DateOnly updated, bool active)
        {
            TypeCategoryList entity = new()
            {
                TypeCategoryListId = id,
                TypeCategoryListName = name,
                LastUpdated = updated,
                Active = active,
            };
            return entity;
        }
    }
}
