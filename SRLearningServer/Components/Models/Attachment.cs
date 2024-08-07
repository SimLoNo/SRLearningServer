using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SRLearningServer.Components.Models
{
    public class Attachment
    {
        [Key]
        [Column(TypeName = "int")]
        public int AttachmentId { get; set; }

        [MaxLength(200)]
        public string AttachmentName { get; set; }

        public string AttachmentUrl { get; set; }

        public DateOnly LastUpdated { get; set; }
        public bool Active { get; set; }


        public ICollection<Card> Cards { get; } = [];
        public ICollection<Result> Results { get; } = [];
    }
}
