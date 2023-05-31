using System.ComponentModel.DataAnnotations;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CommentModel> createComment(CommentModel comment)
    {
        try
        {
            var newComment = await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        catch (ValidationException ex)
        {
            throw new ValidationException(ex.Message);
        }

    }
}