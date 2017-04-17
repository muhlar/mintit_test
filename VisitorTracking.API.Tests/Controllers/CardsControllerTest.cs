using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using VisitorTracking.API.Controllers;
using VisitorTracking.API.Models;
using VisitorTracking.API.Services;
using VisitorTracking.DAL.Entities;

namespace VisitorTracking.API.Tests.Controllers
{
    [TestClass]
    public class CardsControllerTest
    {
        [TestMethod]
        public async Task Post_ShouldReturnNotFound()
        {
            var cardsService = new Mock<ICardsService>();
            var controller = new CardsController(cardsService.Object);

            // Act  
            IHttpActionResult actionResult = await controller.Post(null);

            Assert.IsInstanceOfType(actionResult, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public async Task Post_ShouldReturnGuid()
        {
            var dto = new CardDTO { VisitorFirstName = "Marek", VisitorLastName = "Uhlar", VisitorIDCardNumber = "EU111111" };
            var cardGuid = Guid.NewGuid();

            var cardsService = new Mock<ICardsService>();
            cardsService.Setup(cs => cs.RegisterCard(It.IsAny<Card>())).Returns(Task.FromResult(new Card() { CardGuid = cardGuid }));
            var controller = new CardsController(cardsService.Object);

            var actionResult = await controller.Post(dto);
            var response = actionResult as OkNegotiatedContentResult<Guid>;

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Content, cardGuid);
        }

        [TestMethod]
        public async Task Put_ShouldReturnNotFound()
        {
            var dto = new CardUpdateDTO { CardGuid = Guid.NewGuid(), NewCardState = CardStates.CheckedIn };

            var cardsService = new Mock<ICardsService>();
            cardsService.Setup(cs => cs.UpdateCard(It.IsAny<Card>())).Returns(Task.FromResult(CardUpdateResultEnum.NotFound));
            var controller = new CardsController(cardsService.Object);

            var actionResult = await controller.Put(dto);
            var response = actionResult as OkNegotiatedContentResult<CardUpdateResultEnum>;

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Content, CardUpdateResultEnum.NotFound);
        }

        [TestMethod]
        public async Task Put_ShouldReturnSuccessful()
        {
            var dto = new CardUpdateDTO { CardGuid = Guid.NewGuid(), NewCardState = CardStates.CheckedIn };

            var cardsService = new Mock<ICardsService>();
            cardsService.Setup(cs => cs.UpdateCard(It.IsAny<Card>())).Returns(Task.FromResult(CardUpdateResultEnum.Successful));
            var controller = new CardsController(cardsService.Object);

            var actionResult = await controller.Put(dto);
            var response = actionResult as OkNegotiatedContentResult<CardUpdateResultEnum>;

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Content, CardUpdateResultEnum.Successful);
        }

        [TestMethod]
        public async Task Put_ShouldReturnIsDeactivated()
        {
            var dto = new CardUpdateDTO { CardGuid = Guid.NewGuid(), NewCardState = CardStates.CheckedIn };

            var cardsService = new Mock<ICardsService>();
            cardsService.Setup(cs => cs.UpdateCard(It.IsAny<Card>())).Returns(Task.FromResult(CardUpdateResultEnum.IsDeactivated));
            var controller = new CardsController(cardsService.Object);

            var actionResult = await controller.Put(dto);
            var response = actionResult as OkNegotiatedContentResult<CardUpdateResultEnum>;

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Content, CardUpdateResultEnum.IsDeactivated);
        }
        
    }
}
