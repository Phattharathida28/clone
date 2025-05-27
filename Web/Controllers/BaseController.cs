using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Primitives;
using Persistense;

namespace Web.Controllers;

[RequestLogging]
[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}

public class HttpResponseMessageResult : IActionResult
{
    private readonly HttpResponseMessage _responseMessage;

    public HttpResponseMessageResult(HttpResponseMessage responseMessage) => _responseMessage = responseMessage;

    public async Task ExecuteResultAsync(ActionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)_responseMessage.StatusCode;

        foreach (var header in _responseMessage.Headers) context.HttpContext.Response.Headers.TryAdd(header.Key, new StringValues(header.Value.ToArray()));

        foreach (var header in _responseMessage.Content.Headers) context.HttpContext.Response.Headers.TryAdd(header.Key, new StringValues(header.Value.ToArray()));

        using (var stream = await _responseMessage.Content.ReadAsStreamAsync())
        {
            await stream.CopyToAsync(context.HttpContext.Response.Body);
            await context.HttpContext.Response.Body.FlushAsync();
        }
    }
}
