using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class PostRepositoryTest
{
    [Fact]
    public async void should_to_find_a_post()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        var PostRepository = new PostRepository(appDbContextMock.Object);

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();


        appDbContextMock.Setup(db => db.Posts.FindAsync(postModel.id)).ReturnsAsync(postModel);

        var Result = await PostRepository.findPost(postModel.id);

        Assert.Equal(Result, postModel);
    }
    [Fact]
    public async void should_to_update_total_comments()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        var PostRepository = new PostRepository(appDbContextMock.Object);

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();


        await PostRepository.updateTotalComments(postModel);

        Assert.Equal(1, postModel.totalComments);
    }
    [Fact]
    public async void should_to_uptade_total_likes()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        var PostRepository = new PostRepository(appDbContextMock.Object);

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();


        await PostRepository.updateTotalLikes(postModel);

        Assert.Equal(1, postModel.totalLikes);
    }
    [Fact]
    public async void should_to_create_a_post()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);

        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();

        appDbContextMock.Setup(db => db.Posts.AddAsync(postModel, CancellationToken.None)).ReturnsAsync(appDbContextMock.Object.Entry(postModel));

        var postRepository = new PostRepository(appDbContextMock.Object);

        var result = await postRepository.createPost(postModel);

        Assert.Equal(result, postModel);
    }
    [Fact]
    public async void should_to_added_post_images()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);
        PostImagesModel postImagesModel = new PostImagesModel();
        postImagesModel.id = Guid.NewGuid();
        postImagesModel.imgUrl = "urlteste";
        appDbContextMock.Setup(db => db.Posts_images.AddAsync(postImagesModel, CancellationToken.None)).ReturnsAsync(appDbContextMock.Object.Entry(postImagesModel));

        var postRepository = new PostRepository(appDbContextMock.Object);
        var result = await postRepository.createPostImages(postImagesModel);
        Assert.IsType<PostImagesModel>(result);



    }
    [Fact]
    public void should_to_return_a_list_of_posts()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;
        var appDbContextMock = new Mock<AppDbContext>(options);
        var postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();
        var postModel2 = new PostModel();
        postModel2.id = Guid.NewGuid();
        postModel2.datePost = DateTime.UtcNow;
        postModel2.totalComments = 0;
        postModel2.totalLikes = 0;
        postModel2.userId = Guid.NewGuid();
        PostImagesModel postImagesModel = new PostImagesModel();
        postImagesModel.id = Guid.NewGuid();
        postImagesModel.imgUrl = "urlteste";
        postImagesModel.postId = postModel.id;
        PostImagesModel postImagesModel2 = new PostImagesModel();
        postImagesModel2.id = Guid.NewGuid();
        postImagesModel2.imgUrl = "urlteste";
        postImagesModel2.postId = postModel2.id;
        var userModelTest = new UserModel();

        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone ="77799591703";
        userModelTest.userPhoto = "llll";

         var userModelTest2 = new UserModel();
        userModelTest2.email = "erickjb93@gmail.com";
        userModelTest2.password = "Sirlei231";
        userModelTest2.userName = "erick";
        userModelTest2.telephone ="77799591703";
        userModelTest2.userPhoto = "llll";

        var listPosts = new List<PostModel> { postModel, postModel2 }.AsQueryable();
        var listPostsImages = new List<PostImagesModel> { postImagesModel, postImagesModel2 }.AsQueryable();
        var listUsers = new List<UserModel>{userModelTest, userModelTest2}.AsQueryable();

        var dbSetPostsMock = new Mock<DbSet<PostModel>>();
        var dbSetPostImagesMock = new Mock<DbSet<PostImagesModel>>();
        var dbSetUsers = new Mock<DbSet<UserModel>>();

        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.Provider).Returns(listPosts.Provider);
        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.ElementType).Returns(listPosts.ElementType);
        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.Expression).Returns(listPosts.Expression);
        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.GetEnumerator()).Returns(listPosts.GetEnumerator());

        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.Provider).Returns(listPostsImages.Provider);
        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.ElementType).Returns(listPostsImages.ElementType);
        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.Expression).Returns(listPostsImages.Expression);
        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.GetEnumerator()).Returns(listPostsImages.GetEnumerator());

        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.Provider).Returns(listUsers.Provider);
        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.ElementType).Returns(listUsers.ElementType);
        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.Expression).Returns(listUsers.Expression);
        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.GetEnumerator()).Returns(listUsers.GetEnumerator());




        appDbContextMock.Setup(db => db.Posts).Returns(dbSetPostsMock.Object);
        appDbContextMock.Setup(db => db.Posts_images).Returns(dbSetPostImagesMock.Object);
        appDbContextMock.Setup(db => db.Users).Returns(dbSetUsers.Object);

        var postRepository = new PostRepository(appDbContextMock.Object);

        var result = postRepository.listPosts();

        Assert.IsType<List<PostsLinq>>(result);


    }


}