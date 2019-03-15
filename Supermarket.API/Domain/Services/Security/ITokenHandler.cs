using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Models.Tokens;

namespace Supermarket.API.Domain.Services.Security
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}