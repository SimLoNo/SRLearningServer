namespace SRLearningServer.Components.Models.WebpageModels
{
    public class CardSelectListModel
    {
        public IEnumerable<Models.DTO.CardDto> TargetCards { get; set; }
        public IEnumerable<Models.DTO.CardDto> SourceCards { get; set; }
    }
}
