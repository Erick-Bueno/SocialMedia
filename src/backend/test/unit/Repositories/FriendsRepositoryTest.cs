using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class FriendsRepositoryTest
{
    [Fact]
    public void should_to_find_friends()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        FriendsModel friends = new FriendsModel();
        friends.userId = Guid.NewGuid();
        friends.userId2 = Guid.NewGuid();


        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";


        var friendsRepository = new FriendsRepository(appDbContextMock.Object);

        var userList = new List<FriendsModel> { friends, friends }.AsQueryable();


        var dbSetMock = new Mock<DbSet<FriendsModel>>();
        dbSetMock.As<IQueryable<FriendsModel>>().Setup(m => m.Provider).Returns(userList.Provider);
        dbSetMock.As<IQueryable<FriendsModel>>().Setup(m => m.Expression).Returns(userList.Expression);
        dbSetMock.As<IQueryable<FriendsModel>>().Setup(m => m.ElementType).Returns(userList.ElementType);
        dbSetMock.As<IQueryable<FriendsModel>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

        appDbContextMock.Setup(db => db.Friends).Returns(dbSetMock.Object);


        var result = friendsRepository.findFriends(friends.userId);
        Assert.IsType<int>(result);
        Assert.Equal(2, result);

    }
    [Fact]
    public async void should_to_add_friend()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        FriendsModel friends = new FriendsModel();
        friends.userId = Guid.NewGuid();
        friends.userId2 = Guid.NewGuid();

        var friendsRepository = new FriendsRepository(appDbContextMock.Object);

        appDbContextMock.Setup(db => db.Friends.AddAsync(friends, CancellationToken.None)).ReturnsAsync(appDbContextMock.Object.Entry(friends));

        var result = await friendsRepository.addFriends(friends);

        Assert.IsType<FriendsModel>(result);

    }
    [Fact]
    public void should_to_check_if_users_are_friends()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        FriendsModel friends = new FriendsModel();
        friends.userId = Guid.NewGuid();
        friends.userId2 = Guid.NewGuid();

        var friendsRepository = new FriendsRepository(appDbContextMock.Object);

        var listFriends = new List<FriendsModel> { friends }.AsQueryable();

        var dbSetMockFriends = new Mock<DbSet<FriendsModel>>();

        dbSetMockFriends.As<IQueryable>().Setup(db => db.Provider).Returns(listFriends.Provider);
        dbSetMockFriends.As<IQueryable>().Setup(db => db.ElementType).Returns(listFriends.ElementType);
        dbSetMockFriends.As<IQueryable>().Setup(db => db.Expression).Returns(listFriends.Expression);
        dbSetMockFriends.As<IQueryable>().Setup(db => db.GetEnumerator()).Returns(listFriends.GetEnumerator());

        appDbContextMock.Setup(db => db.Friends).Returns(dbSetMockFriends.Object);

        var result = friendsRepository.checkIfAreFriends(friends.userId, friends.userId2);
        Assert.IsType<FriendsModel>(result);
    }
    [Fact]
    public void should_to_list_friends()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        
        UserModel user = new UserModel();
        user.id = Guid.NewGuid();

        FriendsModel friends = new FriendsModel();
        friends.userId = user.id;
        friends.userId2 = Guid.NewGuid();
        

        var friendsRepository = new FriendsRepository(appDbContextMock.Object);
        var dbSetMockFriends = new Mock<DbSet<FriendsModel>>();
        var dbSetMockUsers = new Mock<DbSet<UserModel>>();

        var listFriends = new List<FriendsModel> { friends }.AsQueryable();
        var listUsers = new List<UserModel> { user }.AsQueryable();

        dbSetMockFriends.As<IQueryable>().Setup(db => db.Provider).Returns(listFriends.Provider);
        dbSetMockFriends.As<IQueryable>().Setup(db => db.ElementType).Returns(listFriends.ElementType);
        dbSetMockFriends.As<IQueryable>().Setup(db => db.Expression).Returns(listFriends.Expression);
        dbSetMockFriends.As<IQueryable>().Setup(db => db.GetEnumerator()).Returns(listFriends.GetEnumerator());

        dbSetMockUsers.As<IQueryable>().Setup(db => db.Provider).Returns(listUsers.Provider);
        dbSetMockUsers.As<IQueryable>().Setup(db => db.ElementType).Returns(listUsers.ElementType);
        dbSetMockUsers.As<IQueryable>().Setup(db => db.Expression).Returns(listUsers.Expression);
    

        appDbContextMock.Setup(db => db.Friends).Returns(dbSetMockFriends.Object);
        appDbContextMock.Setup(db => db.Users).Returns(dbSetMockUsers.Object);

        var result = friendsRepository.listUserFriends(user.id);

        Assert.IsType<List<UserFriendsListLinq>>(result);
    }
}