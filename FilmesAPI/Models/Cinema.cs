﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Cinema
    {
        [Key][Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatory")]
        public string Nome { get; set; }
    }
}