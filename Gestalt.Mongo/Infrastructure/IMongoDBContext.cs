using MongoDB.Driver;

namespace Gestalt.Mongo
{
    public interface IMongoDBContext<T>
    {
        IMongoClient Client { get; }
        IMongoCollection<T> Collection { get; }
        IMongoDatabase Database { get; }
    }
} 