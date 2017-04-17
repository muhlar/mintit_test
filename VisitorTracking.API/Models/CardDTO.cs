using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.API.Models
{
    /// <summary>
    /// Contains information about the Visitor required for card creation/retrieval.
    /// </summary>
    public class CardDTO
    {
        /// <summary>
        /// Visitor's first name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string VisitorFirstName { get; set; }

        /// <summary>   
        /// Visitor's last name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string VisitorLastName { get; set; }

        /// <summary>   
        /// Visitor's ID Card number.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string VisitorIDCardNumber { get; set; }

        /// <summary>
        /// converts DTO object to entity
        /// </summary>
        /// <returns>Card entity</returns>
        public Card ToCard()
        {
            return new Card()
            {
                Visitor = new Visitor()
                {
                    FirstName = VisitorFirstName,
                    LastName = VisitorLastName,
                    IDCardNumber = VisitorIDCardNumber
                }
            };
        }

        ///// <summary>
        ///// Validation.
        ///// </summary>
        ///// <param name="validationContext"></param>
        ///// <returns></returns>
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
            
        //}
    }

    /// <summary>
    /// Contains information for card update.
    /// </summary>
    public class CardUpdateDTO
    {
        /// <summary>
        /// Card GUID
        /// </summary>
        [Required]
        public Guid CardGuid { get; set; }

        /// <summary>
        /// New card state for particular card.
        /// </summary>
        [Required]
        public CardStates NewCardState { get; set; }
        
        /// <summary>
        /// converts DTO object to entity
        /// </summary>
        /// <returns>Card entity</returns>
        public Card ToCard()
        {
            return new Card()
            {
                CardGuid = CardGuid,
                CardState = NewCardState
            };
        }
    }
}