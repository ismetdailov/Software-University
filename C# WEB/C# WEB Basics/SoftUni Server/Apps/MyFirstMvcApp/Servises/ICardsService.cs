using MyFirstMvcApp.ViewModels.Cards;
using System;
using System.Collections.Generic;


namespace MyFirstMvcApp.Servises
{
  public  interface ICardsService
    {
        int AddCard(AddCardInputModel input);
        IEnumerable<CardViewModel> GetAll();
        IEnumerable<CardViewModel> GeByUserId(string userId);
        void AddCardToUserCollection(string userId, int cardId);
        void RemoveCardFromUserCollection(string userId, int cardId);

    }
}
