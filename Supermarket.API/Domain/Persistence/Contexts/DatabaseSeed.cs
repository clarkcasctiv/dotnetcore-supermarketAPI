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
                    //ACObsBdohUONmrQVm94+VjO1rtHQ934BbnCsDztZOodctuaXEb+1gAoFmvjSQlirEg==
                    new User { Email = "admin@mail.com", Password = passwordHasher.HashPassword("123456")},
                    //AGt9K+7F9PsSovlUc+ChGR7/oZPcJdH1tVB4I9WJjOpQM9LjI1c6mK3tbo38ZVin4A==
                    new User { Email = "user@mail.com", Password = passwordHasher.HashPassword("123456")}


                };

                users[0].UserRoles.Add(new UserRole
                {
                    RoleId = context.Roles.SingleOrDefault(r => r.Name == ERole.Administrator.ToString()).Id

                });

                users[1].UserRoles.Add(new UserRole
                {
                    RoleId = context.Roles.SingleOrDefault(r => r.Name == ERole.Common.ToString()).Id

                });

                context.Users.AddRange(users);
                context.SaveChanges();
            }



        }
    }
}