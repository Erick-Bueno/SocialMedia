public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public TokenModel LoggedInBeffore(string email)
    {
        var findUserLogged = _context.Token.Where(tk => tk.Email == email).FirstOrDefault();
        return findUserLogged;
    }

    public UserModel SearchingForEmail(UserLoginDto loginData)
    {
        var FindEmail = _context.Users.Where(u => u.Email == loginData.Email).FirstOrDefault();
        return FindEmail;
    }
}