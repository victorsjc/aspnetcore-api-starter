using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Web.Api.Infrastructure.Auth
{
	public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>, IAuthorizationRequirement
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
	    {
	    	if (!context.User.HasClaim(c => c.Type == "api_roles"))
	        {
	                context.Fail();
	                return Task.FromResult(0);
	        }
	        context.Succeed(requirement);
	        return Task.FromResult(0);
	    }
	}
}