using MongoDB.Driver;

namespace RestApiTest.Services
{
    public interface IMongoDbService
    {
        IMongoClient GetClient();
        IMongoDatabase GetDb();
    }
}