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

        var userExists = userRepository.userRegistred(userDto.email);
        var fileNamePhoto = "";
        var fileUrlPhoto = "";
        if (userExists != null)
        {
            throw new ValidationException("Email já cadastrado");
        }
        if (imagefileuser != null)
        {
            if (imagefileuser.ContentType != "image/jpeg" && imagefileuser.ContentType != "image/png")
            {
                throw new ValidationException("Informe uma imagem valida");
            }
            fileNamePhoto = Guid.NewGuid() + "_" + imagefileuser.FileName;
            await saveUserPhoto(fileNamePhoto, imagefileuser);
            fileUrlPhoto = "https://localhost:7088/UsersProfileImages/" + fileNamePhoto;

        }


        var userModel = convertUserDtoToUserModel(userDto, fileUrlPhoto);
        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.password);
        userModel.password = encryptedPassword;

        var userRegistered = await userRepository.register(userModel);

        var token = jwt.generateJwt(userModel);

        TokenModel tokenmodel = new TokenModel();
        tokenmodel.email = userModel.email;
        tokenmodel.jwt = token;

        var addToken = await tokenRepository.addUserToken(tokenmodel);


        ResponseRegister rp = new ResponseRegister(200, "usuario cadastrado", userRegistered.id, token);
        //adicionar imagem em pasta na web e criar se n existir
        return rp;



    }
    public UserModel convertUserDtoToUserModel(UserRegisterDto userDto, string url)
    {
        UserModel modelUser = new UserModel();

        modelUser.userName = userDto.userName;
        modelUser.email = userDto.email;
        modelUser.password = userDto.password;
        modelUser.telephone = userDto.telephone;
        modelUser.userPhoto = url;

        return modelUser;

    }
    public async Task saveUserPhoto(string img, IFormFile imguser)
    {
        //verifica se o direito existe, e adiciona a foto
        if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\UsersProfileImages\\"))
        {
            Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\UsersProfileImages\\");
        }
        //criando o arquivo no diretorio especificado

        using (var stream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\UsersProfileImages\\" + img))
        {
            await imguser.CopyToAsync(stream);
        }

    }

    public async Task<UserModel> findUser(Guid id)
    {
        var userData = await userRepository.findUser(id);
        return userData;
    }

    


}