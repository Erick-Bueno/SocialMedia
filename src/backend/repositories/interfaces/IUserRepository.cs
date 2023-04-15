public interface IUserRepository
{
    public Task<UserModel> Register(UserModel user);
}