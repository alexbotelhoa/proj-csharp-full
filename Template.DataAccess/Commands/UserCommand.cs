using MediatR;
using Template.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Template.DataAccess.Commands
{
    public class GetAllUsersCommand : IRequest<IActionResult> { public int take { get; set; } public int skip { get; set; } }
    public class GetByIdUserCommand : IRequest<IActionResult> { public int userId { get; set; } }
    public class CreateUserCommand : UserDTO, IRequest<IActionResult> { }
    public class UpdateUserCommand : UserDTO, IRequest<IActionResult> { }
    public class DeleteUserCommand : IRequest<IActionResult> { public int userId { get; set; } }

}
