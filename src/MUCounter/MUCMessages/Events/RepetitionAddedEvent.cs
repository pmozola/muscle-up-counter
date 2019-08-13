using MUCMessages.Interfaces;
using System;

namespace MUCMessages.Events
{

    public class RepetitionAddedEvent : IMessage
    {
        public DateTime Date { get; }

        public RepetitionAddedEvent(DateTime date)
        {
            Date = date;
        }
    }
}
