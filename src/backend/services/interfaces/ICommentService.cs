public interface ICommentService
{
    public Task<Response<CommentModel>> createComment(CommentDto comment);
    public CommentModel convertCommentDtoToCommentModel(CommentDto comment);
    public List<UserCommentsLinq> listComment(Guid idPost);
}