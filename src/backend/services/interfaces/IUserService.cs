public interface IUserService
{
    public Task<ResponseRegister> register(UserRegisterDto userDto, IFormFile imagefileuser); 
    public UserModel convertUserDtoToUserModel(UserRegisterDto userDto, string url);

    public void SaveUserPhoto(string img, IFormFile imgfile);

    public Task<UserModel> FindUserRequester(Guid id);
    
}