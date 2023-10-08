using System.ComponentModel.DataAnnotations;
using Moq;
using Sprache;
using Xunit;

public class RequestServiceTest
{
    [Fact]
    public void convert_requestdto_to_requestmodel_test()
    {
        var requestRepositoryMock = new Mock<IRequestRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var requestService = new RequestService(requestRepositoryMock.Object, userRepositoryMock.Object);

        RequestDto requestDto = new RequestDto();

        requestDto.receiverId = Guid.NewGuid();
        requestDto.requesterId = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;


        RequestsModel requestsModel = new RequestsModel();
        requestsModel.receiverId = requestDto.receiverId;

        requestsModel.requesterId = requestDto.requesterId;
        requestsModel.status = requestDto.status;

        var result = requestService.converteToRequestModel(requestDto);

        Assert.Equal(result.requesterId, requestsModel.requesterId);
        Assert.Equal(result.receiverId, requestsModel.receiverId);
        Assert.Equal(result.status, requestsModel.status);

    }
    [Fact]
    async public void should_add_request()
    {
        var requestRepositorymock = new Mock<IRequestRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        RequestDto requestDto = new RequestDto();
        requestDto.receiverId = Guid.NewGuid();
        requestDto.requesterId = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;
        UserModel userModel = new UserModel();

        RequestsModel requestsModel = new RequestsModel();
        requestsModel.receiverId = requestDto.receiverId;
        requestsModel.requesterId = requestDto.requesterId;
        requestsModel.status = requestDto.status;

        userRepositoryMock.Setup(ur => ur.findUser(requestsModel.receiverId)).ReturnsAsync(userModel);
        userRepositoryMock.Setup(ur => ur.findUser(requestsModel.requesterId)).ReturnsAsync(userModel);
        requestRepositorymock.Setup(rp => rp.findRequest(requestDto.receiverId, requestDto.requesterId)).Returns((RequestsModel)null);
        requestRepositorymock.Setup(rp => rp.createRequest(requestsModel)).ReturnsAsync(requestsModel);
        var requestService = new RequestService(requestRepositorymock.Object, userRepositoryMock.Object);
        var result = await requestService.addRequest(requestDto);
        Assert.True(result);

    }
    [Fact]
    async public void Add_request_exception_request_exists_Test()
    {
        var requestRepositorymock = new Mock<IRequestRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        RequestDto requestDto = new RequestDto();
        requestDto.receiverId = Guid.NewGuid();
        requestDto.requesterId = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;

        RequestsModel requestsModel = new RequestsModel();
        requestsModel.receiverId = requestDto.receiverId;
        requestsModel.requesterId = requestDto.requesterId;
        requestsModel.status = requestDto.status;
        UserModel userModel = new UserModel();

        userRepositoryMock.Setup(ur => ur.findUser(requestsModel.receiverId)).ReturnsAsync(userModel);
        userRepositoryMock.Setup(ur => ur.findUser(requestsModel.requesterId)).ReturnsAsync(userModel);

        requestRepositorymock.Setup(rp => rp.findRequest(requestDto.receiverId, requestDto.requesterId)).Returns(requestsModel);

        requestRepositorymock.Setup(rp => rp.createRequest(requestsModel)).ReturnsAsync(requestsModel);
        var requestService = new RequestService(requestRepositorymock.Object, userRepositoryMock.Object);
        var result = await requestService.addRequest(requestDto);
        Assert.False(result);
    }

    [Fact]
    public void finde_request_return_test()
    {
        var requestRepositoryMock = new Mock<IRequestRepository>();

        var requestRepositorymock = new Mock<IRequestRepository>();
        RequestDto requestDto = new RequestDto();
        requestDto.receiverId = Guid.NewGuid();
        requestDto.requesterId = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;

        RequestsModel requestsModel = new RequestsModel();
        requestsModel.receiverId = requestDto.receiverId;
        requestsModel.requesterId = requestDto.requesterId;
        requestsModel.status = requestDto.status;

        requestRepositoryMock.Setup(rp => rp.findRequest(requestsModel.receiverId, requestsModel.requesterId)).Returns(requestsModel);

        var userRepositoryMock = new Mock<IUserRepository>();

        var requestService = new RequestService(requestRepositoryMock.Object, userRepositoryMock.Object);

        var result = requestService.findRequest(requestsModel.receiverId, requestsModel.requesterId);

        Assert.Equal(result, requestsModel);
    }
    [Fact]
    async public void Add_request_exception_requester_or_receiver_ids_is_null()
    {
        var requestRepositorymock = new Mock<IRequestRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        RequestDto requestDto = new RequestDto();
        requestDto.receiverId = Guid.NewGuid();
        requestDto.requesterId = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;

        RequestsModel requestsModel = new RequestsModel();
        requestsModel.receiverId = requestDto.receiverId;
        requestsModel.requesterId = requestDto.requesterId;
        requestsModel.status = requestDto.status;
        UserModel userModel = new UserModel();

        userRepositoryMock.Setup(ur => ur.findUser(requestsModel.receiverId)).ReturnsAsync((UserModel)null);
        userRepositoryMock.Setup(ur => ur.findUser(requestsModel.requesterId)).ReturnsAsync((UserModel)null);

        requestRepositorymock.Setup(rp => rp.findRequest(requestDto.receiverId, requestDto.requesterId)).Returns(requestsModel);


        var requestService = new RequestService(requestRepositorymock.Object, userRepositoryMock.Object);
        var result = await requestService.addRequest(requestDto);
        Assert.False(result);
    }
    [Fact]
    public void should_to_list_user_requests()
    {
        var requestRepositorymock = new Mock<IRequestRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        RequestDto requestDto = new RequestDto();
        requestDto.receiverId = Guid.NewGuid();
        requestDto.requesterId = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;

        RequestsModel requestsModel = new RequestsModel();
        requestsModel.receiverId = requestDto.receiverId;
        requestsModel.requesterId = requestDto.requesterId;
        requestsModel.status = requestDto.status;
        UserModel userModel = new UserModel();
        var listRequests = new List<RequestsListLinq>();
        requestRepositorymock.Setup(rp => rp.listRequest(requestDto.receiverId)).Returns(listRequests);


        var requestService = new RequestService(requestRepositorymock.Object, userRepositoryMock.Object);
        var result = requestService.listRequests(requestDto.receiverId);

    }
    [Fact]
    public void should_to_delete_request()
    {
        var requestRepositoryMock = new Mock<IRequestRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var requestDto = new RequestDto();

        requestDto.receiverId = Guid.NewGuid();
        requestDto.requesterId = Guid.NewGuid();


        requestRepositoryMock.Setup(rr => rr.deleteRequest(requestDto.receiverId,requestDto.requesterId)).Returns(true);

        var requestService = new RequestService(requestRepositoryMock.Object, userRepositoryMock.Object);

        var result = requestService.deleteRequest(requestDto);

        Assert.True(result);
    }

}