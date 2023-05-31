using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class CommentRepositoryTest
{
    [Fact]
    public async void should_to_create_an_comment()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        var commentRepository = new CommentRepository(appDbContextMock.Object);

        var commentModel = new CommentModel();
        commentModel.userId = Guid.NewGuid();
        commentModel.comment = "comentario";
        commentModel.id = Guid.NewGuid();
        commentModel.postId = Guid.NewGuid();

        appDbContextMock.Setup(db => db.Comments.AddAsync(commentModel, CancellationToken.None)).ReturnsAsync(appDbContextMock.Object.Entry(commentModel));

        var result = await commentRepository.createComment(commentModel);
        Assert.Equal(result, commentModel);
    }
}