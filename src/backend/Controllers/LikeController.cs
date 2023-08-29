namespace LikeController.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Like : ControllerBase
    {
        private readonly ILikeService likeService;

        public Like(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LikesModel>> createLike([FromBody] LikeDto like)
        {
            try
            {
                var responseLikeCreated = await likeService.createLike(like);
                return Ok(responseLikeCreated);
            }
            catch (ValidationException ex)
            {
                var responseError = new Response<LikesModel>(400, ex.Message);
                return BadRequest(responseError);
            }


        }
    }
}