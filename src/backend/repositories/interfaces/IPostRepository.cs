public interface IPostRepository
{
    public Task<PostModel> findPost(Guid id);
    public Task updateTotalComments(PostModel post);
    public Task updateTotalLikes(PostModel post);
    public Task<PostModel> createPost(PostModel post);
    public Task<PostImagesModel> createPostImages(PostImagesModel postImages);
    public List<PostsLinq> listPosts();
}