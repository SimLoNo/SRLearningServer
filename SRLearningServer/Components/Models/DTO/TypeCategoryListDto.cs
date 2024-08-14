using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SRLearningServer.Components.Models.DTO
{
    public class TypeCategoryListDto
    {
        public int TypeCategoryListId { get; set; }

        public string TypeCategoryListName { get; set; }

        public bool Active { get; set; }

        public DateOnly LastUpdated { get; set; }

        public List<TypeDto> Types { get; set; } = new List<TypeDto>();
    }
}
