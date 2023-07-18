public interface ILikeRepository
{
     public Task<LikesModel> createLike(LikesModel Like);
     public LikesModel findLike(Guid userId, Guid postId);

     public Task<bool> removeLike(LikesModel likes);
}