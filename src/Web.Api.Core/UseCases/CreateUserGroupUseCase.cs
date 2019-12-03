using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Core.UseCases
{
    public sealed class CreateUserGroupUseCase : ICreateUserGroupUseCase
    {
        private readonly IGroupRepository _groupRepository;

        public CreateUserGroupUseCase(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(CreateUserGroupRequest message, IOutputPort<CreateUserGroupResponse> outputPort)
        {
            var entity = await _groupRepository.Add(new Group(message.Name, message.Description));
            //var response = await _userRepository.Create(message.FirstName, message.LastName,message.Email, message.UserName, message.Password);
            //outputPort.Handle(response.Success ? new CreateUserGroupResponse(response.Id, true) : new CreateUserGroupResponse(response.Errors.Select(e => e.Description)));
            outputPort.Handle(new CreateUserGroupResponse(entity.guid.ToString(), true));
            return true;
        }
    }
}
