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
        public AttachmentDto CreateAttachmentDto(int id, string name, string url, DateOnly updated, bool active)
        {
            AttachmentDto dto = new()
            {
                AttachmentId = id,
                AttachmentName = name,
                AttachmentUrl = url,
                LastUpdated = updated,
                Active = active,
            };
            return dto;
        }
        public CardDto CreateCardDto(int id, string name, string text, DateOnly updated, bool active)
        {
            CardDto dto = new()
            {
                CardId = id,
                CardName = name,
                CardText = text,
                LastUpdated = updated,
                Active = active,
            };
            return dto;
        }
        public ResultDto CreateResultDto(int id, string text, DateOnly updated, bool active)
        {
            ResultDto dto = new()
            {
                ResultId = id,
                ResultText = text,
                LastUpdated = updated,
                Active = active,
            };
            return dto;
        }

        public TypeDto CreateTypeDto(int id, string name, DateOnly updated, bool active)
        {
            TypeDto dto = new()
            {
                TypeId = id,
                CardTypeName = name,
                LastUpdated = updated,
                Active = active,
            };
            return dto;
        }

        public Attachment CreateAttachment(int id, string name, string url, DateOnly updated, bool active)
        {
            Attachment entity = new()
            {
                AttachmentId = id,
                AttachmentName = name,
                AttachmentUrl = url,
                LastUpdated = updated,
                Active = active,
            };
            return entity;
        }
        public Card CreateCard(int id, string name, string text, DateOnly updated, bool active)
        {
            Card entity = new()
            {
                CardId = id,
                CardName = name,
                CardText = text,
                LastUpdated = updated,
                Active = active,
            };
            return entity;
        }
        public Result CreateResult(int id, string text, DateOnly updated, bool active)
        {
            Result entity = new()
            {
                ResultId = id,
                ResultText = text,
                LastUpdated = updated,
                Active = active,
            };
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
            return entity;
        }
        public Result CreateResultFromDto(ResultDto dto)
        {
            Result entity = new()
            {
                ResultId = dto.ResultId,
                ResultText = dto.ResultText,
                LastUpdated = dto.LastUpdated,
                Active = dto.Active,
            };
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
        public TypeCategoryListDto CreateTypeCategoryListDto(int id, string name, DateOnly updated, bool active)
        {
            TypeCategoryListDto entity = new()
            {
                TypeCategoryListId = id,
                TypeCategoryListName = name,
                LastUpdated = updated,
                Active = active,
            };
            return entity;
        }

        public TypeCategoryList CreateTypeCategoryListFromDto(TypeCategoryListDto dto)
        {
            TypeCategoryList entity = new()
            {
                TypeCategoryListId = dto.TypeCategoryListId,
                TypeCategoryListName = dto.TypeCategoryListName,
                LastUpdated = dto.LastUpdated,
                Active = dto.Active,
            };
            return entity;
        }
    }
}
