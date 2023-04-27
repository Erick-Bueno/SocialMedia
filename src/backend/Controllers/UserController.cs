namespace User.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

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
            catch (ValidationException ex)
            {
                 var error_user_registered = new ReponseErrorRegister(400, ex.Message);
                 return BadRequest(error_user_registered);
                

            }
            
        }
    }
}