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


        public List<CardDto> Cards { get; } = [];
        public List<ResultDto> Results { get; } = [];
    }
}
