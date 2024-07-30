using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Constants;
using Todo.Domain.Entities.Context;
using Todo.Domain.Entities.Identity;

namespace Todo.Domain.Seeds
{
    public class SeedApplication
    {

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<TodoContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            context.Database.Migrate(); // apply all migrations 
            context.Database.EnsureCreated();


            if (!context.Roles.Any())
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = RolesNames.Admin, NormalizedName = RolesNames.Admin });
            }
            if (!context.Users.Any())
            {
                var User1 = new ApplicationUser()
                {
                    ApplicationUserId = Guid.NewGuid(),
                    Email = "TomasEssam@Tomas.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "TomasEssam",
                    IsActive = true,
                    CreationUser = "Tomas",
                    CreationDate = DateTime.UtcNow,
                    ModificationUser = "Tomas",
                    ModificationDate = DateTime.UtcNow,
                };

                var User2 = new ApplicationUser()
                {
                    ApplicationUserId = Guid.NewGuid(),
                    Email = "David@Gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "DavidAmir",
                    IsActive = true,
                    CreationUser = "Tomas",
                    CreationDate = DateTime.UtcNow,
                    ModificationUser = "Tomas",
                    ModificationDate = DateTime.UtcNow,
                };

                var User3 = new ApplicationUser()
                {
                    ApplicationUserId = Guid.NewGuid(),
                    Email = "George@Gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "GeorgeMagdy",
                    IsActive = true,
                    CreationUser = "Tomas",
                    CreationDate = DateTime.UtcNow,
                    ModificationUser = "Tomas",
                    ModificationDate = DateTime.UtcNow,
                };
                var User4 = new ApplicationUser()
                {
                    ApplicationUserId = Guid.NewGuid(),
                    Email = "George2@Gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "GeorgeSobhy",
                    IsActive = true,
                    CreationUser = "Tomas",
                    CreationDate = DateTime.UtcNow,
                    ModificationUser = "Tomas",
                    ModificationDate = DateTime.UtcNow,
                };

                try
                {
                    context.Add(User1);
                    context.Add(User2);
                    context.Add(User3);
                    context.Add(User4);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
           

            }

        }
    }
}
