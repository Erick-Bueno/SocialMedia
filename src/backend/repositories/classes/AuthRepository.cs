public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TokenModel> findUserEmailWithToken(string jwt)
    {
        var tokenRegister =  _context.Token.Where(tk => tk.jwt == jwt).FirstOrDefault();
        return tokenRegister;
    }

    public TokenModel loggedInBeffore(string email)
    {
        var findUserLogged = _context.Token.Where(tk => tk.email == email).FirstOrDefault();
        return findUserLogged;
    }

    public UserModel searchingForEmail(UserLoginDto loginData)
    {
        var findEmail = _context.Users.Where(u => u.email == loginData.email).FirstOrDefault();
        return findEmail;
    }

   
}