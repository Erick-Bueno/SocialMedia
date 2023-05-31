using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class LikeRepositoryTest
{
    [Fact]
    public async void should_to_create_an_like()
    {   
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;
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
}