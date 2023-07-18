using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class AuthRepositoryTest
{
    [Fact]
     public void  should_to_find_user_by_your_email()
    {
       
      
       var options = new DbContextOptionsBuilder<AppDbContext>()
       .UseInMemoryDatabase(databaseName:"teste")
       .Options;

        var appDbContextMock = new Mock<AppDbContext>(options);
        
        UserModel userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.email = "erickjb93@gmail.com";
        userModeltest.password = "Sirlei231";
        userModeltest.userName = "erick";
        userModeltest.telephone ="77799591703";
        userModeltest.userPhoto =  "llll";

        UserLoginDto loginData = new UserLoginDto();
        loginData.email = userModeltest.email;
        loginData.senha = userModeltest.password;

        var users = new List<UserModel>{userModeltest}.AsQueryable();

        var dbsetusersmock = new Mock<DbSet<UserModel>>();
        dbsetusersmock.As<IQueryable<UserModel>>().Setup(x => x.Provider).Returns(users.AsQueryable().Provider);
        dbsetusersmock.As<IQueryable<UserModel>>().Setup(x => x.Expression).Returns(users.AsQueryable().Expression);
        dbsetusersmock.As<IQueryable<UserModel>>().Setup(x => x.ElementType).Returns(users.AsQueryable().ElementType);
        dbsetusersmock.As<IQueryable<UserModel>>().Setup(x => x.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());

        appDbContextMock.Setup(db => db.Users).Returns(dbsetusersmock.Object);
        var authRepository = new AuthRepository(appDbContextMock.Object);
        var result =  authRepository.searchingForEmail(loginData);

        Assert.Equal(userModeltest, result);

    }    

    [Fact]
    public void should_to_find_token_data_from_user_email()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName:"teste")
        .Options;
        TokenModel token = new TokenModel();
        token.email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();
        token.jwt = "asdasdasdasd";
        var appDbContextMock = new Mock<AppDbContext>(options);
        var tokens = new List<TokenModel>{token}.AsQueryable();

      
        
        var dbsetMockToken = new Mock<DbSet<TokenModel>>();
        dbsetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.Provider).Returns(tokens.Provider);
        dbsetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.Expression).Returns(tokens.Expression);
        dbsetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.ElementType).Returns(tokens.ElementType);
        dbsetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.GetEnumerator()).Returns(tokens.GetEnumerator());

        appDbContextMock.Setup(db => db.Token).Returns(dbsetMockToken.Object);

     
       
        
        var authRepository = new AuthRepository(appDbContextMock.Object);
        var result = authRepository.loggedInBeffore(token.email);

        Assert.Equal(token, result);
    }
    [Fact]
    public async void should_to_find_a_email_associated_with_a_token()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);

        var dbSetMockToken = new Mock<DbSet<TokenModel>>();
        UserModel userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.email = "erickjb93@gmail.com";
        userModeltest.password = "Sirlei231";
        userModeltest.userName = "erick";
        userModeltest.telephone ="77799591703";
        userModeltest.userPhoto =  "llll";
        Environment.SetEnvironmentVariable("JWT_SECRET","72ba27d1c9b3d61d75008987546a09c20bf732d1");
        TokenModel token = new TokenModel();
        Jwt jwt = new Jwt();
        token.email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();

        token.jwt = jwt.generateJwt(userModeltest);

        var tokens = new List<TokenModel>{token}.AsQueryable();

        dbSetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.Provider).Returns(tokens.Provider);
        dbSetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.Expression).Returns(tokens.Expression);
        dbSetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.ElementType).Returns(tokens.ElementType);
        dbSetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.GetEnumerator()).Returns(tokens.GetEnumerator());

        appDbContextMock.Setup(db => db.Token).Returns(dbSetMockToken.Object);

        var authRepository = new AuthRepository(appDbContextMock.Object);

        var result = await authRepository.findUserEmailWithToken(token.jwt);

        Assert.Equal(result,token);

       
      
    }
    [Fact]
    public async void must_find_no_email()
    {
       var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;
       var appDbContextMock = new Mock<AppDbContext>(options);
       var dbSetMockToken = new Mock<DbSet<TokenModel>>();
       
       var tokens = new List<TokenModel>(){}.AsQueryable();

       dbSetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.Provider).Returns(tokens.Provider);
       dbSetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.Expression).Returns(tokens.Expression);
       dbSetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.ElementType).Returns(tokens.ElementType);
       dbSetMockToken.As<IQueryable<TokenModel>>().Setup(x => x.GetEnumerator()).Returns(tokens.GetEnumerator());

       appDbContextMock.Setup(db => db.Token).Returns(dbSetMockToken.Object);

       var AuthRepository = new AuthRepository(appDbContextMock.Object);

        TokenModel token = new TokenModel();
        Jwt jwt = new Jwt();
        token.email = "erickjb93@gmail.com";
        token.id = Guid.NewGuid();

       var result = await AuthRepository.findUserEmailWithToken(token.jwt);

       Assert.Equal(result,null);

    }

}