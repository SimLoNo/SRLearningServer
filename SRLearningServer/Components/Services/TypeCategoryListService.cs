using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Components.Repositories;

namespace SRLearningServer.Components.Services
{
    public class TypeCategoryListService : ITypeCategoryListService
    {
        private readonly IDomainToDtoConverter _domainToDtoConverter;
        private readonly IDtoToDomainConverter _dtoToDomainConverter;
        private readonly ITypeCategoryListRepository _typeCategoryListRepository;
        public TypeCategoryListService(IDtoToDomainConverter dtoToDomainConverter, IDomainToDtoConverter domainToDtoConverter, ITypeCategoryListRepository typeCategoryListRepository)
        {
            _domainToDtoConverter = domainToDtoConverter;
            _dtoToDomainConverter = dtoToDomainConverter;
            _typeCategoryListRepository = typeCategoryListRepository;
        }
        public async Task<TypeCategoryListDto> Create(TypeCategoryListDto entity)
        {
            try
            {
                TypeCategoryList typeCategoryList = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                var foundTypeCategoryList = await _typeCategoryListRepository.GetByName(typeCategoryList.TypeCategoryListName);
                if (foundTypeCategoryList is not null)
                {
                    return null;
                }

                typeCategoryList = await _typeCategoryListRepository.Create(typeCategoryList);
                if (entity == null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(typeCategoryList);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<TypeCategoryListDto> Deactivate(TypeCategoryListDto entity)
        {
            return await Deactivate(entity.TypeCategoryListId);
        }

        public async Task<TypeCategoryListDto> Deactivate(int id)
        {
            try
            {
                TypeCategoryList typeCategoryList = await _typeCategoryListRepository.Deactivate(id);
                if (typeCategoryList == null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(typeCategoryList);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<TypeCategoryListDto> Delete(int entity)
        {
            try
            {
                //TypeCategoryList typeCategoryList = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                TypeCategoryList typeCategoryList = await _typeCategoryListRepository.Delete(entity);
                if (typeCategoryList == null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(typeCategoryList);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<TypeCategoryListDto> Get(int id)
        {
            try
            {
                TypeCategoryList typeCategoryList = await _typeCategoryListRepository.Get(id);
                if (typeCategoryList == null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(typeCategoryList, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<TypeCategoryListDto>> GetAll()
        {
            try
            {
                List<TypeCategoryList> list = await _typeCategoryListRepository.GetAll();
                if (list.IsNullOrEmpty())
                {
                    return new();
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(list);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<TypeCategoryListDto> GetByName(string name)
        {
            try
            {
                TypeCategoryList typeCategoryList = await _typeCategoryListRepository.GetByName(name);
                if (typeCategoryList == null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(typeCategoryList, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<TypeCategoryListDto> Update(TypeCategoryListDto entity)
        {
            try
            {
                TypeCategoryList typeCategoryList = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                typeCategoryList = await _typeCategoryListRepository.Update(typeCategoryList);
                if (typeCategoryList == null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(typeCategoryList);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
