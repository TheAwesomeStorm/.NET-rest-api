using System.Linq;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs.Gerente;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;
        
        public GerenteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult AdicionarGerente(CreateGerenteDto dto)
        {
            Gerente gerente = _mapper.Map<Gerente>(dto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarGerentesPorId), new {Id = gerente.Id }, gerente);
        }
    
        [HttpGet("{id}")]
        public IActionResult RecuperarGerentesPorId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente != null)
            {
                ReadGerenteDto gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
                return Ok(gerente);
            }
            return NotFound();
        }
    }
}