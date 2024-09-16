using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendTypeService
    {
        /// <summary>
        /// Sends a POST request to the server to create a new Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task<TypeDto> Create(TypeDto type);
        /// <summary>
        /// Sends a PUT request to the server to deactivate a Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Task<TypeDto> Deactivate(int id);
        /// <summary>
        /// Sends a DELETE request to the server to delete a Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Task<TypeDto> Delete(int id);
        /// <summary>
        /// Sends a GET request to the server to get a Type by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TypeDto> GetById(int id);
        /// <summary>
        /// Sends a GET request to the server to get all Types
        /// </summary>
        /// <returns></returns>
        public Task<List<TypeDto>> GetAll();
        /// <summary>
        /// Sends a PUT request to the server to update a Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        public Task<TypeDto> Update(TypeDto type);
    }
}
