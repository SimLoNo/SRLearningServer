using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SRLearningServer.Components.Models.DTO
{
    public class CardDto
    {
        public int CardId { get; set; }

        public string CardName { get; set; } = string.Empty;

        public string CardText { get; set; } = string.Empty;

        public int? AttachmentId { get; set; }
        public DateOnly LastUpdated { get; set; }

        public bool Active { get; set; }


        public IEnumerable<TypeDto> Types { get; set; } = new List<TypeDto>();
        public IEnumerable<ResultDto> Results { get; set; } = new List<ResultDto>();
        public AttachmentDto? Attachment { get; set; }
    }
}
