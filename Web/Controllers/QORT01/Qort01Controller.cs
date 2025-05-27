using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.QORT01.DTO;
using Application.Features.QORT01.Query;
using Application.Features.QORT01.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.QORT01
{
    public class Qort01Controller : BaseController
    {
        [AllowAnonymous]
        [HttpPost("list")]
        public async Task<ActionResult<List<QOMasterDTO>>> Post([FromBody] List.Query query)
        {
            return Ok(await Mediator.Send(query));
        }

        [AllowAnonymous]
        [HttpGet("detail")]
        public async Task<ActionResult<List<DetailDTO>>> Get([FromQuery] Detail.Query query)
        {
            return Ok(await Mediator.Send(query));
        }

        [AllowAnonymous]
        [HttpPost("save-detail")]
        public async Task<ActionResult<int>> Post([FromBody] Create.Command query)
        {
            return Ok(await Mediator.Send(query));
        }


        [AllowAnonymous]
        [HttpPut("update")]
        public async Task<ActionResult<int>> Put([FromBody] Update.Command query)
        {
            return Ok(await Mediator.Send(query));
        }

        [AllowAnonymous]
        [HttpDelete("delete")]
        public async Task<ActionResult<int>> Delete([FromQuery] Delete.Command query)
        {
            return Ok(await Mediator.Send(query));
        }

        //Dropdown
        [AllowAnonymous]
        [HttpGet("master")]
        public async Task<ActionResult> Master([FromQuery] Master.Query query) => Ok(await Mediator.Send(query));
    }
}
