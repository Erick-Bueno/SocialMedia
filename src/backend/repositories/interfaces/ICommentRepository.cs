public interface ICommentRepository
{
    public Task<CommentModel> createComment(CommentModel comment);
    public List<UserCommentsLinq> listComment(Guid idPost);
    public List<UserCommentsLinq> listCommentSeeMore(Guid idPost, DateTime date);
    
}