using Moq;
using Xunit;
using User.Controllers;
using Microsoft.AspNetCore.Mvc;

public class UserControllerTest
{
    public UserRegisterDto userDto { get; set; }
    public UserControllerTest(){
            userDto =  new UserRegisterDto();
            
           

          
            userDto.Email = "erickjb93@gmail.com";
            userDto.Password = "Sirlei231";
            userDto.UserName = "erick";
            userDto.Telephone ="77799591703";
            userDto.User_Photo =  "llll";
            
        
    }
    [Fact]
    async public void return_okobjectresult_test()
    {
        var UserServiceMock = new Mock<IUserService>();

        var userController = new User.Controllers.User(UserServiceMock.Object);

        ResponseRegister ResponseTest = new ResponseRegister(Guid.NewGuid(), 200, "usuario cadastrado");

        UserServiceMock.Setup(us => us.register(userDto)).ReturnsAsync(ResponseTest);

        var result = await userController.RegisterUser(userDto);
    

        Assert.IsType<OkObjectResult>(result.Result);
    }
     [Fact]
    async public void return_okobjectresultcontent_test()
    {
        var userServiceMock = new Mock<IUserService>();

        var UserControllerTest = new User.Controllers.User(userServiceMock.Object);

        ResponseRegister responseRegisterTest = new ResponseRegister(Guid.NewGuid(), 200, "usuario cadastrado");

        userServiceMock.Setup(us => us.register(userDto)).ReturnsAsync(responseRegisterTest);

        var result = await UserControllerTest.RegisterUser(userDto);

        var OkObjectResult = Assert.IsType<OkObjectResult>(result.Result);
      
        Assert.IsType<ResponseRegister>(OkObjectResult.Value);
    } 
}