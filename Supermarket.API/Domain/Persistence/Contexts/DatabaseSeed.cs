using System.Collections.Generic;
using System.Linq;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services.Security;

namespace Supermarket.API.Domain.Persistence.Contexts
{
    public class DatabaseSeed
    {
        public static void Seed(AppDbContext context, IPasswordHasher passwordHasher)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Count() == 0)
            {
                var roles = new List<Role>
                {
                    new Role { Name = ERole.Common.ToString()},
                    new Role { Name = ERole.Administrator.ToString()}
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            if (context.Users.Count() == 0)
            {
                var users = new List<User>
                    {
                        new User { Email = "admin@mail.com", Password = passwordHasher.HashPassword("123456")},
                        new User { Email = "user@mail.com", Password = passwordHasher.HashPassword("123456")}
                    };

                //users[0].UserRoles.Add(new UserRole
                //{
                //    //RoleId = context.Roles.SingleOrDefault(r => r.Name == ERole.Administrator.ToString()).Id
                //    RoleId = 2
                //});

                //users[1].UserRoles.Add(new UserRole
                //{
                //    //RoleId = context.Roles.SingleOrDefault(r => r.Name == ERole.Common.ToString()).Id
                //    RoleId = 1
                //});

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}