using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SRLearningServer.Components.Models
{
    public class Type
    {
        [Key]
        [Column(TypeName = "int")]
        public int TypeId { get; set; }
        [MaxLength(200)]
        public string CardTypeName { get; set; }
        public DateOnly LastUpdated { get; set; }
        public bool Active { get; set; }

        public ICollection<Card> Cards { get;} = [];
    }
}
