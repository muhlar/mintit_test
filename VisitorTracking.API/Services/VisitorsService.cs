using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisitorTracking.DAL.DataContext;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.API.Services
{
    public interface IVisitorsService
    {
    }

    public class VisitorsService : IVisitorsService
    {
        VisitorTrackingContext _context;

        public VisitorsService(VisitorTrackingContext context)
        {
            _context = context;
        }

        public Visitor CreateOrUpdate(string firstName, string lastName, string iDCardNumber)
        {
            Visitor visitor = _context.Visitors.FirstOrDefault(v => v.IDCardNumber == iDCardNumber.Trim());

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