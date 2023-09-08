using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Template.Core.DTOs.Requests.User
{
    public class GetUserRequestDTO : IRequest<IActionResult>
    {
        public int? userId { get; set; }
        public int? take { get; set; }
        public int? skip { get; set; }
    }
}
