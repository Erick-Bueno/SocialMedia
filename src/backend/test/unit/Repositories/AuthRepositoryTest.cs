using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class AuthRepositoryTest
{
    [Fact]
     public void Email_incorrect_exception_test()
    {
       
      
       var options = new DbContextOptionsBuilder<AppDbContext>()
       .UseInMemoryDatabase(databaseName:"teste")
       .Options;

        var AppDbContextMock = new Mock<AppDbContext>(options);
        
            UserModel userModeltest = new UserModel();
            userModeltest.id = Guid.NewGuid();
            userModeltest.Email = "erickjb93@gmail.com";
            userModeltest.Password = "Sirlei231";
            userModeltest.UserName = "erick";
            userModeltest.Telephone ="77799591703";
            userModeltest.User_Photo =  "llll";

            UserLoginDto loginData = new UserLoginDto();
            loginData.Email = userModeltest.Email;
            loginData.Senha = userModeltest.Password;

            var users = new List<UserModel>{userModeltest}.AsQueryable();

        var dbsetusersmock = new Mock<DbSet<UserModel>>();
        dbsetusersmock.As<IQueryable<UserModel>>().Setup(x => x.Provider).Returns(users.AsQueryable().Provider);
        dbsetusersmock.As<IQueryable<UserModel>>().Setup(x => x.Expression).Returns(users.AsQueryable().Expression);
        dbsetusersmock.As<IQueryable<UserModel>>().Setup(x => x.ElementType).Returns(users.AsQueryable().ElementType);
        dbsetusersmock.As<IQueryable<UserModel>>().Setup(x => x.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());

        AppDbContextMock.Setup(db => db.Users).Returns(dbsetusersmock.Object);
        var AuthRepository = new AuthRepository(AppDbContextMock.Object);
        var result =  AuthRepository.SearchingForEmail(loginData);

         Assert.Equal(userModeltest, result);

    }    

    [Fact]
    public void TestName()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName:"teste")
        .Options;
        TokenModel token = new TokenModel();
        token.Email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();
        token.jwt = "asdasdasdasd";
        var AppDbContextMock = new Mock<AppDbContext>(options);
        var tokens = new List<TokenModel>{token}.AsQueryable();

      
        
        var dbsetMockToken = new Mock<DbSet<TokenModel>>();
        dbsetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.Provider).Returns(tokens.Provider);
        dbsetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.Expression).Returns(tokens.Expression);
        dbsetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.ElementType).Returns(tokens.ElementType);
        dbsetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.GetEnumerator()).Returns(tokens.GetEnumerator());

        AppDbContextMock.Setup(db => db.Token).Returns(dbsetMockToken.Object);

     
       
        
        var AuthRepository = new AuthRepository(AppDbContextMock.Object);
        var result = AuthRepository.LoggedInBeffore(token.Email);

        Assert.Equal(token, result);
    }

}