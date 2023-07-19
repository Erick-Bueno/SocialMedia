

using System.ComponentModel.DataAnnotations;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IUserRepository userRepository;
    public PostService(IPostRepository postRepository, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.webHostEnvironment = webHostEnvironment;
        this.userRepository = userRepository;
    }

    public PostModel convertPostDtoToPostModel(PostDto post)
    {
        var postModel = new PostModel();
        postModel.contentPost = post.contentPost;
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
      

        if(postImages.imgUrl?.Count == null && string.IsNullOrEmpty(post.contentPost)){
            throw new ValidationException("Impossivel criar uma postagem vazia");
        }
        if (postImages.imgUrl != null && postImages.imgUrl.Count > 4)
        {
            throw new ValidationException("informe um numero valido de imagens, numero maximo de imagens por post é 4");
        }
        var postModel = convertPostDtoToPostModel(post);
        var newPost = await postRepository.createPost(postModel);



        if (postImages.imgUrl != null && postImages.imgUrl.Count > 0 && postImages.imgUrl.Count <= 4)

        {

            foreach (var postImage in postImages.imgUrl)
            {

                if (postImage.ContentType != "image/png" && postImage.ContentType != "image/jpg" && postImage.ContentType != "image/jpeg")
                {
                    throw new ValidationException("Informe imagens validas, os formatos aceitos são jpeg e png");
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

    public List<PostsLinq> listPosts(Guid id)
    {
        var listPosts = postRepository.listPosts(id);
        return listPosts;
    }
    public async Task<List<PostsLikeListLinq>> listPostsUserLike(Guid id){
        var findUser = await userRepository.findUser(id);
        if(findUser == null){
            throw new ValidationException("Úsuario não existe");
        }
        var listPostsUserLike = postRepository.listPostsUserLike(id);
        return listPostsUserLike;
    }

    public List<PostsLinq> listPostsSeeMore(string date, Guid id)
    {
       var convertedData = DateTime.Parse(date);
       var listPostsSeeMore = postRepository.listPostsSeeMore(convertedData,id);
       return listPostsSeeMore;
    }
}