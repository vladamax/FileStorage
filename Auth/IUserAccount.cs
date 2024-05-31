using FileStorage.Auth.DTO;
using static FileStorage.Auth.AuthResponses;

namespace FileStorage.Auth
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
