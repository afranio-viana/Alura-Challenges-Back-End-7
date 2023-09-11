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
        CreateMap<UpdateDepoimentosDto, ReadDepoimentosDto>();
        CreateMap<ReadDepoimentosDto, Depoimentos>();
        CreateMap<Depoimentos, ReadDepoimentosHomeDto>();

        CreateMap<CreateDestinosDto, Destinos>();
        CreateMap<Destinos, ReadDestinosDto>();
        CreateMap<UpdateDestinosDto, ReadDestinosDto>();
        CreateMap<ReadDestinosDto, Destinos>();
        CreateMap<ReadDestinosDto, UpdateDestinosDto>();
        CreateMap<Destinos, UpdateDestinosDto>();
        CreateMap<UpdateDestinosDto, Destinos>();
    }
}