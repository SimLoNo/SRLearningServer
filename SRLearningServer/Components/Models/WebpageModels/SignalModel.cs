using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Models.WebpageModels
{
    public class SignalModel
    {
        public TypeDto SignalType { get; set; } = new();
        public IEnumerable<TypeDto> SignalAspects { get; set; } = new List<TypeDto>();
    }
}
