using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VisitorTracking.DAL.DataContext;
using VisitorTracking.DAL.Entities;
using VisitorTracking.DAL.Repositories;

namespace VisitorTracking.API.Services
{
    public interface IVisitorsService
    {
        Task<Visitor> FindOrCreate(string firstName, string lastName, string iDCardNumber);
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
            }

            return visitor;
        }
    }
}