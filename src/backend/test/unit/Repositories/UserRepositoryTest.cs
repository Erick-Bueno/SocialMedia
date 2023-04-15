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

    
        
}