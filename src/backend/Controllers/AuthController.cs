namespace auth.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly IAuthService authService;

        public Auth(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> login([FromBody] UserLoginDto loginDto)
        {
            try
            {
            var login = await authService.login(loginDto);
            return Ok(login);
            }
            catch (ValidationException ex)
            {
                ReponseErrorRegister responseError = new ReponseErrorRegister(400, ex.Message);
                return BadRequest(responseError);
            }
           
        }
        [HttpPost]
        public async Task<ActionResult<TokenModel>> refreshToken([FromBody] string jwt){
            try
            {
                var refreshToken = await authService.RefreshToken(jwt);
                return Ok(refreshToken);
            }
            catch (ValidationException ex)
            {
                 ResponseErrorAuth responseErrorAuth = new ResponseErrorAuth(400, ex.Message);
                 return BadRequest(responseErrorAuth);
            }
        }
    }
}