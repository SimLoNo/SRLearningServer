using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Models.WebpageModels
{
    public class ResultSelectListModel
    {
        public IEnumerable<ResultDto> TargetResults { get; set; }
        public IEnumerable<ResultDto> SourceResults { get; set; }
    }
}
