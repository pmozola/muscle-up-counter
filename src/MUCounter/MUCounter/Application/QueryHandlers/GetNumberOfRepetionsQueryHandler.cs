using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using MUCounter.Database;

namespace MUCounter.Application.QueryHandlers
{
    public class GetNumberOfRepetionsQueryHandler : IRequestHandler<GetNumberOfRepetionsQuery, int>
    {
        private readonly MUCDatabaseContext dbContext;

        public GetNumberOfRepetionsQueryHandler(MUCDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<int> Handle(GetNumberOfRepetionsQuery request, CancellationToken cancellationToken)
        {
            return this.dbContext.Repetitions.CountAsync(cancellationToken);
        }
    }

    public class GetNumberOfRepetionsQuery : IRequest<int>
    {
    }
}
