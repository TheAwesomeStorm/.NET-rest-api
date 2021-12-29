using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "Limite máximo de caracteres excedido.")]
        public string Genero { get; set; }
        [Range(1, 600)]
        public int Duracao { get; set; }
    }
}