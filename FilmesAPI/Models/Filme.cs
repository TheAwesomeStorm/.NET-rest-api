using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key][Required]
        public int Id { get; internal set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "Limite máximo de caracteres excedido.")]
        public string Genero { get; set; }
        [Range(1, 600)]
        public int Duracao { get; set; }
        [JsonIgnore] public virtual List<Sessao> Sessoes { get; set; }
    }
}