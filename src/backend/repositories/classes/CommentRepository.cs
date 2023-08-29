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

    public List<UserCommentsLinq> listComment(Guid idPost)
    {
        var listComment = (from comment in _context.Comments
                           join user in _context.Users on comment.userId equals user.id
                           where comment.postId == idPost
                           select new UserCommentsLinq
                           {
                               userName = user.userName,
                               comment = comment.comment,
                               userPhoto = user.userPhoto
                           }).Take(4).ToList();

        return listComment;
    }

    public List<UserCommentsLinq> listCommentSeeMore(Guid idPost, DateTime date)
    {
        var listCommentSeeMore = (from comment in _context.Comments
                           join user in _context.Users on comment.userId equals user.id
                           where comment.postId == idPost && comment.dateComment < date
                           select new UserCommentsLinq
                           {
                               userName = user.userName,
                               comment = comment.comment,
                               userPhoto = user.userPhoto,
                               dateComment = comment.dateComment
                           }).Take(4).ToList();

        return listCommentSeeMore;
    }
}