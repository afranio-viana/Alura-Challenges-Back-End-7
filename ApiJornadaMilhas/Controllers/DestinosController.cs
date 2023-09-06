
using ApiJornadaMilhas.Data.Dto;
using ApiJornadaMilhas.Services;
using ApiJornadaMilhas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiJornadaMilhas.Controllers;

[ApiController]
[Route("[controller]")]
public class DestinosController : ControllerBase
{
    public MongoDBService _mongoDBService;
    public IMapper _mapper;

    public DestinosController (MongoDBService mongoDBService, IMapper mapper)
    {
        _mongoDBService = mongoDBService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarDestinos ([FromBody] CreateDestinosDto destinosDto)
    {
        try
        {
            Destinos destinos = _mapper.Map<Destinos>(destinosDto);
            await _mongoDBService.CreateAsyncDestinos(destinos);
            return Ok();
        }catch
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<IActionResult> RecuperarDestinos()
    {
        try
        {
            IEnumerable<ReadDestinosDto> destinosDto = await _mongoDBService.GetAsyncDestinos(_mapper);
            return Ok(destinosDto);
        }catch
        {
            return NotFound("Ocorreu algum erro na solicitação ou o banco está vazio");

        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> RecuperarDestinoPorId(string id)
    {
        try
        {
            ReadDestinosDto destinoDto = await _mongoDBService.GetASyncDestinosById(_mapper, id);
            if (destinoDto != null){
                return Ok (destinoDto);
            }else
            {
                return NotFound ("O destino não foi encontrado!");
            }
        }catch
        {
            return NotFound("Algo deu errado na solicitação!!!");
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarDestino (string id, [FromBody] UpdateDestinosDto updateDestinosDto)
    {
        try{
            ReadDestinosDto destino = await _mongoDBService.GetASyncDestinosById(_mapper, id);
            if (destino != null){
                ReadDestinosDto novoUpdateDestinosDto = _mapper.Map<ReadDestinosDto>(updateDestinosDto);
                novoUpdateDestinosDto.Id = destino.Id;
                Destinos updateDestinos = _mapper.Map<Destinos>(novoUpdateDestinosDto);
                await _mongoDBService.PutAsyncDestinos(id, updateDestinos);
                return NoContent();
            }else
            {
                return NotFound ("O destino não foi encontrado");
            }
        }catch{
            return NotFound ("Ocorreu algum erro na solicitação!!!");
        }
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarDestino(string id)
    {
        try{
            ReadDestinosDto destinoDto = await _mongoDBService.GetASyncDestinosById(_mapper, id);
            if (destinoDto != null){
                await _mongoDBService.DeleteAsyncDestinos(id);
                return NoContent();
            }else
            {
                return NotFound ("O destino não foi encontrado!");
            }
        }catch{
            return NotFound("Algo deu errado na solicitação!!!");
        }
    }

}