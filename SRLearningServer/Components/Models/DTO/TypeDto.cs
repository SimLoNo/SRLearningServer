using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SRLearningServer.Components.Models.DTO
{
    public class TypeDto
    {
        public int TypeId { get; set; }
        public string CardTypeName { get; set; }
        public DateOnly LastUpdated { get; set; }
        public bool Active { get; set; }

        public IEnumerable<CardDto> Cards { get; set; } = new List<CardDto>();
        public IEnumerable<TypeCategoryListDto> TypeCategoryLists { get; set; } = new List<TypeCategoryListDto>();
    }
}
