using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SRLearningServer.Components.Models.DTO
{
    public class CardDto
    {
        public int CardId { get; set; }

        public string CardName { get; set; }

        public string CardText { get; set; }

        //public int AttachmentId { get; set; }
        public DateOnly LastUpdated { get; set; }

        public bool Active { get; set; }


        public List<TypeDto> Types { get; } = [];
        public List<ResultDto> Results { get; } = [];
        public AttachmentDto Attachment { get; set; }
    }
}
