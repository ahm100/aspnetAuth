using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Application.Features.Users.Dtos;
using Microsoft.Extensions.Localization;
using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Shared.SharedResources;
using Application.Services;
using Domain.Entities.Concrete;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<UsersController> _localizer;
        private readonly IStringLocalizer<SharedTranslate> _sharedLocalizer;
        private readonly CultureService _cultureService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator,
            IStringLocalizer<UsersController> localizer,
            IStringLocalizer<SharedTranslate> sharedLocalizer,
            CultureService cultureService,
            ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
            _cultureService = cultureService;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id, [FromQuery] string culture = "fa")
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //[HttpGet("with-related-data/{id}")]
        //public async Task<ActionResult<UserDto>> GetUserByIdWithRelatedData(int id)
        //{
        //    var user = await _mediator.Send(new GetUserByIdWithRelatedDataQuery(id));
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

        
        [HttpPost]
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserCommand command, [FromQuery] string culture = "fa")
        {
            var currentCulture = new CultureInfo(culture);
            CultureInfo.CurrentCulture = currentCulture;
            CultureInfo.CurrentUICulture = currentCulture;

            try
            {
                var userId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetUserById), new { id = userId }, userId);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogError($"Validation error: {string.Join(", ", errors)}");
                return BadRequest(new { Errors = errors });
                //return BadRequest(new { Errors = _localizer["GeneralError"] });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create user: {ex.Message}");
                return StatusCode(500, _localizer["InternalServerError"]);
            }
        }

        
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _mediator.Send(command);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    await _mediator.Send(new DeleteUserCommand { Id = id });
        //    return NoContent();
        //}
    }
}
