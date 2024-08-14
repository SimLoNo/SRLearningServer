using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendResultService
    {

        public Task<ResultDto> Create(ResultDto entity);
        public Task<ResultDto> Deactivate(int id);

        public Task<ResultDto> Delete(int id);

        public Task<ResultDto> GetById(int id);
        public Task<List<ResultDto>> GetAll();

        public Task<ResultDto> Update(ResultDto entity);
    }
}
