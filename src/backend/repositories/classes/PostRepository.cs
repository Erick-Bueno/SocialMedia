public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PostModel> findPost(Guid id)
    {
        var findedPost = await _context.Posts.FindAsync(id);
        return findedPost;
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