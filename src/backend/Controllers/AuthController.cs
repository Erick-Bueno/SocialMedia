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

        [HttpPost]
        public async Task<ActionResult<UserModel>> login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var login = await authService.login(loginDto);
                return Ok(login);
            }
            catch (ValidationException ex)
            {
                var responseError = new Response<UserModel>(400, ex.Message);
                return BadRequest(responseError);
            }

        }
        [HttpPost]
        [Route("/refresh")]
        public async Task<ActionResult<TokenModel>> refreshToken([FromBody] jwtDto jwt)
        {
            try
            {

                var refreshToken = await authService.refreshToken(jwt.jwt);
                return Ok(refreshToken);
            }
            catch (ValidationException ex)
            {
                var responseErrorAuth = new Response<TokenModel>(400, ex.Message);
                return BadRequest(responseErrorAuth);
            }
        }
    }
}