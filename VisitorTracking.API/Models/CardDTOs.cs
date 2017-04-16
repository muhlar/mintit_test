﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.API.Models
{
    public class CardDTOs
    {
        [Required]
        public string VisitorFirstName { get; set; }
        [Required]
        public string VisitorLastName { get; set; }
        [Required]
        public string VisitorIDCardNumber { get; set; }

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
    }

    public class CardUpdateDTO
    {
        [Required]
        public Guid CardGuid { get; set; }
        [Required]
        public CardStates NewCardState { get; set; }
        
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