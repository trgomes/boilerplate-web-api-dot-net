using Solution.Application.ViewModels;

namespace Solution.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWT(UserViewModel user, string secretKey);
    }
}
