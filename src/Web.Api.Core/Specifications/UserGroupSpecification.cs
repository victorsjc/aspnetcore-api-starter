using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Specifications
{
    public sealed class UserGroupSpecification : BaseSpecification<Group>
    {
        public UserGroupSpecification(string guid) : base(u => u.guid.ToString()==guid)
        {
        	
        }
    }
}
