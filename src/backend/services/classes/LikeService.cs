using System.ComponentModel.DataAnnotations;

public class LikeService : ILikeService
{
    private readonly ILikeRepository likeRepository;
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public LikeService(ILikeRepository likeRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        this.likeRepository = likeRepository;
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public LikesModel convertLikeDtoToLikeModel(LikeDto Like)
    {
        var likeModel = new LikesModel();
        likeModel.userId = Like.userId;
        likeModel.postId = Like.postId;
        return likeModel;
    }

    public async Task<Response<LikesModel>> createLike(LikeDto Like)
    {
        var LikeModel = convertLikeDtoToLikeModel(Like);
        var findedPost = await postRepository.findPost(LikeModel.postId);
        if (findedPost == null)
        {
            throw new ValidationException("Postagem não encontrada");
        }
        var findedLike = likeRepository.findLike(Like.userId, Like.postId);
        if (findedLike != null)
        {
            var removeLike = await likeRepository.removeLike(findedLike);
            postRepository.updateTotalLikes(findedPost, -1);
            var responseLikeRemoved = new Response<LikesModel>(200, "Like removido");
            return responseLikeRemoved;
        }
        var findedUser = await userRepository.findUser(LikeModel.userId);
        if (findedUser == null)
        {
            throw new ValidationException("Usuário não encontrada");
        }
        var newComment = await likeRepository.createLike(LikeModel);
        var responseLikeCreated = new Response<LikesModel>(200, "Like adicionado");

        await postRepository.updateTotalLikes(findedPost, 1);
        return responseLikeCreated;
    }

  
}