public interface IUserRepository
{
    public Task<UserModel> register(UserModel user);
    public UserModel userRegistred (String Email);
    public Task<UserModel> findUser(Guid id);   
    public List<SearchUserLinq> findFiveFirstUserSearched(string name, Guid? id);  
    public List<SearchUserLinq> findUserSearchedScrolling(Guid id, string name,  Guid? userId);
}