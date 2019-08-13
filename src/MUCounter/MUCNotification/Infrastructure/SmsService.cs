using Microsoft.Extensions.Configuration;

namespace MUCNotification.Infrastructure
{
    public class SmsService
    {
        private readonly IConfiguration config;

        public SmsService(IConfiguration config)
        {
            this.config = config;
        }

        public void Send(string message)
        {

            SMSApi.Api.Client client = new SMSApi.Api.Client(this.config.GetSection("SMSApi").GetValue<string>("login"));
            client.SetPasswordHash(this.config.GetSection("SMSApi").GetValue<string>("password"));

            var smsApi = new SMSApi.Api.SMSFactory(client);
            smsApi.ActionSend()
                    .SetText(message)
                    .SetTo(this.config.GetSection("SMSApi").GetValue<string>("raportSmsNumber"))
                    .Execute();
        }
    }
}
