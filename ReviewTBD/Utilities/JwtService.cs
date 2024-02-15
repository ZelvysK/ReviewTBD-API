using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReviewTBDAPI.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ReviewTBDAPI.Utilities;

public interface IJwtService
{
    string Generate(Guid id);
    JwtSecurityToken Verify(string jwt);
}

public class JwtService(IOptions<AuthConfiguration> authConfiguration) : IJwtService
{
    public string Generate(Guid id)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.Value.SecureKey));
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var header = new JwtHeader(credentials);
        var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
        var securityToken = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public JwtSecurityToken Verify(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(authConfiguration.Value.SecureKey);

        tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
            },
            out SecurityToken validatedToken);

        return (JwtSecurityToken)validatedToken;
    }
}