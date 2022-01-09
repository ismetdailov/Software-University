using MyFirstMvcApp.Data;
using MyFirstMvcApp.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Controllers
{
    public class CardsController : Controller
    {
        private ApplicationDbContext db;

        public CardsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            return this.View();
        }
        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd()
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            //var dbContext = new ApplicationDbContext();
            if (this.Request.FormData["name"].Length <=5)
            {
                return this.Error("Name should be at least 5 charecters long.");
            }
            this.db.Cards.Add(new Card
            {
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"]),
                Name = this.Request.FormData["name"],
                Description = this.Request.FormData["description"],
                ImageUrl =   this.Request.FormData["image"],
                Keyword =this.Request.FormData["keyword"],
            }) ;
            //var viewModel = new DoAddViewModel
            //{
            //    Attack = int.Parse(this.Request.FormData["attack"]),
            //    Health = int.Parse(this.Request.FormData["health"]),
            //};
            //return this.View();
            this.db.SaveChanges();
            return this.Redirect("/Cards/All");
        }
        public HttpResponse All()
        {
           // var db = new ApplicationDbContext();
            var cardsViewModel = this.db.Cards.Select(x => new CardViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Attack = x.Attack,
                Health = x.Health,
                ImageUrl = x.ImageUrl,
                Type = x.Keyword,
            }).ToList();
            return this.View(new AllCardsViewModel { Cards = cardsViewModel });
        }
        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            return this.View();
        }
    }
}
