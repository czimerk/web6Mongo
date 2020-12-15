using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiTest.Services
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IMongoClient _mClient;
        private readonly IMongoDatabase _db;

        public MongoDbService(IConfiguration configuration)
        {
            var dbUri = configuration.GetSection("AppSettings")
                .GetSection("DbUri").Value;
            var databaseId = configuration.GetSection("AppSettings")
                .GetSection("DatabaseId").Value;
            //string dbUri = "mongodb://localhost";
            _mClient = new MongoClient(dbUri);

            var dbList = _mClient.ListDatabases().ToList();
            if (dbList.Select(d => d.ToString()).Contains(databaseId))
            {
                Console.WriteLine($"Error: databse {databaseId} was not found on Uri {dbUri}");
            }

            _db = _mClient.GetDatabase(databaseId);
        }

        public IMongoDatabase GetDb()
        {
            return _db;
        }

        public IMongoClient GetClient()
        {
            return _mClient;
        }
    }
}
