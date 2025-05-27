using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.TM.TMDT01;

namespace Web.Controllers.TM
{
    
    public class Tmdt01Controller : BaseController
    {
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] Create.Command model) => Ok(await Mediator.Send(model));
        
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery] Delete.Command command) => Ok(await Mediator.Send(command));

        [HttpGet("list")]
        public async Task<ActionResult> List([FromQuery] List.Query query) => Ok(await Mediator.Send(query));

        [HttpGet("saveList")]
        public async Task<ActionResult> SaveList([FromQuery] SaveList.Query query) => Ok(await Mediator.Send(query));
    }
}
