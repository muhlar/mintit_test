using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VisitorTracking.API.Models;
using VisitorTracking.API.Services;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.API.Controllers
{
    [RoutePrefix("api/v1/cards")]
    public class CardsController : ApiController
    {
        private ICardsService _cardsService;

        public CardsController(ICardsService cardsService)
        {
            _cardsService = cardsService;
        }
        
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] CardDTOs cardDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCard = await _cardsService.RegisterCard(cardDTO.ToCard());

            return Ok(createdCard.Id);
        }

        [Route("")]
        public IHttpActionResult Put([FromBody] CardUpdateDTO cardDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Card card = cardDTO.ToCard();


            _cardsService.UpdateCard(card);

            return Ok(card.Id);
        }
    }
}
