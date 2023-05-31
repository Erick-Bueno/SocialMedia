using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using auth.Controllers;

public class AuthControllerTest
{
    [Fact]
    async public void should_to_return_response_with_user_data_login()
    {
      var authServiceMock = new Mock<IAuthService>();
      UserLoginDto loginData = new UserLoginDto();
      loginData.email = "erickjb93@gmail.com";
      loginData.senha = "sirlei231";
      ResponseRegister response = new ResponseRegister(200, "Usuário logado com sucesso",Guid.NewGuid(),"jwtTeste");
      authServiceMock.Setup(au => au.login(loginData)).ReturnsAsync(response);

      var Auth = new Auth(authServiceMock.Object);

      var result = await Auth.login(loginData);

      
      Assert.IsType<OkObjectResult>(result.Result);

    }


    [Fact]
    async public void should_to_check_the_content_in_okobjectresult_returned_in_login()
    {
      var authServiceMock = new Mock<IAuthService>();
      UserLoginDto loginData = new UserLoginDto();
      loginData.email = "erickjb93@gmail.com";
      loginData.senha = "sirlei231";
      ResponseRegister response = new ResponseRegister(200, "Usuário logado com sucesso",Guid.NewGuid(),"jwtTeste");
      authServiceMock.Setup(au => au.login(loginData)).ReturnsAsync(response);

      var AuthController = new Auth(authServiceMock.Object);

      var result = await AuthController.login(loginData);

      var okObjectresult = Assert.IsType<ActionResult<UserModel>>(result);
      var content = Assert.IsType<OkObjectResult>(result.Result).Value;
       
      Assert.Equal(response,content);
    }

    [Fact]
    public async void should_to_return_response_with_new_jwt()
    {
      var authServiceMock = new Mock<IAuthService>();

      var authController = new Auth(authServiceMock.Object);

      TokenModel token = new TokenModel();
      token.email = "erickjb93@gmail.com";
      token.id = Guid.NewGuid();
      token.jwt = "jwtteste";
      jwtDto jwt = new jwtDto();
      jwt.jwt = "jwtteste";
      ResponseAuth responseAuth = new ResponseAuth(token.jwt);

      authServiceMock.Setup(sm => sm.refreshToken(token.jwt)).ReturnsAsync(responseAuth);

      var result = await authController.refreshToken(jwt);

      var okObejctResult = Assert.IsType<ActionResult<TokenModel>>(result);
      Assert.IsType<OkObjectResult>(result.Result);
    }
    [Fact]
    async public void should_to_check_the_content_in_okobjectresult_returned_in_refresh_token()
    {
      var authServiceMock = new Mock<IAuthService>();
      TokenModel token = new TokenModel();
      token.email = "erickjb93@gmail.com";
      token.id = Guid.NewGuid();
      token.jwt = "jwtteste";

      ResponseAuth responseAuth = new ResponseAuth(token.jwt);
      authServiceMock.Setup(au => au.refreshToken(token.jwt)).ReturnsAsync(responseAuth);

      var authController = new Auth(authServiceMock.Object);
      jwtDto jwt = new jwtDto();
      jwt.jwt = "jwtteste";
      var result = await authController.refreshToken(jwt);

      var okObjectresult = Assert.IsType<ActionResult<TokenModel>>(result);
      var content = Assert.IsType<OkObjectResult>(result.Result).Value;
      
      Assert.Equal(responseAuth,content);
    }
}