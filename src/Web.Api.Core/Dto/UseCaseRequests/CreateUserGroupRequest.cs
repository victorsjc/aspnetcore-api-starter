using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using System.Collections.Generic;

namespace Web.Api.Core.Dto.UseCaseRequests
{
  public class CreateUserGroupRequest : IUseCaseRequest<CreateUserGroupResponse>
  {
    public string Name { get; }
    public string Description { get; }
    public string Role { get; }
    public List<string> Members { get; }

    public CreateUserGroupRequest(string name, string description, string role, List<string> members)
    {
      Name = name;
      Description = description;
      Members = members;
      Role = role;
    }
  }
}
