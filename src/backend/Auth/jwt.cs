using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


public class Jwt:Ijwt
{
    public string generateJwt(UserModel user){
        DotNetEnv.Env.Load();
        String jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
        Console.WriteLine(jwtSecret);
        var generateToken = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSecret);
        var contentToken = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = generateToken.CreateToken(contentToken);
        return generateToken.WriteToken(token);
    }
}