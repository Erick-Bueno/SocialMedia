using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class LikeRepositoryTest
{
    [Fact]
    public async void should_to_create_an_like()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        var likeRepository = new LikeRepository(appDbContextMock.Object);

        var likeModel = new LikesModel();
        likeModel.id = Guid.NewGuid();
        likeModel.postId = Guid.NewGuid();
        likeModel.userId = Guid.NewGuid();


        appDbContextMock.Setup(db => db.Likes.AddAsync(likeModel, CancellationToken.None)).ReturnsAsync(appDbContextMock.Object.Entry(likeModel));

        var result = await likeRepository.createLike(likeModel);
        Assert.Equal(result, likeModel);
    }
    [Fact]
    public void should_to_check_if_user_has_already_liked_that_post()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        var LikeRepository = new LikeRepository(appDbContextMock.Object);

        LikesModel likesModel = new LikesModel();
        likesModel.id = Guid.NewGuid();
        likesModel.postId = Guid.NewGuid();
        likesModel.userId = Guid.NewGuid();
        
        UserModel userModel = new UserModel();
        userModel.id = Guid.NewGuid();

        var dbsetlikemock = new Mock<DbSet<LikesModel>>();

        var listLikesModel = new List<LikesModel> { likesModel }.AsQueryable();

        dbsetlikemock.As<IQueryable>().Setup(db => db.Provider).Returns(listLikesModel.Provider);
        dbsetlikemock.As<IQueryable>().Setup(db => db.ElementType).Returns(listLikesModel.ElementType);
        dbsetlikemock.As<IQueryable>().Setup(db => db.Expression).Returns(listLikesModel.Expression);
        dbsetlikemock.As<IQueryable>().Setup(db => db.GetEnumerator()).Returns(listLikesModel.GetEnumerator());

        appDbContextMock.Setup(db => db.Likes).Returns(dbsetlikemock.Object);

        var result =  LikeRepository.findLike(likesModel.userId, likesModel.postId);

        Assert.IsType<LikesModel>(result);
        





    }
    [Fact]
    public async void TestName()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        var LikeRepository = new LikeRepository(appDbContextMock.Object);
        LikesModel likesModel = new LikesModel();
        likesModel.id = Guid.NewGuid();
        likesModel.postId = Guid.NewGuid();
        likesModel.userId = Guid.NewGuid();
        appDbContextMock.Setup(db => db.Likes.Remove(likesModel)).Returns(appDbContextMock.Object.Entry(likesModel));

        var result = await LikeRepository.removeLike(likesModel);
        Assert.True(result);

    }
}