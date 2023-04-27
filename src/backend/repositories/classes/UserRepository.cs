public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }


    async public Task<UserModel> Register(UserModel user)
    {
      
        
              var registeredUser = await _context.Users.AddAsync(user);
              
                await _context.SaveChangesAsync();
                return user;
   
   
      
    }

    public bool user_registred(string Email)
    {
        var UserRegistred = _context.Users.Where(u => u.Email == Email);
        if(UserRegistred.Any()){
            return true;
        }
        return false;
    }
}