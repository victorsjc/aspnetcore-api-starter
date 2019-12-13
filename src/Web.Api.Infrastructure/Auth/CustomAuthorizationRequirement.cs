using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Infrastructure.Auth
{
	public class CustomAuthorizationRequirement : IAuthorizationRequirement
	{
	    public string Role { get; }

	    public CustomAuthorizationRequirement(string role)
	    {
	        Role = role;
	    }
	}
}