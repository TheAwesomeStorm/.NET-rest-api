using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Requests
{
    public class ConfirmarEmailRequest
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string CodigoDeAtivacao { get; set; }
    }
}