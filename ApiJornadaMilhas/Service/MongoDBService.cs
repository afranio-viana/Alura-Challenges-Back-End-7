using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using ApiJornadaMilhas.Models;
using ApiJornadaMilhas.Data.Dto;
using AutoMapper;

namespace ApiJornadaMilhas.Services;

public class MongoDBService {

    private readonly IMongoCollection<Depoimentos> _depoimentosCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _depoimentosCollection = database.GetCollection<Depoimentos>(mongoDBSettings.Value.CollectionName); 
    }

    
    //Um serviço para inserção no banco
    public async Task CreateAsync(Depoimentos depoimento)
    {
        await _depoimentosCollection.InsertOneAsync(depoimento);
        return;
    }

    //Um serviço para retornar dados do banco
    public async Task<IEnumerable<ReadDepoimentosDto>> GetAsync(IMapper _mapper)
    {
        List<Depoimentos> depoimentos = await _depoimentosCollection.Find(new BsonDocument()).ToListAsync();
        return  _mapper.Map<List<ReadDepoimentosDto>>(depoimentos);
    }

}