public interface IUserRepository
{
    public Task<UserModel> Register(UserModel user);
    public bool user_registred (String Email);
}