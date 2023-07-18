public interface IFriendsService
{
    public Task<Response<FriendsModel>> addFriends(FriendsDto friends);
    public List<UserFriendsListLinq> listUserFriends(Guid id);
    public int findFriends(Guid id);
    public FriendsModel convertFriendsDtoToFriendsModel(FriendsDto friends);
}