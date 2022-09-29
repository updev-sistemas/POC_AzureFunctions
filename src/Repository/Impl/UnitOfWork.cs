using Entities;
using MongoDbFactory;
using Repository.Contracts;

namespace Repository.Imp
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MongoDbContext? mongoDb;

        public UnitOfWork(MongoDbContext mongoDbContext)
        {
            ArgumentNullException.ThrowIfNull(mongoDbContext, nameof(mongoDbContext));

            this.mongoDb = mongoDbContext;
            var database = mongoDb.GetDatabase();

            _states = new StateRepository(database);
        }

        private readonly IDefaultRepository<StateBson>? _states;
        public IDefaultRepository<StateBson> States => _states!;
    }
}
