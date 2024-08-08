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
            throw new NotImplementedException();
        }

        public TypeCategoryList Deactivate(int id)
        {
            throw new NotImplementedException();
        }

        public TypeCategoryList Delete(TypeCategoryList entity)
        {
            throw new NotImplementedException();
        }

        public TypeCategoryList Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TypeCategoryList> GetAll()
        {
            throw new NotImplementedException();
        }

        public TypeCategoryList GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public TypeCategoryList Update(TypeCategoryList entity)
        {
            throw new NotImplementedException();
        }
    }
}
