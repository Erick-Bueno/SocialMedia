public interface IPostService
{
    public Task<Response<PostModel>> createPost(PostDto post, PostImagesDto postImages);

    public PostModel convertPostDtoToPostModel(PostDto post);

    public PostImagesModel toCreatePostImagesModel(string urlImg, Guid postId);

    public Task savePostImages(string imgName, IFormFile img);

    public List<PostsLinq> listPosts(Guid id);

    public Task<List<PostsLikeListLinq>> listPostsUserLike(Guid id);

    public List<PostsLinq> listPostsSeeMore( string date, Guid id);
    public List<PostsLikeListLinq> listPostsUserLikeSeeMore(Guid id, string date);
    public List<PostsLinq> listPostsUserCreated(Guid id);
    public List<PostsLinq> listPostsUserCreatedSeeMore(Guid id, string date);
    public List<PostsLinq> findFiveFirstPostsSearched(string name, Guid? userId);  
    public List<PostsLinq> findPostsSearchedScrolling(Guid? userId, string date, string name);

}