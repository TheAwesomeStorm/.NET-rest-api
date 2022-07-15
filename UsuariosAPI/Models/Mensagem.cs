using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace UsuariosAPI.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatario, string assunto, int usuarioId, string codigoDeAtivacao)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(element => MailboxAddress.Parse(element)));
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/usuario/confirmar?UsuarioId={usuarioId}&CodigoDeAtivacao={codigoDeAtivacao}";
        }
    }
}