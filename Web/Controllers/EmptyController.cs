using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers;

[AllowAnonymous]
public class EmptyController : ControllerBase
{
    [HttpGet]
    public IActionResult Index() => NotFound("Backoffice API Server started,request to api/{controller}/{action}");
}
