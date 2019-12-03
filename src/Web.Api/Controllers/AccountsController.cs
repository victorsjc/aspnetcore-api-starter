﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;
using Web.Api.Serialization;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using System.Net;
using Web.Api.Core.Specifications;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly RegisterUserPresenter _registerUserPresenter;
        private readonly ICreateUserGroupUseCase _createUserGroupUseCase;
        private readonly CreateUserGroupPresenter _createUserGroupPresenter;
        private readonly IGroupRepository _groupRepository;

        public AccountsController(IRegisterUserUseCase registerUserUseCase,
            RegisterUserPresenter registerUserPresenter,
            ICreateUserGroupUseCase createUserGroupUseCase,
            CreateUserGroupPresenter createUserGroupPresenter,
            IGroupRepository groupRepository)
        {
            _registerUserUseCase = registerUserUseCase;
            _registerUserPresenter = registerUserPresenter;
            _createUserGroupPresenter = createUserGroupPresenter;
            _createUserGroupUseCase = createUserGroupUseCase;
            _groupRepository = groupRepository;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.Request.RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _registerUserUseCase.Handle(new RegisterUserRequest(request.FirstName, request.LastName, request.Email, request.UserName, request.Password), _registerUserPresenter);
            return _registerUserPresenter.ContentResult;
        }

        [HttpPost("/usergroups")]
        public async Task<ActionResult> UserGroup([FromBody] Models.Request.CreateUserGroupRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _createUserGroupUseCase.Handle(new CreateUserGroupRequest(request.Name, request.Description), _createUserGroupPresenter);
            return _createUserGroupPresenter.ContentResult;
        }

        [HttpGet("/usergroups/{id}")]
        public async Task<ActionResult> GetUserGroup(string id)
        {
            if (id == null)
            {
                return BadRequest(ModelState);
            }
            var entity = await _groupRepository.GetSingleBySpec(new UserGroupSpecification(id));
            var result = new JsonContentResult();
            result.StatusCode = (int)HttpStatusCode.OK;
            result.Content = JsonSerializer.SerializeObject(entity);
            return result;
        }
    }
}
