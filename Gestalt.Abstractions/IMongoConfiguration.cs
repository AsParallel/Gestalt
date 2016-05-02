namespace Gestalt.AppSettings
{
    public interface IMongoConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}