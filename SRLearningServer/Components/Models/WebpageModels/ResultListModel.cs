using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Models.WebpageModels
{
    public class ResultListModel
    {
        public List<ResultDto> Results { get; set; } = new();
        public List<int> SelectedResultIds { get; set; } = new();
    }
}
