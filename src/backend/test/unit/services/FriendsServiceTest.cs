using Moq;
using Xunit;

public class FriendsServiceTest
{
    [Fact]
    public async void should_to_find_the_quantity_of_users_friends()
    {
        var friedsRepository = new Mock<IFriendsRepository>();
        var requestsRepository = new Mock<IRequestRepository>();
        var friendsService = new FriendsService(friedsRepository.Object, requestsRepository.Object);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";


        friedsRepository.Setup(ur => ur.findFriends(userModelTest.id)).Returns(1);

        var result = friendsService.findFriends(userModelTest.id);

        Assert.Equal(result, 1);
    }
    [Fact]
    public void should_convert_friendsDto_to_friendsModel()
    {
        var friedsRepository = new Mock<IFriendsRepository>();
        var requestsRepository = new Mock<IRequestRepository>();
        var friendsService = new FriendsService(friedsRepository.Object, requestsRepository.Object);


        FriendsDto friendsDto = new FriendsDto();

        var result = friendsService.convertFriendsDtoToFriendsModel(friendsDto);

        Assert.IsType<FriendsModel>(result);
    }
    [Fact]
    public async void should_to_add_friend()
    {
        var friedsRepository = new Mock<IFriendsRepository>();
        var requestsRepository = new Mock<IRequestRepository>();
        var friendsService = new FriendsService(friedsRepository.Object, requestsRepository.Object);
        var friendsModel = new FriendsModel();

        var friendsDto = new FriendsDto();
        var requestModel = new RequestsModel();
        friedsRepository.Setup(fr => fr.addFriends(It.IsAny<FriendsModel>())).ReturnsAsync(friendsModel);
        requestsRepository.Setup(rr => rr.findRequest(friendsDto.receiverId, friendsDto.requesterId)).Returns(requestModel);
        Response<FriendsModel> response = new Response<FriendsModel>(200, "Solicitação de amizade aceita");
        var result = await friendsService.addFriends(friendsDto);

        Assert.Equal(result.Message, response.Message);
        Assert.Equal(result.Status, response.Status);
    }
    [Fact]
    public async void should_to_error_response_if_are_friends()
    {
        var friedsRepository = new Mock<IFriendsRepository>();
        var requestsRepository = new Mock<IRequestRepository>();
        var friendsService = new FriendsService(friedsRepository.Object, requestsRepository.Object);
        var friendsModel = new FriendsModel();

        var friendsDto = new FriendsDto();
        var requestModel = new RequestsModel();
        friedsRepository.Setup(fr => fr.checkIfAreFriends(friendsDto.receiverId, friendsDto.requesterId)).Returns(friendsModel);
        friedsRepository.Setup(fr => fr.addFriends(It.IsAny<FriendsModel>())).ReturnsAsync(friendsModel);
        requestsRepository.Setup(rr => rr.findRequest(friendsDto.receiverId, friendsDto.requesterId)).Returns(requestModel);
        Response<FriendsModel> responseError = new Response<FriendsModel>(400, "usuarios ja sao amigos");
        var result = await friendsService.addFriends(friendsDto);

        Assert.Equal(result.Message, responseError.Message);
        Assert.Equal(result.Status, responseError.Status);
    }
    [Fact]
    public async void should_to_error_response_if_request_exists()
    {
        var friedsRepository = new Mock<IFriendsRepository>();
        var requestsRepository = new Mock<IRequestRepository>();
        var friendsService = new FriendsService(friedsRepository.Object, requestsRepository.Object);
        var friendsModel = new FriendsModel();

        var friendsDto = new FriendsDto();
        var requestModel = new RequestsModel();
        friedsRepository.Setup(fr => fr.addFriends(It.IsAny<FriendsModel>())).ReturnsAsync(friendsModel);
        requestsRepository.Setup(rr => rr.findRequest(friendsDto.receiverId, friendsDto.requesterId)).Returns((RequestsModel)null);
        Response<FriendsModel> responseError = new Response<FriendsModel>(400, "Erro ao aceitar solicitação");
        var result = await friendsService.addFriends(friendsDto);

        Assert.Equal(responseError.Message, result.Message);
        Assert.Equal(responseError.Status, result.Status);
    }
    [Fact]
    public void should_to_list_friends()
    {
        var friedsRepository = new Mock<IFriendsRepository>();
        var requestsRepository = new Mock<IRequestRepository>();
        var friendsService = new FriendsService(friedsRepository.Object, requestsRepository.Object);
        var friendsModel = new FriendsModel();
        Guid id = Guid.NewGuid();
        var listUserFriends = new List<UserFriendsListLinq>();
        friedsRepository.Setup(fr => fr.listUserFriends(id)).Returns(listUserFriends);

        var result = friendsService.listUserFriends(id);

        Assert.IsType<List<UserFriendsListLinq>>(result);
    }
}