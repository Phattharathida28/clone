using Application.Features.SU.Parameter;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers.SU;

public class ParameterController : BaseController
{
    [HttpGet("{group}/{code?}")]
    public async Task<IActionResult> Get([FromRoute] Detail.Query query) => Ok(await Mediator.Send(query));
}
