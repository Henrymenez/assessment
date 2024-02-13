using System.Net;

namespace Movie.Service.Utility;

public class ServiceResponse<T> where T : class
{
    /// <summary>
    /// Data returned from the public service method
    /// </summary>
    public T? Data { get; set; }
    /// <summary>
    /// Agreed Http Status Code to be sent in controller as a response
    /// </summary>
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    /// <summary>
    /// Message to be returned back to the API client.
    /// It could either be a failure message or a prompt to notify the api client about a successful operation being carried out
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// ResponseCode From AppResponseClass
    /// It could either be a failure message or a prompt to notify the api client about a successful operation being carried out
    /// </summary>

    public bool Success { get; set; }


}

public class ServiceResponse
{
    /// <summary>
    /// Agreed Http Status Code to be sent in controller as a response
    /// </summary>
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    /// <summary>
    /// Message to be returned back to the API client.
    /// It could either be a failure message or a prompt to notify the api client about a successful operation being carried out
    /// </summary>
    public string? Message { get; set; }
    public string? ResponseCode { get; set; }
    public bool Success { get; set; }
}
