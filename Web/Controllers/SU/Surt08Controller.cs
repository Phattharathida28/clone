using Application.Features.SU.SURT08;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers.SU;

public class Surt08Controller : BaseController
{
    [HttpPost("list")]
    public async Task<ActionResult> List([FromBody] List.Query query) => Ok(await Mediator.Send(query));
}
