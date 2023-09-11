using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using ApiJornadaMilhas.Models;
using ApiJornadaMilhas.Data.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiJornadaMilhas.Services;

public class MongoDBService {

    private readonly IMongoCollection<Depoimentos> _depoimentosCollection;
    private readonly IMongoCollection<Destinos> _destinosCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _depoimentosCollection = database.GetCollection<Depoimentos>(mongoDBSettings.Value.CollectionNameDepoimentos);
        _destinosCollection = database.GetCollection<Destinos>(mongoDBSettings.Value.CollectionNameDestinos); 
    }

    //DEPOIMENTOS
    
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

    public async Task<ReadDepoimentosDto> GetAsyncById(IMapper _mapper, string id)
    {
        Depoimentos depoimento = await _depoimentosCollection.Find(depoimento => depoimento.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<ReadDepoimentosDto>(depoimento);
    }
    public async Task<int> GetAsyncSize ()
    {
        int tamanho = _depoimentosCollection.AsQueryable().Count();
        return tamanho;
    }   

    public async Task<IEnumerable<ReadDepoimentosHomeDto>> GetAsyncRandom(IMapper _mapper)
    {
        List<Depoimentos> depoimentos =  _depoimentosCollection.AsQueryable().Sample(3).ToList();
        return _mapper.Map<List<ReadDepoimentosHomeDto>>(depoimentos);
    }   

    public async Task PutAsync (string id, Depoimentos updateDepoimento)
    {
        await _depoimentosCollection.ReplaceOneAsync(depoimento => depoimento.Id == id, updateDepoimento);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        await _depoimentosCollection.DeleteOneAsync(depoimento => depoimento.Id == id);
        return;
    }

    //Serviços de DESTINOS
    public async Task CreateAsyncDestinos (Destinos destinos)
    {
        await _destinosCollection.InsertOneAsync(destinos);
        return;
    }

    public async Task<IEnumerable<ReadDestinosDto>> GetAsyncDestinos(IMapper _mapper)
    {
        List<Destinos> destinos = await _destinosCollection.Find(new BsonDocument()).ToListAsync();
        return _mapper.Map<List<ReadDestinosDto>>(destinos);
    }

    public async Task<ReadDestinosDto> GetASyncDestinosById (IMapper _mapper, string id)
    {
        Destinos destinos = await _destinosCollection.Find(destino => destino.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<ReadDestinosDto>(destinos);
    }

    public async Task<IEnumerable<ReadDestinosDto>> GetASyncDestinosByName (IMapper _mapper, string nome)
    {
        List<Destinos> destinos = await _destinosCollection.Find(destino => destino.Nome.ToLower() == nome.ToLower()).ToListAsync();
        return _mapper.Map<List<ReadDestinosDto>>(destinos);
    }

    public async Task UpdateAsyncDestinos(string id, Destinos updateDestinos)
    {
        await _destinosCollection.ReplaceOneAsync(destinos => destinos.Id == id, updateDestinos);
        return;
    }
    
    public async Task DeleteAsyncDestinos (string id)
    {
        await _destinosCollection.DeleteOneAsync(destinos => destinos.Id == id);
        return;
    }
}