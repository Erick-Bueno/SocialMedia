using Comment.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

using Xunit;

public class CommentControllerTest
{
    [Fact]
    public async void should_to_return_a_response_with_a_message()
    {
        var commentServiceMock = new Mock<ICommentService>();

        var commentController = new CommentController(commentServiceMock.Object);

        var commentDto = new CommentDto();
        commentDto.postId = Guid.NewGuid();
        commentDto.comment = "comentario";
        commentDto.userId = Guid.NewGuid();

        var responseCommentCreated = new Response<CommentModel>(200, "Comentario adicionado");

        commentServiceMock.Setup(cs => cs.createComment(commentDto)).ReturnsAsync(responseCommentCreated);

        var result = await commentController.createComment(commentDto);

        var okobjectresult = Assert.IsType<ActionResult<CommentModel>>(result);

        var content = Assert.IsType<OkObjectResult>(okobjectresult.Result);

        Assert.Equal(content.Value, responseCommentCreated);


    }
    [Fact]
    public async void should_to_check_the_content_in_okobjectresult_returned_when_comment_created()
    {
       var commentServiceMock = new Mock<ICommentService>();

        var commentController = new CommentController(commentServiceMock.Object);

        var commentDto = new CommentDto();
        commentDto.postId = Guid.NewGuid();
        commentDto.comment = "comentario";
        commentDto.userId = Guid.NewGuid();

        var responseCommentCreated = new Response<CommentModel>(200, "Comentario adicionado");

        commentServiceMock.Setup(cs => cs.createComment(commentDto)).ReturnsAsync(responseCommentCreated);

        var result = await commentController.createComment(commentDto);

        var okobjectresult = Assert.IsType<ActionResult<CommentModel>>(result);

        var content = Assert.IsType<OkObjectResult>(okobjectresult.Result).Value;

        Assert.IsType<Response<CommentModel>>(content);
    }
}