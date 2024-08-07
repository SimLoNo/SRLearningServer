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

        public CardService(IDomainToDtoConverter domainToDtoConverter, IDtoToDomainConverter dtoToDomainConverter, ICardRepository cardRepository)
        {
            _domainToDtoConverter = domainToDtoConverter;
            _dtoToDomainConverter = dtoToDomainConverter;
            _cardRepository = cardRepository;
        }

        public CardDto Create(CardDto entity)
        {
            try
            {
                Card card = _dtoToDomainConverter.ConvertToCardFromCardDto(entity);
                card = Task.Run(() => _cardRepository.Create(card)).Result;
                return _domainToDtoConverter.ConvertToCardDtoFromCard(card, true);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
                return _domainToDtoConverter.ConvertToCardDtoFromCard(card, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public CardDto Delete(CardDto entity)
        {
            try
            {
                Card card = _dtoToDomainConverter.ConvertToCardFromCardDto(entity);
                card = Task.Run(() => _cardRepository.Delete(card)).Result;
                return _domainToDtoConverter.ConvertToCardDtoFromCard(card, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }   
        }

        public CardDto Get(int id)
        {
            try
            {
                Card card = Task.Run(() => _cardRepository.Get(id)).Result;
                return _domainToDtoConverter.ConvertToCardDtoFromCard(card, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CardDto> GetAll()
        {
            try
            {
                List<Card> cards = Task.Run(() => _cardRepository.GetAll()).Result.ToList();
                if (cards.IsNullOrEmpty())
                {
                    return null;
                }
                return _domainToDtoConverter.ConvertToCardDtoFromCard(cards, true);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<CardDto> GetByType(List<List<TypeDto>> typeId)
        {
            List<List<Models.Type>> typeList = new List<List<Models.Type>>();
            foreach (List<TypeDto> type in typeId)
            {
                foreach (TypeDto t in type)
                {
                    if(t.TypeId !> 0)
                    {
                        type.Remove(t);
                    }
                }
                typeList.Add(_dtoToDomainConverter.ConvertToTypeFromTypeDto(type).ToList());
            }
            List<Card> cards = Task.Run(() => _cardRepository.GetByType(typeList)).Result.ToList();
            if (cards.IsNullOrEmpty())
            {
                return null;
            }
            return _domainToDtoConverter.ConvertToCardDtoFromCard(cards, true).ToList();
        }
    }
}
