using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using ApiJornadaMilhas.Models;

namespace ApiJornadaMilhas.Services;

public class MongoDBService {

    private readonly IMongoCollection<Depoimentos> _depoimentosCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _depoimentosCollection = database.GetCollection<Depoimentos>(mongoDBSettings.Value.CollectionName);
    }

    //public async Task<List<Depoimentos>> GetAsync() { }
    public async Task CreateAsync(Depoimentos depoimento) {
        await _depoimentosCollection.InsertOneAsync(depoimento);
        return;
    }

}