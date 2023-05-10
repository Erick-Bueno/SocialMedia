using Microsoft.AspNetCore.SignalR;

public class HubSolicitation:Hub
{
    private readonly IRequestService requestService;
    private readonly IUserService userService;
    private Dictionary<string, string> UsersConnecteds { get; set; } = new Dictionary<string, string>();
   

    public HubSolicitation(IRequestService requestService, IUserService userService)
    {
        this.requestService = requestService;
        this.userService = userService;
    }
    public Dictionary<string, string> getUsersConnecteds(){
        return UsersConnecteds;
    }
    public async Task sendingrequest(Guid receiver_id, Guid requester_id, StatusEnum status){
        RequestDto requestDto = new RequestDto();
        requestDto.Receiver_id = receiver_id;
        requestDto.Requester_id = requester_id;
        requestDto.status =  status;
        var addrequest = await requestService.addRequest(requestDto);
        if(addrequest == false){
            await Clients.Caller.SendAsync("SolicitationError", "Erro ao enviar solicitação");
            return;
        }
        if(UsersConnecteds.ContainsKey((receiver_id.ToString()))){
            var connectionId = UsersConnecteds[receiver_id.ToString()];
            var userData = await userService.FindUserRequester(requester_id);
            var RequestData = requestService.FindRequest(receiver_id, requester_id);
            await Clients.Client(connectionId).SendAsync("receivingrequest",requester_id,status, userData.UserName, userData.User_Photo, RequestData.RequestDate );
        }
    }
    public override async Task OnConnectedAsync()
    {
        //quando o usuario se conectar ao hub("websocket") pegaremos o seu id atraves da url e pegaremos o seu id de conexão e adicionamos em um dicionario
        var Userid = Context.GetHttpContext().Request.Query["userId"];
        var connectionId = Context.ConnectionId;
        UsersConnecteds[Userid] = connectionId;

        await base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var Userid = Context.GetHttpContext().Request.Query["userId"];
        UsersConnecteds.Remove(Userid);
        return base.OnDisconnectedAsync(exception);
    }
}