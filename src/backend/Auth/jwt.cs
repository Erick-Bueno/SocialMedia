using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class Jwt:Ijwt
{
    public string generateJwt(UserModel user){
        String jwt_secret = Environment.GetEnvironmentVariable("JWT_SECRET");
        var generatetoken = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwt_secret);
        var contentTtoken = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = generatetoken.CreateToken(contentTtoken);
        return generatetoken.WriteToken(token);
    }
}