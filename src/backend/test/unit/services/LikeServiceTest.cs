using System.ComponentModel.DataAnnotations;
using Moq;
using Xunit;

public class LikeServiceTest
{
    [Fact]
    public void should_to_convert_likedto_to_likemodel()
    {
        var LikeRepositoryMock = new Mock<ILikeRepository>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var LikeService = new LikeService(LikeRepositoryMock.Object, postRepositoryMock.Object);
        
        var LikeDto = new LikeDto();
        LikeDto.postId = Guid.NewGuid();
        LikeDto.userId = Guid.NewGuid();
        

        var Result = LikeService.convertLikeDtoToLikeModel(LikeDto);
        Assert.IsType<LikesModel>(Result);
    }
    [Fact]
    public async void should_to_create_a_like()
    {
        var LikeRepositoryMock = new Mock<ILikeRepository>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var LikeService = new LikeService(LikeRepositoryMock.Object, postRepositoryMock.Object);

               
        var LikeDto = new LikeDto();
        LikeDto.postId = Guid.NewGuid();
        LikeDto.userId = Guid.NewGuid();

       

        var LikesModel = new LikesModel();
        LikesModel.postId = LikeDto.postId;
        LikesModel.userId = LikeDto.userId;

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();
        

        LikeRepositoryMock.Setup(lr => lr.createLike(It.IsAny<LikesModel>())).ReturnsAsync(LikesModel);
        postRepositoryMock.Setup(pr => pr.findPost(LikesModel.postId)).ReturnsAsync(postModel);
        var responseLikeCreated = new Response<LikesModel>(200, "comentario adicionado");

        var Result = await LikeService.createLike(LikeDto);
        Assert.IsType<Response<LikesModel>>(Result);
    }
    [Fact]
    public async void should_throw_validation_exception_when_post_is_not_finded()
    {
        var LikeRepositoryMock = new Mock<ILikeRepository>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var LikeService = new LikeService(LikeRepositoryMock.Object, postRepositoryMock.Object);


        var LikeDto = new LikeDto();
        LikeDto.postId = Guid.NewGuid();
        LikeDto.userId = Guid.NewGuid();

       

        var LikesModel = new LikesModel();
        LikesModel.postId = LikeDto.postId;
        LikesModel.userId = LikeDto.userId;

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();


        LikeRepositoryMock.Setup(lr => lr.createLike(It.IsAny<LikesModel>())).ReturnsAsync(LikesModel);
        postRepositoryMock.Setup(pr => pr.findPost(LikesModel.postId)).ReturnsAsync((PostModel)null);


        var Result = LikeService.createLike(LikeDto);
        await Assert.ThrowsAsync<ValidationException>(() => Result);
    }
}