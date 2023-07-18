using Moq;
using Xunit;
using FriendsController.Controllers;
using Microsoft.AspNetCore.Mvc;

public class FriendsControllerTest
{
    [Fact]
    public async void should_to_return_a_response_with_message ()
    {
      var friendsServiceMock = new Mock<IFriendsService>();
      var friendsController = new Friends(friendsServiceMock.Object);
      FriendsDto friendsDto = new FriendsDto();
      friendsDto.receiverId = Guid.NewGuid();
      friendsDto.requesterId = Guid.NewGuid();
      Response<FriendsModel> response = new Response<FriendsModel>(200, "Solicitação de amizade aceita");
      friendsServiceMock.Setup(fs => fs.addFriends(friendsDto)).ReturnsAsync(response);
      var result = await friendsController.addFriend(friendsDto);
      var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
      Assert.Equal(okObjectResult.Value,response );
    }
    [Fact]
    public void should_to_return_a_list_of_friends()
    {
      var friendsServiceMock = new Mock<IFriendsService>();
      var friendsController = new Friends(friendsServiceMock.Object);
      var id = Guid.NewGuid();
      var listFriends = new List<UserFriendsListLinq>();
      friendsServiceMock.Setup(fs => fs.listUserFriends(id)).Returns(listFriends);

      var result = friendsController.listFriends(id);
      var action = Assert.IsType<ActionResult<FriendsModel>>(result);
      Assert.IsType<OkObjectResult>(action.Result);
    }
}