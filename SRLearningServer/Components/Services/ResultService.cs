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
        

        public ResultDto Create(ResultDto entity)
        {
            try
            {
                Result result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                result = Task.Run(() => _resultRepository.Create(result)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(result, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public ResultDto Deactivate(ResultDto entity)
        {
          return Deactivate(entity.ResultId);
        }

        public ResultDto Deactivate(int id)
        {
            try
            {
                Result result = Task.Run(() => _resultRepository.Deactivate(id)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(result, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public ResultDto Delete(ResultDto entity)
        {
            try
            {
                Result result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                result = Task.Run(() => _resultRepository.Delete(result)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(result, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public ResultDto Get(int id)
        {
            try
            {
                Result result = Task.Run(() => _resultRepository.Get(id)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(result, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<ResultDto> GetAll()
        {
            try
            {
                List<Result> results = Task.Run(() => _resultRepository.GetAll()).Result.ToList();
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

        public ResultDto Update(ResultDto entity)
        {
            try
            {
                Result result = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                result = Task.Run(() => _resultRepository.Update(result)).Result;
                if (result is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(result);
            }
            catch (Exception ex)
            {

                throw null;
            }
        }
    }
}
