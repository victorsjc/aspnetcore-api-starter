using System.Collections.Generic;

namespace Web.Api.Models.Request
{
	public class CreateUserGroupRequest
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Role { get; set; }
		public List<User> Members { get; set; }
	}
}