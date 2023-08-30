using ApiJornadaMilhas.Data.Dto;
using ApiJornadaMilhas.Models;
using ApiJornadaMilhas.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace ApiJornadaMilhas.Controllers;

[ApiController]
[Route("[controller]")]
public class DepoimentosController : ControllerBase
{
    private MongoDBService _mongoDbService;
    private IMapper _mapper;
    public DepoimentosController(MongoDBService mongoDBService, IMapper mapper)
    {
        _mongoDbService = mongoDBService;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> AdicionarDepoimento ([FromBody] CreateDepoimentoDto depoimentoDto)
    {
        Depoimentos depoimento = _mapper.Map<Depoimentos>(depoimentoDto);
        await _mongoDbService.CreateAsync(depoimento);
        return Ok();
    }

    [HttpGet]
    public async Task<IEnumerable<ReadDepoimentosDto>> RecuperarDepoimento()
    {
        return await _mongoDbService.GetAsync(_mapper);
    }

}