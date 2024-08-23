using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Models.WebpageModels
{
    public class SignalModel
    {
        public TypeDto SignalType { get; set; } = new();
        public IEnumerable<TypeDto> AllowedSignalAspects { get; set; } = new List<TypeDto>();
        public IEnumerable<TypeDto> SelectedSignalAspects { get; set; } = new List<TypeDto>();

    }
}
