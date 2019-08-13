using MassTransit;
using MUCMessages.Events;
using System;
using System.Threading.Tasks;

namespace MUCounter.Application.IntegrationEventHandlers
{
    public class SendMessageConsumer : IConsumer<RepetitionAddedEvent>
    {
        public Task Consume(ConsumeContext<RepetitionAddedEvent> context)
        {
            Console.WriteLine($"Receive message value: {context.Message.Date}");

            return Task.CompletedTask;
        }

    }


   
}
