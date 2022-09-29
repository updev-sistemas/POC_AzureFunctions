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

namespace StateManagerAzf.Functions
{
    public class Remove
    {
        private readonly IStateService? service;

        public Remove(IStateService service)
        {
            this.service = service;
        }

        [FunctionName("RemoveCity")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var payload = JsonConvert.DeserializeObject<CityRemove>(requestBody);

            var state = new State
            {
                Name = payload.Name,
                Uf = payload.Uf,
            };

            var responseMessage = await this.service.RemoveCityByStateAsync(state, payload.CityName).ConfigureAwait(false);

            return new OkObjectResult(responseMessage);
        }
    }
}