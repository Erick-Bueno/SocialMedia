using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class jwtTest
{
    [Fact]
    public void should_generate_Jwt()
    {
        UserModel userModeltest = new UserModel();
        userModeltest.id = Guid.NewGuid();
        userModeltest.email = "erickjb93@gmail.com";
        userModeltest.password = "Sirlei231";
        userModeltest.userName = "erick";
        userModeltest.telephone ="77799591703";
        userModeltest.userPhoto =  "llll";

        Jwt jwt = new Jwt();
        Environment.SetEnvironmentVariable("JWT_SECRET", "72ba27d1c9b3d61d75008987546a09c20bf732d1");

        var result = jwt.generateJwt(userModeltest);

        Assert.IsType<String>(result);
    }
    [Fact]
   async  public void should_to_add_token()
    {
      var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName:"teste")
      .Options;

      var dbContextMock = new Mock<AppDbContext>(options);

      var tokenModel = new TokenModel();
      tokenModel.email = "erickjb93@gmail.com";
      tokenModel.id = Guid.NewGuid();
      tokenModel.jwt = "jwtTeste";

      dbContextMock.Setup(db => db.Token.AddAsync(It.IsAny<TokenModel>(), CancellationToken.None)).ReturnsAsync(dbContextMock.Object.Entry(tokenModel));

      var tokenRepository = new TokenRepository(dbContextMock.Object);

      var result = await tokenRepository.addUserToken(tokenModel);

      Assert.Equal(result, tokenModel);
    }
    [Fact]
    async public void should_update_token()
    {
      var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName:"teste")
      .Options;

      var appDbContextMock = new Mock<AppDbContext>(options);

      var tokenModel = new TokenModel();
      tokenModel.email = "erickjb93@gmail.com";
      tokenModel.id = Guid.NewGuid();
      tokenModel.jwt = "jwtTeste";


      appDbContextMock.Setup(db => db.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

      var tokenRepository = new TokenRepository(appDbContextMock.Object);
      var result = await tokenRepository.updateToken(tokenModel, "jwtnovo");

      Assert.Equal(result, tokenModel);
    }

   
}