using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistense;
public class RequestLoggingAttribute : ActionFilterAttribute
{
    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        IActivityLogService activityLogService = context.HttpContext.RequestServices.GetService(typeof(IActivityLogService)) as IActivityLogService;
        ICleanDbContext cleanDbContext = context.HttpContext.RequestServices.GetService(typeof(ICleanDbContext)) as ICleanDbContext;

        HttpRequest request = context.HttpContext.Request;
        List<string> ignorePath = new() { "localize", "menu", "setting", "parameter" };

        if (request.Path.Value.Split("/").Any(a => ignorePath.Contains(a)))
        {
            await next();
        }
        else
        {
            List<object> query = new();
            List<object> body = new();
            string activityTypeCode = string.Empty;

            foreach (ControllerParameterDescriptor param in context.ActionDescriptor.Parameters)
            {
                if (param.ParameterInfo.CustomAttributes.Any(a => a.AttributeType == typeof(FromBodyAttribute)))
                {
                    object entity = context.ActionArguments[param.Name];
                    body.Add(entity);
                }
            }

            foreach (var param in request.Query)
            {
                query.Add(param);
            }

            switch (request.Method)
            {
                case "GET":
                    activityTypeCode = "2001";
                    break;
                case "POST":
                    activityTypeCode = "2002";
                    break;
                case "PUT":
                    activityTypeCode = "2003";
                    break;
                case "PATCH":
                    activityTypeCode = "2004";
                    break;
                case "DELETE":
                    activityTypeCode = "2005";
                    break;
            }

            object json = new
            {
                TraceId = request.HttpContext.TraceIdentifier,
                Payload = new
                {
                    Url = request.GetDisplayUrl(),
                    Method = request.Method,
                    Query = query,
                    Body = body,
                }
            };

            await activityLogService.Log(cleanDbContext, JsonSerializer.Serialize<dynamic>(json), activityTypeCode, default);

            await next();
        }
    }
}
