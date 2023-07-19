using Microsoft.EntityFrameworkCore;
using MySqlConnector;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PostModel> createPost(PostModel post)
    {
        try
        {
            var newPost = await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("erro ao criar post");
        }

    }

    public async Task<PostImagesModel> createPostImages(PostImagesModel postImages)
    {
        var newPostImage = await _context.AddAsync(postImages);
        await _context.SaveChangesAsync();
        return postImages;
    }

    public async Task<PostModel> findPost(Guid id)
    {
        var findedPost = await _context.Posts.FindAsync(id);
        return findedPost;
    }

    public List<PostsLinq> listPosts(Guid id = default(Guid))
    {
        var userId = id;
        var ListPosts = (
        from post in _context.Posts
        join post_image in _context.Posts_images on post.id equals post_image.postId
        into postImages
        from post_image in postImages.DefaultIfEmpty()
        join user in _context.Users on post.userId equals user.id
        group post_image by new
        {
            post.id,
            post.contentPost,
            post.datePost,
            post.totalComments,
            post.totalLikes,
            user.userName,
            user.userPhoto,
        } into p
        orderby p.Key.datePost descending
        select new PostsLinq
        {
            postId = p.Key.id,
            contentPost = p.Key.contentPost,
            postImages = p.Select(pi => pi.imgUrl).ToList(),
            postDate = p.Key.datePost.AddHours(-3).ToLongTimeString(),
            totalComments = p.Key.totalComments,
            totalLikes = p.Key.totalLikes,
            userName = p.Key.userName,
            userPhoto = p.Key.userPhoto,

        }).ToList();

        var ListPostsLikes = (
        from post in ListPosts
        join like in _context.Likes.Where(l => l.userId == userId) on post.postId equals like.postId into likes
        from like in likes.DefaultIfEmpty()
        select new PostsLinq
        {
            postId = post.postId,
            contentPost = post.contentPost,
            postImages = post.postImages,
            postDate = post.postDate,
            totalComments = post.totalComments,
            totalLikes = post.totalLikes,
            userName = post.userName,
            userPhoto = post.userPhoto,
            isfavorited = like != null
        }).Take(5).ToList();
        return ListPostsLikes;
    }

    public async Task updateTotalComments(PostModel post)
    {
        post.totalComments += 1;
        await _context.SaveChangesAsync();

    }

    public async Task updateTotalLikes(PostModel post, int increment)
    {
        post.totalLikes += increment;
        await _context.SaveChangesAsync();
    }
    public List<PostsLikeListLinq> listPostsUserLike(Guid id)
    {
        var ListUserLikePosts = (
        from post in _context.Posts
        join post_image in _context.Posts_images on post.id equals post_image.postId
        into postImages
        from post_image in postImages.DefaultIfEmpty()
        join user in _context.Users on post.userId equals user.id
        join like in _context.Likes on post.id equals like.postId
        group post_image by new
        {
            post.id,
            post.contentPost,
            post.datePost,
            post.totalComments,
            post.totalLikes,
            user.userName,
            user.userPhoto,
            like.userId
        } into p
        orderby p.Key.datePost descending
        select new PostsLikeListLinq
        {
            postId = p.Key.id,
            likeUserId = p.Key.userId,
            contentPost = p.Key.contentPost,
            postImages = p.Select(pi => pi.imgUrl).ToList(),
            postDate = p.Key.datePost.AddHours(-3).ToLongTimeString(),
            totalComments = p.Key.totalComments,
            totalLikes = p.Key.totalLikes,
            userName = p.Key.userName,
            userPhoto = p.Key.userPhoto,

        }).Where(l => l.likeUserId == id).Take(5).ToList();
        return ListUserLikePosts;
    }
     public List<PostsLinq> listPostsSeeMore( DateTime data, Guid id = default(Guid)){
        var userId = id;
        var ListPosts = (
        from post in _context.Posts
        join post_image in _context.Posts_images on post.id equals post_image.postId
        into postImages
        from post_image in postImages.DefaultIfEmpty()
        join user in _context.Users on post.userId equals user.id
        group post_image by new
        {
            post.id,
            post.contentPost,
            post.datePost,
            post.totalComments,
            post.totalLikes,
            user.userName,
            user.userPhoto,
        } into p
        orderby p.Key.datePost descending
        where p.Key.datePost < data
        select new PostsLinq
        {
            postId = p.Key.id,
            contentPost = p.Key.contentPost,
            postImages = p.Select(pi => pi.imgUrl).ToList(),
            postDate = p.Key.datePost.AddHours(-3).ToLongTimeString(),
            totalComments = p.Key.totalComments,
            totalLikes = p.Key.totalLikes,
            userName = p.Key.userName,
            userPhoto = p.Key.userPhoto,

        }).ToList();

        var ListPostsLikes = (
        from post in ListPosts
        join like in _context.Likes.Where(l => l.userId == userId) on post.postId equals like.postId into likes
        from like in likes.DefaultIfEmpty()
        select new PostsLinq
        {
            postId = post.postId,
            contentPost = post.contentPost,
            postImages = post.postImages,
            postDate = post.postDate,
            totalComments = post.totalComments,
            totalLikes = post.totalLikes,
            userName = post.userName,
            userPhoto = post.userPhoto,
            isfavorited = like != null
        }).Take(5).ToList();
        return ListPostsLikes;
     }

}