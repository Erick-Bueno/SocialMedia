using BCrypt.Net;
public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IWebHostEnvironment webHostEnvironment;

    public UserService(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
    {
        this.userRepository = userRepository;
        this.webHostEnvironment = webHostEnvironment;
    }

      async public Task<ResponseRegister> register(UserRegisterDto userDto, IFormFile imagefileuser)
    {
        if(imagefileuser.ContentType != "image/jpeg" && imagefileuser.ContentType != "image/png"){
            throw new Exception("Informe uma imagem valida");
        }
        var fileurlphoto = Guid.NewGuid() + imagefileuser.FileName;
        SaveUserPhoto(fileurlphoto, imagefileuser);
        var userModel = convertUserDtoToUserModel(userDto, fileurlphoto);
        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
        userModel.Password = encryptedPassword;

        var UserRegistered = await userRepository.Register(userModel);
        ResponseRegister rp = new ResponseRegister(UserRegistered.id, 200, "usuario cadastrado");
        //adicionar imagem em pasta na web e criar se n existir
        return rp;
        


    } 
    public UserModel convertUserDtoToUserModel(UserRegisterDto userDto, string url){
        UserModel modelUser = new UserModel();
 
        modelUser.UserName = userDto.UserName;
        modelUser.Email = userDto.Email;
        modelUser.Password = userDto.Password;
        modelUser.Telephone = userDto.Telephone;
        modelUser.User_Photo = url;

        return modelUser;

    }
     public async void SaveUserPhoto(string img, IFormFile imguser){
        //verifica se o direito existe, e adiciona a foto
        if(!Directory.Exists(webHostEnvironment.WebRootPath + "\\UsersProfileImages\\" + img)){
            Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\UsersProfileImages\\");
        }
        //possibilitando o salvamento de arquivos no meu path do wwwroot
        var stream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\UsersProfileImages\\" + img);

        //copiando o arquivo do meu Iform file(fluxo de entrada) para o stram fluxo de saida
        await imguser.CopyToAsync(stream);

     }
}