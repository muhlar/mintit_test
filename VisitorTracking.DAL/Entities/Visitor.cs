using System;
using System.ComponentModel.DataAnnotations;
using VisitorTracking.DAL.DataContext;

namespace VisitorTracking.DAL.Entities
{
    public class Visitor : IAuditedEntity
    {
        public int Id { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, StringLength(20)]
        public string IDCardNumber { get; set; }
    }
}
