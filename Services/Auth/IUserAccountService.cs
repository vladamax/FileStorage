using static FileStorage.Auth.AuthResponses;
using FileStorage.Auth.DTO;

namespace FileStorage.Services.Auth
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreateAccount(UserDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
