using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.DTOs.Usuario
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}