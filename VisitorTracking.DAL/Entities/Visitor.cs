using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VisitorTracking.DAL.DataContext;

namespace VisitorTracking.DAL.Entities
{
    public class Visitor : IAuditedEntity
    {
        public int Id { get; set; }

        //TODO make required after implementation
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required, StringLength(20)]
        [Index(IsUnique = true)]
        public string IDCardNumber { get; set; }
    }
}
