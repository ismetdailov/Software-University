using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
