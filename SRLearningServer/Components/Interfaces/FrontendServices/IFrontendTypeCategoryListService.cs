using SRLearningServer.Components.Models.DTO;
using System.Net.Http;

namespace SRLearningServer.Components.Interfaces.FrontendServices
{
    public interface IFrontendTypeCategoryListService
    {
        /// <summary>
        /// Sends a GET request to the server to get all TypeCategoryList
        /// </summary>
        /// <returns></returns>
        public Task<List<TypeCategoryListDto>> GetAll();
        /// <summary>
        /// Sends a GET request to the server to get a TypeCategoryList by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TypeCategoryListDto> GetById(int id);
        /// <summary>
        /// Sends a POST request to the server to create a new TypeCategoryList
        /// </summary>
        /// <param name="typeCategoryListDto"></param>
        /// <returns></returns>
        public Task<TypeCategoryListDto> Create(TypeCategoryListDto typeCategoryListDto);
        /// <summary>
        /// Sends a PUT request to the server to update a TypeCategoryList
        /// </summary>
        /// <param name="typeCategoryListDto"></param>
        /// <returns></returns>

        public Task<TypeCategoryListDto> Update(TypeCategoryListDto typeCategoryListDto);
        /// <summary>
        /// Sends a PUT request to the server to deactivate a TypeCategoryList
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TypeCategoryListDto> Deactivate(int id);
        /// <summary>
        /// Sends a DELETE request to the server to delete a TypeCategoryList
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TypeCategoryListDto> Delete(int id);
        /// <summary>
        /// Sends a GET request to the server to get a TypeCategoryList by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<TypeCategoryListDto> GetByName(string name);
    }
}
