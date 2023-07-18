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


        await PostRepository.updateTotalLikes(postModel, 1);

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

        var dbsetlike = new Mock<DbSet<LikesModel>>();

        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var userModelTest2 = new UserModel();
        userModelTest2.email = "erickjb93@gmail.com";
        userModelTest2.password = "Sirlei231";
        userModelTest2.userName = "erick";
        userModelTest2.telephone = "77799591703";
        userModelTest2.userPhoto = "llll";

        var LikesModel = new LikesModel();
        LikesModel.id = Guid.NewGuid();
        LikesModel.postId = postModel.id;
        LikesModel.userId = userModelTest.id;

        var LikesModel2 = new LikesModel();
        LikesModel2.id = Guid.NewGuid();
        LikesModel2.postId = postModel2.id;
        LikesModel2.userId = userModelTest2.id;



        var listPosts = new List<PostModel> { postModel, postModel2 }.AsQueryable();
        var listPostsImages = new List<PostImagesModel> { postImagesModel, postImagesModel2 }.AsQueryable();
        var listUsers = new List<UserModel> { userModelTest, userModelTest2 }.AsQueryable();

        var listLikes = new List<LikesModel> { LikesModel, LikesModel2 }.AsQueryable();

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

        dbsetlike.As<IQueryable<LikesModel>>().Setup(pi => pi.Provider).Returns(listLikes.Provider);
        dbsetlike.As<IQueryable<LikesModel>>().Setup(pi => pi.ElementType).Returns(listLikes.ElementType);
        dbsetlike.As<IQueryable<LikesModel>>().Setup(pi => pi.Expression).Returns(listLikes.Expression);
        dbsetlike.As<IQueryable<LikesModel>>().Setup(pi => pi.GetEnumerator()).Returns(listLikes.GetEnumerator());




        appDbContextMock.Setup(db => db.Posts).Returns(dbSetPostsMock.Object);
        appDbContextMock.Setup(db => db.Posts_images).Returns(dbSetPostImagesMock.Object);
        appDbContextMock.Setup(db => db.Users).Returns(dbSetUsers.Object);
        appDbContextMock.Setup(db => db.Likes).Returns(dbsetlike.Object);

        var postRepository = new PostRepository(appDbContextMock.Object);

        var result = postRepository.listPosts(userModelTest.id);

        Assert.IsType<List<PostsLinq>>(result);


    }
    [Fact]
    public void should_to_list_the_posts_that_user_likes()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);
        var postRepository = new PostRepository(appDbContextMock.Object);

        var dbSetPostsMock = new Mock<DbSet<PostModel>>();
        var dbSetPostImagesMock = new Mock<DbSet<PostImagesModel>>();
        var dbSetUsers = new Mock<DbSet<UserModel>>();
        var dbsetLikes = new Mock<DbSet<LikesModel>>();

        PostModel postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();

        PostImagesModel postImagesModel = new PostImagesModel();
        postImagesModel.id = Guid.NewGuid();
        postImagesModel.imgUrl = "urlteste";
        postImagesModel.postId = postModel.id;

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var LikesModel = new LikesModel();
        LikesModel.id = Guid.NewGuid();
        LikesModel.postId = postModel.id;
        LikesModel.userId = userModelTest.id;

        var postList = new List<PostModel> { postModel }.AsQueryable();
        var likeList = new List<LikesModel> { LikesModel }.AsQueryable();
        var postImageList = new List<PostImagesModel> { postImagesModel }.AsQueryable();
        var userList = new List<UserModel> { userModelTest }.AsQueryable();

        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.Provider).Returns(postList.Provider);
        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.ElementType).Returns(postList.ElementType);
        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.Expression).Returns(postList.Expression);
        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.GetEnumerator()).Returns(postList.GetEnumerator());

        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.Provider).Returns(postImageList.Provider);
        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.ElementType).Returns(postImageList.ElementType);
        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.Expression).Returns(postImageList.Expression);
        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.GetEnumerator()).Returns(postImageList.GetEnumerator());

        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.Provider).Returns(userList.Provider);
        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.ElementType).Returns(userList.ElementType);
        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.Expression).Returns(userList.Expression);
        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.GetEnumerator()).Returns(userList.GetEnumerator());

        dbsetLikes.As<IQueryable<LikesModel>>().Setup(pi => pi.Provider).Returns(likeList.Provider);
        dbsetLikes.As<IQueryable<LikesModel>>().Setup(pi => pi.ElementType).Returns(likeList.ElementType);
        dbsetLikes.As<IQueryable<LikesModel>>().Setup(pi => pi.Expression).Returns(likeList.Expression);
        dbsetLikes.As<IQueryable<LikesModel>>().Setup(pi => pi.GetEnumerator()).Returns(likeList.GetEnumerator());

        appDbContextMock.Setup(db => db.Likes).Returns(dbsetLikes.Object);
        appDbContextMock.Setup(db => db.Users).Returns(dbSetUsers.Object);
        appDbContextMock.Setup(db => db.Posts).Returns(dbSetPostsMock.Object);
        appDbContextMock.Setup(db => db.Posts_images).Returns(dbSetPostImagesMock.Object);


        var result = postRepository.listPostsUserLike(userModelTest.id);

        Assert.IsType<List<PostsLikeListLinq>>(result);







    }
    [Fact]
    public void should_to_list_next_posts()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "teste").Options;

        var appDbContextMock = new Mock<AppDbContext>(options);
        var postRepository = new PostRepository(appDbContextMock.Object);

        var dbSetPostsMock = new Mock<DbSet<PostModel>>();
        var dbSetPostImagesMock = new Mock<DbSet<PostImagesModel>>();
        var dbSetUsers = new Mock<DbSet<UserModel>>();
        var dbsetLikes = new Mock<DbSet<LikesModel>>();
        PostModel postModel = new PostModel();
        postModel.id = Guid.NewGuid();
        postModel.datePost = DateTime.UtcNow;
        postModel.totalComments = 0;
        postModel.totalLikes = 0;
        postModel.userId = Guid.NewGuid();

        PostImagesModel postImagesModel = new PostImagesModel();
        postImagesModel.id = Guid.NewGuid();
        postImagesModel.imgUrl = "urlteste";
        postImagesModel.postId = postModel.id;

        UserModel userModelTest = new UserModel();
        userModelTest.id = Guid.NewGuid();
        userModelTest.email = "erickjb93@gmail.com";
        userModelTest.password = "Sirlei231";
        userModelTest.userName = "erick";
        userModelTest.telephone = "77799591703";
        userModelTest.userPhoto = "llll";

        var LikesModel = new LikesModel();
        LikesModel.id = Guid.NewGuid();
        LikesModel.postId = postModel.id;
        LikesModel.userId = userModelTest.id;

        var postList = new List<PostModel> { postModel }.AsQueryable();
        var likeList = new List<LikesModel> { LikesModel }.AsQueryable();
        var postImageList = new List<PostImagesModel> { postImagesModel }.AsQueryable();
        var userList = new List<UserModel> { userModelTest }.AsQueryable();

        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.Provider).Returns(postList.Provider);
        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.ElementType).Returns(postList.ElementType);
        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.Expression).Returns(postList.Expression);
        dbSetPostsMock.As<IQueryable<PostModel>>().Setup(p => p.GetEnumerator()).Returns(postList.GetEnumerator());

        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.Provider).Returns(postImageList.Provider);
        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.ElementType).Returns(postImageList.ElementType);
        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.Expression).Returns(postImageList.Expression);
        dbSetPostImagesMock.As<IQueryable<PostImagesModel>>().Setup(pi => pi.GetEnumerator()).Returns(postImageList.GetEnumerator());

        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.Provider).Returns(userList.Provider);
        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.ElementType).Returns(userList.ElementType);
        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.Expression).Returns(userList.Expression);
        dbSetUsers.As<IQueryable<UserModel>>().Setup(pi => pi.GetEnumerator()).Returns(userList.GetEnumerator());

        dbsetLikes.As<IQueryable<LikesModel>>().Setup(pi => pi.Provider).Returns(likeList.Provider);
        dbsetLikes.As<IQueryable<LikesModel>>().Setup(pi => pi.ElementType).Returns(likeList.ElementType);
        dbsetLikes.As<IQueryable<LikesModel>>().Setup(pi => pi.Expression).Returns(likeList.Expression);
        dbsetLikes.As<IQueryable<LikesModel>>().Setup(pi => pi.GetEnumerator()).Returns(likeList.GetEnumerator());

        appDbContextMock.Setup(db => db.Likes).Returns(dbsetLikes.Object);
        appDbContextMock.Setup(db => db.Users).Returns(dbSetUsers.Object);
        appDbContextMock.Setup(db => db.Posts).Returns(dbSetPostsMock.Object);
        appDbContextMock.Setup(db => db.Posts_images).Returns(dbSetPostImagesMock.Object);

        var date = new DateTime();

        var result = postRepository.listPostsSeeMore(userModelTest.id, date);

        Assert.IsType<List<PostsLinq>>(result);

    }


}