namespace ApiJornadaMilhas.Models;

public class MongoDBSettings
{

    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionNameDepoimentos { get; set; } = null!;
    public string CollectionNameDestinos {get; set;} = null!;

}