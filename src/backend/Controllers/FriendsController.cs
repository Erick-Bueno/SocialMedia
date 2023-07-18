namespace FriendsController.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Friends : ControllerBase
    {
        private readonly IFriendsService friendsService;

        public Friends(IFriendsService friendsService)
        {
            this.friendsService = friendsService;
        }

        [HttpPost]
        public async Task<ActionResult<FriendsModel>> addFriend([FromBody] FriendsDto friends)
        {
            var newFriend = await friendsService.addFriends(friends);
            return Ok(newFriend);
        }
        [HttpGet("{id}")]
        public ActionResult<FriendsModel> listFriends([FromRoute] Guid id){
            var listUserFriends = friendsService.listUserFriends(id);
            return Ok(listUserFriends);
        }
    }
}