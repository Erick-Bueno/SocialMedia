using Microsoft.AspNetCore.SignalR;

public class HubSolicitation:Hub
{
    private readonly IRequestService requestService;
    private readonly IUserService userService;
    private readonly IFriendsRepository friendsRepository;
    private Dictionary<string, string> usersConnecteds { get; set; } = new Dictionary<string, string>();
   

    public HubSolicitation(IRequestService requestService, IUserService userService, IFriendsRepository friendsRepository)
    {
        this.requestService = requestService;
        this.userService = userService;
        this.friendsRepository = friendsRepository;
    }
    public Dictionary<string, string> getUsersConnecteds(){
        return usersConnecteds;
    }
    public async Task sendingRequest(Guid receiver_id, Guid requester_id, StatusEnum status){
        RequestDto requestDto = new RequestDto();
        requestDto.receiverId = receiver_id;
        requestDto.requesterId = requester_id;
        requestDto.status =  status;
        var areFriends = friendsRepository.checkIfAreFriends(receiver_id, requester_id);
        if(areFriends != null){
            await Clients.Caller.SendAsync("SolicitationError", "Esses usuarios ja são amigos");
            return;
        }
        var addRequest = await requestService.addRequest(requestDto);
        if(addRequest == false){
            await Clients.Caller.SendAsync("SolicitationError", "Erro ao enviar solicitação");
            return;
        }
        if(usersConnecteds.ContainsKey((receiver_id.ToString()))){
            var connectionId = usersConnecteds[receiver_id.ToString()];
            var userData = await userService.findUser(requester_id);
            var requestData = requestService.findRequest(receiver_id, requester_id);
            await Clients.Client(connectionId).SendAsync("receivingRequest",requester_id,status, userData.userName, userData.userPhoto, requestData.RequestDate );
        }
    }
    public override async Task OnConnectedAsync()
    {
        //quando o usuario se conectar ao hub("websocket") pegaremos o seu id atraves da url e pegaremos o seu id de conexão e adicionamos em um dicionario
        var Userid = Context.GetHttpContext().Request.Query["userId"];
        var connectionId = Context.ConnectionId;
        usersConnecteds[Userid] = connectionId;

        await base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var Userid = Context.GetHttpContext().Request.Query["userId"];
        usersConnecteds.Remove(Userid);
        return base.OnDisconnectedAsync(exception);
    }
}