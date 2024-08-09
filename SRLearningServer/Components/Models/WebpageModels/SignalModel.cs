using SRLearningServer.Components.Models.DTO;

namespace SRLearningServer.Components.Models.WebpageModels
{
    public class SignalModel
    {
        public TypeDto SignalType { get; set; }
        public List<TypeDto> SignalAspects { get; set; } = new();
    }
}
