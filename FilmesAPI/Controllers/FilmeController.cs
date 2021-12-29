using System;
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
        private static int id = 1;

        [HttpPost]
        public void AdicionarFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            _filmes.Add(filme);
            Console.WriteLine($"Filme {filme.Titulo} cadastrado");
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return _filmes;
        }
        
        [HttpGet("{id}")]
        public Filme RecuperarFilmePorId(int id)
        {
            return _filmes.FirstOrDefault(filme => filme.Id == id);
        }
    }
}