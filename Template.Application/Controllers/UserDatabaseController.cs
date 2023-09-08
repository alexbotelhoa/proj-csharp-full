using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

using Template.Core.DTOs;
using Template.Core.Helpers;
using Template.Core.Exceptions;
using Template.Core.DTOs.Requests.User;
using Template.DataAccess.Services.Interfaces;

namespace Template.Application.Controllers;

[ApiController]
[Route("api/v1/db/users")]
[ApiKey("TemplateApiKey")]
public class UserDatabaseController : ControllerBase
{
    private readonly ILogger<UserDatabaseController> _logger;
    private readonly IUserDatabaseService _userDatabaseService;

    public UserDatabaseController(ILogger<UserDatabaseController> logger, IUserDatabaseService userDatabaseService)
    {
        _logger = logger;
        _userDatabaseService = userDatabaseService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAllUsersAsync(
        [FromHeader(Name = "Channel-Id"), Required] string channelId,
        [FromHeader(Name = "Aplication"), Required] string aplication,
        [FromQuery] int skip,
        [FromQuery] int take)
    {
        var headerRequest = new HeaderRequestHelper { channelId = channelId, aplication = aplication };
        var queryRequest = new GetUserRequestDTO { skip = skip, take = take };

        try
        {
            ApiResponse<IEnumerable<UserDTO>> response = await _userDatabaseService.GetAllUsers(headerRequest, queryRequest);
            if (response.Data.Count() == 0) return NotFound(response);
            return Ok(response);
        }
        catch (HttpException ex)
        {
            return StatusCode((int)ex.Code, new { Mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro: {ex.Message}");
            return StatusCode(500, new { Mensagem = $"Ocorreu um problema na chamada do serviço. ChannelId: {channelId}" });
        }
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<ApiResponse<UserDTO>>> GetByIdUserAsync(
        [FromHeader(Name = "Channel-Id"), Required] string channelId,
        [FromHeader(Name = "Aplication"), Required] string aplication,
        [FromRoute, Required] int userId)
    {
        var headerRequest = new HeaderRequestHelper { channelId = channelId, aplication = aplication };
        var queryRequest = new GetUserRequestDTO { userId = userId };

        try
        {
            ApiResponse<UserDTO> response = await _userDatabaseService.GetByIdUser(headerRequest, queryRequest);            
            if (response.Data == null) return NotFound(response);
            response.Url = UrlHelper.GetFullUrl(HttpContext);
            return Ok(response);
        }
        catch (HttpException ex)
        {
            return StatusCode((int)ex.Code, new { Mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro: {ex.Message}");
            return StatusCode(500, new { Mensagem = $"Ocorreu um problema na chamada do serviço. ChannelId: {channelId}" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserDTO>>> CreateUserAsync(
        [FromHeader(Name = "Channel-Id"), Required] string channelId,
        [FromHeader(Name = "Aplication"), Required] string aplication,
        [FromBody, Required] UserDTO bodyRequest)
    {
        var headerRequest = new HeaderRequestHelper { channelId = channelId, aplication = aplication };

        try
        {
            ApiResponse<UserDTO> response = await _userDatabaseService.CreateUser(headerRequest, bodyRequest);
            response.Url = UrlHelper.GetFullUrl(HttpContext, (int)response.Data.Id);
            return Created(response.Url, response);
        }
        catch (HttpException ex)
        {
            return StatusCode((int)ex.Code, new { Mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro: {ex.Message}");
            return StatusCode(500, new { Mensagem = $"Ocorreu um problema na chamada do serviço. ChannelId: {channelId}" });
        }
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<ApiResponse<UserDTO>>> UpdateUserAsync(
        [FromHeader(Name = "Channel-Id"), Required] string channelId,
        [FromHeader(Name = "Aplication"), Required] string aplication,
        [FromBody, Required] UserDTO bodyRequest,
        [FromRoute, Required] int userId)
    {
        var headerRequest = new HeaderRequestHelper { channelId = channelId, aplication = aplication };
        var queryRequest = new GetUserRequestDTO { userId = userId };

        try
        {
            ApiResponse<UserDTO> response = await _userDatabaseService.UpdateUser(headerRequest, bodyRequest, queryRequest);            
            if (response.Data == null) return NotFound(response);
            return Ok(response);
        }
        catch (HttpException ex)
        {
            return StatusCode((int)ex.Code, new { Mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro: {ex.Message}");
            return StatusCode(500, new { Mensagem = $"Ocorreu um problema na chamada do serviço. ChannelId: {channelId}" });
        }
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteUserAsync(
        [FromHeader(Name = "Channel-Id"), Required] string channelId,
        [FromHeader(Name = "Aplication"), Required] string aplication,
        [FromRoute, Required] int userId)
    {
        var headerRequest = new HeaderRequestHelper { channelId = channelId, aplication = aplication };
        var queryRequest = new GetUserRequestDTO { userId = userId };

        try
        {
            ApiResponse<bool> response = await _userDatabaseService.DeleteUser(headerRequest, queryRequest);
            if (!response.Data) return NotFound(response);
            return Ok(response);
        }
        catch (HttpException ex)
        {
            return StatusCode((int)ex.Code, new { Mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro: {ex.Message}");
            return StatusCode(500, new { Mensagem = $"Ocorreu um problema na chamada do serviço. ChannelId: {channelId}" });
        }
    }
}