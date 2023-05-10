using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using MySqlConnector;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserModel> FindUserRequester(Guid id)
    {
        var findUser = await _context.Users.FindAsync(id);
        return findUser;
    }

    async public Task<UserModel> Register(UserModel user)
    {
        try
        {
            
                var registeredUser = await _context.Users.AddAsync(user);
              
                await _context.SaveChangesAsync();
                return user;
        }
        catch (DbException ex)
        {
                throw new ValidationException("Erro ao cadastrar um usuario");
        }
   
   
      
    }

    public UserModel user_registred(string Email)
    {
        var UserRegistred = _context.Users.Where(u => u.Email == Email).FirstOrDefault();
        return UserRegistred;
    }
}