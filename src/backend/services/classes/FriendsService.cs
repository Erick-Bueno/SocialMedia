using System.ComponentModel.DataAnnotations;

public class FriendsService:IFriendsService
{
    private readonly IFriendsRepository friendsRepository;
    private readonly IRequestRepository requestRepository;
   

    public FriendsService(IFriendsRepository friendsRepository, IRequestRepository requestRepository)
    {
        this.friendsRepository = friendsRepository;
        this.requestRepository = requestRepository;
        
    }

    public async Task<Response<FriendsModel>> addFriends(FriendsDto friends)
    {
        //testar
       var areFriends = friendsRepository.checkIfAreFriends(friends.receiverId, friends.requesterId);
       if(areFriends != null){
         Response<FriendsModel> responseError = new Response<FriendsModel>(400, "usuarios ja sao amigos");
         return responseError;
       }
       var friendsModel = convertFriendsDtoToFriendsModel(friends);
       var newFriend = await friendsRepository.addFriends(friendsModel);
       var findRequest = requestRepository.findRequest(friends.receiverId, friends.requesterId);
       if (findRequest == null){
         Response<FriendsModel> responseError = new Response<FriendsModel>(400, "Erro ao aceitar solicitação");
         return responseError;
       }
       var deleteRequest = requestRepository.deleteRequest(friends.receiverId, friends.requesterId);
       Response<FriendsModel> response = new Response<FriendsModel>(200, "Solicitação de amizade aceita");
       return response;



    }

    public FriendsModel convertFriendsDtoToFriendsModel(FriendsDto friends)
    {
        FriendsModel friendsModel = new FriendsModel();
        friendsModel.userId = friends.requesterId;
        friendsModel.userId2 = friends.receiverId;
        return friendsModel;
    }

    public int findFriends(Guid id)
    {
        var countFriends = friendsRepository.findFriends(id);
        return countFriends;
    }

    public List<UserFriendsListLinq> listUserFriends(Guid id)
    {
        var listUserFriends = friendsRepository.listUserFriends(id);
        return listUserFriends;
    }


}