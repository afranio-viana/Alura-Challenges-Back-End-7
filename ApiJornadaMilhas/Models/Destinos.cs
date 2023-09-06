
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiJornadaMilhas.Models;

public class Destinos
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string Foto {get; set;}
    public string Nome {get; set;}
    public float Preco {get; set;}
}