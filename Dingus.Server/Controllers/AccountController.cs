using System.Threading.Tasks;
using Dingus.Server.Models;
using Dingus.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dingus.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;
        public AccountController(UserService userService)
        {
            _userService = userService; 
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Auth([FromBody]User userParam)
        {
            User user = _userService.Auth(userParam.Login, userParam.Password);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]User userParam)
        {
            if(ModelState.IsValid)
            {
                await _userService.Register(userParam);

                User user = _userService.Auth(userParam.Login, userParam.Password);
                if (user != null)
                {
                    return Ok(user);
                }
            }

            return NotFound();
        }
    }
}