using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ManagerStates;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ValuesObjects;

namespace StateManagerAzf.Functions
{
    public class Notification
    {
        private readonly IStateService? service;

        public Notification(IStateService service)
        {
            this.service = service;
        }

        [FunctionName("Notification")]
        public async Task RunAsync([TimerTrigger("1 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var states = await this.service!.ListStatesForNotifiedAsync().ConfigureAwait(false);

            if (states != null)
            {
                foreach (var state in states)
                {
                    try
                    {
                        SendEmail(state);
                        await this.service!.UpdateStateAsync(state).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        log.LogError(ex.Message, ex);  
                        _ = ex;
                    }
                }
            }
        }


        private static void SendEmail(State state)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("0568c084db5362", "aa262112a57439"),
                EnableSsl = true
            };

            var sb = new StringBuilder();

            sb.AppendLine($"<h3>{state.Uf} - {state.Name}</h3>");
            sb.AppendLine($"<h5>Lista de Cidades</h5>");
            sb.AppendLine("<ul>");
            foreach (var city in state.Cities)
            {
                sb.AppendLine($"<li>{city.Name}</li>");
            }
            sb.AppendLine("</ul>");

            client.Send("antonio_barros@atlantico.com.br", "antonio_barros@atlantico.com.br", "Report de Uso", sb.ToString());
        }
    }
}
