using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SRLearningServer.Components.Models.DTO
{
    public class ResultDto
    {
        [Column(TypeName = "int")]
        public int ResultId { get; set; }
        public string ResultText { get; set; }
        //public int? AttachmentId { get; set; }

        public bool Active { get; set; }
        public DateOnly LastUpdated { get; set; }


        public List<CardDto> Cards { get; } = [];
        public AttachmentDto Attachment { get; set; }
    }
}
