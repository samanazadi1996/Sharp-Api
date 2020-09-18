using Data;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Dto;
using System;
using System.Threading.Tasks;


namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(ILogger<AccountService> logger, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<User> GetCurentUserAsync() => _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        public async Task<bool> RegisterUserAsync(RegisterUserDto userDto)
        {
            try
            {
                var db = new ApplicationDbContext();
                var user = ConvertRegisterDtoToEntity(userDto);
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Registering - " + userDto.Email);      
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        private User ConvertRegisterDtoToEntity(RegisterUserDto userDto)
        {
            var user = new User()
            {
                UserName = userDto.Email.Trim(),
                Email = userDto.Email.Trim(),
                DateOfBirth = userDto.DateOfBirth,
                FirstName = userDto.FirstName.Trim(),
                LastName = userDto.LastName.Trim(),
                Gender = userDto.Gender,
                PasswordHash = userDto.Password,
                PhoneNumber = userDto.PhoneNumber.Trim()
            };
            return user;
        }

    }
}
