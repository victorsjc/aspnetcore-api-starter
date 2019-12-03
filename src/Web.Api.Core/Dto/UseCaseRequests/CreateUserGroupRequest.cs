using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
  public class CreateUserGroupRequest : IUseCaseRequest<CreateUserGroupResponse>
  {
    public string Name { get; }
    public string Description { get; }

    public CreateUserGroupRequest(string name, string description)
    {
      Name = name;
      Description = description;
    }
  }
}
