using System.Threading.Tasks;
using VisitorTracking.DAL.Entities;
using VisitorTracking.DAL.Repositories;

namespace VisitorTracking.API.Services
{
    public interface IVisitorsService
    {
        Task<Visitor> FindOrCreate(string firstName, string lastName, string iDCardNumber);
        Task<Card> GetActiveCard(Visitor visitor);
    }

    public class VisitorsService : IVisitorsService
    {
        IVisitorsRepository _repository;

        public VisitorsService(IVisitorsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Visitor> FindOrCreate(string firstName, string lastName, string iDCardNumber)
        {
            Visitor visitor = await _repository.GetVisitorByIDCardAsync(iDCardNumber);

            if (visitor == null)
            {
                visitor = new Visitor() { FirstName = firstName, LastName = lastName, IDCardNumber = iDCardNumber };
            }
            else
            {
                visitor.FirstName = firstName;
                visitor.LastName = lastName;
                await _repository.UpdateAsync(visitor);
            }

            return visitor;
        }

        public async Task<Card> GetActiveCard(Visitor visitor)
        {
            return await _repository.GetActiveCardAsync(visitor);
        }
    }
}