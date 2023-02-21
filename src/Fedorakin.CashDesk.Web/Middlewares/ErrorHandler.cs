using Fedorakin.CashDesk.Web.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Fedorakin.CashDesk.Web.Middlewares;

public class ErrorHandler
{
    private const string ContentType = "application/json";
    private readonly RequestDelegate _next;

    public ErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = ContentType;

            var tuple = GetStatusAndMessage(error);
            response.StatusCode = tuple.status;

            var result = JsonSerializer.Serialize(new { message = tuple.message });
            await response.WriteAsync(result);
        }
    }

    private (int status, string message) GetStatusAndMessage(Exception exception) =>
        exception switch
        {
            InvalidPageNumberException => ((int)HttpStatusCode.BadRequest, exception.Message),
            InvalidPageSizeException => ((int)HttpStatusCode.BadRequest, exception.Message),
            SelfCheckoutBusyException => ((int)HttpStatusCode.BadRequest, exception.Message),
            SelfCheckoutUnactiveException => ((int)HttpStatusCode.BadRequest, exception.Message),
            ElementNotFoundException => ((int)HttpStatusCode.NotFound, exception.Message),
            ValidationException => ((int)HttpStatusCode.BadRequest, exception.Message),
            CartEmptyException => ((int)HttpStatusCode.BadRequest, exception.Message),
            UnauthorizedException => ((int)HttpStatusCode.Unauthorized, exception.Message),
            _ => ((int)HttpStatusCode.InternalServerError, exception.Message)
        };
}
