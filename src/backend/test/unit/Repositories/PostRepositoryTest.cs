using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class PostRepositoryTest
{
    [Fact]
    public async void should_to_find_a_post()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;
        var AppDbContextMock = new Mock<AppDbContext>(options);

        var PostRepository = new PostRepository(AppDbContextMock.Object);

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();
    

        AppDbContextMock.Setup(db => db.Posts.FindAsync(postModel.id)).ReturnsAsync(postModel);

        var Result = await PostRepository.findPost(postModel.id);

        Assert.Equal(Result, postModel);
    }
    [Fact]
    public async void should_to_update_total_comments()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;
        var AppDbContextMock = new Mock<AppDbContext>(options);

        var PostRepository = new PostRepository(AppDbContextMock.Object);

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();
        

        await PostRepository.updateTotalComments(postModel);

        Assert.Equal(1, postModel.totalComments);
    }
    [Fact]
    public async void should_to_uptade_total_likes()
    {
         var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;
        var AppDbContextMock = new Mock<AppDbContext>(options);

        var PostRepository = new PostRepository(AppDbContextMock.Object);

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();
        

        await PostRepository.updateTotalLikes(postModel);

        Assert.Equal(1, postModel.totalLikes);
    }
   
}