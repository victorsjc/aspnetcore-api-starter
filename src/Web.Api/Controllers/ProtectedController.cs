﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Web.Api.Infrastructure.Identity;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    //[Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public ProtectedController(UserManager<AppUser> userManager){
            _userManager = userManager;
        }

        // GET api/protected/home
        [Authorize(Policy="Administrator")]
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            //var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            ClaimsPrincipal currentUser = this.User;
            var username = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var roles = currentUser.FindFirst("api_roles").Value;
            //var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            //var email = currentUser.FindFirst(c => c.Type == "sub")?.Value;
            var user = await _userManager.FindByNameAsync(username);
            return Ok(roles);
        }
    }
}
