namespace AuthServer.Core
{
    public interface IAuthService
    {
        Task<string> GenerateJwtTokenAsync(User user);

    }
}