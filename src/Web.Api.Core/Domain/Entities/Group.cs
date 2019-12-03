using System;
using System.Collections.Generic;
using System.Linq;
using Web.Api.Core.Shared;

namespace Web.Api.Core.Domain.Entities
{
	public class Group : BaseEntity
	{
		public Guid guid { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<UserGroup> UsersGroup { get; set; }

		public Group(string name, string description)
		{
			Name = name;
			Description = description;
			guid = Guid.NewGuid();
		}
	}
}