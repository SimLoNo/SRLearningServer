using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SRLearningServer.Components.Models
{
    public class Result
    {
        [Key]
        [Column(TypeName = "int")]
        public int ResultId { get; set; }
        [MaxLength(200)]
        public string ResultText { get; set; }
        /*[Column(TypeName = "int")]
        public int? AttachmentId { get; set; }*/

        public bool Active { get; set; }
        public DateOnly LastUpdated { get; set; }


        public ICollection<Card> Cards { get;} = [];
        public Attachment? Attachment { get; set; }
    }
}
