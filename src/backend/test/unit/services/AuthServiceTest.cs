using System.ComponentModel.DataAnnotations;
using Moq;
using Xunit;

public class AuthServiceTest
{
    [Fact]
    public void User_not_registred_Exception_Test()
    {
            var BCryptMock = new Mock<IBcryptTest>();   
          
            UserModel userModeltest = new UserModel();
            userModeltest.id = Guid.NewGuid();
            userModeltest.Email = "erickjb93@gmail.com";
            userModeltest.Password = "Sirlei231";
            userModeltest.UserName = "erick";
            userModeltest.Telephone ="77799591703";
            userModeltest.User_Photo =  "llll";

            UserLoginDto loginData = new UserLoginDto();
            loginData.Email = userModeltest.Email;
            loginData.Senha = userModeltest.Password;

            var user = new List<UserModel>{};


        var AuthRepositoryMock = new Mock<IAuthRepository>();
        AuthRepositoryMock.Setup(ar => ar.SearchingForEmail(loginData)).Returns((UserModel)null);

        var AuthService = new AuthService(AuthRepositoryMock.Object, BCryptMock.Object);

        Assert.Throws<ValidationException>(() =>  AuthService.login(loginData));
        
    }
    [Fact]
    public void User_password_invalid_exception_Test()
    {
        var AuthRepositoryMock = new Mock<IAuthRepository>();
        var BCryptMock = new Mock<IBcryptTest>();       
        
        var AuthService = new AuthService(AuthRepositoryMock.Object, BCryptMock.Object);
        var PasswordCrypt = BCrypt.Net.BCrypt.HashPassword("Sirlei231");
        UserModel userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.Email = "erickjb93@gmail.com";
        userModeltest.Password = PasswordCrypt;
        userModeltest.UserName = "erick";
        userModeltest.Telephone ="77799591703";
        userModeltest.User_Photo =  "llll";

        
        UserLoginDto loginData = new UserLoginDto();
        loginData.Email = userModeltest.Email;
        loginData.Senha = "senhainvalida";

        

        var user = new List<UserModel>{userModeltest}.AsQueryable();

        AuthRepositoryMock.Setup(ar => ar.SearchingForEmail(loginData)).Returns(userModeltest);

       BCryptMock.Setup(bc => bc.verify(loginData.Senha,userModeltest.Password)).Returns(false);

        Assert.Throws<ValidationException>(()=> AuthService.login(loginData));




    }
}