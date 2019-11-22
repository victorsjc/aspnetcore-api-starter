using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Infrastructure.Identity;
using Microsoft.Extensions.Logging;

namespace Web.Api.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory, UserManager<AppUser> userManager)
        {
            var appUser = await userManager.FindByNameAsync("demouser@microsoft.com");
            var user = new User("demo", "user", appUser.Id, appUser.UserName);
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
}
