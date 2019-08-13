using System;
using System.Threading.Tasks;

using MassTransit;
using MUCMessages.Events;
using MUCNotification.Infrastructure;

namespace MUCNotification.Application.IntegrationEventHandlers
{
    public class RepetitionAddedEventHandler : IConsumer<RepetitionAddedEvent>
    {
        private readonly DailyRepsCounterRepository repository;

        public RepetitionAddedEventHandler(DailyRepsCounterRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<RepetitionAddedEvent> context)
        {
            var todayRepetition = await repository.GetCurrent();

            if (todayRepetition != null)
            {
                todayRepetition.AddRep();

                await repository.Update(todayRepetition);
            }
            else
            {
                todayRepetition = new DailyRepsCounter(DateTime.Now.Date);
                todayRepetition.AddRep();

                await repository.Add(todayRepetition);
            }
        }
       
    }
}
