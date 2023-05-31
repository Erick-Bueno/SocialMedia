using Moq;
using Xunit;

public class RequestServiceTest
{
    [Fact]
    public void convert_requestdto_to_requestmodel_test()
    {
        var requestRepositoryMock = new Mock<IRequestRepository>();

        var requestService = new RequestService(requestRepositoryMock.Object);

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
    async public void Add_request_return_test()
    {
        var requestRepositorymock = new Mock<IRequestRepository>();
        RequestDto requestDto = new RequestDto();
        requestDto.receiverId = Guid.NewGuid();
        requestDto.requesterId = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;

        RequestsModel requestsModel = new RequestsModel();
        requestsModel.receiverId = requestDto.receiverId;
        requestsModel.requesterId = requestDto.requesterId;
        requestsModel.status = requestDto.status;
        requestRepositorymock.Setup(rp => rp.findRequest(requestDto.receiverId, requestDto.requesterId)).Returns((RequestsModel)null);
        requestRepositorymock.Setup(rp => rp.createRequest(requestsModel)).ReturnsAsync(requestsModel);
        var requestService = new RequestService(requestRepositorymock.Object);
        var result = await requestService.addRequest(requestDto);
        Assert.True(result);

    }
    [Fact]
    async public void Add_request_exception_request_exists_Test()
    {
        var requestRepositorymock = new Mock<IRequestRepository>();
        RequestDto requestDto = new RequestDto();
        requestDto.receiverId = Guid.NewGuid();
        requestDto.requesterId = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;

        RequestsModel requestsModel = new RequestsModel();
        requestsModel.receiverId = requestDto.receiverId;
        requestsModel.requesterId = requestDto.requesterId;
        requestsModel.status = requestDto.status;
        requestRepositorymock.Setup(rp => rp.findRequest(requestDto.receiverId, requestDto.requesterId)).Returns(requestsModel);
        requestRepositorymock.Setup(rp => rp.createRequest(requestsModel)).ReturnsAsync(requestsModel);
        var requestService = new RequestService(requestRepositorymock.Object);
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


        var requestService = new RequestService(requestRepositoryMock.Object);

        var result = requestService.findRequest(requestsModel.receiverId, requestsModel.requesterId);

        Assert.Equal(result, requestsModel);
    }
}