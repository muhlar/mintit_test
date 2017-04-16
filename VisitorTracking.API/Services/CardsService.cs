using System;
using System.Linq;
using VisitorTracking.DAL.DataContext;
using VisitorTracking.DAL.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace VisitorTracking.API.Services
{
    public interface ICardsService
    {
        Task<Card> RegisterCard(Card card);
        Card UpdateCard(Card updatedCard);
    }

    public class CardsService : ICardsService
    {
        VisitorTrackingContext _context;
        IVisitorsService _visitorService;

        public CardsService(VisitorTrackingContext context, IVisitorsService visitorService)
        {
            _context = context;
            _visitorService = visitorService;
        }

        public async Task<Card> RegisterCard(Card card)
        {
            Visitor visitor = await _visitorService.FindOrCreate(card.Visitor.FirstName,
                                                             card.Visitor.LastName,
                                                             card.Visitor.IDCardNumber);

            Card newCard = new Card() { Visitor = visitor, CardGuid = Guid.NewGuid() };
            _context.Cards.Add(newCard);
            _context.SaveChanges();

            return newCard;
        }

        public Card UpdateCard(Card updatedCard)
        {
            Card card = _context.Cards
                                .Include(b => b.Visitor)
                                .SingleOrDefault(c => c.CardGuid == updatedCard.CardGuid);

            if (card != null)
            {
                card.CardState = updatedCard.CardState;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Card not found!");
            }

            return card;
        }
    }
}