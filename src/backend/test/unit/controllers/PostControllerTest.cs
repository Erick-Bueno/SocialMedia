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

        postServiceMock.Setup(ps => ps.listPostsSeeMore(date.ToString(), userModelTest.id )).Returns(listPostsLinq);

        var result = postController.listPostsSeeMore(userModelTest.id, date.ToString());

        var okObjectResult = Assert.IsType<ActionResult<PostModel>>(result);
        var content = Assert.IsType<OkObjectResult>(okObjectResult.Result);
        var response = new List<PostsLinq>();
        Assert.Equal(response, content.Value);
    }
    [Fact]
    public async void should_to_return_a_response_content_whit_list_next_posts_user_like()
    {
        var postServiceMock = new Mock<IPostService>();
        var postController = new Post(postServiceMock.Object);


        PostDto postDto = new PostDto();


        PostImagesDto postImagesDto = new PostImagesDto();

        var id = Guid.NewGuid();

        List<PostsLikeListLinq> listPostLike = new List<PostsLikeListLinq>();
        postServiceMock.Setup(ps => ps.listPostsUserLikeSeeMore(id, new DateTime().ToString())).Returns(listPostLike);

        var result = await postController.listPostsUserLikeSeeMore(id, new DateTime().ToString());

        var createdResult = Assert.IsType<ActionResult<PostModel>>(result);
        var content = Assert.IsType<OkObjectResult>(createdResult.Result);
        var response = new List<PostsLikeListLinq>();
        Assert.Equal(response, content.Value);
    }
    [Fact]
    public void should_to_return_a_response_content_whit_list_posts_user_created()
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

        var listPost = new List<PostsLinq>();

        postServiceMock.Setup(ps => ps.listPostsUserCreated(userModelTest.id)).Returns(listPost);

        var result = postController.listPostUserCreated(userModelTest.id);

        var createdResult = Assert.IsType<ActionResult<PostModel>>(result);
        var content = Assert.IsType<OkObjectResult>(createdResult.Result);
    
        Assert.Equal(listPost, content.Value);
    }
    [Fact]
    public void should_to_return_a_response_content_whit_list_next_posts_user_created()
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

        var listPost = new List<PostsLinq>();

        postServiceMock.Setup(ps => ps.listPostsUserCreatedSeeMore(userModelTest.id, new DateTime().ToString())).Returns(listPost);

        var result = postController.listPostUserCreatedSeeMore(userModelTest.id,new DateTime().ToString());

        var createdResult = Assert.IsType<ActionResult<PostModel>>(result);
        var content = Assert.IsType<OkObjectResult>(createdResult.Result);
    
        Assert.Equal(listPost, content.Value);
    }
    [Fact]
    public void should_to_return_a_response_with_list_first_five_posts_searched()
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

        var listPostFirstSearched = new List<PostsLinq>();

        postServiceMock.Setup(ps => ps.findFiveFirstPostsSearched("erick", userModelTest.id)).Returns(listPostFirstSearched);
        
        var postData = new SearchedDataDto();
        postData.name = "erick";
        postData.userId = userModelTest.id;

        var result = postController.findFiveFirstPostsSearched(postData);

        var createdResult = Assert.IsType<ActionResult<List<PostModel>>>(result);
        var content = Assert.IsType<OkObjectResult>(createdResult.Result);
    
        Assert.Equal(listPostFirstSearched, content.Value);
    }  
     [Fact]
    public void should_to_return_a_response_with_list_next_five_posts_searched()
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

        var listPostNextSearched = new List<PostsLinq>();
        var data = new DateTime();
        postServiceMock.Setup(ps => ps.findPostsSearchedScrolling(userModelTest.id,data.ToString(),"erick" )).Returns(listPostNextSearched);
        
        var postData = new NextPostsSearched();
        postData.name = "erick";
        postData.userId = userModelTest.id;
        postData.date = data.ToString();

        var result = postController.findPostsSearchedScrolling(postData);

        var createdResult = Assert.IsType<ActionResult<List<PostModel>>>(result);
        var content = Assert.IsType<OkObjectResult>(createdResult.Result);
    
        Assert.Equal(listPostNextSearched, content.Value);
    }    
}