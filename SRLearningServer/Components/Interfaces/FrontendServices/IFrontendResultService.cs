using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendResultService
    {
        /// <summary>
        /// Sends a POST request to the server to create a new Result
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<ResultDto> Create(ResultDto entity);
        /// <summary>
        /// Sends a PUT request to the server to deactivate a Result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResultDto> Deactivate(int id);
        /// <summary>
        /// Sends a DELETE request to the server to delete a Result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Task<ResultDto> Delete(int id);
        /// <summary>
        /// Sends a GET request to the server to get a Result by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Task<ResultDto> GetById(int id);
        /// <summary>
        /// Sends a GET request to the server to get all Results
        /// </summary>
        /// <returns></returns>
        public Task<List<ResultDto>> GetAll();
        /// <summary>
        /// Sends a PUT request to the server to update a Result
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public Task<ResultDto> Update(ResultDto entity);
    }
}
