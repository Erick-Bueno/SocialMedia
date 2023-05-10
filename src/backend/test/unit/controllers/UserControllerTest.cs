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
    async public void should_to_effect_user_register()
    {
        var filephotomock = new Mock<IFormFile>();
        var memoryStream = new MemoryStream();
        var writeFile = new StreamWriter(memoryStream);
        writeFile.Write("arquivo de teste");
        writeFile.Flush();
        memoryStream.Position = 0;
        var filePhoto = "imagem.jpg";
        userDto.userimagefile = filephotomock.Object;
        var UserServiceMock = new Mock<IUserService>();

        

        var userController = new User.Controllers.User(UserServiceMock.Object);

         ResponseRegister ResponseTest = new ResponseRegister(200,"usuario cadastrado", Guid.NewGuid(), "testejwt");

        UserServiceMock.Setup(us => us.register(userDto,userDto.userimagefile)).ReturnsAsync(ResponseTest);

        var result = await userController.RegisterUser(userDto,userDto.userimagefile);
    

        Assert.IsType<OkObjectResult>(result.Result);
    }
     [Fact]
    async public void should_to_check_the_content_in_okobjectresult_returned_in_register()
    {
        var userServiceMock = new Mock<IUserService>();

        
        var UserControllerTest = new User.Controllers.User(userServiceMock.Object);

        ResponseRegister responseRegisterTest = new ResponseRegister(200,"usuario cadastrado", Guid.NewGuid(),"testejwt");

        userServiceMock.Setup(us => us.register(userDto,userDto.userimagefile)).ReturnsAsync(responseRegisterTest);

        var result = await UserControllerTest.RegisterUser(userDto,userDto.userimagefile);

        var OkObjectResult = Assert.IsType<OkObjectResult>(result.Result);
      
        Assert.IsType<ResponseRegister>(OkObjectResult.Value);
    } 
}