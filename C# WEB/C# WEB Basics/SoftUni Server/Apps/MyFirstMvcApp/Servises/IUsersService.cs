using System;
using MyFirstMvcApp.ViewModels.Cards;

namespace MyFirstMvcApp.Servises
{
 public interface IUsersService
    {
        string CreateUser(string username, string email, string password);
       string GetUserId(string username, string password);
        bool IsUsernameAvialable(string username);
        bool IsEmailAvialable(string email);
    }
}
