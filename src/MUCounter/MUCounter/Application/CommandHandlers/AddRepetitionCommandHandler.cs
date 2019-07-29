using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using MUCounter.Application.Domain;
using MUCounter.Database;

namespace MUCounter.Application.CommandHandlers
{
    public class AddRepetitionCommandHandler : IRequestHandler<AddRepetitionCommand, int>
    {
        private readonly MUCDatabaseContext dbContext;

        public AddRepetitionCommandHandler(MUCDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Handle(AddRepetitionCommand request, CancellationToken cancellationToken)
        {
            var newRepetition = new MuscleUpRepetition(DateTime.Now);

            await this.dbContext.Repetitions.AddAsync(newRepetition, cancellationToken).ConfigureAwait(false);
            await this.dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return newRepetition.Id;
        }
    }

    public class AddRepetitionCommand : IRequest<int>
    {
    }
}
