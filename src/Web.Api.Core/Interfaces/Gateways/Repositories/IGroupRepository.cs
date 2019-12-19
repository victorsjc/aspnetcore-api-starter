using System.Threading.Tasks;
using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Domain;
using Web.Api.Core.Dto.GatewayResponses.Repositories;

namespace Web.Api.Core.Interfaces.Gateways.Repositories
{
    public interface IGroupRepository  : IRepository<Group>
    {
    	Task<Group> Create(string name, string description, string role, ICollection<string> userIds);
    	Task<PagedResult<User>> GetUsers(string guid);
    	List<Group> GetUserGroups(int id);
    }
}
