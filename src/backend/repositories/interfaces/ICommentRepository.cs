public interface ICommentRepository
{
    public Task<CommentModel> createComment(CommentModel comment);
    
}