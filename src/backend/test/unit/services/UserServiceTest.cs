using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Text;
using Moq;
using Xunit;

public class UserServiceTest
{
    public UserRegisterDto userDto { get; set; }
    public UserModel userModelTest { get; set; }
    public UserServiceTest()
    {

        userDto = new UserRegisterDto();
        userModelTest = new UserModel();



        userDto.email = "erickjb93@gmail.com";
        userDto.password = "Sirlei231";
        userDto.userName = "erick";
        userDto.telephone = "77799591703";
        userDto.userPhoto = "llll";
        userDto.userImageFile = null;

        var encrypted_password = BCrypt.Net.BCrypt.HashPassword(userDto.password);
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = userDto.email;
        userModelTest.password = encrypted_password;
        userModelTest.userName = userDto.userName;
        userModelTest.telephone = userDto.telephone;
        userModelTest.userPhoto = userDto.userPhoto;
    }
    [Fact]
    async public void register_user_service_test()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var tokenRepository = new Mock<ITokenRepository>();
        var jwt = new Mock<Ijwt>();

        var userService = new UserService(userRepositoryMock.Object, IWebHostEnvironmentMock.Object, tokenRepository.Object, jwt.Object);

        //simulated formfile
        var filePhotoMock = new Mock<IFormFile>();
        var memoryStream = new MemoryStream();
        var writeFile = new StreamWriter(memoryStream);
        writeFile.Write("arquivo de teste");
        writeFile.Flush();
        memoryStream.Position = 0;
        var filePhoto = "imagem.jpg";
        userDto.userImageFile = filePhotoMock.Object;
        filePhotoMock.Setup(f => f.FileName).Returns(filePhoto);
        filePhotoMock.Setup(f => f.ContentType).Returns("image/jpeg");



        userRepositoryMock
        .Setup(ur => ur.register(It.IsAny<UserModel>()))
        .ReturnsAsync(userModelTest);



        var result = await userService.register(userDto, userDto.userImageFile);





