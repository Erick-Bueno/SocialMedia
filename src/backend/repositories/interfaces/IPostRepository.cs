public interface IPostRepository
{
    public Task<PostModel> findPost(Guid id);
    public Task updateTotalComments(PostModel post);
    public Task updateTotalLikes(PostModel post, int increment);
    public Task<PostModel> createPost(PostModel post);
    public Task<PostImagesModel> createPostImages(PostImagesModel postImages);
    public List<PostsLinq> listPosts(Guid id);
    public List<PostsLikeListLinq> listPostsUserLike(Guid id);
    public List<PostsLinq> listPostsSeeMore(DateTime data, Guid id);
    public List<PostsLikeListLinq> listPostsUserLikeSeeMore(Guid id, DateTime date);
    public List<PostsLinq> listPostsUserCreated(Guid id);
    public List<PostsLinq> listPostsUserCreatedSeeMore(Guid id, DateTime date);
    public List<PostsLinq> findFiveFirstPostsSearched(string name, Guid? userId);  
    public List<PostsLinq> findPostsSearchedScrolling(DateTime date, string name, Guid? userId);

    
}