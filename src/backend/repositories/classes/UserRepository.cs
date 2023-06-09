using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<UserModel> findUser(Guid id)
    {
        var findUser = await _context.Users.FindAsync(id);
        return findUser;
    }

    async public Task<UserModel> register(UserModel user)
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

    public UserModel userRegistred(string Email)
    {
        var userRegistred = _context.Users.Where(u => u.email == Email).FirstOrDefault();
        return userRegistred;
    }
    public int findFriends(Guid id)
    {
        var listFriends = _context.Friends.Where(f => f.userId == id || f.userId2 == id);
        var countFriends = listFriends.Count();
        return countFriends;
    }
}