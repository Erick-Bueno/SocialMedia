public interface IUserService
{
     Task<ResponseRegister> register(UserRegisterDto userDto, IFormFile imagefileuser); 
    UserModel convertUserDtoToUserModel(UserRegisterDto userDto, string url);

    public void SaveUserPhoto(string img, IFormFile imgfile);
    
}