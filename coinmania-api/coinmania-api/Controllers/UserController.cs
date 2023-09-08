using System.Text.Json;
using BusinessLogicLayer.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMSOfficeSharp;

namespace coinmania_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

        
    [HttpPost]
    [Route("/api/User/RegisterUser")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand item)
    {
        var response = await _mediator.Send(item);

        if (!response.Succeeded)
        {
            return BadRequest(response.Errors);
        }

        return Ok(response);
    }

    [HttpPost]
    [Route("/api/User/LogIn")]
    public async Task<IActionResult> LogIn([FromBody] LogInCommand item)
    {
        var response = await _mediator.Send(item);
        
        if (response.Succeeded)
        {
            return Ok(response);
        }

        return BadRequest();
    }
    
    [HttpPost]
    [Route("/api/User/sign-in/confirm")]
    public async Task<IActionResult> ConfirmPhoneSignIn([FromBody]SignInConfirmationCommand item)
    {
        var result = await _mediator.Send(item);
        
        if (string.IsNullOrEmpty(result.Item2))
        {
            return Unauthorized();
        }

        var userInfo = new
        {
            phone = result.Item1.Phone,
            firstName = result.Item1.FirstName,
            lastName = result.Item1.LastName,
            id = result.Item1.UserId
        };
        return Ok(new
        {
            token = result.Item2,
            data = userInfo
        });
    }
    
    [HttpDelete]
    [Route("/api/User/delete")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand item)
    {
        var response = await _mediator.Send(item);

        if (response)
        {
            return Ok(response);
        }

        return BadRequest();
    }

    [HttpPost]
    [Authorize]
    [Route("/api/User/users")]
    public async Task<IActionResult> GetUsers([FromBody] GetUsersCommand item)
    {
        var users = await _mediator.Send(item);

        return Ok(users);
    }

    [HttpGet]
    [Route("/api/sendsms/{phone}/{token}/")]
    public async Task<IActionResult> SendSms(string phone, string token)
    {
        var s = new Sender {ApiKey = "c6dc66605ae848fcb5863720d2e61288", MessageTitle = "message"};
        
        s.Send("Confirmation token: " + token, "995"+phone);

        return Ok();
    }
    
    [HttpPost]
    [Route("/api/User/find")]
    public async Task<IActionResult> FindUser([FromBody]FindUserCommand item)
    {
        var response = await _mediator.Send(item);

        if (response)
        {
            return Ok(response);
        }

        return BadRequest();
    }

    [HttpPost]
    [Route("/api/User/sign-up/confirm")]
    public async Task<IActionResult> ConfirmPhoneSignUp([FromBody]SignUpConfirmationCommand item)
    {
        var response = await _mediator.Send(item);
        
        if (response.Succeeded)
        {
            return Ok(response);
        }

        return BadRequest();
    }
}