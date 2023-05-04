using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;


public class UserRepositoryTest
{

  
    [Fact]
   async public void register_user_return_test()
    {
      
    
    
    
      UserModel userModeltest = new UserModel();
      userModeltest.id = Guid.NewGuid();
      userModeltest.Email = "erickjb93@gmail.com";
      userModeltest.Password = "Sirlei231";
      userModeltest.UserName = "erick";
      userModeltest.Telephone ="77799591703";
      userModeltest.User_Photo =  "llll";
    
  
      
      
 
      var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;  

      var DbContextMock = new Mock<AppDbContext>(options);
      
   
      

      DbContextMock
        .Setup(db => db.Users.AddAsync(It.IsAny<UserModel>(),CancellationToken.None))
        .ReturnsAsync(DbContextMock.Object.Entry(userModeltest));

       DbContextMock.Setup(db => db.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

      var UserRepository = new UserRepository(DbContextMock.Object);

      var result = await UserRepository.Register(userModeltest);

      

      Assert.IsType<UserModel>(result);
        
        
      



    }

    [Fact]
     public void user_registred_return_true_test()
    {
      
      UserModel userModeltest = new UserModel();
      userModeltest.id = Guid.NewGuid();
      userModeltest.Email = "erickjb93@gmail.com";
      userModeltest.Password = "Sirlei231";
      userModeltest.UserName = "erick";
      userModeltest.Telephone ="77799591703";
      userModeltest.User_Photo =  "llll";

    var users = new List<UserModel>{userModeltest}.AsQueryable();
       
      
     
      var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName:"teste")
      .Options;

      var DbContextMock = new Mock<AppDbContext>(options);

      //simulando o objeto Iqueryable
      var dbSetMock = new Mock<DbSet<UserModel>>();
      dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.Provider).Returns(users.AsQueryable().Provider);
      dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.Expression).Returns(users.AsQueryable().Expression);
      dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.ElementType).Returns(users.AsQueryable().ElementType);
      dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
      
      DbContextMock.Setup(dc => dc.Users).Returns(dbSetMock.Object); 
      //---------------------------------
      var UserRepository = new UserRepository(DbContextMock.Object);

      var result = UserRepository.user_registred(userModeltest.Email);

      Assert.True(result);

    }
    [Fact]
    public void user_registred_return_false_test()
    {
      UserModel userModeltest = new UserModel();
      userModeltest.id = Guid.NewGuid();
      userModeltest.Email = "erickjb93@gmail.com";
      userModeltest.Password = "Sirlei231";
      userModeltest.UserName = "erick";
      userModeltest.Telephone ="77799591703";
      userModeltest.User_Photo =  "llll";

    var users = new List<UserModel>{}.AsQueryable();
      var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName:"teste")
      .Options;

      var DbContextMock = new Mock<AppDbContext>(options);

      var dbSetMock = new Mock<DbSet<UserModel>>();
      dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.Provider).Returns(users.AsQueryable().Provider);
      dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.Expression).Returns(users.AsQueryable().Expression);
      dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.ElementType).Returns(users.AsQueryable().ElementType);
      dbSetMock.As<IQueryable<UserModel>>().Setup(x => x.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());

      DbContextMock.Setup(db =>db.Users).Returns(dbSetMock.Object);
      
      var UserRepository = new UserRepository(DbContextMock.Object);
      var result = UserRepository.user_registred(userModeltest.Email);
      Assert.False(result);
    }

    [Fact]
    public async void find_user_requester_test()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;

        var AppDbContextMock = new Mock<AppDbContext>(options);

        UserModel userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.Email = "erickjb93@gmail.com";
        userModeltest.Password = "Sirlei231";
        userModeltest.UserName = "erick";
        userModeltest.Telephone ="77799591703";
        userModeltest.User_Photo =  "llll";
        
        AppDbContextMock.Setup(db => db.Users.FindAsync(userModeltest.id)).ReturnsAsync(userModeltest);

        var UserRepository = new UserRepository(AppDbContextMock.Object);

        var result = await UserRepository.FindUserRequester(userModeltest.id);

        Assert.Equal(result, userModeltest);
    }
 
 
    
        
}