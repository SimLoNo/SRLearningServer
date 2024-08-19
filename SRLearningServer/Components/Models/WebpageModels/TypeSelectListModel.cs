namespace SRLearningServer.Components.Models.WebpageModels
{
    public class TypeSelectListModel
    {
        public IEnumerable<Models.DTO.TypeDto> TargetTypes { get; set; }
        public IEnumerable<Models.DTO.TypeDto> SourceTypes { get; set; }
    }
}
