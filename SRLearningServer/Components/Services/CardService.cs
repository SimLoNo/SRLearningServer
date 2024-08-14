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
    public class CardService : ICardService
    {
        private readonly IDomainToDtoConverter _domainToDtoConverter;
        private readonly IDtoToDomainConverter _dtoToDomainConverter;
        private readonly ICardRepository _cardRepository;

        public CardService(IDtoToDomainConverter dtoToDomainConverter, IDomainToDtoConverter domainToDtoConverter, ICardRepository cardRepository)
        {
            _domainToDtoConverter = domainToDtoConverter;
            _dtoToDomainConverter = dtoToDomainConverter;
            _cardRepository = cardRepository;
        }

        public async Task<CardDto> Create(CardDto entity)
        {
            try
            {
                Card card = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                card = await _cardRepository.Create(card);
                return _domainToDtoConverter.ConvertToDtoFromDomain(card, true);

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<CardDto> Deactivate(CardDto entity)
        {
            return await Deactivate(entity.CardId);
        }

        public async Task<CardDto> Deactivate(int id)
        {
            try
            {
                Card card = await _cardRepository.Deactivate(id);
                return _domainToDtoConverter.ConvertToDtoFromDomain(card, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<CardDto> Delete(CardDto entity)
        {
            try
            {
                Card card = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                card = await _cardRepository.Delete(card);
                return _domainToDtoConverter.ConvertToDtoFromDomain(card, true);
            }
            catch (Exception ex)
            {

                return null;
            }   
        }

        public async Task<CardDto> Get(int id)
        {
            try
            {
                Card card = await _cardRepository.Get(id);
                return _domainToDtoConverter.ConvertToDtoFromDomain(card, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<CardDto>> GetAll()
        {
            try
            {
                List<Card> cards = await _cardRepository.GetAll();
                if (cards.IsNullOrEmpty())
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(cards, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<CardDto>> GetByType(List<List<TypeDto>> typeId)
        {
            try
            {
                List<List<Models.Type>> typeList = new List<List<Models.Type>>();
                foreach (List<TypeDto> type in typeId)
                {
                    
                    typeList.Add(_dtoToDomainConverter.ConvertToDomainFromDto(type).ToList());
                }
                List<Card> cards = await _cardRepository.GetByType(typeList);
                if (cards.IsNullOrEmpty())
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(cards, true).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }

        public async Task<CardDto> Update(CardDto entity)
        {
            try
            {
                Card card = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                card = await _cardRepository.Update(card);
                if (card is null)
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToDtoFromDomain(card);
            }
            catch (Exception ex)
            {

                throw null;
            }
        }
    }
}
