using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts.Persistence;
using Shared;
using System.Net;
using Application.Contracts.ServiceInterfaces;
using Infrastructure.Services;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(IUserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // we handle with exception middleware no try cach here
            //try
            //{
                var user = await _userService.RegisterUserAsync(model);
                var token = _tokenService.GenerateToken(model.Role,user);
                return Ok(new { Token = token });
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //var response = new ResponseModel<TokenData>();
            //try
            //{

            //    if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            //    {
            //        response.statusCode = (int)HttpStatusCode.BadRequest;
            //        response.message = "Username or password is invalid";
            //        return BadRequest(response);
            //    }
            //    var token = await _userService.LoginUserAsync(model);
            //    if (token == null)
            //    {
            //        response.statusCode = (int)HttpStatusCode.Unauthorized;
            //        response.message = "The user could not be authenticated";
            //        return Unauthorized(response);
            //    }
            //    response.statusCode = (int)HttpStatusCode.OK;
            //    response.data = new TokenData { access_token = token, refresh_token = null };
            //    response.message = "User authenticated successfully";
            //    return Ok(response);

            //}
            //catch (Exception ex)
            //{
            //    response.statusCode = (int)HttpStatusCode.InternalServerError;
            //    response.message = ex.Message;
            //    return StatusCode(500, response);
            //}
            try
            {
                var user = await _userService.LoginUserAsync(model);
                var token = _tokenService.GenerateToken(user.Role,user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }

        //[Authorize(Policy = "SuperUsersPolicy")]
        //[HttpHead("auth")]
        //public ActionResult Authentication()
        //{
        //    try
        //    {
        //        return StatusCode((int)HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500);
        //    }
        //}
    
}
