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

    public List<SearchUserLinq> findFiveFirstUserSearched(string name)
    {
        var listUsers = _context.Users.Where(u => u.userName.Contains(name)).Take(5).OrderByDescending(u => u.id).Select(u => new SearchUserLinq
        {
            name = u.userName,
            photo = u.userPhoto,
            email = u.email,
            id = u.id
        }).ToList();
        return listUsers;
    }
    public List<SearchUserLinq> findUserSearchedScrolling(Guid id, string name)
    {
        var listNextUsers = _context.Users.Where(u => u.userName.Contains(name) && u.id.CompareTo(id) < 0).OrderByDescending(u => u.id).Take(5).Select(u => new SearchUserLinq
        {
            name = u.userName,
            photo = u.userPhoto,
            email = u.email,
            id = u.id
        }).ToList();
        return listNextUsers;
    }

}