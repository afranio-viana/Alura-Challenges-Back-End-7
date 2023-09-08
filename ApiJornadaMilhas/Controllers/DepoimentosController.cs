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
        try
        {
            Depoimentos depoimento = _mapper.Map<Depoimentos>(depoimentoDto);
            await _mongoDbService.CreateAsync(depoimento);
            return CreatedAtAction(nameof(RecuperarDepoimentoPorId),new{id = depoimento.Id},depoimento);
        }catch
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<IActionResult> RecuperarDepoimento()
    {
        try
        {
            IEnumerable<ReadDepoimentosDto> depoimentos = await _mongoDbService.GetAsync(_mapper); 
            return Ok(depoimentos);
        }catch
        {
            return NotFound("Ocorreu algum erro na solicitação!");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> RecuperarDepoimentoPorId(string id)
    {
        try{
            ReadDepoimentosDto depoimentosDto = await _mongoDbService.GetAsyncById(_mapper,id);
            return Ok(depoimentosDto);

        }catch
        {
            return NotFound("Ocorreu algum erro na solicitação!");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarDepoimento (string id, [FromBody] UpdateDepoimentosDto updateDepoimentosDto)
    {
        try
        {   
            ReadDepoimentosDto depoimento= await _mongoDbService.GetAsyncById(_mapper, id);
            ReadDepoimentosDto novoUpdateDepoimentoDto = _mapper.Map<ReadDepoimentosDto>(updateDepoimentosDto);
            novoUpdateDepoimentoDto.Id = depoimento.Id;
            Depoimentos updateDepoimento = _mapper.Map<Depoimentos>(novoUpdateDepoimentoDto);
            await _mongoDbService.PutAsync(id,updateDepoimento);
            return NoContent();
        }catch
        {
            return NotFound("Id não encontrado");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarDepoimentoPorId (string id)
    {
        ReadDepoimentosDto depoimento = await _mongoDbService.GetAsyncById(_mapper, id);
        try
        {
            await _mongoDbService.DeleteAsync(depoimento.Id);
            return NoContent();
        }catch
        {
            return NotFound("Id não encontrado");
        }
    }

}