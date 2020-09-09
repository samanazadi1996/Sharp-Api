using Services.Dto;
using Entities;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        Task<bool> RegisterUser(RegisterUserDto userDto);
    }
}