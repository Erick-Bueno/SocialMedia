using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using auth.Controllers;

public class AuthControllerTest
{
    [Fact]
    async public void should_to_effect_user_login()
    {
       var AuthServiceMock = new Mock<IAuthService>();
        UserLoginDto loginData = new UserLoginDto();
        loginData.Email = "erickjb93@gmail.com";
        loginData.Senha = "sirlei231";
        ResponseRegister response = new ResponseRegister(200, "Usuário logado com sucesso",Guid.NewGuid(),"jwtTeste");
       AuthServiceMock.Setup(au => au.login(loginData)).ReturnsAsync(response);

       var Auth = new Auth(AuthServiceMock.Object);

       var Result = await Auth.login(loginData);

       Assert.IsType<OkObjectResult>(Result);

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

       var Auth = new Auth(AuthServiceMock.Object);

       var Result = await Auth.login(loginData);

       var Okobjectresult = Assert.IsType<OkObjectResult>(Result);
       var content = Assert.IsType<ResponseRegister>(Okobjectresult.Value);
       
       Assert.Equal(response,content);
    }
}