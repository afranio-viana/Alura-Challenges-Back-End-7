
using ApiJornadaMilhas.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApiJornadaMilhas.Data.Dto;

namespace ApiJornadaMilhas.Controllers;

[ApiController]
[Route("depoimentos-home")]
public class SortearDepoimentos : ControllerBase
{
    public MongoDBService _mongoDbService;
    public IMapper _mapper;

    public SortearDepoimentos (MongoDBService mongoDbService, IMapper mapper)
    {
        _mongoDbService = mongoDbService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> SortearTresDepoimentos()
    {
        int tamanho =  await _mongoDbService.GetAsyncSize();
        if(tamanho>=3)
        {
            return Ok(await _mongoDbService.GetAsyncRandom(_mapper));
        }else
        {
            return NotFound("Número de registros no banco é inferior a 3");
        }

        

    }
}