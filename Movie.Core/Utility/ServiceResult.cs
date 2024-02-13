using System.Net;

namespace Movie.Core.Utility;

public abstract class ServiceResult
{
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
}
