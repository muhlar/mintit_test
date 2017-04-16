using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisitorTracking.DAL.DataContext;

namespace VisitorTracking.API.Services
{
    public interface ICardsService
    {
    }

    public class CardsService : ICardsService
    {
        VisitorTrackingContext _context;

        public CardsService(VisitorTrackingContext context)
        {
            _context = context;
        }

        public int RegisterCard(string firstName, string LastName, string IDCardNumber)
        {
            return 0;
        }
    }
}