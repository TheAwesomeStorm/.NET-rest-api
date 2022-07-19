using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Requests
{
    public class RecuperarSenhaRequest
    {
        [Required]
        public string Email { get; set; }
    }
}