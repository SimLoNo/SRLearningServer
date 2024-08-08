using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Components.Repositories;
using System.Linq.Expressions;

namespace SRLearningServer.Components.Services
{
    public class TypeService : ITypeService
    {
        private readonly IDomainToDtoConverter _domainToDtoConverter;
        private readonly IDtoToDomainConverter _dtoToDomainConverter;
        private readonly ITypeRepository _typeRepository;
        public TypeService(IDtoToDomainConverter dtoToDomainConverter, IDomainToDtoConverter domainToDtoConverter, ITypeRepository typeRepository)
        {
            _domainToDtoConverter = domainToDtoConverter;
            _dtoToDomainConverter = dtoToDomainConverter;
            _typeRepository = typeRepository;
        }

        public TypeDto Create(TypeDto entity)
        {
            Models.Type type = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
            type = Task.Run(() => _typeRepository.Create(type)).Result;
            return _domainToDtoConverter.ConvertToDtoFromDomain(type, true);
        }

        public TypeDto Deactivate(TypeDto entity)
        {
            return Deactivate(entity.TypeId);
        }

        public TypeDto Deactivate(int id)
        {
            try
            {
                Models.Type type = Task.Run(() => _typeRepository.Deactivate(id)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(type, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public TypeDto Delete(TypeDto entity)
        {
            try
            {
                Models.Type type = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                type = Task.Run(() => _typeRepository.Delete(type)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(type, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public TypeDto Get(int id)
        {
            try
            {
                Models.Type type = Task.Run(() => _typeRepository.Get(id)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(type, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TypeDto> GetAll()
        {
            try
            {
                List<Models.Type> types = Task.Run(() => _typeRepository.GetAll()).Result.ToList();
                if (types.IsNullOrEmpty())
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(types, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
