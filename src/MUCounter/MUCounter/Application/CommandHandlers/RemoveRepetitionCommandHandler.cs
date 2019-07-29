using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using MUCounter.Database;

namespace MUCounter.Application.CommandHandlers
{
    public class RemoveRepetitionCommandHandler : AsyncRequestHandler<RemoveRepetitionCommand>
    {
        private readonly MUCDatabaseContext dbContext;

        public RemoveRepetitionCommandHandler(MUCDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected override Task Handle(RemoveRepetitionCommand request, CancellationToken cancellationToken)
        {
            var lastRepetition =  this.dbContext.Repetitions.OrderByDescending(x => x.Date).First();
            this.dbContext.Repetitions.Remove(lastRepetition);

            return this.dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public class RemoveRepetitionCommand : IRequest
    {
    }
}
