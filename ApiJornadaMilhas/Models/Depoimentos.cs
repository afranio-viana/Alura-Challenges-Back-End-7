using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiJornadaMilhas.Models;

public class Depoimentos
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string Foto {get; set;}
    public string Depoimento {get; set;}
    public string Pessoa {get; set;}
}