        Assert.IsType<ResponseRegister>(result);
    }
    [Fact]
    public void convert_userDto_to_userModel_test()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var filePhotoMock = new Mock<IFormFile>();
        var memoryStream = new MemoryStream();
        var writeFile = new StreamWriter(memoryStream);
        writeFile.Write("arquivo de teste");
        writeFile.Flush();
        memoryStream.Position = 0;
        var filePhoto = "imagem.jpg";
        var jwt = new Mock<Ijwt>();
        userDto.userImageFile = filePhotoMock.Object;
        filePhotoMock.Setup(f => f.FileName).Returns(filePhoto);
        filePhotoMock.Setup(f => f.ContentType).Returns("image/jpeg");

        var urlphoto = Guid.NewGuid() + userDto.userImageFile.FileName;
        var tokenRepository = new Mock<ITokenRepository>();
        var UserRepositoryMock = new Mock<IUserRepository>();
        UserRepositoryMock.Setup(ur => ur.register(userModelTest)).ReturnsAsync(userModelTest);

        var userService = new UserService(UserRepositoryMock.Object, IWebHostEnvironmentMock.Object, tokenRepository.Object, jwt.Object);

        var result = userService.convertUserDtoToUserModel(userDto, urlphoto);

        Assert.IsType<UserModel>(result);

    }
    [Fact]
    public void image_profile_user_save_test()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        IWebHostEnvironmentMock.Setup(wh => wh.WebRootPath).Returns("C:\\Users\\erick\\Documents\\GitHub\\SocialMedia\\src\\backend\\wwwroot");
        var tokenRepository = new Mock<ITokenRepository>();
        var nomeimg = "teste.png";
        var IFormFileMock = new Mock<IFormFile>();
        var streamMock = new MemoryStream();
        IFormFileMock.Setup(ff => ff.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None)).Returns(Task.FromResult(0));

        var jwt = new Mock<Ijwt>();

        var userRepositoryMock = new Mock<IUserRepository>();
        var userService = new UserService(userRepositoryMock.Object, IWebHostEnvironmentMock.Object, tokenRepository.Object, jwt.Object);

        userService.saveUserPhoto(nomeimg, IFormFileMock.Object);

        Assert.True(Directory.Exists("C:\\Users\\erick\\Documents\\GitHub\\SocialMedia\\src\\backend\\wwwroot"));


    }
    [Fact]
    async public void Verify_fileContentType_is_jpg_or_png_Exception_test()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var filePhotoMock = new Mock<IFormFile>();

        var tokenRepository = new Mock<ITokenRepository>();
        var memoryStream = new MemoryStream();
        var writeFile = new StreamWriter(memoryStream);
        writeFile.Write("arquivo de teste");
        writeFile.Flush();
        memoryStream.Position = 0;

        userDto.userImageFile = filePhotoMock.Object;

        filePhotoMock.Setup(f => f.ContentType).Returns("image/csv");

        var userMockRepository = new Mock<IUserRepository>();

        userMockRepository.Setup(um => um.register(userModelTest)).ReturnsAsync(userModelTest);
        var jwt = new Mock<Ijwt>();
        var userService = new UserService(userMockRepository.Object, IWebHostEnvironmentMock.Object, tokenRepository.Object, jwt.Object);

        var result = userService.register(userDto, userDto.userImageFile);

        await Assert.ThrowsAsync<ValidationException>(() => result);

    }
    [Fact]
    async public void User_registred_true_Exception_test()
    {
        var filePhotoMock = new Mock<IFormFile>();
        var memoryStream = new MemoryStream();
        var writeFile = new StreamWriter(memoryStream);
        writeFile.Write("arquivo de teste");
        writeFile.Flush();
        memoryStream.Position = 0;
        userDto.userImageFile = filePhotoMock.Object;
        var jwt = new Mock<Ijwt>();
        var tokenRepository = new Mock<ITokenRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var userService = new UserService(userRepositoryMock.Object, IWebHostEnvironmentMock.Object, tokenRepository.Object, jwt.Object);

        userRepositoryMock.Setup(ur => ur.userRegistred(userDto.email)).Returns(userModelTest);

        var result = userService.register(userDto, userDto.userImageFile);

        await Assert.ThrowsAsync<ValidationException>(() => result);
    }
    [Fact]
    public async void find_user_requester_test()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        UserModel userModelTest = new UserModel();
        var IWebHostEnvironmentmock = new Mock<IWebHostEnvironment>();
        var tokenRepositoryMock = new Mock<ITokenRepository>();
        var jwtmock = new Mock<Ijwt>();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        userRepositoryMock.Setup(ur => ur.findUser(userModelTest.id)).ReturnsAsync(userModelTest);

        var userService = new UserService(userRepositoryMock.Object, IWebHostEnvironmentmock.Object, tokenRepositoryMock.Object, jwtmock.Object);

        var result = await userService.findUser(userModelTest.id);

        Assert.Equal(result, userModelTest);
    }
    [Fact]
    public async void should_to_add_user_no_profile_picture()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        UserModel userModelTest = new UserModel();
        var IWebHostEnvironmentmock = new Mock<IWebHostEnvironment>();
        var tokenRepositoryMock = new Mock<ITokenRepository>();
        var jwtmock = new Mock<Ijwt>();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var userService = new UserService(userRepositoryMock.Object, IWebHostEnvironmentmock.Object, tokenRepositoryMock.Object, jwtmock.Object);
        userRepositoryMock
       .Setup(ur => ur.register(It.IsAny<UserModel>()))
       .ReturnsAsync(userModelTest);
        var token = "token";
        jwtmock.Setup(jwt => jwt.generateJwt(It.IsAny<UserModel>())).Returns(token);
        ResponseRegister rp = new ResponseRegister(200, "usuario cadastrado", token);
        var result = await userService.register(userDto, null);

        Assert.IsType<ResponseRegister>(result);
        Assert.Equal(result.Jwt, rp.Jwt);
        Assert.Equal(result.Status, rp.Status);
        Assert.Equal(result.Message, rp.Message);




    }
    [Fact]
    public void should_to_list_five_first_user_searched()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        UserModel userModelTest = new UserModel();
        var IWebHostEnvironmentmock = new Mock<IWebHostEnvironment>();
        var tokenRepositoryMock = new Mock<ITokenRepository>();
        var jwtmock = new Mock<Ijwt>();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var userModelTest2 = new SearchUserLinq();
        var listFiveUsers = new List<SearchUserLinq>{userModelTest2};

        userRepositoryMock.Setup(ur => ur.findFiveFirstUserSearched(userModelTest2.name, userModelTest.id)).Returns(listFiveUsers);
        var userService = new UserService(userRepositoryMock.Object, IWebHostEnvironmentmock.Object,tokenRepositoryMock.Object, jwtmock.Object);

        var result = userService.findFiveFirstUserSearched(userModelTest2.name,userModelTest.id);
        Assert.IsType<List<SearchUserLinq>>(result);
    }
    [Fact]
    public void should_to_list_next_users_searched()
    {
           var userRepositoryMock = new Mock<IUserRepository>();
        UserModel userModelTest = new UserModel();
        var IWebHostEnvironmentmock = new Mock<IWebHostEnvironment>();
        var tokenRepositoryMock = new Mock<ITokenRepository>();
        var jwtmock = new Mock<Ijwt>();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var userModelTest2 = new SearchUserLinq();

        userModelTest2.name = userModelTest.userName;
        userModelTest2.id = userModelTest.id;

        var listFiveUsers = new List<SearchUserLinq>{userModelTest2};

        userRepositoryMock.Setup(ur => ur.findUserSearchedScrolling(userModelTest.id,userModelTest.userName,userModelTest.id)).Returns(listFiveUsers);
        var userService = new UserService(userRepositoryMock.Object, IWebHostEnvironmentmock.Object,tokenRepositoryMock.Object, jwtmock.Object);

        var result = userService.findUserSearchedScrolling(userModelTest2.id,userModelTest2.name, userModelTest.id);
        Assert.IsType<List<SearchUserLinq>>(result);
    }

}