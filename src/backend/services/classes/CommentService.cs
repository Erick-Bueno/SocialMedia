using System.ComponentModel.DataAnnotations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    public CommentModel convertCommentDtoToCommentModel(CommentDto comment)
    {
        var CommentModel = new CommentModel();
        CommentModel.comment = comment.comment;
        CommentModel.postId = comment.postId;
        CommentModel.userId = comment.userId;

        return CommentModel;
    }

    public async Task<Response<CommentModel>> createComment(CommentDto comment)
    {
        var commentModel = convertCommentDtoToCommentModel(comment);
        var newComment = await commentRepository.createComment(commentModel);
        var responseCommentCreated = new Response<CommentModel>(200, "Comentario adicionado");
        var findedPost = await postRepository.findPost(newComment.postId);
        if(findedPost == null){
            throw new ValidationException("Postagem não encontrada");
        }
        await postRepository.updateTotalComments(findedPost);
        return responseCommentCreated;
    }
}