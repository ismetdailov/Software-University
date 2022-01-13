using MyFirstMvcApp.Data;
using MyFirstMvcApp.Servises;
using MyFirstMvcApp.ViewModels;
using MyFirstMvcApp.ViewModels.Cards;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Linq;

namespace MyFirstMvcApp.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            return this.View();
        }
        [HttpPost]
        public HttpResponse Add(AddCardInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            //var dbContext = new ApplicationDbContext();
            if (string.IsNullOrEmpty(model.Name)|| model.Name.Length <5 || model.Name.Length>15)
            {
                return this.Error("Name should be at betwee 5 and 15 charecters long.");
            }
            if (string.IsNullOrWhiteSpace(model.Image))
            {
                return this.Error("The image i required!");
            }
            if (!Uri.TryCreate(model.Image, UriKind.Absolute, out _))
            {
                return this.Error("Image url dhould be valid!");
            }
            if (string.IsNullOrWhiteSpace(model.Keyword))
            {
                return this.Error("Keyword is required!");
            }
            if (model.Attack <0 )
            {
                return this.Error("Attack should be non-negative integer.");
            }
            if (model.Health<0)
            {
                return this.Error("Health should be non-negative integer.");
            }
            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length>200)
            {
                return this.Error("Description is required and its length should be most 200 charecters");
            }
            this.cardsService.AddCard(model);
            return this.Redirect("/Cards/All");
        }
        public HttpResponse All()
        {
            // var db = new ApplicationDbContext();if (!this.IsUserSignedIn())
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            var cardsViewModel = this.cardsService.GetAll();
            return this.View(cardsViewModel);
        }
        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
        
            var viewModel = this.cardsService.GeByUserId(this.GetUserId());
            return this.View(viewModel);
        }
        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            var userId = this.GetUserId();
            this.cardsService.AddCardToUserCollection(userId, cardId);
            return this.Redirect("/Cards/All");
        }
        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            var userId = this.GetUserId();
            this.cardsService.RemoveCardFromUserCollection(userId, cardId);
            return this.Redirect("/Cards/Collection");
        }
    }
}
