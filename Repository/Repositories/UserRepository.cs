using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entity;
using Repository.Contracts;

namespace Repository.Repositories
{
    public class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User {Username = "admin", Password = "j#$rTfz!329)", Role = "manager" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}
