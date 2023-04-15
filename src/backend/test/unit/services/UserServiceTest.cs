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
        var UserRepositoryMock = new Mock<IUserRepository>();
            
        var UserService = new UserService(UserRepositoryMock.Object);

    
       

        UserRepositoryMock
        .Setup(ur => ur.Register(It.IsAny<UserModel>()))
        .ReturnsAsync(userModeltest);



        var result  = await UserService.register(userDto);

   
        

        
        Assert.IsType<ResponseRegister>(result);
    }
     [Fact]
    public void convert_userDto_to_userModel_test()
    {
        var UserRepositoryMock = new Mock<IUserRepository>();
        UserRepositoryMock.Setup(ur => ur.Register(userModeltest)).ReturnsAsync(userModeltest);

        var UserService = new UserService(UserRepositoryMock.Object);

        var result = UserService.convertUserDtoToUserModel(userDto);

        Assert.IsType<UserModel>(result);

    } 
}