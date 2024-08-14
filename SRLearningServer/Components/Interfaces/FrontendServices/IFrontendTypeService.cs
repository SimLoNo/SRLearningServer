using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendTypeService
    {
        public Task<TypeDto> Create(TypeDto type);

        public Task<TypeDto> Deactivate(int id);

        public Task<TypeDto> Delete(int id);
        public Task<TypeDto> GetById(int id);
        public Task<List<TypeDto>> GetAll();

        public Task<TypeDto> Update(TypeDto type);
    }
}
