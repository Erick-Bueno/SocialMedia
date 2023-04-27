using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class jwtTest
{
    [Fact]
    public void Generate_Jwt_Test()
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
   async  public void add_token_test()
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
}