using System.ComponentModel.DataAnnotations;
using backend.Migrations;
using Moq;
using Xunit;

public class PostServiceTest
{
    [Fact]
    public async void should_throw_validation_exception_if_neither_image_nor_text_is_passed()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var formfilemock = new Mock<IFormFile>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);


        PostDto postDto = new PostDto();
        postDto.contentPost = "";
        postDto.userId = Guid.NewGuid();

        PostImagesDto postImagesDto = new PostImagesDto();
        postImagesDto.imgUrl = null;



        await Assert.ThrowsAsync<ValidationException>(() => postService.createPost(postDto, postImagesDto));

    }
    [Fact]
    public async void should_to_added_post_with_images()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var formfilemock = new Mock<IFormFile>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";

        postDto.userId = Guid.NewGuid();
        postRepositoryMock.Setup(pr => pr.createPost(postModel)).ReturnsAsync(postModel);
        PostImagesDto postImagesDto = new PostImagesDto();

        postImagesDto.imgUrl = new List<IFormFile> { formfilemock.Object, formfilemock.Object, formfilemock.Object, formfilemock.Object };
        formfilemock.Setup(fr => fr.FileName).Returns("postimg.png");
        formfilemock.Setup(fr => fr.ContentType).Returns("image/png");
        IWebHostEnvironmentMock.Setup(we => we.WebRootPath).Returns("C:\\Users\\erick\\Documents\\GitHub\\SocialMedia\\src\\backend\\wwwroot");
        formfilemock.Setup(fl => fl.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None)).Returns(Task.FromResult(0));
        Response<PostModel> rp = new Response<PostModel>(200, "Postagem concluida");
        var reuslt = await postService.createPost(postDto, postImagesDto);

        Assert.Equal(reuslt.Status, rp.Status);
        Assert.Equal(reuslt.Message, rp.Message);
    }
    [Fact]
    public async void should_to_added_post_no_images()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var formfilemock = new Mock<IFormFile>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";

        postDto.userId = Guid.NewGuid();
        postRepositoryMock.Setup(pr => pr.createPost(postModel)).ReturnsAsync(postModel);
        PostImagesDto postImagesDto = new PostImagesDto();

        postImagesDto.imgUrl = new List<IFormFile> { };

        IWebHostEnvironmentMock.Setup(we => we.WebRootPath).Returns("C:\\Users\\erick\\Documents\\GitHub\\SocialMedia\\src\\backend\\wwwroot");
        formfilemock.Setup(fl => fl.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None)).Returns(Task.FromResult(0));
        Response<PostModel> rp = new Response<PostModel>(200, "Postagem concluida");
        var reuslt = await postService.createPost(postDto, postImagesDto);

        Assert.Equal(reuslt.Status, rp.Status);
        Assert.Equal(reuslt.Message, rp.Message);
    }
    [Fact]
    public async void should_not_to_add_post()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var formfilemock = new Mock<IFormFile>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";

        postDto.userId = Guid.NewGuid();
        postRepositoryMock.Setup(pr => pr.createPost(postModel)).ReturnsAsync(postModel);
        PostImagesDto postImagesDto = new PostImagesDto();

        postImagesDto.imgUrl = new List<IFormFile> { formfilemock.Object, formfilemock.Object, formfilemock.Object, formfilemock.Object, formfilemock.Object };

        IWebHostEnvironmentMock.Setup(we => we.WebRootPath).Returns("C:\\Users\\erick\\Documents\\GitHub\\SocialMedia\\src\\backend\\wwwroot");
        formfilemock.Setup(fl => fl.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None)).Returns(Task.FromResult(0));


        await Assert.ThrowsAsync<ValidationException>(() => postService.createPost(postDto, postImagesDto));
    }
    [Fact]
    public async void should_throw_validation_exception_if_fileContentType_is_not_jpg_or_png()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var formfilemock = new Mock<IFormFile>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        var postModel = new PostModel();
        PostImagesModel postImagesModel = new PostImagesModel();
        postImagesModel.id = Guid.NewGuid();
        postImagesModel.imgUrl = "urlteste";

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";

        postDto.userId = Guid.NewGuid();

        PostImagesDto postImagesDto = new PostImagesDto();
        formfilemock.Setup(fr => fr.ContentType).Returns("img/csv");
        postImagesDto.imgUrl = new List<IFormFile> { formfilemock.Object };



        await Assert.ThrowsAsync<ValidationException>(() => postService.createPost(postDto, postImagesDto));


    }

    [Fact]
    public void should_to_convert_post_dto_to_post_model()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";

        postDto.userId = Guid.NewGuid();

        var result = postService.convertPostDtoToPostModel(postDto);

        Assert.IsType<PostModel>(result);
    }
    [Fact]
    public void should_to_convert_post_images_dto_to_post_images_model()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        PostImagesDto postImagesDto = new PostImagesDto();
        var imgUrl = "urlteste";
        var result = postService.toCreatePostImagesModel(imgUrl, Guid.NewGuid());
        Assert.IsType<PostImagesModel>(result);


    }
    [Fact]
    public async void should_to_save_post_image()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        var formfilemock = new Mock<IFormFile>();
        var streamMemory = new MemoryStream();
        
        var path = "C:\\caminho\\ficticio\\do\\diretorio\\raiz";
    
        IWebHostEnvironmentMock.Setup(we => we.WebRootPath).Returns(path);
        formfilemock.Setup(fl => fl.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None)).Returns(Task.FromResult(0));
        await postService.savePostImages("aaaaa", formfilemock.Object);
        IWebHostEnvironmentMock.Verify(x => x.WebRootPath, Times.AtLeastOnce());
        formfilemock.Verify(f => f.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None), Times.Once);


    }
    [Fact]
    public void should_to_list_posts()
    {
        Guid id = Guid.NewGuid();
        var postRepositoryMock = new Mock<IPostRepository>();
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        var listPost = new List<PostsLinq>();
        postRepositoryMock.Setup(pr => pr.listPosts(id)).Returns(listPost);

        var result = postService.listPosts(id);

        Assert.IsType<List<PostsLinq>>(result);
    }
    [Fact]
    public async void should_to_list_the_posts_that_user_likes()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";
        List<PostsLikeListLinq> listPostsLinq = new List<PostsLikeListLinq>();

        userRepositoryMock.Setup(ur => ur.findUser(userModelTest.id)).ReturnsAsync(userModelTest);
        postRepositoryMock.Setup(pr => pr.listPostsUserLike(userModelTest.id)).Returns(listPostsLinq);

        var result = await postService.listPostsUserLike(userModelTest.id);
        Assert.IsType<List<PostsLikeListLinq>>(result);

    }
    [Fact]
    public async void should_thrown_exception_if_user_not_exists()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";
        List<PostsLinq> listPostsLinq = new List<PostsLinq>();

        userRepositoryMock.Setup(ur => ur.findUser(userModelTest.id)).ReturnsAsync((UserModel)null);

        var result = postService.listPostsUserLike(userModelTest.id);
        await Assert.ThrowsAsync<ValidationException>(() => result);

    }
    [Fact]
    public void should_to_list_next_posts()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";
        var date = new DateTime();

        List<PostsLinq> listPostsLinq = new List<PostsLinq>();

        postRepositoryMock.Setup(pr => pr.listPostsSeeMore(date, userModelTest.id)).Returns(listPostsLinq);

        var result = postService.listPostsSeeMore(date.ToString(), userModelTest.id);

        Assert.IsType<List<PostsLinq>>(result);
    }
    [Fact]
    public async void should_to_list_next_posts_that_user_likes()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object, userRepositoryMock.Object);
        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";
        List<PostsLikeListLinq> listPostsLinq = new List<PostsLikeListLinq>();

        postRepositoryMock.Setup(pr => pr.listPostsUserLikeSeeMore(userModelTest.id, new DateTime())).Returns(listPostsLinq);

        var result = postService.listPostsUserLikeSeeMore(userModelTest.id, new DateTime().ToString());
        Assert.IsType<List<PostsLikeListLinq>>(result);

    }
    [Fact]
    public void should_to_list_posts_created_by_user()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postService = new PostService(postRepositoryMock.Object, webHostEnvironmentMock.Object, userRepositoryMock.Object);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var listPostsUserCreated = new List<PostsLinq>();

        postRepositoryMock.Setup(pr => pr.listPostsUserCreated(userModelTest.id)).Returns(listPostsUserCreated);

        var result = postService.listPostsUserCreated(userModelTest.id);

        Assert.IsType<List<PostsLinq>>(result);

    }
    [Fact]
    public void should_to_list_next_posts_created_by_user()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postService = new PostService(postRepositoryMock.Object, webHostEnvironmentMock.Object, userRepositoryMock.Object);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var listPostsUserCreated = new List<PostsLinq>();

        postRepositoryMock.Setup(pr => pr.listPostsUserCreatedSeeMore(userModelTest.id, new DateTime())).Returns(listPostsUserCreated);

        var result = postService.listPostsUserCreatedSeeMore(userModelTest.id, new DateTime().ToString());

        Assert.IsType<List<PostsLinq>>(result);
    }
    [Fact]
    public void should_to_list_first_five_posts_searched()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postService = new PostService(postRepositoryMock.Object, webHostEnvironmentMock.Object, userRepositoryMock.Object);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var listPostsUsersSearched = new List<PostsLinq>();

        postRepositoryMock.Setup(pr => pr.findFiveFirstPostsSearched("erick", userModelTest.id)).Returns(listPostsUsersSearched);

        var result = postService.findFiveFirstPostsSearched("erick", userModelTest.id);

        Assert.IsType<List<PostsLinq>>(result);
    }
    [Fact]
    public void should_to_list_next_five_posts_searched()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postService = new PostService(postRepositoryMock.Object, webHostEnvironmentMock.Object, userRepositoryMock.Object);

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var listPostsUsersSearched = new List<PostsLinq>();
        var date = new DateTime();
        postRepositoryMock.Setup(pr => pr.findPostsSearchedScrolling(date, "erick", userModelTest.id)).Returns(listPostsUsersSearched);

        var result = postService.findPostsSearchedScrolling(userModelTest.id, date.ToString(), "erick");

        Assert.IsType<List<PostsLinq>>(result);
    }
}