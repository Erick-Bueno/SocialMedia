public interface IUserService
{
    public Task<ResponseRegister> register(UserRegisterDto userDto, IFormFile imagefileuser); 
    public UserModel convertUserDtoToUserModel(UserRegisterDto userDto, string url);
    public Task saveUserPhoto(string img, IFormFile imgfile);
    public Task<UserModel> findUser(Guid id);
    public List<SearchUserLinq> findFiveFirstUserSearched(string name,Guid? id);  
    public List<SearchUserLinq> findUserSearchedScrolling(Guid id, string name, Guid? userId);

    
}