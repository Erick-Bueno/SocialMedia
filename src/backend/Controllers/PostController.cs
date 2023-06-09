namespace PostController.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class Post : ControllerBase
    {
        private readonly IPostService postService;

        public Post(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        public async Task<ActionResult<PostModel>> createPost([FromForm] PostDto postDto, [FromForm] PostImagesDto? postImages)
        {
            try
            {
                var newPost = await postService.createPost(postDto, postImages);
                return Created("https://localhost:7088/api/Post",newPost);
            }
            catch (ValidationException ex)
            {
                Response<PostModel> responseError = new Response<PostModel>(400, ex.Message);
                return BadRequest(responseError); 
            }
            catch(DbUpdateException ex){
                 Response<PostModel> responseError = new Response<PostModel>(500, ex.Message);
                 return BadRequest(responseError); 
            }

        }
        [HttpGet]
        public ActionResult<PostModel> listPosts(){
            var listPosts = postService.listPosts();
            return Ok(listPosts);
        }
    }
}