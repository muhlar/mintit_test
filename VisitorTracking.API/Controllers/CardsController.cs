using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        // GET api/cards
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/cards/5
        [Route("id:int")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/cards
        [Route("")]
        public IHttpActionResult Post(CardDTO card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCard = _cardsService.RegisterCard(card.ToCard());

            return Ok(createdCard.Id);
        }

        // PUT api/cards/5
        [Route("id:int")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/cards/5
        [Route("id:int")]
        public void Delete(int id)
        {
        }
    }
}
