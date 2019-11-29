using System;
using System.Collections.Generic;
using System.Linq;
using Web.Api.Core.Shared;

namespace Web.Api.Core.Domain.Entities
{
	public class Group : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<UserGroup> UsersGroup { get; set; }
	}
}