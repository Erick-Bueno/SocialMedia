public interface ILikeRepository
{
     public Task<LikesModel> createLike(LikesModel Like);
}