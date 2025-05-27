using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Filters.ProgramCode;

internal class ProgramCodeAuthorizationHandler : AuthorizationHandler<ProgramCodeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProgramCodeRequirement requirement)
    {
        if (context.User == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        AuthorizationFilterContext mvcContext = context.Resource as AuthorizationFilterContext;
        ControllerActionDescriptor descriptor = mvcContext?.ActionDescriptor as ControllerActionDescriptor;

        if (descriptor != null)
        {
            string ctrlName = descriptor.ControllerName.ToLower();
            List<string> programCodes = context.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(s => s.Value).ToList();
            programCodes.AddRange(["Localize", "Menu", "Parameter", "Demo", "Setting"]);

            if (programCodes.Any(a => a.ToLower() == ctrlName)) context.Succeed(requirement);
            else context.Fail();
        }

        return Task.CompletedTask;
    }
}
