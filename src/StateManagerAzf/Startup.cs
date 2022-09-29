using Common;
using ManagerStates;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MongoDbFactory;
using Repository.Contracts;
using Repository.Imp;

[assembly: FunctionsStartup(typeof(StateManager.Startup))]
namespace StateManager
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            _ = builder.Services.AddTransient<MongoDbConnectionString>();
            _ = builder.Services.AddTransient<MongoDbContext>();
            _ = builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
            _ = builder.Services.AddSingleton<IStateService, StateService>();
        }
    }
}
