using ApiJornadaMilhas.Models;
using ApiJornadaMilhas.Data.Dto;
using AutoMapper;

namespace ApiJornadaMilhas.Profiles;

public class DepoimentosProfile : Profile
{
    public DepoimentosProfile ()
    {
        CreateMap<CreateDepoimentoDto, Depoimentos>();
    }
}