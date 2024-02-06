using System.Net;

namespace HorusApp.Abstractions
{
    public class ResponseBase<T>
    {
        public T Response { get; set; }
        public TypeReponse Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public enum TypeReponse
    {
        Ok,
        Error,
        ErroConnectivity
    }
}
