using Application.Features.ET.ETRT02;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers.ET;

public class Etrt02Controller : BaseController
{
    [HttpGet("list")]
    public async Task<ActionResult> List([FromQuery] List.Query query) => Ok(await Mediator.Send(query));

    [HttpGet("master")]
    public async Task<ActionResult> Master([FromQuery] Master.Query query) => Ok(await Mediator.Send(query));

    [HttpGet("detail")]
    public async Task<ActionResult> Detail([FromQuery] Detail.Query query) => Ok(await Mediator.Send(query));

    [HttpPost("save")]
    public async Task<ActionResult> Save([FromBody] Save.Command command) => Ok(await Mediator.Send(command));

    [HttpDelete("delete")]
    public async Task<ActionResult> Delete([FromQuery] Delete.Command command) => Ok(await Mediator.Send(command));
}
