namespace Publisher.Configuration;

public class DatabaseSettings
{
    public string MongoDbConnectionString { get; set; }
    public string MongoDbDatabaseName { get; set; }
    public string MongoDbCollectionName { get; set; }
}