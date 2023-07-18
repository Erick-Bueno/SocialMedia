namespace User.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Common;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService userService { get; set; }
        public IFriendsService friendsService { get; set; }

        public UserController(IUserService userService, IFriendsService friendsService)
        {
            this.userService = userService;
            this.friendsService = friendsService;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> registerUser([FromForm] UserRegisterDto user, IFormFile? userimagefile)
        {
            try
            {
                var userRegistered = await userService.register(user, userimagefile);
                return Ok(userRegistered);
            }
            catch (ValidationException ex)
            {
                var errorUserRegistered = new Response<UserModel>(400, ex.Message);
                return BadRequest(errorUserRegistered);


            }
            catch (DbException ex)
            {
                var errorUserRegistered = new Response<UserModel>(500, ex.Message);
                return StatusCode(500, errorUserRegistered);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> findUser([FromRoute] Guid id)
        {
            try
            {
                var userData = await userService.findUser(id);
                var countFriends = friendsService.findFriends(id);
                var responseFindedUser = new ResponseUserFinded(200, "Usuario encontrado", userData.userName, userData.userPhoto, countFriends);
                return Ok(responseFindedUser);
            }
            catch (System.NullReferenceException ex)
            {
                var responseError = new Response<UserModel>(400, ex.Message);
                return BadRequest(responseError);
            }

        }
    }
}