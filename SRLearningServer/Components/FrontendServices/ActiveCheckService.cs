using SRLearningServer.Components.Interfaces.FrontendServices;
using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.FrontendServices
{
    public class ActiveCheckService : IActiveCheckService
    {
        public List<string> CheckActive(IEnumerable<CardDto> entities)
        {
            List<string> inactiveEntities = new();
            foreach (CardDto entity in entities)
            {
                string returnstring = CheckActive(entity);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(CardDto entity)
        {
            if (entity.Active == false)
            {
                return $"Kort: {entity.CardName} er inaktiv";
            }
            return "";
        }

        public List<string> CheckActive(IEnumerable<TypeCategoryListDto> entities)
        {
            List<string> inactiveEntities = new();
            foreach (TypeCategoryListDto entity in entities)
            {
                string returnstring = CheckActive(entity);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(TypeCategoryListDto entity)
        {
            if (entity.Active == false)
            {
                return $"Type kategori: {entity.TypeCategoryListName} er inaktiv";
            }
            return "";
        }

        public List<string> CheckActive(IEnumerable<AttachmentDto> entities)
        {
            List<string> inactiveEntities = new();
            foreach (AttachmentDto entity in entities)
            {
                string returnstring = CheckActive(entity);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(AttachmentDto entity)
        {
            if (entity.Active == false)
            {
                return $"Vedhæftning: {entity.AttachmentName} er inaktiv";
            }
            return "";
        }

        public List<string> CheckActive(IEnumerable<ResultDto> entities)
        {
            List<string> inactiveEntities = new();
            foreach (ResultDto entity in entities)
            {
                string returnstring = CheckActive(entity);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(ResultDto entity)
        {
            if (entity.Active == false)
            {
                return $"Resultat: {entity.ResultText} er inaktiv";
            }
            return "";
        }

        public List<string> CheckActive(IEnumerable<TypeDto> entities)
        {
            List<string> inactiveEntities = new();
            foreach (TypeDto entity in entities)
            {
                string returnstring = CheckActive(entity);
                if (returnstring != "")
                {
                    inactiveEntities.Add(returnstring);
                }
            }
            return inactiveEntities;
        }

        public string CheckActive(TypeDto entity)
        {
            if (entity.Active == false)
            {
                return $"Type: {entity.CardTypeName} er inaktiv";
            }
            return "";
        }
    }
}
