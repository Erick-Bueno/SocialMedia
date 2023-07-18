public interface IPostRepository
{
    public Task<PostModel> findPost(Guid id);
    public Task updateTotalComments(PostModel post);
    public Task updateTotalLikes(PostModel post, int increment);
    public Task<PostModel> createPost(PostModel post);
    public Task<PostImagesModel> createPostImages(PostImagesModel postImages);
    public List<PostsLinq> listPosts(Guid id);
    public List<PostsLikeListLinq> listPostsUserLike(Guid id);
    public List<PostsLinq> listPostsSeeMore(Guid id, DateTime data);
    
}