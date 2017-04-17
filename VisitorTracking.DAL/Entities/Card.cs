using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VisitorTracking.DAL.DataContext;

namespace VisitorTracking.DAL.Entities
{
    public class Card : IAuditedEntity
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public Guid CardGuid { get; set; }

        //TODO make required after implementation
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [Required]
        [DefaultValue(CardStates.CheckedOut)]
        public CardStates CardState { get; set; }

        [Required]
        public Visitor Visitor { get; set; }
    }

    public enum CardStates
    {
        CheckedOut = 0,
        CheckedIn = 1,
        Deactivated = 2
    }

    public enum CardUpdateResultEnum
    {
        DBUpdateFailed = 0,
        Successful = 1,
        IsDeactivated = 2,
        NotFound = 3
    }
}
