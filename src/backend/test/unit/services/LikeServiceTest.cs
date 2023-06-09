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
        var userRepositoryMock = new Mock<IUserRepository>();
        var LikeService = new LikeService(LikeRepositoryMock.Object, postRepositoryMock.Object, userRepositoryMock.Object);

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
        var userRepositoryMock = new Mock<IUserRepository>();
        var LikeService = new LikeService(LikeRepositoryMock.Object, postRepositoryMock.Object, userRepositoryMock.Object);
        var userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";
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
        postModel.userId = userModelTest.id;


        LikeRepositoryMock.Setup(lr => lr.createLike(It.IsAny<LikesModel>())).ReturnsAsync(LikesModel);
        postRepositoryMock.Setup(pr => pr.findPost(LikesModel.postId)).ReturnsAsync(postModel);

        userRepositoryMock.Setup(ur => ur.findUser( LikesModel.userId)).ReturnsAsync(userModelTest);
        var responseLikeCreated = new Response<LikesModel>(200, "comentario adicionado");

        var Result = await LikeService.createLike(LikeDto);
        Assert.IsType<Response<LikesModel>>(Result);
    }
    [Fact]
    public async void should_throw_validation_exception_when_post_is_not_finded()
    {
        var LikeRepositoryMock = new Mock<ILikeRepository>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var LikeService = new LikeService(LikeRepositoryMock.Object, postRepositoryMock.Object, userRepositoryMock.Object);


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

    public async void should_throw_validation_exception_when_user_is_not_finded()
    {
        var LikeRepositoryMock = new Mock<ILikeRepository>();
        var postRepositoryMock = new Mock<IPostRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var LikeService = new LikeService(LikeRepositoryMock.Object, postRepositoryMock.Object, userRepositoryMock.Object);


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

        userRepositoryMock.Setup(ur => ur.findUser(postModel.userId)).ReturnsAsync((UserModel)null);


        var Result = LikeService.createLike(LikeDto);
        await Assert.ThrowsAsync<ValidationException>(() => Result);
    }
}