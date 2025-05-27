using Application.Features.SU.Setting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers.SU;

public class SettingController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] Edit.Command command) => Ok(await Mediator.Send(command));
}
