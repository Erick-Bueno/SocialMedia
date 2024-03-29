using Moq;
using Xunit;
using User.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class UserControllerTest
{
    public UserRegisterDto userDto { get; set; }
    public UserControllerTest()
    {
        userDto = new UserRegisterDto();

        userDto.email = "erickjb93@gmail.com";
        userDto.password = "Sirlei231";
        userDto.userName = "erick";
        userDto.telephone = "77799591703";
        userDto.userPhoto = "llll";


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

        ResponseRegister responseTest = new ResponseRegister(200, "usuario cadastrado", "testejwt");

        userServiceMock.Setup(us => us.register(userDto, userDto.userImageFile)).ReturnsAsync(responseTest);

        var result = await userController.registerUser(userDto, userDto.userImageFile);


        Assert.IsType<OkObjectResult>(result.Result);
    }
    [Fact]
    async public void should_to_return_a_response_with_user_authentication_data()
    {
        var userServiceMock = new Mock<IUserService>();


        var friendsServiceMock = new Mock<IFriendsService>();


        var userController = new UserController(userServiceMock.Object, friendsServiceMock.Object);

        ResponseRegister responseRegisterTest = new ResponseRegister(200, "usuario cadastrado", "testejwt");

        userServiceMock.Setup(us => us.register(userDto, userDto.userImageFile)).ReturnsAsync(responseRegisterTest);

        var result = await userController.registerUser(userDto, userDto.userImageFile);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);

        Assert.Equal(okObjectResult.Value, responseRegisterTest);
    }
    [Fact]
    public async void should_to_return_a_response_with_user_data()
    {
        var userServiceMock = new Mock<IUserService>();
        var friendsServiceMock = new Mock<IFriendsService>();



        var UserControllerTest = new UserController(userServiceMock.Object, friendsServiceMock.Object);
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

        Assert.Equal(200, responseFindedUser.Status);
        Assert.Equal("Usuario encontrado", responseFindedUser.Message);
        Assert.Equal("erick", responseFindedUser.Name);
        Assert.Equal("llll", responseFindedUser.Profileimage);
        Assert.Equal(0, responseFindedUser.Friendsquantity);

    }
    [Fact]
    public void should_to_return_fisrt_users_searched_list()
    {
        var userServiceMock = new Mock<IUserService>();


        var friendsServiceMock = new Mock<IFriendsService>();


        var userController = new UserController(userServiceMock.Object, friendsServiceMock.Object);

        var userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.email = "erickjb93@gmail.com";
        userModeltest.password = "Sirlei231";
        userModeltest.userName = "erick";
        userModeltest.telephone = "77799591703";
        userModeltest.userPhoto = "llll";
        var userModelTest2 = new SearchUserLinq();
        var listFirstFiveUsers = new List<SearchUserLinq>{userModelTest2};
         
     

        userServiceMock.Setup(us => us.findFiveFirstUserSearched(userModelTest2.name, userModeltest.id)).Returns(listFirstFiveUsers);



        var result = userController.findFiveFirstUserSearched(userModelTest2.name,  userModeltest.id);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        
        Assert.IsType<List<SearchUserLinq>>(okObjectResult.Value);
    }
     [Fact]
    public void should_to_return_next_users_searched_list()
    {
        var userServiceMock = new Mock<IUserService>();


        var friendsServiceMock = new Mock<IFriendsService>();


        var userController = new UserController(userServiceMock.Object, friendsServiceMock.Object);

        var userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.email = "erickjb93@gmail.com";
        userModeltest.password = "Sirlei231";
        userModeltest.userName = "erick";
        userModeltest.telephone = "77799591703";
        userModeltest.userPhoto = "llll";

        
        var UserSearchDto = new ListNextUsersSearchedDto();
        UserSearchDto.name = userModeltest.userName;
        UserSearchDto.id = userModeltest.id;
        UserSearchDto.userId = userModeltest.id;

        var userModelTest2 = new SearchUserLinq();
        userModelTest2.name = userModeltest.userName;
        userModelTest2.id = userModeltest.id;
        var listFirstFiveUsers = new List<SearchUserLinq>{userModelTest2};
        userServiceMock.Setup(us => us.findUserSearchedScrolling(UserSearchDto.id,UserSearchDto.name, UserSearchDto.userId )).Returns(listFirstFiveUsers);



        var result = userController.findUserSearchedScrolling(UserSearchDto);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        
        Assert.IsType<List<SearchUserLinq>>(okObjectResult.Value);
    }
}