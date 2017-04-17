using System.Threading.Tasks;
using System.Web.Http;
using VisitorTracking.API.Models;
using VisitorTracking.API.Services;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.API.Controllers
{
    /// <summary>
    /// Cards controller.
    /// </summary>
    [RoutePrefix("api/v1/cards")]
    public class CardsController : ApiController
    {
        private ICardsService _cardsService;

        /// <summary>
        /// Cards controller constructor.
        /// </summary>
        /// <param name="cardsService"></param>
        public CardsController(ICardsService cardsService)
        {
            _cardsService = cardsService;
        }

        /// <summary>
        /// Creates new card in case Visitor doesn't have any active card,
        /// otherwise returns the active card.
        /// </summary>
        /// <returns>Created/retrieved card GUID.</returns>
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] CardDTO cardDTO)
        {
                if (cardDTO == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdCard = await _cardsService.RegisterCard(cardDTO.ToCard());

                return Ok(createdCard.CardGuid);
        }

        /// <summary>
        /// Updates particular card.
        /// </summary>
        /// <returns>
        /// Card update result. Codes:
        /// DBUpdateFailed = 0, Successful = 1, IsDeactivated = 2, NotFound = 3
        /// </returns>
        [Route("")]
        public async Task<IHttpActionResult> Put([FromBody] CardUpdateDTO cardDTO)
        {
            if (cardDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Card card = cardDTO.ToCard();

            CardUpdateResultEnum result = await _cardsService.UpdateCard(card);

            return Ok(result);
        }
    }
}
