namespace PostController.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<PostModel>> createPost([FromForm] PostDto postDto, [FromForm] PostImagesDto? postImages)
        {
            try
            {
                var newPost = await postService.createPost(postDto, postImages);
                return Created("https://localhost:7088/api/Post", newPost);
            }
            catch (ValidationException ex)
            {
                Response<PostModel> responseError = new Response<PostModel>(400, ex.Message);
                return BadRequest(responseError);
            }
            catch (DbUpdateException ex)
            {
                Response<PostModel> responseError = new Response<PostModel>(500, ex.Message);
                return BadRequest(responseError);
            }

        }
        [Route("list/{id?}")]
        [HttpGet]

        public ActionResult<PostModel> listPosts([FromRoute] Guid id)
        {

            var listPosts = postService.listPosts(id);
            return Ok(listPosts);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PostModel>> listPostsUserLiked([FromRoute] Guid id)
        {
            try
            {
                var listPostsUserLiked = await postService.listPostsUserLike(id);
                return Ok(listPostsUserLiked);
            }
            catch (ValidationException ex)
            {
                Response<PostModel> responseError = new Response<PostModel>(400, ex.Message);
                return BadRequest(responseError);
            }

        }
        [HttpPost("seemore/{id?}")]
        public ActionResult<PostModel> listPostsSeeMore([FromRoute] Guid id, [FromBody] string date)
        {

            var listPostsSeeMore = postService.listPostsSeeMore(date, id);
            return Ok(listPostsSeeMore);
        }
        [HttpGet("seemorelike/{id}")]
        [Authorize]
        public async Task<ActionResult<PostModel>> listPostsUserLikeSeeMore([FromRoute] Guid id, [FromBody] string date)
        {
            var listPostsUserLiked = postService.listPostsUserLikeSeeMore(id, date);
            return Ok(listPostsUserLiked);
        }
        [HttpGet("userposts/{id}")]
        [Authorize]
        public ActionResult<PostModel> listPostUserCreated([FromRoute] Guid id)
        {
            var listPostUserCreated = postService.listPostsUserCreated(id);
            return Ok(listPostUserCreated);
        }
        [HttpPost("userpostseemore/{id}")]
        [Authorize]
        public ActionResult<PostModel> listPostUserCreatedSeeMore([FromRoute] Guid id, [FromBody] string date)
        {
            var listPostUserCreatedSeeMore = postService.listPostsUserCreatedSeeMore(id, date);
            return Ok(listPostUserCreatedSeeMore);
        }
        [HttpPost("searchfisrt")]
        public ActionResult<List<PostModel>> findFiveFirstPostsSearched([FromBody] SearchedDataDto postData)
        {   
            
            var listFirstPostsUserSearched = postService.findFiveFirstPostsSearched(postData.name, postData.userId);
            return Ok(listFirstPostsUserSearched);
        }
        [HttpPost("search")]
        public ActionResult<List<PostModel>> findPostsSearchedScrolling([FromBody] NextPostsSearched postData)
        {
            var listNextPostsUserSearched = postService.findPostsSearchedScrolling(postData.userId, postData.date, postData.name);
            return Ok(listNextPostsUserSearched);
        }
    }
}