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
      userModeltest.Email = "erickjb93@gmail.com";
      userModeltest.Password = "Sirlei231";
      userModeltest.UserName = "erick";
      userModeltest.Telephone ="77799591703";
      userModeltest.User_Photo =  "llll";

        Jwt jwt = new Jwt();
        
        var result = jwt.generateJwt(userModeltest);

        Assert.IsType<String>(result);
    }
    [Fact]
   async  public void should_to_add_token()
    {
      var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName:"teste")
      .Options;

      var DbContextMock = new Mock<AppDbContext>(options);

      var tokenModel = new TokenModel();
      tokenModel.Email = "erickjb93@gmail.com";
      tokenModel.id = Guid.NewGuid();
      tokenModel.jwt = "jwtTeste";

      DbContextMock.Setup(db => db.Token.AddAsync(It.IsAny<TokenModel>(), CancellationToken.None)).ReturnsAsync(DbContextMock.Object.Entry(tokenModel));

     var TokenRepository = new TokenRepository(DbContextMock.Object);

     var result = await TokenRepository.addUserToken(tokenModel);

     Assert.Equal(result, tokenModel);
    }
    [Fact]
    async public void should_update_token()
    {
      var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseInMemoryDatabase(databaseName:"teste")
      .Options;

      var AppDbContextMock = new Mock<AppDbContext>(options);

      var tokenModel = new TokenModel();
      tokenModel.Email = "erickjb93@gmail.com";
      tokenModel.id = Guid.NewGuid();
      tokenModel.jwt = "jwtTeste";


      AppDbContextMock.Setup(db => db.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

      var TokenRepository = new TokenRepository(AppDbContextMock.Object);
      var result = await TokenRepository.UpdateToken(tokenModel, "jwtnovo");

      Assert.Equal(result, tokenModel);
    }

   
}