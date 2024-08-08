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

        public CardDto Create(CardDto entity)
        {
            try
            {
                Card card = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                card = Task.Run(() => _cardRepository.Create(card)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(card, true);

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public CardDto Deactivate(CardDto entity)
        {
            return Deactivate(entity.CardId);
        }

        public CardDto Deactivate(int id)
        {
            try
            {
                Card card = Task.Run(() => _cardRepository.Deactivate(id)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(card, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public CardDto Delete(CardDto entity)
        {
            try
            {
                Card card = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                card = Task.Run(() => _cardRepository.Delete(card)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(card, true);
            }
            catch (Exception ex)
            {

                return null;
            }   
        }

        public CardDto Get(int id)
        {
            try
            {
                Card card = Task.Run(() => _cardRepository.Get(id)).Result;
                return _domainToDtoConverter.ConvertToDtoFromDomain(card, true);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<CardDto> GetAll()
        {
            try
            {
                List<Card> cards = Task.Run(() => _cardRepository.GetAll()).Result.ToList();
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

        public List<CardDto> GetByType(List<List<TypeDto>> typeId)
        {
            try
            {
                List<List<Models.Type>> typeList = new List<List<Models.Type>>();
                foreach (List<TypeDto> type in typeId)
                {
                    foreach (TypeDto t in type)
                    {
                        if (t.TypeId! > 0)
                        {
                            type.Remove(t);
                        }
                    }
                    typeList.Add(_dtoToDomainConverter.ConvertToDomainFromDto(type).ToList());
                }
                List<Card> cards = Task.Run(() => _cardRepository.GetByType(typeList)).Result.ToList();
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

        public CardDto Update(CardDto entity)
        {
            try
            {
                Card card = _dtoToDomainConverter.ConvertToDomainFromDto(entity);
                card = Task.Run(() => _cardRepository.Update(card)).Result;
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
