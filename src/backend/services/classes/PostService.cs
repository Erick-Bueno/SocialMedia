

using System.ComponentModel.DataAnnotations;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;
    private readonly IWebHostEnvironment webHostEnvironment;
    public PostService(IPostRepository postRepository, IWebHostEnvironment webHostEnvironment)
    {
        this.postRepository = postRepository;
        this.webHostEnvironment = webHostEnvironment;
    }

    public PostModel convertPostDtoToPostModel(PostDto post)
    {
        var postModel = new PostModel();
        postModel.contentPost = post.contentPost;
        postModel.totalComments = post.totalComments;
        postModel.totalLikes = post.totalLikes;
        postModel.userId = post.userId;
        return postModel;
    }

    public PostImagesModel toCreatePostImagesModel(string urlImg, Guid postId)
    {
        PostImagesModel postImagesModel = new PostImagesModel();
        postImagesModel.postId = postId;
        postImagesModel.imgUrl = urlImg;
        return postImagesModel;
    }

    public async Task<Response<PostModel>> createPost(PostDto post, PostImagesDto postImages)
    {
        if (postImages.imgUrl != null && postImages.imgUrl.Count > 4)
        {
            throw new ValidationException("informe um numero valido de imagens");
        }
        var postModel = convertPostDtoToPostModel(post);
        var newPost = await postRepository.createPost(postModel);



        if (postImages.imgUrl != null && postImages.imgUrl.Count > 0 && postImages.imgUrl.Count <= 4)

        {

            foreach (var postImage in postImages.imgUrl)
            {

                if (postImage.ContentType != "image/png" && postImage.ContentType != "image/jpg" && postImage.ContentType != "image/jpeg")
                {
                    throw new ValidationException("Informe imagens validas");
                }
                var NameImg = Guid.NewGuid() + "_" + postImage.FileName;
                var urlImg = "https://localhost:7088/PostsImages/" + NameImg;
                await savePostImages(NameImg, postImage);
                var postImageModel = toCreatePostImagesModel(urlImg, postModel.id);
                var newPostImage = await postRepository.createPostImages(postImageModel);
            }

        }
        Response<PostModel> response = new Response<PostModel>(200, "Postagem concluida");
        return response;
    }


    public async Task savePostImages(string imgName, IFormFile img)
    {
        if (!Directory.Exists(webHostEnvironment.WebRootPath + "//PostsImages//"))
        {
            Directory.CreateDirectory(webHostEnvironment.WebRootPath + "//PostsImages//");
        }
        using (var imgPost = File.Create(webHostEnvironment.WebRootPath + "//PostsImages//" + imgName))
        {
            await img.CopyToAsync(imgPost);
        }
    }

    public List<PostsLinq> listPosts()
    {
        var listPosts = postRepository.listPosts();
        return listPosts;
    }
}