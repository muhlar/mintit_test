using System;

namespace VisitorTracking.DAL.DataContext
{
    public interface IAuditedEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
