using PostController.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

using Xunit;

public class PostControllerTest
{
    [Fact]
    public async void should_to_return_a_response_content()
    {
        var postServiceMock = new Mock<IPostService>();
        var postController = new Post(postServiceMock.Object);
    

        PostDto postDto = new PostDto();
   

        PostImagesDto postImagesDto = new PostImagesDto();
      

       Response<PostModel> response = new Response<PostModel>(200, "Postagem concluida");

        postServiceMock.Setup(ps => ps.createPost(postDto,postImagesDto)).ReturnsAsync(response);

        var result = await postController.createPost(postDto, postImagesDto);

        var createdResult = Assert.IsType<ActionResult<PostModel>>(result);
        var content = Assert.IsType<CreatedResult>(createdResult.Result);
        Assert.Equal(response, content.Value);

    

    }
}