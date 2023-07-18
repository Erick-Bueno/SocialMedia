using Moq;
using Xunit;
using User.Controllers;
using Microsoft.AspNetCore.Mvc;

public class UserControllerTest
{
    public UserRegisterDto userDto { get; set; }
    public UserControllerTest(){
        userDto =  new UserRegisterDto();
            
        userDto.email = "erickjb93@gmail.com";
        userDto.password = "Sirlei231";
        userDto.userName = "erick";
        userDto.telephone ="77799591703";
        userDto.userPhoto =  "llll";
            
        
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
        userDto.userImageFile = filephotomock.Object;
        var userServiceMock = new Mock<IUserService>();
        var friendsServiceMock = new Mock<IFriendsService>();
        

        var userController = new UserController(userServiceMock.Object, friendsServiceMock.Object);

        ResponseRegister responseTest = new ResponseRegister(200,"usuario cadastrado", Guid.NewGuid(), "testejwt");

        userServiceMock.Setup(us => us.register(userDto,userDto.userImageFile)).ReturnsAsync(responseTest);

        var result = await userController.registerUser(userDto,userDto.userImageFile);
    

        Assert.IsType<OkObjectResult>(result.Result);
    }
     [Fact]
    async public void should_to_return_a_response_with_user_authentication_data()
    {
        var userServiceMock = new Mock<IUserService>();

        
         var friendsServiceMock = new Mock<IFriendsService>();
        

        var userController = new UserController(userServiceMock.Object, friendsServiceMock.Object);

        ResponseRegister responseRegisterTest = new ResponseRegister(200,"usuario cadastrado", Guid.NewGuid(),"testejwt");

        userServiceMock.Setup(us => us.register(userDto,userDto.userImageFile)).ReturnsAsync(responseRegisterTest);

        var result = await userController.registerUser(userDto,userDto.userImageFile);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
      
        Assert.Equal(okObjectResult.Value, responseRegisterTest);
    } 
    [Fact]
    public async void should_to_return_a_response_with_user_data ()
    {
        var userServiceMock = new Mock<IUserService>();
        var friendsServiceMock = new Mock<IFriendsService>();
        

     
        var UserControllerTest = new UserController(userServiceMock.Object,friendsServiceMock.Object);
        var userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.email = "erickjb93@gmail.com";
        userModeltest.password = "Sirlei231";
        userModeltest.userName = "erick";
        userModeltest.telephone = "77799591703";
        userModeltest.userPhoto = "llll";

        userServiceMock.Setup(us => us.findUser(userModeltest.id)).ReturnsAsync(userModeltest);

        var result = await UserControllerTest.findUser(userModeltest.id);

        var content = Assert.IsType<OkObjectResult>(result.Result).Value;

         var responseFindedUser = new ResponseUserFinded(200, "Usuario encontrado", userModeltest.userName, userModeltest.userPhoto, 0);
        
        Assert.Equal(200 ,responseFindedUser.Status);
        Assert.Equal("Usuario encontrado" ,responseFindedUser.Message);
        Assert.Equal("erick" ,responseFindedUser.Name);
        Assert.Equal("llll" ,responseFindedUser.Profileimage);
        Assert.Equal(0 ,responseFindedUser.Friendsquantity);
       
    }
}