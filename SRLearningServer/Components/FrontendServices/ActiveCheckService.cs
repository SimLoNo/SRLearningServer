using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class ActiveCheckService : IActiveCheckService
    {
        public List<string> CheckActive(IEnumerable<CardDto> entities, bool active)
        {
            List<string> inactiveEntities = new();
            foreach (CardDto entity in entities)
            {
                string returnstring = CheckActive(entity, active);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(CardDto entity, bool active)
        {
            if (entity.Active == active)
            {
                return $"Kort: {entity.CardName}";
            }
            return "";
        }

        public List<string> CheckActive(IEnumerable<TypeCategoryListDto> entities, bool active)
        {
            List<string> inactiveEntities = new();
            foreach (TypeCategoryListDto entity in entities)
            {
                string returnstring = CheckActive(entity, active);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(TypeCategoryListDto entity, bool active)
        {
            if (entity.Active == active)
            {
                return $"Type kategori: {entity.TypeCategoryListName}";
            }
            return "";
        }

        public List<string> CheckActive(IEnumerable<AttachmentDto> entities, bool active)
        {
            List<string> inactiveEntities = new();
            foreach (AttachmentDto entity in entities)
            {
                string returnstring = CheckActive(entity, active);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(AttachmentDto entity, bool active)
        {
            if (entity.Active == active)
            {
                return $"Vedhæftning: {entity.AttachmentName}";
            }
            return "";
        }

        public List<string> CheckActive(IEnumerable<ResultDto> entities, bool active)
        {
            List<string> inactiveEntities = new();
            foreach (ResultDto entity in entities)
            {
                string returnstring = CheckActive(entity, active);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(ResultDto entity, bool active)
        {
            if (entity.Active == active)
            {
                return $"Resultat: {entity.ResultText}";
            }
            return "";
        }

        public List<string> CheckActive(IEnumerable<TypeDto> entities, bool active)
        {
            List<string> inactiveEntities = new();
            foreach (TypeDto entity in entities)
            {
                string returnstring = CheckActive(entity, active);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(TypeDto entity, bool active)
        {
            if (entity.Active == active)
            {
                return $"Type: {entity.CardTypeName}";
            }
            return "";
        }
    }
}
