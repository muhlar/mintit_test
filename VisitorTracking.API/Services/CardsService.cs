using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisitorTracking.DAL.DataContext;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.API.Services
{
    public interface ICardsService
    {
        Card RegisterCard(Card card);
    }

    public class CardsService : ICardsService
    {
        VisitorTrackingContext _context;
        VisitorsService _visitorService;

        public CardsService(VisitorTrackingContext context, VisitorsService visitorService)
        {
            _context = context;
            _visitorService = visitorService;
        }

        public Card RegisterCard(Card card)
        {
            Visitor visitor = _visitorService.CreateOrUpdate(card.Visitor.FirstName, card.Visitor.LastName, card.Visitor.IDCardNumber);

            Card newCard = new Card() { Visitor = visitor };
            _context.Cards.Add(newCard);
            _context.SaveChanges();

            return newCard;
        }
    }
}