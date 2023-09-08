using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Template.Core.Helpers.Interfaces;

namespace Template.Core.Helpers
{
    public class ApplicationResult<TResultMessage> : IApplicationResult<TResultMessage>, IActionResult
    {
        public ApplicationResult() { }

        public TResultMessage Result { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public List<string> Validations { get; set; } = new List<string>();
        public string Protocolo {  get; set; }
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
        public bool AutoAssignHttpStatusCode { get; set; }
        public BaseRequest Request { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(this));
            context.HttpContext.Response.Headers.Add("Content-Type", "application/json");
            if (AutoAssignHttpStatusCode)
            {
                List<string> validations = Validations;
                if (validations != null && validations.Count > 0)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode;
                    goto IL_0110;

                    IL_0110:
                    await stringContent.CopyToAsync(context.HttpContext.Response.Body);
                }
            }
        }

        public void SetProtocol()
        {
            if (Request != null) Protocolo = Request.GetHeader("Protocolo");
        }

        public ApplicationResult<TResultMessage> Set(HttpStatusCode httpStatusCode, string message)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
            return this;
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToOk(string message = "Registro processado com sucesso!")
        {
            return Set(HttpStatusCode.OK, message);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToCreated(string message = "Registro cadastrado com sucesso!")
        {
            return Set(HttpStatusCode.Created, message);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToNotFound(string message = "Nenhum registro localizado!")
        {
            return Set(HttpStatusCode.NotFound, message);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToNoContent(string message = "Registro não encontrado!")
        {
            return Set(HttpStatusCode.NoContent, message);
        }

        public ApplicationResult<TResultMessage> SetHttpStatusToUnprocesssableEntity(string message = "Chamada não processada!")
        {
            return Set(HttpStatusCode.UnprocessableEntity, message);
        }
    }
}
