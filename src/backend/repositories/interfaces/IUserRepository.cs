public interface IUserRepository
{
    public Task<UserModel> register(UserModel user);
    public UserModel userRegistred (String Email);
    public Task<UserModel> findUser(Guid id);

    
   
}