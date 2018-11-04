using KorepetycjeNaJuz.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace KorepetycjeNaJuz.Infrastructure
{
    public class DatabaseSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            KorepetycjeContext context = serviceProvider.GetRequiredService<KorepetycjeContext>();
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            context.Database.EnsureCreated();

            if(!context.Users.Any())
            {
                User user = new User()
                {
                    // Required minimum
                    Email = "user@example.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user@example.com",

                    // Additional
                    Description = "Example User",
                    FirstName = "Janusz",
                    LastName = "Admin",
                    IsCoach = true,
                    Telephone = "656-233-222"

                };
                System.Threading.Tasks.Task<IdentityResult> result = userManager.CreateAsync( user, "Password@123" );
                result.Wait();
                Console.WriteLine(result.Result.Succeeded);


            }

        }
    }
}
