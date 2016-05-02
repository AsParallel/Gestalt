using MongoDB.Driver;

namespace Gestalt.DataAccess
{
    public interface IMongoDBContext<T>
    {
        IMongoClient Client { get; }
        IMongoCollection<T> Collection { get; }
        IMongoDatabase Database { get; }
    }
} 