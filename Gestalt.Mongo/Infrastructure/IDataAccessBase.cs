

namespace Gestalt.Mongo
{  
    //intentionally leaving this here 
    public interface IDataAccessBase<T>
    {
        string GetCollectionName();
    }
}