using Microsoft.AspNetCore.Mvc;
using Movie.Core.Utility;
using Movie.Service.Utility;
using System.Net;
using System.Security.Claims;

namespace Movie.API.Controllers.Shared;

public class BaseController : ControllerBase
{
    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ParseResult(ServiceResult response)
    {
        if (response.HttpStatusCode == HttpStatusCode.OK)
        {
            return base.Ok(response);
        }
        else if (response.HttpStatusCode == HttpStatusCode.BadRequest)
        {
            return base.BadRequest(response);
        }
        else
        {
            throw new InvalidOperationException("Unsupported Result Status");
        }
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ComputeApiResponse<T>(ServiceResponse<T> serviceResponse) where T : BaseRecord
    {

        switch (serviceResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                var response = new ApiRecordResponse<T>(true, serviceResponse.Message, serviceResponse.Data);
                return Ok(response);

            case HttpStatusCode.Unauthorized:
                response = new ApiRecordResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return Unauthorized(response);

            case HttpStatusCode.NotFound:
                response = new ApiRecordResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return NotFound(response);

            case HttpStatusCode.BadRequest:
                response = new ApiRecordResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return BadRequest(response);
            default:
                throw new ArgumentOutOfRangeException("HTTP Status Code Could Not Be Deciphered", nameof(serviceResponse.StatusCode));

        }
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ComputeResponse<T>(ServiceResponse<T> serviceResponse) where T : class
    {
        switch (serviceResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                var response = new ApiResponse<T>(true, serviceResponse.Message, serviceResponse.Data);
                return Ok(response);

            case HttpStatusCode.Unauthorized:
                response = new ApiResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return Unauthorized(response);

            case HttpStatusCode.NotFound:
                response = new ApiResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return NotFound(response);

            case HttpStatusCode.BadRequest:
                response = new ApiResponse<T>(false, serviceResponse.Message, serviceResponse.Data);
                return BadRequest(response);
            default:
                throw new ArgumentOutOfRangeException("HTTP Status Code Could Not Be Deciphered", nameof(serviceResponse.StatusCode));

        }
    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult ComputeResponse(ServiceResponse serviceResponse)
    {
        switch (serviceResponse.StatusCode)
        {
            case HttpStatusCode.OK:
                var response = new ApiResponse(true, serviceResponse.Message);
                return Ok(response);

            case HttpStatusCode.Unauthorized:
                response = new ApiResponse(false, serviceResponse.Message);
                return Unauthorized(response);

            case HttpStatusCode.NotFound:
                response = new ApiResponse(false, serviceResponse.Message);
                return NotFound(response);

            case HttpStatusCode.BadRequest:
                response = new ApiResponse(false, serviceResponse.Message);
                return BadRequest(response);
            case HttpStatusCode.NoContent:
                response = new ApiResponse(false, serviceResponse.Message);
                return NoContent();
            default:
                throw new ArgumentOutOfRangeException("HTTP Status Code Could Not Be Deciphered", nameof(serviceResponse.StatusCode));

        }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public string GetUserId()
    {
        ClaimsPrincipal user = HttpContext.User;
        string userId = user.FindFirstValue("UserId")!;
        return userId;
    }
}
