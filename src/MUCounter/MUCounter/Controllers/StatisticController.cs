using MediatR;
using Microsoft.AspNetCore.Mvc;
using MUCounter.Application.QueryHandlers;
using System.Threading.Tasks;

namespace MUCounter.Controllers
{
    [Route("api/[controller]")]
    public class StatisticController : Controller
    {
        private readonly IMediator mediator;

        public StatisticController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalRepetition()
        {
            return this.Ok(await this.mediator.Send(new GetDaysStatisticQuery()));
        }
    }
}
