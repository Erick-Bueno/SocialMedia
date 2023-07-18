using System.ComponentModel.DataAnnotations;

public class FriendsService:IFriendsService
{
    private readonly IFriendsRepository friendsRepository;
   

    public FriendsService(IFriendsRepository friendsRepository)
    {
        this.friendsRepository = friendsRepository;
        
    }

    public async Task<Response<FriendsModel>> addFriends(FriendsDto friends)
    {
       var friendsModel = convertFriendsDtoToFriendsModel(friends);
       var newFriend = await friendsRepository.addFriends(friendsModel);
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