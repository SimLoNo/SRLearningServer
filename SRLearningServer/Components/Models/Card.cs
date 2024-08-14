using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SRLearningServer.Components.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [MaxLength(200)]
        public string CardName { get; set; }

        [MaxLength(300)]
        public string CardText { get; set; }

        [Column(TypeName = "int")]
        [ForeignKey("Attachment")]
        public int? AttachmentId { get; set; }
        public DateOnly LastUpdated { get; set; }

        public bool Active { get; set; }


        public ICollection<Type> Types { get;} = [];
        public ICollection<Result> Results { get;} = [];
        public Attachment? Attachment { get; set; }
    }
}
