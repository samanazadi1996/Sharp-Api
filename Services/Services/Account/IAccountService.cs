using Entities;
using Services.Dto;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto userDto);
        Task<User> GetCurentUserAsync();
    }
}