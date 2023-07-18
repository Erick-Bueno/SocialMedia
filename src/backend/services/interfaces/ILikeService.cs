public interface ILikeService
{
    public Task<Response<LikesModel>> createLike(LikeDto Like);
    public LikesModel convertLikeDtoToLikeModel(LikeDto Like);
    
}