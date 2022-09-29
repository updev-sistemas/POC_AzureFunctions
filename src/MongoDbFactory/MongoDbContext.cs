using Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MongoDbFactory
{
    public class MongoDbContext
    {
        #region Construct Method and Attributes
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbConnectionString> config)
        {
            Validate(config.Value);

            MongoClient clientMongoDb = GenerateMongoClient(config.Value.Dsn!);

            _database = clientMongoDb.GetDatabase(config.Value.DbName!);
        }
        #endregion

        #region Private Methods
        private static MongoClient GenerateMongoClient(string connectionString)
            => new(connectionString);

        private static void Validate(MongoDbConnectionString config)
        {
            if (config is null)
            {
                throw new Exception("Configuração do MongoDB é inválida.");
            }

            if (string.IsNullOrEmpty(config.Dsn) || string.IsNullOrEmpty(config.DbName))
            {
                throw new Exception("Configuração do MongoDB é inválida.");
            }
        }
        #endregion

        #region Public Methods
        public IMongoDatabase GetDatabase()
            => this._database;
        #endregion
    }
}