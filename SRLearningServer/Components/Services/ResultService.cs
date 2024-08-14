using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Interfaces.Converters;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Interfaces.Services;
using SRLearningServer.Components.Models;
using SRLearningServer.Components.Models.DTO;
using SRLearningServer.Components.Repositories;

namespace SRLearningServer.Components.Services
{
    public class ResultService : IResultService
    {
        private readonly IDomainToDtoConverter _domainToDtoConverter;
        private readonly IDtoToDomainConverter _dtoToDomainConverter;
        private readonly IResultRepository _resultRepository;

        public ResultService(IDtoToDomainConverter dtoToDomainConverter, IDomainToDtoConverter domainToDtoConverter, IResultRepository resultRepository)
        {
            _domainToDtoConverter = domainToDtoConverter;
            _dtoToDomainConverter = dtoToDomainConverter;
            _resultRepository = resultRepository;
        }
        

        public async Task<ResultDto> Create(ResultDto entity)
        {
            try
            {
                Result result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                result = await _resultRepository.Create(result);
                if(result is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(result, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<ResultDto> Deactivate(ResultDto entity)
        {
          return await Deactivate(entity.ResultId);
        }

        public async Task<ResultDto> Deactivate(int id)
        {
            try
            {
                Result result = await _resultRepository.Deactivate(id);
                if (result is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(result, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<ResultDto> Delete(int entity)
        {
            try
            {
                ///Result result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                Result result = await _resultRepository.Delete(entity);
                if (result is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(result, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<ResultDto> Get(int id)
        {
            try
            {
                Result result = await _resultRepository.Get(id);
                if (result is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(result, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<ResultDto>> GetAll()
        {
            try
            {
                List<Result> results = await _resultRepository.GetAll();
                if (results.IsNullOrEmpty())
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(results, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<ResultDto> Update(ResultDto entity)
        {
            try
            {
                Result result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                result = await _resultRepository.Update(result);
                if (result is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(result);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
