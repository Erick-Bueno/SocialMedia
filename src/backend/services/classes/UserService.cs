using BCrypt.Net;
public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

      async public Task<ResponseRegister> register(UserRegisterDto userDto)
    {
        var userModel = convertUserDtoToUserModel(userDto);
        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
        userModel.Password = encryptedPassword;

        var UserRegistered = await userRepository.Register(userModel);
        ResponseRegister rp = new ResponseRegister(UserRegistered.id, 200, "usuario cadastrado");
        //adicionar imagem em pasta na web e criar se n existir
        return rp;
        


    } 
    public UserModel convertUserDtoToUserModel(UserRegisterDto userDto){
        UserModel modelUser = new UserModel();
 
        modelUser.UserName = userDto.UserName;
        modelUser.Email = userDto.Email;
        modelUser.Password = userDto.Password;
        modelUser.Telephone = userDto.Telephone;
        modelUser.User_Photo = userDto.User_Photo;

        return modelUser;

    }
}