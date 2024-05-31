namespace FileStorage.Auth
{
    public class AuthResponses
    {
            public record class GeneralResponse(bool Success, string Message);
            public record class LoginResponse(bool Success, string Token, string Message);
    }
}
