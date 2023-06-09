public interface IPostService
{
    public Task<Response<PostModel>> createPost(PostDto post, PostImagesDto postImages);

    public PostModel convertPostDtoToPostModel(PostDto post);

    public PostImagesModel toCreatePostImagesModel(string urlImg, Guid postId);

    public Task savePostImages(string imgName,IFormFile img);

    public List<PostsLinq> listPosts ();
   
}