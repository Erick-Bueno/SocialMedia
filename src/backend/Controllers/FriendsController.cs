namespace FriendsController.Controllers
{
    using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<FriendsModel>> addFriend([FromBody] FriendsDto friends)
        {
            var newFriend = await friendsService.addFriends(friends);
            return Ok(newFriend);
        }
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<FriendsModel> listFriends([FromRoute] Guid id){
            var listUserFriends = friendsService.listUserFriends(id);
            return Ok(listUserFriends);
        }
    }
}