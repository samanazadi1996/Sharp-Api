using Common;
using Data;
using Services.Dto;
using Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Common.Exceptions;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly UserManager<User> _userManager;

        public AccountService(ILogger<AccountService> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<bool> RegisterUser(RegisterUserDto userDto)
        {
            try
            {
                var db = new ApplicationDbContext();
                var user = await ConvertRegisterDtoToEntity(userDto);
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
        private async Task<User> ConvertRegisterDtoToEntity(RegisterUserDto userDto)
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
