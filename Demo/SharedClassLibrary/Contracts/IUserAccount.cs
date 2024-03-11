using SharedClassLibrary.DTOs;
using static SharedClassLibrary.DTOs.ServiceResponse;

namespace SharedClassLibrary.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResposne> CreateAccount(UserDto userDto);
        Task<LoginResposne> LoginAccount(LoginDto loginDto);
    }
}
