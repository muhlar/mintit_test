using System.Data.Entity;
using System.Threading.Tasks;
using VisitorTracking.DAL.DataContext;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.DAL.Repositories
{
    public interface IVisitorsRepository
    {
        Task<Visitor> GetVisitorByIDCardAsync(string IDCardNumber);
    }

    public class VisitorsRepository : IVisitorsRepository
    {
        VisitorTrackingContext _context;
        public VisitorsRepository(VisitorTrackingContext context)
        {
            _context = context;
        }

        public async Task<Visitor> GetVisitorByIDCardAsync(string IDCardNumber)
        {
            return await _context.Visitors.SingleOrDefaultAsync(v => v.IDCardNumber == IDCardNumber);
        }
    }
}
