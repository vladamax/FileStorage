using FileStorage.Auth;
using FileStorage.Auth.DTO;
using FileStorage.Core.Entities;
using FileStorage.DBContext;
using static FileStorage.Auth.AuthResponses;

namespace FileStorage.Services.Auth
{
    public class AccountService(FileStorageContext fileStorageContext, IUserAccount accountRepository) : IUserAccountService
    {
        public async Task<GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            GeneralResponse response = await accountRepository.CreateAccount(userDTO);

            if(response.Success)
            {
                User user = new User(userDTO.Email);
                fileStorageContext.Users.Add(user);
                fileStorageContext.SaveChanges();
            }

            return response;
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            return await accountRepository.LoginAccount(loginDTO);
        }
    }
}
