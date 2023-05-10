using System.ComponentModel.DataAnnotations;
using Moq;
using Xunit;


public class AuthServiceTest
{
    [Fact]
    async public void should_thrown_exception_invalid_email()
    {
            var BCryptMock = new Mock<IBcryptTest>();   
            var TokenRepositoryMock = new Mock<ITokenRepository>();
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

        var jwt = new Mock<Jwt>();

        var AuthService = new AuthService(AuthRepositoryMock.Object, BCryptMock.Object, TokenRepositoryMock.Object, jwt.Object);

        await Assert.ThrowsAsync<ValidationException>(() =>  AuthService.login(loginData));
        
    }
    [Fact]
    async public void should_thrown_exception_invalid_password()
    {
        var AuthRepositoryMock = new Mock<IAuthRepository>();
        var BCryptMock = new Mock<IBcryptTest>();       
        var TokenRepositoryMock = new Mock<ITokenRepository>();
         var jwt = new Mock<Jwt>();
        var AuthService = new AuthService(AuthRepositoryMock.Object, BCryptMock.Object, TokenRepositoryMock.Object, jwt.Object);
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

        await Assert.ThrowsAsync<ValidationException>(()=> AuthService.login(loginData));




    }
    [Fact]
    async public void should_to_effect_user_login_who_never_logged_in_before()
    {
     var AuthRepositoryMock = new Mock<IAuthRepository>();

     var bCrypt = new Mock<IBcryptTest>();

     var TokenRepositoryMock = new Mock<ITokenRepository>();
     
      UserModel userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.Email = "erickjb93@gmail.com";
        userModeltest.Password =  BCrypt.Net.BCrypt.HashPassword("senhainvalida");
        userModeltest.UserName = "erick";
        userModeltest.Telephone ="77799591703";
        userModeltest.User_Photo =  "llll";

        
        UserLoginDto loginData = new UserLoginDto();
        loginData.Email = userModeltest.Email;
        loginData.Senha = "senhainvalida";
       

        TokenModel token = new TokenModel();
        token.Email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();
        token.jwt = "jwtteste";
       var jwt = new Mock<Jwt>();
        var AuthService = new AuthService(AuthRepositoryMock.Object,bCrypt.Object, TokenRepositoryMock.Object, jwt.Object);

        

     AuthRepositoryMock.Setup(ar => ar.SearchingForEmail(loginData)).Returns(userModeltest);
     AuthRepositoryMock.Setup(ar => ar.LoggedInBeffore(loginData.Email)).Returns((TokenModel)null);
     TokenRepositoryMock.Setup(tk => tk.addUserToken(token)).ReturnsAsync(token);
     bCrypt.Setup(bc => bc.verify(loginData.Senha, userModeltest.Password)).Returns(true);

     var Result =await AuthService.login(loginData);

     ResponseRegister responseSucess = new ResponseRegister(200, "Úsuario logado com sucesso", userModeltest.id,"tokenteste");

     Assert.IsType<ResponseRegister>(Result);


    }
    [Fact]
    async public void should_to_effect_user_login_who_have_already_logged_in_before()
    {
        var AuthRepositoryMock = new Mock<IAuthRepository>();

     var bCrypt = new Mock<IBcryptTest>();

     var TokenRepositoryMock = new Mock<ITokenRepository>();
     
      UserModel userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.Email = "erickjb93@gmail.com";
        userModeltest.Password =  BCrypt.Net.BCrypt.HashPassword("senhainvalida");
        userModeltest.UserName = "erick";
        userModeltest.Telephone ="77799591703";
        userModeltest.User_Photo =  "llll";

        
        UserLoginDto loginData = new UserLoginDto();
        loginData.Email = userModeltest.Email;
        loginData.Senha = "senhainvalida";
       

        TokenModel token = new TokenModel();
        token.Email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();
        token.jwt = "jwtteste";
        var newJwt = "jwtnovo";
       var jwt = new Mock<Jwt>();
        var AuthService = new AuthService(AuthRepositoryMock.Object,bCrypt.Object, TokenRepositoryMock.Object, jwt.Object);

        

     AuthRepositoryMock.Setup(ar => ar.SearchingForEmail(loginData)).Returns(userModeltest);
     AuthRepositoryMock.Setup(ar => ar.LoggedInBeffore(loginData.Email)).Returns(token);
     TokenRepositoryMock.Setup(tk => tk.UpdateToken(token, newJwt)).ReturnsAsync(token);
     bCrypt.Setup(bc => bc.verify(loginData.Senha, userModeltest.Password)).Returns(true);

     var Result = await AuthService.login(loginData);

     ResponseRegister responseSucess = new ResponseRegister(200, "Úsuario logado com sucesso", userModeltest.id,"tokenteste");

     Assert.IsType<ResponseRegister>(Result);
    
    }
    
}