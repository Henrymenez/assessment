using Microsoft.AspNetCore.Diagnostics;
using Movie.Core.Utility;
using System.Net;
using System.Text.Json;

namespace Movie.API.Middlewares;
public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                IExceptionHandlerFeature? contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    app.Logger.LogError($"{contextFeature.Error}");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    string text = JsonSerializer.Serialize(new ErrorResult
                    {
                        IsSuccessful = false,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = contextFeature.Error.Message
                    });
                    await context.Response.WriteAsync(text);

                }
            });
        });
    }
}