namespace Comment.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult<CommentModel>> createComment([FromBody] CommentDto comment)
        {
            try
            {
                var ResponseCommentCreated = await commentService.createComment(comment);
                return Ok(ResponseCommentCreated);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}