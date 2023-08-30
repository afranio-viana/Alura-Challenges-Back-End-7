
using ApiJornadaMilhas.Models;
using MongoDB.Driver;

namespace ApiJornadaMilhas.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDBAtlasConnection");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("GerenciadorDeTarefaApi");
    }

    public IMongoCollection<Depoimentos> Depoimentos => _database.GetCollection<Depoimentos>("Depoimentos");
}