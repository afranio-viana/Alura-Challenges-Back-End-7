using ApiJornadaMilhas.Models;
using ApiJornadaMilhas.Data.Dto;
using AutoMapper;
using MongoDB.Driver;

namespace ApiJornadaMilhas.Profiles;

public class DepoimentosProfile : Profile
{
    public DepoimentosProfile ()
    {
        CreateMap<CreateDepoimentoDto, Depoimentos>();
        CreateMap<Depoimentos, ReadDepoimentosDto>();
    }
}