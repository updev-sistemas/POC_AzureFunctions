using Entities;
using MongoDB.Driver;
using Repository.Contracts;
using System.Linq.Expressions;

namespace Repository.Imp
{
    public class StateRepository : IDefaultRepository<StateBson>
    {
        private readonly IMongoCollection<StateBson>? collection;

        public StateRepository(IMongoDatabase context)
        {
            this.collection = context.GetCollection<StateBson>("States");
        }

        public async Task CreateAsync(StateBson target)
            => await collection!.InsertOneAsync(target);

        public async Task<List<StateBson>>? FindAllAsync()
            => await collection!.Find(_ => true).ToListAsync();

        public async Task<StateBson?> FindByIdAsync(string id)
            => await collection!.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<StateBson>>? QueryAsync(Expression<Func<StateBson, bool>> expression)
            => await collection!.Find(expression).ToListAsync();

        public async Task RemoveAsync(string id)
            => await collection!.DeleteOneAsync(x => x.Id == id);

        public async Task UpdateAsync(string id, StateBson target)
            => await collection!.ReplaceOneAsync(x => x.Id == id, target);

    }
}
