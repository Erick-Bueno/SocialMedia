public interface IPostRepository
{
    public Task<PostModel> findPost(Guid id);
    public Task updateTotalComments(PostModel post);
    public Task updateTotalLikes(PostModel post);
}