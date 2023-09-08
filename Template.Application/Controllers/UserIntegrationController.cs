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
using Template.Integration.Services.Interfaces;

namespace Template.Application.Controllers;

[ApiController]
[Route("api/v1/int/users")]
[ApiKey("TemplateApiKey")]
public class UserIntegrationController : ControllerBase
{
    private readonly ILogger<UserDatabaseController> _logger;
    private readonly IUserIntegrationService _userIntegrationService;

    public UserIntegrationController(IUserIntegrationService userIntegrationService, ILogger<UserDatabaseController> logger)
    {
        _logger = logger;
        _userIntegrationService = userIntegrationService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserDTO>>>> GetAllUsersAsync(
        [FromHeader(Name = "Channel-Id"), Required] string channelId,
        [FromHeader(Name = "Aplication"), Required] string aplication)
    {
        var headerRequest = new HeaderRequestHelper { channelId = channelId, aplication = aplication };

        try
        {
            ApiResponse<IEnumerable<UserDTO>> response = await _userIntegrationService.GetAllUsers(headerRequest);
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
            ApiResponse<UserDTO> response = await _userIntegrationService.GetByIdUser(headerRequest, queryRequest);            
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

    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserDTO>>> CreateUsereAsync(
        [FromHeader(Name = "Channel-Id"), Required] string channelId,
        [FromHeader(Name = "Aplication"), Required] string aplication,
        [FromBody, Required] UserDTO bodyRequest)
    {
        var headerRequest = new HeaderRequestHelper { channelId = channelId, aplication = aplication };

        try
        {
            ApiResponse<UserDTO> response = await _userIntegrationService.CreateUser(headerRequest, bodyRequest);
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

        try
        {
            ApiResponse<UserDTO> response = await _userIntegrationService.UpdateUser(headerRequest, bodyRequest, userId);            
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

        try
        {
            ApiResponse<bool> response = await _userIntegrationService.DeleteUser(headerRequest, userId);
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