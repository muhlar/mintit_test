using System;

namespace VisitorTracking.DAL.DataContext
{
    public interface IAuditedEntity
    {
        int CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        int ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
