using System;
using System.Collections.Generic;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> _filmes = new List<Filme>();

        [HttpPost]
        public void AdicionarFilme([FromBody] Filme filme)
        {
            _filmes.Add(filme);
            Console.WriteLine(filme.Titulo);
        }

        [HttpGet]
        public void Teste()
        {
            Console.WriteLine("'oi'");
        }
    }
}