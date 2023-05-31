using System.ComponentModel.DataAnnotations;

public class LikeService : ILikeService
{
    private readonly ILikeRepository likeRepository;
    private readonly IPostRepository postRepository;

    public LikeService(ILikeRepository likeRepository, IPostRepository postRepository)
    {
        this.likeRepository = likeRepository;
        this.postRepository = postRepository;
    }

    public LikesModel convertLikeDtoToLikeModel(LikeDto Like)
    {
        var likeModel = new LikesModel();
        likeModel.userId= Like.userId;
        likeModel.postId = Like.postId;
        return likeModel;
    }

    public async Task<Response<LikesModel>> createLike(LikeDto Like)
    {
        var LikeModel = convertLikeDtoToLikeModel(Like);
        var newComment = await likeRepository.createLike(LikeModel);
        var responseLikeCreated = new Response<LikesModel>(200, "Comentario adicionado");
        var findedPost = await postRepository.findPost(newComment.postId);
        if(findedPost == null){
            throw new ValidationException("Postagem não encontrada");
        }
        await postRepository.updateTotalComments(findedPost);
        return responseLikeCreated;
    }
}