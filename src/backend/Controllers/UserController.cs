namespace User.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        public IUserService userService { get; set; }

        public User(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> RegisterUser([FromBody] UserRegisterDto user)
        {
            var user_registered = await userService.register(user);
            return Ok(user_registered);
        }
    }
}