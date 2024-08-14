using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRLearningServer.Components.Models
{
    public class TypeCategoryList
    {
        [Key]
        public int TypeCategoryListId { get; set; }

        [MaxLength(100)]
        public string TypeCategoryListName { get; set; }

        public bool Active { get; set; }

        public DateOnly LastUpdated { get; set; }

        public ICollection<Type> Types { get; } = [];
    }
}
