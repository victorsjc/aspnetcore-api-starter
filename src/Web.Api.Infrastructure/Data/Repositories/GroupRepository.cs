using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces.Gateways.Repositories;

namespace Web.Api.Infrastructure.Data.Repositories
{
    internal sealed class GroupRepository : EfRepository<Group>, IGroupRepository
    {
    	public GroupRepository(AppDbContext appDbContext): base(appDbContext)
    	{ 

    	}

    }
 }