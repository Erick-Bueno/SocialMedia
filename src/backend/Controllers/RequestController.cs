namespace RequestController.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Request : ControllerBase
    {
        private readonly IRequestService requestService;

        public Request(IRequestService requestService){
            this.requestService = requestService;
        }
        [HttpGet("{id}")]
        public ActionResult<List<RequestsModel>> listRequests([FromRoute] Guid id)
        {
            var listRequests = requestService.listRequests(id);
            return Ok(listRequests);
        }
        [HttpPost]
        public ActionResult<Boolean> deleteRequest([FromBody] RequestDto requestDto){
            var deleteRequest = requestService.deleteRequest(requestDto);
            return Ok(deleteRequest);
        }
    }
}