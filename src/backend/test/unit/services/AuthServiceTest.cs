using System.ComponentModel.DataAnnotations;
using Moq;
using Xunit;


public class AuthServiceTest
{
    [Fact]
    async public void should_thrown_exception_invalid_email()
    {
        var bcryptMock = new Mock<IBcryptTest>();
        var TokenRepositoryMock = new Mock<ITokenRepository>();
        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        UserLoginDto loginData = new UserLoginDto();
        loginData.email = userModelTest.email;
        loginData.senha = userModelTest.password;

        var user = new List<UserModel> { };


        var authRepositoryMock = new Mock<IAuthRepository>();
        authRepositoryMock.Setup(ar => ar.searchingForEmail(loginData)).Returns((UserModel)null);

        var userRepositoryMock = new Mock<IUserRepository>();

        var jwt = new Mock<Jwt>();

        var authService = new AuthService(authRepositoryMock.Object, bcryptMock.Object, TokenRepositoryMock.Object, jwt.Object, userRepositoryMock.Object);

        await Assert.ThrowsAsync<ValidationException>(() => authService.login(loginData));

    }
    [Fact]
    async public void should_thrown_exception_invalid_password()
    {
        var authRepositoryMock = new Mock<IAuthRepository>();
        var bcryptMock = new Mock<IBcryptTest>();
        var tokenRepositoryMock = new Mock<ITokenRepository>();
        var jwt = new Mock<Jwt>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var authService = new AuthService(authRepositoryMock.Object, bcryptMock.Object, tokenRepositoryMock.Object, jwt.Object, userRepositoryMock.Object);
        var passwordCrypt = BCrypt.Net.BCrypt.HashPassword("Sirlei231");
        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = passwordCrypt;
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";


        UserLoginDto loginData = new UserLoginDto();
        loginData.email = userModelTest.email;
        loginData.senha = "senhainvalida";



        var user = new List<UserModel> { userModelTest }.AsQueryable();

        authRepositoryMock.Setup(ar => ar.searchingForEmail(loginData)).Returns(userModelTest);

        bcryptMock.Setup(bc => bc.verify(loginData.senha, userModelTest.password)).Returns(false);

        await Assert.ThrowsAsync<ValidationException>(() => authService.login(loginData));




    }
    [Fact]
    async public void should_to_effect_user_login_who_never_logged_in_before()
    {
        var authRepositoryMock = new Mock<IAuthRepository>();

        var bcrypt = new Mock<IBcryptTest>();

        var tokenRepositoryMock = new Mock<ITokenRepository>();
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");
        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = BCrypt.Net.BCrypt.HashPassword("senhainvalida");
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";


        UserLoginDto loginData = new UserLoginDto();
        loginData.email = userModelTest.email;
        loginData.senha = "senhainvalida";


        TokenModel token = new TokenModel();
        token.email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();
        token.jwt = "jwtteste";
        var jwt = new Mock<Jwt>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var authService = new AuthService(authRepositoryMock.Object, bcrypt.Object, tokenRepositoryMock.Object, jwt.Object, userRepositoryMock.Object);



        authRepositoryMock.Setup(ar => ar.searchingForEmail(loginData)).Returns(userModelTest);
        authRepositoryMock.Setup(ar => ar.loggedInBeffore(loginData.email)).Returns((TokenModel)null);
        tokenRepositoryMock.Setup(tk => tk.addUserToken(token)).ReturnsAsync(token);
        bcrypt.Setup(bc => bc.verify(loginData.senha, userModelTest.password)).Returns(true);

        var Result = await authService.login(loginData);

        ResponseRegister responseSucess = new ResponseRegister(200, "Úsuario logado com sucesso", userModelTest.id, "tokenteste");

        Assert.IsType<ResponseRegister>(Result);


    }
    [Fact]
    async public void should_to_effect_user_login_who_have_already_logged_in_before()
    {
        var authRepositoryMock = new Mock<IAuthRepository>();

        var bcrypt = new Mock<IBcryptTest>();

        var tokenRepositoryMock = new Mock<ITokenRepository>();

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = BCrypt.Net.BCrypt.HashPassword("senhainvalida");
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";


        UserLoginDto loginData = new UserLoginDto();
        loginData.email = userModelTest.email;
        loginData.senha = "senhainvalida";


        TokenModel token = new TokenModel();
        token.email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();
        token.jwt = "jwtteste";
        var newJwt = "jwtnovo";
        var jwt = new Mock<Jwt>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var AuthService = new AuthService(authRepositoryMock.Object, bcrypt.Object, tokenRepositoryMock.Object, jwt.Object, userRepositoryMock.Object);



        authRepositoryMock.Setup(ar => ar.searchingForEmail(loginData)).Returns(userModelTest);
        authRepositoryMock.Setup(ar => ar.loggedInBeffore(loginData.email)).Returns(token);
        tokenRepositoryMock.Setup(tk => tk.updateToken(token, newJwt)).ReturnsAsync(token);
        bcrypt.Setup(bc => bc.verify(loginData.senha, userModelTest.password)).Returns(true);

        var result = await AuthService.login(loginData);

        ResponseRegister responseSucess = new ResponseRegister(200, "Úsuario logado com sucesso", userModelTest.id, "tokenteste");

        Assert.IsType<ResponseRegister>(result);

    }

    [Fact]
    public async void should_throw_exception_invalid_jwt()
    {
        var authRepositoryMock = new Mock<IAuthRepository>();
        var bcryptMock = new Mock<IBcryptTest>();
        var tokenRepositoryMock = new Mock<ITokenRepository>();
        var jwtMock = new Mock<Ijwt>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var authService = new AuthService(authRepositoryMock.Object, bcryptMock.Object, tokenRepositoryMock.Object, jwtMock.Object, userRepositoryMock.Object);

        TokenModel token = new TokenModel();
        token.email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();
        token.jwt = "jwtteste";

        authRepositoryMock.Setup(ap => ap.findUserEmailWithToken(token.jwt.ToString())).Returns(Task.FromResult<TokenModel>(null));



        await Assert.ThrowsAsync<ValidationException>(() => authService.refreshToken(token.jwt.ToString()));
    }
    [Fact]
    public async void should_to_generate_new_token()
    {
        var authRepositoryMock = new Mock<IAuthRepository>();
        var bcryptMock = new Mock<IBcryptTest>();
        var tokenRepositoryMock = new Mock<ITokenRepository>();
        var jwtMock = new Mock<Ijwt>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var authService = new AuthService(authRepositoryMock.Object, bcryptMock.Object, tokenRepositoryMock.Object, jwtMock.Object, userRepositoryMock.Object);

        TokenModel token = new TokenModel();
        token.email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();
        token.jwt = "jwtteste";

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = BCrypt.Net.BCrypt.HashPassword("senhainvalida");
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        authRepositoryMock.Setup(ap => ap.findUserEmailWithToken(token.jwt.ToString())).ReturnsAsync(token);

        userRepositoryMock.Setup(ur => ur.userRegistred(token.email)).Returns(userModelTest);

        jwtMock.Setup(jm => jm.generateJwt(userModelTest)).Returns(token.jwt);

        tokenRepositoryMock.Setup(tr => tr.updateToken(token, token.jwt)).ReturnsAsync(token);

        ResponseAuth responseAuth = new ResponseAuth(token.jwt);

        var result = await authService.refreshToken(token.jwt);
        Assert.IsType<ResponseAuth>(result);
    }

}