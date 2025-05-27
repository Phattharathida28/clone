using Application.Features.SU.SURT05;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers.SU;
public class Surt05Controller : BaseController
{
    [HttpGet("list")]
    public async Task<ActionResult> List([FromQuery] List.Query query) => Ok(await Mediator.Send(query));

    [HttpDelete("delete")]
    public async Task<ActionResult> Delete([FromQuery] Delete.Command command) => Ok(await Mediator.Send(command));

    [HttpGet("detail")]
    public async Task<ActionResult> Detail([FromQuery] Detail.Query query) => Ok(await Mediator.Send(query));

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] Create.Command model) => Ok(await Mediator.Send(model));

    [HttpPut("update")]
    public async Task<ActionResult> Update([FromBody] Edit.Command model) => Ok(await Mediator.Send(model));

}
