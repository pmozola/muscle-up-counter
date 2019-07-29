using System;

namespace MUCounter.Application.Domain
{
    public class MuscleUpRepetition
    {
        MuscleUpRepetition()
        {
        }

        public MuscleUpRepetition(DateTime date)
        {
            this.Date = date;
        }

        public int Id { get; private set; }

        public DateTime Date { get; private set; }
    }
}
