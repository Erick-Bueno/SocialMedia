using System.ComponentModel.DataAnnotations;
using Moq;
using Xunit;

public class PostServiceTest
{
    [Fact]
    public async void should_to_added_post_with_images()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var formfilemock = new Mock<IFormFile>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object);
        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";
        postDto.totalComments = 0;
        postDto.totalLikes = 0;
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
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object);
        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";
        postDto.totalComments = 0;
        postDto.totalLikes = 0;
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
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object);
        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";
        postDto.totalComments = 0;
        postDto.totalLikes = 0;
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
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object);
        var postModel = new PostModel();
        PostImagesModel postImagesModel = new PostImagesModel();
        postImagesModel.id = Guid.NewGuid();
        postImagesModel.imgUrl = "urlteste";

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";
        postDto.totalComments = 0;
        postDto.totalLikes = 0;
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
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object);

        PostDto postDto = new PostDto();
        postDto.contentPost = "conteudo";
        postDto.totalComments = 0;
        postDto.totalLikes = 0;
        postDto.userId = Guid.NewGuid();

        var result = postService.convertPostDtoToPostModel(postDto);

        Assert.IsType<PostModel>(result);
    }
    [Fact]
    public void should_to_convert_post_images_dto_to_post_images_model()
    {
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object);
        PostImagesDto postImagesDto = new PostImagesDto();
        var imgUrl = "urlteste";
        var result = postService.toCreatePostImagesModel(imgUrl, Guid.NewGuid());
        Assert.IsType<PostImagesModel>(result);


    }
    [Fact]
    public void should_to_save_post_image()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object);
        var formfilemock = new Mock<IFormFile>();
        var streamMemory = new MemoryStream();
        IWebHostEnvironmentMock.Setup(we => we.WebRootPath).Returns("C:\\Users\\erick\\Documents\\GitHub\\SocialMedia\\src\\backend\\wwwroot");
        formfilemock.Setup(fl => fl.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None)).Returns(Task.FromResult(0));
        var imgname = "aaaaa.png";
        postService.savePostImages(imgname, formfilemock.Object);
        Assert.True(Directory.Exists($"C:\\Users\\erick\\Documents\\GitHub\\SocialMedia\\src\\backend\\wwwroot\\PostsImages"));


    }
    [Fact]
    public void TestName()
    {
        var postRepositoryMock = new Mock<IPostRepository>();
        var IWebHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        var postService = new PostService(postRepositoryMock.Object, IWebHostEnvironmentMock.Object);
        var listPost = new List<PostsLinq>();
        postRepositoryMock.Setup(pr => pr.listPosts()).Returns(listPost);

        var result = postService.listPosts();

        Assert.IsType<List<PostsLinq>>(result);
    }
}