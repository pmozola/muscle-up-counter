using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using MUCounter.Database;

namespace MUCounter.Application.QueryHandlers
{
    public class GetDaysStatisticQueryHandler : IRequestHandler<GetDaysStatisticQuery, GetDaysStatisticViewModel[]>
    {
        private readonly MUCDatabaseContext dbContext;

        public GetDaysStatisticQueryHandler(MUCDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<GetDaysStatisticViewModel[]> Handle(GetDaysStatisticQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Repetitions
                .GroupBy(x => x.Date.Date)
                .Select(x => new GetDaysStatisticViewModel(x.Key, x.Count()))
                .ToArrayAsync();
        }
    }

    public class GetDaysStatisticQuery : IRequest<GetDaysStatisticViewModel[]>
    {

    }

    public class GetDaysStatisticViewModel
    {
    public GetDaysStatisticViewModel(DateTime date, int numberOfRepetition)
        {
            this.Date = date;
            this.NumberOfRepetition = numberOfRepetition;
        }

        public DateTime Date { get; }

        public int NumberOfRepetition { get; }
    }
}
