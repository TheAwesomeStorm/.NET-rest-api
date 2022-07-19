using System.Collections.Generic;
using FilmesAPI.Data.DTOs.Filme;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeService _filmeService;
        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {
            ReadFilmeDto readDto = _filmeService.AdicionarFilme(filmeDto);
            return CreatedAtAction(nameof(RecuperarFilmePorId), new { Id = readDto.Id}, readDto);
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular", Policy = "idadeMinima")]
        public IActionResult RecuperarFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            List<ReadFilmeDto> readDtos = _filmeService.RecuperarFilmes(classificacaoEtaria);
            if (readDtos != null)
            {
                return Ok(readDtos);
            }
            return NotFound();
        }
        
        [HttpGet("{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {
            ReadFilmeDto readDto = _filmeService.RecuperarFilmePorId(id);
            if (readDto != null)
            {
                return Ok(readDto);
            }
            return NotFound();
        }
        
        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Result result = _filmeService.AtualizarFilme(id, filmeDto);
            
            if (result.IsFailed) return NotFound();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeletarFilme(int id)
        {
            Result result = _filmeService.DeletarFilme(id);

            if (result.IsFailed) return NotFound();
            return NoContent();
        }
    }
}