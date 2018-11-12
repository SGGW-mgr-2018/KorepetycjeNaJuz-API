using KorepetycjeNaJuz.Core.Enums;
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

            InitializeLessonStatuses(context);

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
                    Telephone = "656-233-222"

                };
                System.Threading.Tasks.Task<IdentityResult> result = userManager.CreateAsync( user, "Password@123" );
                result.Wait();
                Console.WriteLine(result.Result.Succeeded);
            }
        }

        private static void InitializeLessonStatuses(KorepetycjeContext context)
        {
            if (context.LessonStatuses.Any())
                return;

            var lessonStatusesIds = Enum.GetValues(typeof(LessonStatuses)).Cast<int>().ToList();
            foreach (var statusId in lessonStatusesIds)
            {
                var statusName = ((LessonStatuses)statusId).ToString();
                var lessonStatus = new LessonStatus
                {
                    Name = statusName
                };
                context.LessonStatuses.Add(lessonStatus);
            }
            context.SaveChanges();
        }
    }
}
