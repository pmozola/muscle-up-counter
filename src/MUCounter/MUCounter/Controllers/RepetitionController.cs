using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MUCounter.Application.CommandHandlers;
using MUCounter.Application.QueryHandlers;

namespace MUCounter.Controllers
{
    [Route("api/[controller]")]
    public class RepetitionController : Controller
    {
        private readonly IMediator mediator;

        public RepetitionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return this.Ok(await this.mediator.Send(new AddRepetitionCommand()));
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalRepetition()
        {
            return this.Ok(await this.mediator.Send(new GetNumberOfRepetionsQuery()));
        }


        [HttpDelete("")]
        public async Task<IActionResult> RemoveLastRepetition()
        {
            return this.Ok(await this.mediator.Send(new RemoveRepetitionCommand()));
        }
    }
}
