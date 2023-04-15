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
}