using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Specifications;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Domain;
using Web.Api.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Infrastructure.Data.Repositories
{
    internal sealed class GroupRepository : EfRepository<Group>, IGroupRepository
    {
    	private readonly IMapper _mapper;

    	public GroupRepository(AppDbContext appDbContext, IMapper mapper): base(appDbContext)
    	{ 
    		_mapper = mapper;
    	}

    	public async Task<Group> Create(string name, string description, string role, ICollection<string> userIds)
    	{
    		var entity = new Group(name, description, role);
            Group repo = await Add(entity);

            foreach(string id in userIds)
            {
                var user = _appDbContext.Users.Where(u => u.IdentityId == id).Single();
                repo.UsersGroup.Add(new UserGroup(){
                	UserId = user.Id,
                	GroupId = repo.Id
                });
            }
            await _appDbContext.SaveChangesAsync();
            return repo;
    	}

    	public List<User> GetUsers(string guid)
    	{
    		var specification = new UserGroupSpecification(guid);
    		var groupRepo = GetSingleBySpec(specification);
    		var result = _appDbContext.Users.Where(u => u.UsersGroup.Any(ug => ug.GroupId == groupRepo.Id)).ToList();
    		return result;
    	}

    	public List<Group> GetUserGroups(int id)
    	{
    		var result = _appDbContext.Groups.Where(g => g.UsersGroup.Any(ug => ug.UserId == id)).ToList();
    		return result;
    	}
    }
 }