using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using VisitorTracking.DAL.DataContext;
using VisitorTracking.DAL.Entities;
using System.Threading.Tasks;

namespace VisitorTracking.DAL.Repositories
{
    public interface ICardsRepository
    {
        Task<Card> AddAsync(Card card);
        Task<CardUpdateResultEnum> UpdateAsync(Card card);
        Task<IEnumerable<Card>> GetAllCardsAsync();
        Task<Card> GetCardByGuidAsync(Guid guid);
    }

    public class CardsRepository : ICardsRepository
    {
        VisitorTrackingContext _context;
        public CardsRepository(VisitorTrackingContext context)
        {
            _context = context;
        }

        public async Task<Card> AddAsync(Card card)
        {
            _context.Cards.Add(card);
            if (card.Visitor.Id != 0)
                _context.Visitors.Attach(card.Visitor);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<CardUpdateResultEnum> UpdateAsync(Card card)
        {
            _context.Entry(card).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0 
                         ? CardUpdateResultEnum.Successful
                         : CardUpdateResultEnum.DBUpdateFailed;
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _context.Cards
                                 .Include(c => c.Visitor)
                                 .ToListAsync();
        }
        
        public async Task<Card> GetCardByGuidAsync(Guid guid)
        {
            return await _context.Cards
                                 .Include(b => b.Visitor)
                                 .SingleOrDefaultAsync(c => c.CardGuid == guid);
        }
    }
}
