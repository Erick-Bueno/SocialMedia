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
    [Fact]
    public void should_to_list_comments()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName:"teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);
        var commentDbSetMock = new Mock<DbSet<CommentModel>>();
        var userDbSetMock = new Mock<DbSet<UserModel>>();
        var commentModel = new CommentModel();

        commentModel.userId = Guid.NewGuid();
        commentModel.comment = "comentario";
        commentModel.id = Guid.NewGuid();
        commentModel.postId = Guid.NewGuid();

        UserModel userModel = new UserModel();
        userModel.id = Guid.NewGuid();

        var commentRepository = new CommentRepository(appDbContextMock.Object);
        
        var listComments = new List<CommentModel>{commentModel}.AsQueryable();
        var listUsers = new List<UserModel>{userModel}.AsQueryable();

        commentDbSetMock.As<IQueryable>().Setup(db => db.Provider).Returns(listComments.Provider);
        commentDbSetMock.As<IQueryable>().Setup(db => db.ElementType).Returns(listComments.ElementType);
        commentDbSetMock.As<IQueryable>().Setup(db => db.Expression).Returns(listComments.Expression);
        commentDbSetMock.As<IQueryable>().Setup(db => db.GetEnumerator()).Returns(listComments.GetEnumerator());

        userDbSetMock.As<IQueryable>().Setup(db => db.Provider).Returns(listUsers.Provider);
        userDbSetMock.As<IQueryable>().Setup(db => db.ElementType).Returns(listUsers.ElementType);
        userDbSetMock.As<IQueryable>().Setup(db => db.Expression).Returns(listUsers.Expression);
        userDbSetMock.As<IQueryable>().Setup(db => db.GetEnumerator()).Returns(listUsers.GetEnumerator());

        appDbContextMock.Setup(db => db.Comments).Returns(commentDbSetMock.Object);
        appDbContextMock.Setup(db => db.Users).Returns(userDbSetMock.Object);

        var result = commentRepository.listComment(commentModel.id);

        Assert.IsType<List<UserCommentsLinq>>(result);


    }
}