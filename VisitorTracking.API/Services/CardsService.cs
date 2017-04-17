using System;
using VisitorTracking.DAL.Entities;
using System.Threading.Tasks;
using VisitorTracking.DAL.Repositories;

namespace VisitorTracking.API.Services
{
    public interface ICardsService
    {
        Task<Card> RegisterCard(Card card);
        Task<CardUpdateResultEnum> UpdateCard(Card updatedCard);
    }

    public class CardsService : ICardsService
    {
        ICardsRepository _cardsRepository;
        IVisitorsService _visitorsService;

        public CardsService(ICardsRepository cardsRepository, IVisitorsService visitorsService)
        {
            _cardsRepository = cardsRepository;
            _visitorsService = visitorsService;
        }

        public async Task<Card> RegisterCard(Card card)
        {
            //creates new visitor or updates existing one
            Visitor visitor = await _visitorsService.FindOrCreate(card.Visitor.FirstName,
                                                             card.Visitor.LastName,
                                                             card.Visitor.IDCardNumber);

            //try to find active card for visitor
            if (visitor.Id != 0)
                card = await _visitorsService.GetActiveCard(visitor) ?? card;

            //create new card or return existing one
            if (card.Id == 0)
            {
                card.Visitor = visitor;
                card.CardGuid = Guid.NewGuid();
                return await _cardsRepository.AddAsync(card);
            }
            else
                return card;
        }

        public async Task<CardUpdateResultEnum> UpdateCard(Card updatedCard)
        {
            Card card = await _cardsRepository.GetCardByGuidAsync(updatedCard.CardGuid);

            if (card != null)
            {
                if (card.CardState == CardStates.Deactivated)
                    return CardUpdateResultEnum.IsDeactivated;
                card.CardState = updatedCard.CardState;
                return await _cardsRepository.UpdateAsync(card);
            }
            else
                return CardUpdateResultEnum.NotFound;
        }
    }
}