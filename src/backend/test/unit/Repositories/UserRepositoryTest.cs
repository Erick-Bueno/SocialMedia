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
    public void should_to_find_first_five_users_searched()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        var userRepository = new UserRepository(appDbContextMock.Object);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";


        var listUsers = new List<UserModel> { userModelTest }.AsQueryable();

        var dbSetUserMock = new Mock<DbSet<UserModel>>();

        dbSetUserMock.As<IQueryable<UserModel>>().Setup(x => x.Provider).Returns(listUsers.Provider);
        dbSetUserMock.As<IQueryable<UserModel>>().Setup(x => x.Expression).Returns(listUsers.Expression);
        dbSetUserMock.As<IQueryable<UserModel>>().Setup(x => x.ElementType).Returns(listUsers.ElementType);
        dbSetUserMock.As<IQueryable<UserModel>>().Setup(x => x.GetEnumerator()).Returns(listUsers.GetEnumerator());

        appDbContextMock.Setup(db => db.Users).Returns(dbSetUserMock.Object);

        var result = userRepository.findFiveFirstUserSearched(userModelTest.userName, userModelTest.id);

        Assert.IsType<List<SearchUserLinq>>(result);
    }
    [Fact]
    public void should_to_list_next_users_searched()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        var userRepository = new UserRepository(appDbContextMock.Object);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";


        var listUsers = new List<UserModel> { userModelTest }.AsQueryable();

        var dbSetUserMock = new Mock<DbSet<UserModel>>();

        dbSetUserMock.As<IQueryable<UserModel>>().Setup(x => x.Provider).Returns(listUsers.Provider);
        dbSetUserMock.As<IQueryable<UserModel>>().Setup(x => x.Expression).Returns(listUsers.Expression);
        dbSetUserMock.As<IQueryable<UserModel>>().Setup(x => x.ElementType).Returns(listUsers.ElementType);
        dbSetUserMock.As<IQueryable<UserModel>>().Setup(x => x.GetEnumerator()).Returns(listUsers.GetEnumerator());

        appDbContextMock.Setup(db => db.Users).Returns(dbSetUserMock.Object);

        var result = userRepository.findUserSearchedScrolling(userModelTest.id ,userModelTest.userName,userModelTest.id);

        Assert.IsType<List<SearchUserLinq>>(result);
    }

}