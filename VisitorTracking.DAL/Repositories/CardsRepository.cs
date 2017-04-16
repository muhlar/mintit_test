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

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _context.Cards
                           .Include(c => c.Visitor)
                           .ToListAsync();
        }
        
        public async Task<Card> GetCardByGuidAsync(Guid guid)
        {
            return await _context.Cards.SingleOrDefaultAsync(c => c.CardGuid == guid);
        }
    }
}
