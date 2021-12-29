using System.Collections.Generic;
using System.Linq;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> _filmes = new List<Filme>();
        private static int _id = 1;

        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {
            filme.Id = _id++;
            _filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperarFilmePorId), new { Id = filme.Id}, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return _filmes;
        }
        
        [HttpGet("{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {
            Filme filme = _filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                Ok(filme);
            }
            return NotFound();
        }
    }
}