public interface IUserService
{
     Task<ResponseRegister> register(UserRegisterDto userDto); 
    UserModel convertUserDtoToUserModel(UserRegisterDto userDto);
}