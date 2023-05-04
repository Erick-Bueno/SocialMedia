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
      
        requestDto.Receiver_id = Guid.NewGuid();
        requestDto.Requester_id = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;


        RequestsModel requestsModel = new RequestsModel();
        requestsModel.Receiver_id = requestDto.Receiver_id;
       
        requestsModel.Requester_id = requestDto.Requester_id;
        requestsModel.status = requestDto.status;

        var result = requestService.ConverteToRequestModel(requestDto);

        Assert.Equal(result.Requester_id, requestsModel.Requester_id);
        Assert.Equal(result.Receiver_id, requestsModel.Receiver_id);
         Assert.Equal(result.status, requestsModel.status);

    }
    [Fact]
    async public void Add_request_return_test()
    {
       var requestRepositorymock = new Mock<IRequestRepository>();
       RequestDto requestDto = new RequestDto();
       requestDto.Receiver_id = Guid.NewGuid();
       requestDto.Requester_id = Guid.NewGuid();
       requestDto.status = StatusEnum.pending;
       
       RequestsModel requestsModel = new RequestsModel();
       requestsModel.Receiver_id =  requestDto.Receiver_id;
       requestsModel.Requester_id = requestDto.Requester_id;
       requestsModel.status = requestDto.status;
       requestRepositorymock.Setup(rp => rp.FindRequest(requestDto.Receiver_id, requestDto.Requester_id)).Returns((RequestsModel)null);
       requestRepositorymock.Setup(rp => rp.createRequest(requestsModel)).ReturnsAsync(requestsModel);
       var RequestService = new RequestService(requestRepositorymock.Object);
       var result = await RequestService.addRequest(requestDto);
       Assert.True(result);

    }
    [Fact]
    async public void Add_request_exception_request_exists_Test()
    {
       var requestRepositorymock = new Mock<IRequestRepository>();
       RequestDto requestDto = new RequestDto();
       requestDto.Receiver_id = Guid.NewGuid();
       requestDto.Requester_id = Guid.NewGuid();
       requestDto.status = StatusEnum.pending;
       
       RequestsModel requestsModel = new RequestsModel();
       requestsModel.Receiver_id =  requestDto.Receiver_id;
       requestsModel.Requester_id = requestDto.Requester_id;
       requestsModel.status = requestDto.status;
       requestRepositorymock.Setup(rp => rp.FindRequest(requestDto.Receiver_id, requestDto.Requester_id)).Returns(requestsModel);
       requestRepositorymock.Setup(rp => rp.createRequest(requestsModel)).ReturnsAsync(requestsModel);
       var RequestService = new RequestService(requestRepositorymock.Object);
       var result = await RequestService.addRequest(requestDto);
       Assert.False(result);
    }

    [Fact]
    public void finde_request_return_test()
    {
        var requestRepositoryMock = new Mock<IRequestRepository>();

       var requestRepositorymock = new Mock<IRequestRepository>();
       RequestDto requestDto = new RequestDto();
       requestDto.Receiver_id = Guid.NewGuid();
       requestDto.Requester_id = Guid.NewGuid();
       requestDto.status = StatusEnum.pending;
       
       RequestsModel requestsModel = new RequestsModel();
       requestsModel.Receiver_id =  requestDto.Receiver_id;
       requestsModel.Requester_id = requestDto.Requester_id;
       requestsModel.status = requestDto.status;

        requestRepositoryMock.Setup(rp => rp.FindRequest(requestsModel.Receiver_id, requestsModel.Requester_id)).Returns(requestsModel);


        var requestService = new RequestService(requestRepositoryMock.Object);

        var result = requestService.FindRequest(requestsModel.Receiver_id, requestsModel.Requester_id);

        Assert.Equal(result, requestsModel);
    }
}