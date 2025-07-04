namespace WeddingInvitationApi.Configuration;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string DatabaseName { get; set; } = "barunson";
    public string CollectionName { get; set; } = "wedding";
}