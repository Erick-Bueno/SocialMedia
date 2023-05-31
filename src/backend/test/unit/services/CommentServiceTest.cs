using System.ComponentModel.DataAnnotations;
using Moq;
using Xunit;

public class CommentServiceTest
{
    [Fact]
    public void should_to_convert_commentdto_to_commentmodel()
    {
        var CommentRepositoryMock = new Mock<ICommentRepository>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var CommentService = new CommentService(CommentRepositoryMock.Object, postRepositoryMock.Object);

        var commentDto = new CommentDto();
        commentDto.postId = Guid.NewGuid();
        commentDto.comment = "comentario";
        commentDto.userId = Guid.NewGuid();

        var Result = CommentService.convertCommentDtoToCommentModel(commentDto);

        Assert.IsType<CommentModel>(Result);
    }
    [Fact]
    public async void should_to_create_an_comment()
    {
        var CommentRepositoryMock = new Mock<ICommentRepository>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var CommentService = new CommentService(CommentRepositoryMock.Object, postRepositoryMock.Object);


        var commentDto = new CommentDto();
        commentDto.postId = Guid.NewGuid();
        commentDto.comment = "comentario";
        commentDto.userId = Guid.NewGuid();



        var CommentModel = new CommentModel();
        CommentModel.userId = commentDto.userId;
        CommentModel.comment = commentDto.comment;
        CommentModel.id = Guid.NewGuid();
        CommentModel.postId = commentDto.postId;

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();


        CommentRepositoryMock.Setup(cr => cr.createComment(It.IsAny<CommentModel>())).ReturnsAsync(CommentModel);
        postRepositoryMock.Setup(pr => pr.findPost(CommentModel.postId)).ReturnsAsync(postModel);
        var responseCommentCreated = new Response<CommentModel>(200, "comentario adicionado");

        var Result = await CommentService.createComment(commentDto);
        Assert.IsType<Response<CommentModel>>(Result);

    }
    [Fact]
    public async void should_throw_validation_exception_when_post_is_not_finded()
    {
        var CommentRepositoryMock = new Mock<ICommentRepository>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var CommentService = new CommentService(CommentRepositoryMock.Object, postRepositoryMock.Object);


        var commentDto = new CommentDto();
        commentDto.postId = Guid.NewGuid();
        commentDto.comment = "comentario";
        commentDto.userId = Guid.NewGuid();



        var CommentModel = new CommentModel();
        CommentModel.userId = commentDto.userId;
        CommentModel.comment = commentDto.comment;
        CommentModel.id = Guid.NewGuid();
        CommentModel.postId = commentDto.postId;

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();


        CommentRepositoryMock.Setup(cr => cr.createComment(It.IsAny<CommentModel>())).ReturnsAsync(CommentModel);
        postRepositoryMock.Setup(pr => pr.findPost(CommentModel.postId)).ReturnsAsync((PostModel)null);


        var Result = CommentService.createComment(commentDto);
        await Assert.ThrowsAsync<ValidationException>(() => Result);
    }
}