
using LikeController.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

using Xunit;

public class LikeControllerTest
{
    [Fact]
    public async void should_to_return_a_response_with_a_message()
    {
        var LikeServiceMock = new Mock<ILikeService>();

        var LikeController = new Like(LikeServiceMock.Object);

        var LikeDto = new LikeDto();
        LikeDto.postId = Guid.NewGuid();
        LikeDto.userId = Guid.NewGuid();

        var responseLikeCreated = new Response<LikesModel>(200, "Like adicionado");

        LikeServiceMock.Setup(ls => ls.createLike(LikeDto)).ReturnsAsync(responseLikeCreated);

        var result = await LikeController.createLike(LikeDto);

        var okobjectresult = Assert.IsType<ActionResult<LikesModel>>(result);

        var content = Assert.IsType<OkObjectResult>(okobjectresult.Result);
        Assert.Equal(content.Value, responseLikeCreated);


    }
    [Fact]
    public async void should_to_check_the_content_in_okobjectresult_returned_when_like_created()
    {
        var LikeServiceMock = new Mock<ILikeService>();

        var LikeController = new Like(LikeServiceMock.Object);

        var LikeDto = new LikeDto();
        LikeDto.postId = Guid.NewGuid();
        LikeDto.userId = Guid.NewGuid();

        var responseLikeCreated = new Response<LikesModel>(200, "Like adicionado");


        LikeServiceMock.Setup(ls => ls.createLike(LikeDto)).ReturnsAsync(responseLikeCreated);

        var result = await LikeController.createLike(LikeDto);

        var okobjectresult = Assert.IsType<ActionResult<LikesModel>>(result);

        var content = Assert.IsType<OkObjectResult>(okobjectresult.Result).Value;

        Assert.IsType<Response<LikesModel>>(content);
    }
}