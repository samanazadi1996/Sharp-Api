using Common.Exceptions;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Dto;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Filters;

namespace MyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiResultFilter]

    public class AccountController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAccountService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;

        public AccountController(IJwtService jwtService, IAccountService userService, ILogger<AccountController> logger,UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _jwtService = jwtService;
            _userService = userService;
        }
        public async Task<ApiResult<RegisterUserDto>> Register(RegisterUserDto user)
        {
            var result = await _userService.RegisterUser(user);
            if (result)
                return Ok();
            return BadRequest();
        }
        public async Task<ApiResult<string>> Gettoken(LoginDto user)
        {
            var person =await _userManager.FindByNameAsync(user.Email);
            if (person is null)
                throw new BadRequestException("نام کاربری اشتباه است");
            var IsPasswordValid = await _userManager.CheckPasswordAsync(person, user.Password);
            if (!IsPasswordValid)
                throw new BadRequestException("رمز عبور اشتباه است");

            var result = await _jwtService.GenerateAsync(person);
            if (string.IsNullOrEmpty(result))
                return BadRequest();
            return result;
        }
        [Authorize]
        public async Task<ApiResult<string>> test(string a)
        {
            return "ok";
        }
    }
}
