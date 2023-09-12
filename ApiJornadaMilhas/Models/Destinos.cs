
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiJornadaMilhas.Models;

public class Destinos
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string Foto1 {get; set;}
    public string Foto2 {get; set;}
    public string Nome {get; set;}
    public float Preco {get; set;}
    [MaxLength(160, ErrorMessage = "O Campo Meta deve possuir no máximo 160 caracters")]
    public string Meta {get; set;}
    public string? TextoDescritivo {get; set;}
}