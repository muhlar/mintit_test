using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VisitorTracking.DAL.DataContext;

namespace VisitorTracking.DAL.Entities
{
    public class Card : IAuditedEntity
    {
        public int Id { get; set; }
        
        //TODO make required after implementation
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [DefaultValue(CardStates.CheckedOut)]
        public CardStates CardState { get; set; }

        [Required]
        public Visitor Visitor { get; set; }
    }

    public enum CardStates
    {
        CheckedOut = 1,
        CheckedIn = 2
    }    
}
