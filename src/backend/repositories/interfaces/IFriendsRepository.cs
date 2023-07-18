public interface IFriendsRepository
{
    public Task<FriendsModel> addFriends(FriendsModel friends);
    public List<UserFriendsListLinq> listUserFriends(Guid id);
    public int findFriends(Guid id);
    public FriendsModel checkIfAreFriends (Guid receiverId, Guid requesterId);
    
}