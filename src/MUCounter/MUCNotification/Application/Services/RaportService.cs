using MUCNotification.Infrastructure;

namespace MUCNotification.Application.Services
{
    public class RaportService : IRaportService
    {
        private readonly DailyRepsCounterRepository repository;
        private readonly SmsService smsService;

        public RaportService(DailyRepsCounterRepository repository, SmsService smsService)
        {
            this.repository = repository;
            this.smsService = smsService;
        }

        public void Raport()
        {
            var todayReps = this.repository.GetCurrent().GetAwaiter().GetResult();

            if (todayReps == null)
            {
                return;
            }

            this.smsService.Send($"Hi, yesterday you did {todayReps.RepetitionCounter} repetitions of Muscle-up! Nice!");
        }
    }
}
