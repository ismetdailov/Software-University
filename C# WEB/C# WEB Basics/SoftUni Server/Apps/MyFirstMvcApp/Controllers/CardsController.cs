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
        public HttpResponse Add()
        {
            return this.View();
        }
        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd()
        {
            var dbContext = new ApplicationDbContext();
            if (this.Request.FormData["name"].Length <=5)
            {
                return this.Error("Name should be at least 5 charecters long.");
            }
            dbContext.Cards.Add(new Card
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
            dbContext.SaveChanges();
            return this.Redirect("/");
        }
        public HttpResponse All()
        {
            var db = new ApplicationDbContext();
            var cardsViewModel = db.Cards.Select(x => new CardViewModel
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
            return this.View();
        }
    }
}
