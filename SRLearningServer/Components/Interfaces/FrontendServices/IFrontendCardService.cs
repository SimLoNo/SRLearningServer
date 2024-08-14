using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendCardService
    {
        public Task<CardDto> Create(CardDto cardDto);
        public Task<CardDto> Deactivate(int id);
        public Task<CardDto> Delete(int id);
        public Task<CardDto> GetByid(int id);
        public Task<List<CardDto>> GetAll();
        public Task<List<CardDto>> GetByType(List<List<TypeDto>> typeList);
        public Task<CardDto> Update(CardDto cardDto);
    }
}
