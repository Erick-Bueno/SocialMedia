using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;


public class UserRepositoryTest
{


    [Fact]
    async public void should_to_register_user()
    {




        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";





        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        var dbContextMock = new Mock<AppDbContext>(options);




        dbContextMock
        .Setup(db => db.Users.AddAsync(It.IsAny<UserModel>(), CancellationToken.None))
        .ReturnsAsync(dbContextMock.Object.Entry(userModelTest));

        dbContextMock.Setup(db => db.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var userRepository = new UserRepository(dbContextMock.Object);

        var result = await userRepository.register(userModelTest);



        Assert.IsType<UserModel>(result);






    }

    [Fact]
    public void should_to_check_if_user_is_already_registered()
    {

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var users = new List<UserModel> { userModelTest }.AsQueryable();



        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "teste")
        .Options;

        var dbContextMock = new Mock<AppDbContext>(options);

        //simulando o objeto Iqueryable
        var dbSetMock = new Mock<DbSet<UserModel>>();
        dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.Provider).Returns(users.AsQueryable().Provider);
        dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.Expression).Returns(users.AsQueryable().Expression);
        dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.ElementType).Returns(users.AsQueryable().ElementType);
        dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());

        dbContextMock.Setup(dc => dc.Users).Returns(dbSetMock.Object);
        //---------------------------------
        var userRepository = new UserRepository(dbContextMock.Object);

        var result = userRepository.userRegistred(userModelTest.email);

        Assert.Equal(result, userModelTest);

    }
    [Fact]
    public void should_to_verify_user_not_registered()
    {
        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var users = new List<UserModel> { }.AsQueryable();
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "teste")
        .Options;

        var dbContextMock = new Mock<AppDbContext>(options);

        var dbSetMock = new Mock<DbSet<UserModel>>();
        dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.Provider).Returns(users.AsQueryable().Provider);
        dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.Expression).Returns(users.AsQueryable().Expression);
        dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.ElementType).Returns(users.AsQueryable().ElementType);
        dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());

        dbContextMock.Setup(db => db.Users).Returns(dbSetMock.Object);


        var userRepository = new UserRepository(dbContextMock.Object);
        var result = userRepository.userRegistred(userModelTest.email);
        Assert.Equal(result, null);
    }

    [Fact]
    public async void should_to_find_requester_user()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        appDbContextMock.Setup(db => db.Users.FindAsync(userModelTest.id)).ReturnsAsync(userModelTest);

        var userRepository = new UserRepository(appDbContextMock.Object);

        var result = await userRepository.findUser(userModelTest.id);

        Assert.Equal(result, userModelTest);
    }
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


        var userRepository = new UserRepository(appDbContextMock.Object);

        var userList = new List<FriendsModel> { friends, friends }.AsQueryable();


        var dbSetMock = new Mock<DbSet<FriendsModel>>();
        dbSetMock.As<IQueryable<FriendsModel>>().Setup(m => m.Provider).Returns(userList.Provider);
        dbSetMock.As<IQueryable<FriendsModel>>().Setup(m => m.Expression).Returns(userList.Expression);
        dbSetMock.As<IQueryable<FriendsModel>>().Setup(m => m.ElementType).Returns(userList.ElementType);
        dbSetMock.As<IQueryable<FriendsModel>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

        appDbContextMock.Setup(db => db.Friends).Returns(dbSetMock.Object);


        var result = userRepository.findFriends(friends.userId);
        Assert.IsType<int>(result);
        Assert.Equal(2, result);

    }




}