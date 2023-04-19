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
        public async Task<ActionResult<UserModel>> RegisterUser([FromForm] UserRegisterDto user, IFormFile userimagefile)
        {
            try
            {
                var user_registered = await userService.register(user, userimagefile);
                return Ok(user_registered);
            }
            catch (System.Exception ex)
            {
                 var error_user_registered = new ReponseErrorRegister(400, ex.Message);
                 return BadRequest(error_user_registered);

            }
            
        }
    }
}