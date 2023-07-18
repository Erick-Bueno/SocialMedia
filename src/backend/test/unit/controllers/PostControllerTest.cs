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

        postServiceMock.Setup(ps => ps.createPost(postDto, postImagesDto)).ReturnsAsync(response);

        var result = await postController.createPost(postDto, postImagesDto);

        var createdResult = Assert.IsType<ActionResult<PostModel>>(result);
        var content = Assert.IsType<CreatedResult>(createdResult.Result);
        Assert.Equal(response, content.Value);



    }
    [Fact]
    public async void should_to_return_a_response_content_whit_list_posts_user_like()
    {
        var postServiceMock = new Mock<IPostService>();
        var postController = new Post(postServiceMock.Object);


        PostDto postDto = new PostDto();


        PostImagesDto postImagesDto = new PostImagesDto();

        var id = Guid.NewGuid();

        List<PostsLikeListLinq> listPostLike = new List<PostsLikeListLinq>();
        postServiceMock.Setup(ps => ps.listPostsUserLike(id)).ReturnsAsync(listPostLike);

        var result = await postController.listPostsUserLiked(id);

        var createdResult = Assert.IsType<ActionResult<PostModel>>(result);
        var content = Assert.IsType<OkObjectResult>(createdResult.Result);
        var response = new List<PostsLikeListLinq>();
        Assert.Equal(response, content.Value);
    }
    [Fact]
    public void should_to_return_list_with_next_posts()
    {
        var postServiceMock = new Mock<IPostService>();
        var postController = new Post(postServiceMock.Object);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";
        
        var date = new DateTime();
        List<PostsLinq> listPostsLinq = new List<PostsLinq>();

        postServiceMock.Setup(ps => ps.listPostsSeeMore(userModelTest.id, date.ToString() )).Returns(listPostsLinq);

        var result = postController.listPostsSeeMore(userModelTest.id, date.ToString());

        var okObjectResult = Assert.IsType<ActionResult<PostModel>>(result);
        var content = Assert.IsType<OkObjectResult>(okObjectResult.Result);
        var response = new List<PostsLinq>();
        Assert.Equal(response, content.Value);
    }
}