using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ManagerStates;
using ValuesObjects;

namespace StateManager
{
    public class Register
    {
        private readonly IStateService? service;

        public Register(IStateService service)
        {
            this.service = service;
        }

        [FunctionName("RegisterState")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var state = JsonConvert.DeserializeObject<State>(requestBody);            

            var responseMessage = await this.service.RegisterStateAsync(state).ConfigureAwait(false);

            return new OkObjectResult(responseMessage);
        }
    }
}
