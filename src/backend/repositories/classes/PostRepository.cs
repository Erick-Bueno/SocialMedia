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

    public List<PostsLinq> listPosts()
    {
        var ListPosts = (
        from post in _context.Posts 
        join post_image in _context.Posts_images on post.id equals post_image.postId 
        into postImages from post_image in postImages.DefaultIfEmpty() 
        join user in _context.Users on post.userId equals user.id
        group post_image by new {
            post.id,
            post.contentPost,
            post.datePost,
            post.totalComments,
            post.totalLikes,
            user.userName,
            user.userPhoto
        } into p
        orderby p.Key.datePost descending
        select new PostsLinq 
        { 
        postId = p.Key.id,
        contentPost = p.Key.contentPost, 
        postImages = p.Select(pi => pi.imgUrl).ToList(),
        postDate =  p.Key.datePost, 
        totalComments =  p.Key.totalComments, 
        totalLikes = p.Key.totalLikes,
        userName = p.Key.userName,
        userPhoto = p.Key.userPhoto
        }).ToList();
        return ListPosts;
    }

    public async Task updateTotalComments(PostModel post)
    {
        post.totalComments += 1;
        await _context.SaveChangesAsync();

    }

    public async Task updateTotalLikes(PostModel post)
    {
        post.totalLikes += 1;
        await _context.SaveChangesAsync();
    }

}