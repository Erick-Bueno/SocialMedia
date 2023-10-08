public class FriendsRepository : IFriendsRepository
{
    private readonly AppDbContext _context;

    public FriendsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FriendsModel> addFriends(FriendsModel friends)
    {
  

            var newFriend = await _context.Friends.AddAsync(friends);
            await _context.SaveChangesAsync();
            return friends;

    }

    public FriendsModel checkIfAreFriends(Guid receiverId, Guid requesterId)
    {
        var areFriends = _context.Friends.Where(f => (f.userId == requesterId && f.userId2 == receiverId) || (f.userId == receiverId && f.userId2 == requesterId)).FirstOrDefault();
        return areFriends;
    }

    public int findFriends(Guid id)
    {
        var listFriends = _context.Friends.Where(f => f.userId == id || f.userId2 == id);
        var countFriends = listFriends.Count();
        return countFriends;
    }

    public List<UserFriendsListLinq> listUserFriends(Guid id)
    {
        var query = (from friend in _context.Friends
                     join user in _context.Users on (friend.userId == id ? friend.userId2 : friend.userId) equals user.id
                     where friend.userId == id || friend.userId2 == id
                     select new UserFriendsListLinq
                     {
                         nome = user.userName,
                         email = user.email
                     }).Distinct().ToList();
        return query;
    }
}