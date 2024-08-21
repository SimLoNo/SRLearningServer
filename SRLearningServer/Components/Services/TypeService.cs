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

        public async Task<TypeDto> Create(TypeDto entity)
        {
            try
            {
                Models.Type type = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                type = await _typeRepository.Create(type);
                return _domainToDtoConverter.ConvertToDtoFromDomain(type, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<TypeDto> Deactivate(TypeDto entity)
        {
            return await Deactivate(entity.TypeId);
        }

        public async Task<TypeDto> Deactivate(int id)
        {
            try
            {
                Models.Type type = await _typeRepository.Deactivate(id);
                return _domainToDtoConverter.ConvertToDtoFromDomain(type, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<TypeDto> Delete(int entity)
        {
            try
            {
                //Models.Type type = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                Models.Type type = await _typeRepository.Delete(entity);
                return _domainToDtoConverter.ConvertToDtoFromDomain(type, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<TypeDto> Get(int id)
        {
            try
            {
                Models.Type type = await _typeRepository.Get(id);
                return _domainToDtoConverter.ConvertToDtoFromDomain(type, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<TypeDto>> GetAll()
        {
            try
            {
                List<Models.Type> types = await _typeRepository.GetAll();
                if (types.IsNullOrEmpty())
                {
                    return new();
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(types, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<TypeDto> Update(TypeDto entity)
        {
            try
            {
                Models.Type type = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                type = await _typeRepository.Update(type);
                if (type is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(type);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
