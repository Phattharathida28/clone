using Application.Features.SU.Localize;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers.SU;

public class LocalizeController : BaseController
{
    [HttpGet("{lang}")]
    public async Task<IActionResult> Get([FromRoute] List.Query query) => Ok(await Mediator.Send(query));

    [HttpGet("{module}/{lang}")]
    public async Task<IActionResult> GetByModule([FromRoute] List.Query query) => Ok(await Mediator.Send(query));
}
