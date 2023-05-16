using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using auth.Controllers;

public class AuthControllerTest
{
    [Fact]
    async public void should_to_return_response_with_user_data_login()
    {
       var AuthServiceMock = new Mock<IAuthService>();
        UserLoginDto loginData = new UserLoginDto();
        loginData.Email = "erickjb93@gmail.com";
        loginData.Senha = "sirlei231";
        ResponseRegister response = new ResponseRegister(200, "Usuário logado com sucesso",Guid.NewGuid(),"jwtTeste");
       AuthServiceMock.Setup(au => au.login(loginData)).ReturnsAsync(response);

       var Auth = new Auth(AuthServiceMock.Object);

       var Result = await Auth.login(loginData);

       var okokbejctResult = Assert.IsType<ActionResult<UserModel>>(Result);
       Assert.IsType<OkObjectResult>(Result.Result);

    }


    [Fact]
    async public void should_to_check_the_content_in_okobjectresult_returned_in_login()
    {
       var AuthServiceMock = new Mock<IAuthService>();
        UserLoginDto loginData = new UserLoginDto();
        loginData.Email = "erickjb93@gmail.com";
        loginData.Senha = "sirlei231";
        ResponseRegister response = new ResponseRegister(200, "Usuário logado com sucesso",Guid.NewGuid(),"jwtTeste");
       AuthServiceMock.Setup(au => au.login(loginData)).ReturnsAsync(response);

       var AuthController = new Auth(AuthServiceMock.Object);

       var Result = await AuthController.login(loginData);

       var Okobjectresult = Assert.IsType<ActionResult<UserModel>>(Result);
       var content = Assert.IsType<OkObjectResult>(Result.Result).Value;
       
       Assert.Equal(response,content);
    }

    [Fact]
    public async void should_to_return_response_with_new_jwt()
    {
      var AuthServiceMock = new Mock<IAuthService>();

      var AuthController = new Auth(AuthServiceMock.Object);

      TokenModel token = new TokenModel();
      token.Email = "erickjb93@gmail.com";
      token.id = Guid.NewGuid();
      token.jwt = "jwtteste";

      ResponseAuth responseAuth = new ResponseAuth(token.jwt);

      AuthServiceMock.Setup(sm => sm.RefreshToken(token.jwt)).ReturnsAsync(responseAuth);

      var result = await AuthController.refreshToken(token.jwt);

      var okbejctResult = Assert.IsType<ActionResult<TokenModel>>(result);
      Assert.IsType<OkObjectResult>(result.Result);
    }
    [Fact]
    async public void should_to_check_the_content_in_okobjectresult_returned_in_refresh_token()
    {
      var AuthServiceMock = new Mock<IAuthService>();
       TokenModel token = new TokenModel();
       token.Email = "erickjb93@gmail.com";
       token.id = Guid.NewGuid();
       token.jwt = "jwtteste";

       ResponseAuth responseAuth = new ResponseAuth(token.jwt);
       AuthServiceMock.Setup(au => au.RefreshToken(token.jwt)).ReturnsAsync(responseAuth);

       var AuthController = new Auth(AuthServiceMock.Object);

       var Result = await AuthController.refreshToken(token.jwt);

       var Okobjectresult = Assert.IsType<ActionResult<TokenModel>>(Result);
       var content = Assert.IsType<OkObjectResult>(Result.Result).Value;
       
       Assert.Equal(responseAuth,content);
    }
}