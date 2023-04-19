using System.Text;
using Moq;
using Xunit;

public class UserServiceTest
{
    public UserRegisterDto userDto { get; set; }
    public UserModel userModeltest {get; set;}
    public UserServiceTest(){

            userDto =  new UserRegisterDto();
            userModeltest = new UserModel();
           

          
            userDto.Email = "erickjb93@gmail.com";
            userDto.Password = "Sirlei231";
            userDto.UserName = "erick";
            userDto.Telephone ="77799591703";
            userDto.User_Photo =  "llll";
            userDto.userimagefile = null;

            var encrypted_password  = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            userModeltest.id = Guid.NewGuid();
            userModeltest.Email = userDto.Email;
            userModeltest.Password = encrypted_password;
            userModeltest.UserName = userDto.UserName;
            userModeltest.Telephone = userDto.Telephone;
            userModeltest.User_Photo =  userDto.User_Photo;
    }
    [Fact]
    async public void register_user_service_test()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var UserRepositoryMock = new Mock<IUserRepository>();
            
        var UserService = new UserService(UserRepositoryMock.Object, IWebHostEnvironmentMock.Object);

        //simulated formfile
        var filephotomock = new Mock<IFormFile>();
        var memoryStream = new MemoryStream();
        var writeFile = new StreamWriter(memoryStream);
        writeFile.Write("arquivo de teste");
        writeFile.Flush();
        memoryStream.Position = 0;
        var filePhoto = "imagem.jpg";
        userDto.userimagefile = filephotomock.Object;
        filephotomock.Setup(f => f.FileName).Returns(filePhoto);
        filephotomock.Setup(f => f.ContentType).Returns("image/jpeg");
      
        

        UserRepositoryMock
        .Setup(ur => ur.Register(It.IsAny<UserModel>()))
        .ReturnsAsync(userModeltest);



        var result  = await UserService.register(userDto, userDto.userimagefile);

   
        

        
        Assert.IsType<ResponseRegister>(result);
    }
     [Fact]
    public void convert_userDto_to_userModel_test()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var filephotomock = new Mock<IFormFile>();
        var memoryStream = new MemoryStream();
        var writeFile = new StreamWriter(memoryStream);
        writeFile.Write("arquivo de teste");
        writeFile.Flush();
        memoryStream.Position = 0;
        var filePhoto = "imagem.jpg";
        userDto.userimagefile = filephotomock.Object;
        filephotomock.Setup(f => f.FileName).Returns(filePhoto);
        filephotomock.Setup(f => f.ContentType).Returns("image/jpeg");

        var urlphoto = Guid.NewGuid() + userDto.userimagefile.FileName;
      
        var UserRepositoryMock = new Mock<IUserRepository>();
        UserRepositoryMock.Setup(ur => ur.Register(userModeltest)).ReturnsAsync(userModeltest);

        var UserService = new UserService(UserRepositoryMock.Object, IWebHostEnvironmentMock.Object);

        var result = UserService.convertUserDtoToUserModel(userDto, urlphoto);

        Assert.IsType<UserModel>(result);

    } 
    [Fact]
    public void image_profile_user_save_test()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        IWebHostEnvironmentMock.Setup(wh => wh.WebRootPath).Returns("C:\\Users\\erick\\Documents\\GitHub\\SocialMedia\\src\\backend\\wwwroot");

        var nomeimg = "teste.png";
        var IFormFileMock = new Mock<IFormFile>();
        var StreamMock = new MemoryStream();
        IFormFileMock.Setup(ff => ff.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None)).Returns(Task.FromResult(0));
     

   
        var userRepositoryMock = new Mock<IUserRepository>();
        var userService = new UserService(userRepositoryMock.Object,IWebHostEnvironmentMock.Object);

        userService.SaveUserPhoto(nomeimg, IFormFileMock.Object);
        
        Assert.True(Directory.Exists("C:\\Users\\erick\\Documents\\GitHub\\SocialMedia\\src\\backend\\wwwroot"));
        
        
    }
     [Fact]
   async public void Verify_fileContentType_is_jpg_or_png_Exception_test()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var filephotomock = new Mock<IFormFile>();
        var memoryStream = new MemoryStream();
        var writeFile = new StreamWriter(memoryStream);
        writeFile.Write("arquivo de teste");
        writeFile.Flush();
        memoryStream.Position = 0;
        var filePhoto = "imagem.jpg";
        userDto.userimagefile = filephotomock.Object;
        filephotomock.Setup(f => f.FileName).Returns(filePhoto);
        filephotomock.Setup(f => f.ContentType).Returns("image/csv");  

        var UserMockRepository = new Mock<IUserRepository>();

        UserMockRepository.Setup(um => um.Register(userModeltest)).ReturnsAsync(userModeltest);

        var UserService = new UserService(UserMockRepository.Object, IWebHostEnvironmentMock.Object);

        var result = UserService.register(userDto, userDto.userimagefile);

        await Assert.ThrowsAsync<Exception>(() => result);

    } 
}