using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models;
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
        public TypeCategoryList Create(TypeCategoryList entity)
        {
            try
            {
                TypeCategoryList typeCategoryList = Task.Run(() => _typeCategoryListRepository.GetByName(entity.TypeCategoryListName)).Result;
                if (typeCategoryList != null)
                {
                    return null;
                }

                entity = Task.Run(() => _typeCategoryListRepository.Create(entity)).Result;
                if (entity == null)
                {
                    return null;
                }
                return entity;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public TypeCategoryList Deactivate(TypeCategoryList entity)
        {
            return Deactivate(entity.TypeCategoryListId);
        }

        public TypeCategoryList Deactivate(int id)
        {
            try
            {
                TypeCategoryList typeCategoryList = Task.Run(() => _typeCategoryListRepository.Deactivate(id)).Result;
                if (typeCategoryList == null)
                {
                    return null;
                }
                return typeCategoryList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public TypeCategoryList Delete(TypeCategoryList entity)
        {
            try
            {
                TypeCategoryList typeCategoryList = Task.Run(() => _typeCategoryListRepository.Delete(entity)).Result;
                if (typeCategoryList == null)
                {
                    return null;
                }
                return typeCategoryList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public TypeCategoryList Get(int id)
        {
            try
            {
                TypeCategoryList typeCategoryList = Task.Run(() => _typeCategoryListRepository.Get(id)).Result;
                if (typeCategoryList == null)
                {
                    return null;
                }
                return typeCategoryList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<TypeCategoryList> GetAll()
        {
            try
            {
                List<TypeCategoryList> list = Task.Run(() => _typeCategoryListRepository.GetAll()).Result;
                if (list.IsNullOrEmpty())
                {
                    return null;
                }
                return list;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public TypeCategoryList GetByName(string name)
        {
            try
            {
                TypeCategoryList typeCategoryList = Task.Run(() => _typeCategoryListRepository.GetByName(name)).Result;
                if (typeCategoryList == null)
                {
                    return null;
                }
                return typeCategoryList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public TypeCategoryList Update(TypeCategoryList entity)
        {
            try
            {
                TypeCategoryList typeCategoryList = Task.Run(() => _typeCategoryListRepository.Update(entity)).Result;
                if (typeCategoryList == null)
                {
                    return null;
                }
                return typeCategoryList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
