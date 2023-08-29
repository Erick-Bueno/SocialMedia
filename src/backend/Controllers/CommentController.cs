namespace CommentController.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Comment : ControllerBase
    {
        private readonly ICommentService commentService;

        public Comment(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentModel>> createComment([FromBody] CommentDto comment)
        {
            try
            {
                var ResponseCommentCreated = await commentService.createComment(comment);
                return Ok(ResponseCommentCreated);
            }
            catch (ValidationException ex)
            {
                var responseError = new Response<CommentModel>(400, ex.Message);
                return BadRequest(responseError);
            }


        }
        [HttpGet("{id}")]
        public ActionResult<CommentModel> listComment([FromRoute] Guid id){
            var listComments = commentService.listComment(id);
            return Ok(listComments);
        }
        [HttpPost("seemore/{id}")]
        public ActionResult<CommentModel> listCommentSeeMore([FromRoute] Guid id, [FromBody] string date){
            var listComments = commentService.listCommentSeeMore(id, date);
            return Ok(listComments);
        }
    }
}