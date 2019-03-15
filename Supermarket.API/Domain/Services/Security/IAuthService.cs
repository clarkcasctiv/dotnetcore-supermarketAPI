
using System.Threading.Tasks;
using Supermarket.API.Domain.Services.Communication;

namespace Supermarket.API.Domain.Services.Security
{
    public interface IAuthService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string email, string password);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail);
        void RevokeRefreshToken(string refreshToken);
    }
}