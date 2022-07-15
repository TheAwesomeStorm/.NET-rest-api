using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Enviar(string[] destinatario, string assunto, int usuarioId, string codigoDeAtivacao)
        {
            Mensagem mensagem = new Mensagem(destinatario, assunto, usuarioId, codigoDeAtivacao);
            MimeMessage mensagemDeEmail = CriarCorpoDoEmail(mensagem);
            EnviarEmail(mensagemDeEmail);
        }

        private MimeMessage CriarCorpoDoEmail(Mensagem mensagem)
        {
            string remetente = _configuration.GetValue<string>("EmailSettings:From");
            MimeMessage mensagemDeEmail = new MimeMessage();
            mensagemDeEmail.From.Add(MailboxAddress.Parse(remetente));
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };
            return mensagemDeEmail;
        }

        private void EnviarEmail(MimeMessage mensagem)
        {
            using (SmtpClient client = new SmtpClient())
            {
                string smtpServer = _configuration.GetValue<string>("EmailSettings:SmtpServer");
                int port = _configuration.GetValue<int>("EmailSettings:Port");
                string remetente = _configuration.GetValue<string>("EmailSettings:From");
                string password = _configuration.GetValue<string>("EmailSettings:Password");
                try
                {
                    client.Connect(smtpServer, port, true);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(remetente, password);
                    client.Send(mensagem);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}