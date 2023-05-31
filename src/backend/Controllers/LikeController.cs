namespace Like.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService likeService;

        public LikeController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpPost]
        public async Task<ActionResult<LikesModel>> createLike([FromBody] LikeDto like)
        {
            try
            {
                var responseLikeCreated = await likeService.createLike(like);
                return Ok(responseLikeCreated);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}