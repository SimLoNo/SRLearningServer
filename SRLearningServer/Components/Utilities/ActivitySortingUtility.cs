using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Utilities;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Utilities
{
    public class ActivitySortingUtility : IActivitySortingUtility
    {
        public IEnumerable<AttachmentDto> SortActiveByStatus(IEnumerable<AttachmentDto> entities, bool removeValue = false)
        {
            List<AttachmentDto> sortedEntities = new List<AttachmentDto>();

            foreach (AttachmentDto entity in entities)
            {
                var result = SortActiveByStatus(entity, removeValue);
                if (result != null)
                {
                    sortedEntities.Add(result);
                }
            }
            return sortedEntities;
        }

        public AttachmentDto? SortActiveByStatus(AttachmentDto entity, bool removeValue = false)
        {
            if (entity.Active == removeValue)
            {
                return null;
            }
            else
            {
                if (!entity.Results.IsNullOrEmpty())
                {
                    entity.Results = SortActiveByStatus(entity.Results, removeValue);
                }
                if(!entity.Cards.IsNullOrEmpty())
                {
                    entity.Cards = SortActiveByStatus(entity.Cards, removeValue);
                }
                return entity;
            }
        }

        public IEnumerable<CardDto> SortActiveByStatus(IEnumerable<CardDto> entities, bool removeValue = false)
        {
            List<CardDto> sortedEntities = new();

            foreach (CardDto entity in entities)
            {
                var result = SortActiveByStatus(entity, removeValue);
                if (result != null)
                {
                    sortedEntities.Add(result);
                }
            }
            return sortedEntities;
        }

        public CardDto? SortActiveByStatus(CardDto entity, bool removeValue = false)
        {
            if (entity.Active == removeValue)
            {
                return null;
            }
            else
            {
                if (!entity.Types.IsNullOrEmpty())
                {
                    entity.Types = SortActiveByStatus(entity.Types, removeValue);
                }
                if (!entity.Results.IsNullOrEmpty())
                {
                    entity.Results = SortActiveByStatus(entity.Results, removeValue);
                }
                if(entity.Attachment != null)
                {
                    entity.Attachment = SortActiveByStatus(entity.Attachment, removeValue);
                }
                return entity;
            }
        }

        public IEnumerable<ResultDto> SortActiveByStatus(IEnumerable<ResultDto> entities, bool removeValue = false)
        {
            List<ResultDto> sortedEntities = new();

            foreach (ResultDto entity in entities)
            {
                var result = SortActiveByStatus(entity, removeValue);
                if (result != null)
                {
                    sortedEntities.Add(result);
                }
            }
            return sortedEntities;
        }

        public ResultDto? SortActiveByStatus(ResultDto entity, bool removeValue = false)
        {
            if (entity.Active == removeValue)
            {
                return null;
            }
            else
            {
                if (!entity.Cards.IsNullOrEmpty())
                {
                    entity.Cards = SortActiveByStatus(entity.Cards, removeValue);
                }
                if (entity.Attachment != null)
                {
                    entity.Attachment = SortActiveByStatus(entity.Attachment, removeValue);
                }
                return entity;
            }
        }

        public IEnumerable<TypeCategoryListDto> SortActiveByStatus(IEnumerable<TypeCategoryListDto> entities, bool removeValue = false)
        {
            List<TypeCategoryListDto> sortedEntities = new();

            foreach (TypeCategoryListDto entity in entities)
            {
                var result = SortActiveByStatus(entity, removeValue);
                if (result != null)
                {
                    sortedEntities.Add(result);
                }
            }
            return sortedEntities;
        }

        public TypeCategoryListDto? SortActiveByStatus(TypeCategoryListDto entity, bool removeValue = false)
        {
            if (entity.Active == removeValue)
            {
                return null;
            }
            else
            {
                if (!entity.Types.IsNullOrEmpty())
                {
                    entity.Types = SortActiveByStatus(entity.Types, removeValue);
                }
                return entity;
            }
        }

        public IEnumerable<TypeDto> SortActiveByStatus(IEnumerable<TypeDto> entities, bool removeValue = false)
        {
            List<TypeDto> sortedEntities = new();

            foreach (TypeDto entity in entities)
            {
                var result = SortActiveByStatus(entity, removeValue);
                if (result != null)
                {
                    sortedEntities.Add(result);
                }
            }
            return sortedEntities;
        }

        public TypeDto? SortActiveByStatus(TypeDto entity, bool removeValue = false)
        {
            if (entity.Active == removeValue)
            {
                return null;
            }
            else
            {
                if (!entity.Cards.IsNullOrEmpty())
                {
                    entity.Cards = SortActiveByStatus(entity.Cards, removeValue);
                }
                if (!entity.TypeCategoryLists.IsNullOrEmpty())
                {
                    entity.TypeCategoryLists = SortActiveByStatus(entity.TypeCategoryLists, removeValue);
                }
                return entity;
            }
        }
    }
}
