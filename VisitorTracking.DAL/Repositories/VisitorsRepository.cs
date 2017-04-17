using System.Data.Entity;
using System.Threading.Tasks;
using VisitorTracking.DAL.DataContext;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.DAL.Repositories
{
    public interface IVisitorsRepository
    {
        Task<Visitor> GetVisitorByIDCardAsync(string IDCardNumber);
        Task<Card> GetActiveCardAsync(Visitor visitor);
        Task<int> UpdateAsync(Visitor visitor);
    }

    public class VisitorsRepository : IVisitorsRepository
    {
        VisitorTrackingContext _context;
        public VisitorsRepository(VisitorTrackingContext context)
        {
            _context = context;
        }

        public async Task<int> UpdateAsync(Visitor visitor)
        {
            _context.Entry(visitor).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<Visitor> GetVisitorByIDCardAsync(string IDCardNumber)
        {
            return await _context.Visitors.SingleOrDefaultAsync(v => v.IDCardNumber == IDCardNumber);
        }

        public async Task<Card> GetActiveCardAsync(Visitor visitor)
        {
            return await _context.Cards.SingleOrDefaultAsync(c => c.CardState != CardStates.Deactivated
                                                                  && c.Visitor.Id == visitor.Id);
        }
    }
}
