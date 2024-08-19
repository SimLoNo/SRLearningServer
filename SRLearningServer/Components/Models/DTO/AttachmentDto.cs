using System.ComponentModel.DataAnnotations;

namespace SRLearningServer.Components.Models.DTO
{
    public class AttachmentDto
    {
        public int AttachmentId { get; set; }

        public string AttachmentName { get; set; }

        public string AttachmentUrl { get; set; }

        public DateOnly LastUpdated { get; set; }
        public bool Active { get; set; }


        public IEnumerable<CardDto> Cards { get; set; } = new List<CardDto>();
        public IEnumerable<ResultDto> Results { get; set; } = new List<ResultDto>();
    }
}
