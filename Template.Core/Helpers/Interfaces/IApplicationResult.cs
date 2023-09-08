using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Template.Core.Helpers.Interfaces
{
    public interface IApplicationResult<TResultMessage> : IActionResult
    {
        TResultMessage Result { get; set; }
        string Message { get; set; }
        string Url { get; set; }
        List<string> Validations { get; set; }
        public string Protocolo { get; set; }
        HttpStatusCode HttpStatusCode { get; set; }
        bool AutoAssignHttpStatusCode { get; set; }
    }
}
