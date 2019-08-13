using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using MUCMessages.Events;
using MUCounter.Application.Domain;
using MUCounter.Application.IntegrationEventHandlers;
using MUCounter.Database;

namespace MUCounter.Application.CommandHandlers
{
    public class AddRepetitionCommandHandler : IRequestHandler<AddRepetitionCommand, int>
    {
        private readonly MUCDatabaseContext dbContext;
        private readonly IBus serviceBusClient;

        public AddRepetitionCommandHandler(MUCDatabaseContext dbContext, IBus serviceBusClient)
        {
            this.dbContext = dbContext;
            this.serviceBusClient = serviceBusClient;
        }

        public async Task<int> Handle(AddRepetitionCommand request, CancellationToken cancellationToken)
        {
            var newRepetition = new MuscleUpRepetition(DateTime.Now);

            await this.dbContext.Repetitions.AddAsync(newRepetition, cancellationToken).ConfigureAwait(false);
            await this.dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await serviceBusClient.Publish(new RepetitionAddedEvent(newRepetition.Date));
            return newRepetition.Id;
        }
    }

    public class AddRepetitionCommand : IRequest<int>
    {
    }
}
