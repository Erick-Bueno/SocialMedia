public interface IPostService
{
    public Task<Response<PostModel>> createPost(PostDto post, PostImagesDto postImages);

    public PostModel convertPostDtoToPostModel(PostDto post);

    public PostImagesModel toCreatePostImagesModel(string urlImg, Guid postId);

    public Task savePostImages(string imgName, IFormFile img);

    public List<PostsLinq> listPosts(Guid id);

    public Task<List<PostsLikeListLinq>> listPostsUserLike(Guid id);

    public List<PostsLinq> listPostsSeeMore(Guid id, string date);

}