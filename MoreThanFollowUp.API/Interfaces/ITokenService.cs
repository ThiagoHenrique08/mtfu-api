using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MoreThanFollowUp.API.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config);

        String GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
    }
}
