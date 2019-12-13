using Microsoft.AspNetCore;
using System.Threading.Tasks;

namespace Web.Api.Infrastructure.Auth
{
	public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>, IAuthorizationRequirement
	{
		protected override Task HandleAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
	    {
	    	if (!context.User.HasClaim(c => c.Type == "api_roles"))
	        {
	                context.Fail();
	                return;
	        }
	        context.Succeed(requirement);
	        return Task.FromResult(0);
	    }
	}
}