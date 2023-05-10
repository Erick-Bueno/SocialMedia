using Microsoft.AspNetCore.SignalR;
using Moq;
using Xunit;

public class HubsolicitationTest
{
    [Fact]
   public async void should_send_request_to_requester_when_addrequest_returns_false ()
    {
        
        var requestServiceMock = new Mock<IRequestService>();
        var userserviceMock = new Mock<IUserService>();
        //simulando cliente
        var clienteMock= new Mock<IClientProxy>();
        //simulando o envio de algo 
        var CallerMock= new Mock<IHubCallerClients>();
        
        RequestDto requestDto = new RequestDto();
        requestDto.Receiver_id = Guid.NewGuid();
        requestDto.Requester_id = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;

        var HubSolicitation = new HubSolicitation(requestServiceMock.Object, userserviceMock.Object);


       
        //simulando que um cliente esta conectado
        CallerMock.Setup(cm => cm.Caller).Returns(clienteMock.Object);

        //estou dizendo para o meu hub usar essa instancia de callermock para enviar algo para o cliente 
        HubSolicitation.Clients = CallerMock.Object;

        requestServiceMock.Setup(rq => rq.addRequest(It.IsAny<RequestDto>())).ReturnsAsync(false);
        await HubSolicitation.sendingrequest(requestDto.Receiver_id,requestDto.Requester_id, requestDto.status);

        
        CallerMock.Verify(cm => cm.Caller,Times.Once);

      
    }
    [Fact]
    public async void should_just_execute_addrequest()
    {
       var requestServiceMock = new Mock<IRequestService>();
       var userserviceMock = new Mock<IUserService>();
       var clienteMock = new Mock<IClientProxy>();
       var callerMock = new Mock<IHubCallerClients>();

        RequestDto requestDto = new RequestDto();
        requestDto.Receiver_id = Guid.NewGuid();
        requestDto.Requester_id = Guid.NewGuid();
        requestDto.status = StatusEnum.pending;

       requestServiceMock.Setup(rs => rs.addRequest(It.IsAny<RequestDto>())).ReturnsAsync(true);

       callerMock.Setup(cm => cm.Caller).Returns(clienteMock.Object);

       var Hubsolicitation = new HubSolicitation(requestServiceMock.Object, userserviceMock.Object);

    

       Hubsolicitation.Clients = callerMock.Object;

       await Hubsolicitation.sendingrequest(requestDto.Receiver_id, requestDto.Requester_id, requestDto.status);

       callerMock.Verify(cm => cm.Caller, Times.Never);

    }
    [Fact]
    public async void should_request_to_receiver_when_receiver_are_in_usersconnecteds()
    {
      var requestServiceMock = new Mock<IRequestService>();
      var userserviceMock = new Mock<IUserService>();
      var clienteMock = new Mock<IClientProxy>();
      var callerMock = new Mock<IHubCallerClients>();

      
      RequestDto requestDto = new RequestDto();
      var userid = Guid.NewGuid();
      var connectionId = Guid.NewGuid();
      requestDto.Receiver_id = Guid.NewGuid();
      requestDto.Requester_id = Guid.NewGuid();
      requestDto.status = StatusEnum.pending;

      UserModel userModeltest = new UserModel();
      userModeltest.id = Guid.NewGuid();
      userModeltest.Email = "erickjb93@gmail.com";
      userModeltest.Password = "Sirlei231";
      userModeltest.UserName = "erick";
      userModeltest.Telephone ="77799591703";
      userModeltest.User_Photo =  "llll";

      RequestsModel requestsModel = new RequestsModel();
      requestsModel.id = Guid.NewGuid();
      requestsModel.RequestDate = DateTime.UtcNow;
      

      HubSolicitation hubSolicitation = new HubSolicitation(requestServiceMock.Object,userserviceMock.Object);
      var usersConnecteds = hubSolicitation.getUsersConnecteds();
      usersConnecteds[userid.ToString()] = connectionId.ToString();
      
      requestServiceMock.Setup(rs => rs.addRequest(It.IsAny<RequestDto>())).ReturnsAsync(true);
      userserviceMock.Setup(us => us.FindUserRequester(requestDto.Requester_id)).ReturnsAsync(userModeltest);
      requestServiceMock.Setup(rs => rs.FindRequest(userid, requestDto.Requester_id)).Returns(requestsModel);

      callerMock.Setup(cm => cm.Client(connectionId.ToString())).Returns(clienteMock.Object);

      hubSolicitation.Clients = callerMock.Object;

      await hubSolicitation.sendingrequest(userid, requestDto.Requester_id,StatusEnum.pending);

      callerMock.Verify(cm => cm.Client(connectionId.ToString()), Times.Once);
      
    }
    [Fact]
    async public void should_just_execute_addrequest_when_receiver_are_not_in_userconnecteds()
    {
      var requestServiceMock = new Mock<IRequestService>();
      var userserviceMock = new Mock<IUserService>();
      var clienteMock = new Mock<IClientProxy>();
      var callerMock = new Mock<IHubCallerClients>();

      
      RequestDto requestDto = new RequestDto();
      var userid = Guid.NewGuid();
      var connectionId = Guid.NewGuid();
      requestDto.Receiver_id = Guid.NewGuid();
      requestDto.Requester_id = Guid.NewGuid();
      requestDto.status = StatusEnum.pending;

      UserModel userModeltest = new UserModel();
      userModeltest.id = Guid.NewGuid();
      userModeltest.Email = "erickjb93@gmail.com";
      userModeltest.Password = "Sirlei231";
      userModeltest.UserName = "erick";
      userModeltest.Telephone ="77799591703";
      userModeltest.User_Photo =  "llll";

      RequestsModel requestsModel = new RequestsModel();
      requestsModel.id = Guid.NewGuid();
      requestsModel.RequestDate = DateTime.UtcNow;
      

      HubSolicitation hubSolicitation = new HubSolicitation(requestServiceMock.Object,userserviceMock.Object);
    
      
      requestServiceMock.Setup(rs => rs.addRequest(It.IsAny<RequestDto>())).ReturnsAsync(true);
      userserviceMock.Setup(us => us.FindUserRequester(requestDto.Requester_id)).ReturnsAsync(userModeltest);
      requestServiceMock.Setup(rs => rs.FindRequest(userid, requestDto.Requester_id)).Returns(requestsModel);

      callerMock.Setup(cm => cm.Client(connectionId.ToString())).Returns(clienteMock.Object);

      hubSolicitation.Clients = callerMock.Object;

      await hubSolicitation.sendingrequest(userid, requestDto.Requester_id,StatusEnum.pending);

      callerMock.Verify(cm => cm.Client(connectionId.ToString()), Times.Never);
    }
}