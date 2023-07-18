using System.ComponentModel.DataAnnotations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        this.userRepository = userRepository;
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
        var findedPost = await postRepository.findPost(commentModel.postId);
        if (findedPost == null)
        {
            throw new ValidationException("Postagem não encontrada");
        }
        var findedUser = await userRepository.findUser(commentModel.userId);
        if(findedUser == null)
        {
            throw new ValidationException("Usuário não encontrada");
        }
        var newComment = await commentRepository.createComment(commentModel);
        var responseCommentCreated = new Response<CommentModel>(200, "Comentario adicionado");

        await postRepository.updateTotalComments(findedPost);
        return responseCommentCreated;
    }

    public List<UserCommentsLinq> listComment(Guid idPost)
    {
       var listComment = commentRepository.listComment(idPost);
       return listComment;
    }
}