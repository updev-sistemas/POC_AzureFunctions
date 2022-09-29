using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ManagerStates;

namespace StateManager
{
    public class States
    {
        private readonly IStateService? service;

        public States(IStateService service)
        {
            this.service = service;
        }

        [FunctionName("ListStates")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var states = await this.service!.ListStatesAsync().ConfigureAwait(false);

            return new OkObjectResult(states);
        }
    }
}
