using System.ComponentModel.DataAnnotations;
using BCrypt.Net;
public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly ITokenRepository tokenRepository;
    private readonly IWebHostEnvironment webHostEnvironment;

    private readonly Ijwt jwt;

    public UserService(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, ITokenRepository tokenRepository, Ijwt jwt)
    {
        this.userRepository = userRepository;
        this.webHostEnvironment = webHostEnvironment;
        this.tokenRepository = tokenRepository;
        this.jwt = jwt;
    }

      async public Task<ResponseRegister> register(UserRegisterDto userDto, IFormFile imagefileuser)
    {
        var userExists = userRepository.user_registred(userDto.Email);
        if(userExists == true){
            throw new ValidationException("Email já cadastrado");
        }
        if(imagefileuser.ContentType != "image/jpeg" && imagefileuser.ContentType != "image/png"){
            throw new ValidationException("Informe uma imagem valida");
        }
        var fileurlphoto = Guid.NewGuid() + imagefileuser.FileName;
        SaveUserPhoto(fileurlphoto, imagefileuser);
        var userModel = convertUserDtoToUserModel(userDto, fileurlphoto);
        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
        userModel.Password = encryptedPassword;

        var UserRegistered = await userRepository.Register(userModel);

        var token = jwt.generateJwt(userModel);
    
        TokenModel tokenmodel = new TokenModel();
        tokenmodel.Email = userModel.Email;
        tokenmodel.jwt = token;

        var addtoken = await tokenRepository.addUserToken(tokenmodel);
      
        
        ResponseRegister rp = new ResponseRegister( 200,"usuario cadastrado",UserRegistered.id, token);
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
        //criando o arquivo no diretorio especificado
        var stream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\UsersProfileImages\\" + img);

        //copiar o conteudo do arquivo e salva no aqrquivo criado
        await imguser.CopyToAsync(stream);

     }
}