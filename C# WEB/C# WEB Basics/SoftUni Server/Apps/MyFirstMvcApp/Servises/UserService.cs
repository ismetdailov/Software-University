﻿using MyFirstMvcApp.Data;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.Servises
{
    public class UserService : IUsersService
    {
        private readonly ApplicationDbContext db;
        public UserService()
        {
            this.db = new ApplicationDbContext();
        }
        public void CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Role = IdentityRole.User,
                Password = ComputeHash(password),
            };
            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public bool IsEmailAvialable(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameAvialable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        public bool IsUserValid(string username, string password)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Username == username);
            return user.Password == ComputeHash(password);
        }


        private static string ComputeHash(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}