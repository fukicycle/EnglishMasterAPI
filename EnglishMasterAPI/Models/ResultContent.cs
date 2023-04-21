using System.Net;

namespace EnglishMasterAPI.Models
{
    public class ResultContent<T>
    {
        public string Message { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public T Content { get; set; }
    }
}
