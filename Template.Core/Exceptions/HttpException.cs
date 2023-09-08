using System;
using System.Net;
using System.Net.Http;

namespace Template.Core.Exceptions
{
    public class HttpException : HttpRequestException
    {
        public HttpStatusCode Code { get; }
        public Object Json { get; }

        public HttpException(HttpStatusCode code, object json, string mensagem) : base(mensagem)
        {
            Code = code;
            Json = json;
        }

        public HttpException(HttpStatusCode code, string mensagem) : base(mensagem)
        {
            Code = code;
        }

        public HttpException(HttpStatusCode code, string mensagem, Exception inner) : base(mensagem)
        {
            Code = code;
        }

        public HttpException(int v, string mensagem) : base(mensagem) { } 
    }
}
