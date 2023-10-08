public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }


    public UserModel searchingForEmail(UserLoginDto loginData)
    {
        var findEmail = _context.Users.Where(u => u.email == loginData.email).FirstOrDefault();
        return findEmail;
    }

   
}