using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.DTOs.Usuario;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("cadastro")]
        public IActionResult CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            Result result = _usuarioService.CadastrarUsuario(usuarioDto);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }

        [HttpGet]
        [Route("confirmar")]
        public IActionResult ConfirmarUsuario([FromQuery] ConfirmarEmailRequest request)
        {
            Result result = _usuarioService.ConfirmarEmail(request);
            if (result.IsFailed) return StatusCode(500);
            return Ok(result.Successes);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LogarUsuario(LoginRequest request)
        {
            Result result = _usuarioService.LogarUsuário(request);
            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult DeslogarUsuario()
        {
            Result result = _usuarioService.DeslogarUsuario();
            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }

        [HttpPost]
        [Route("recover")]
        public IActionResult RecuperarSenha(RecuperarSenhaRequest request)
        {
            Result result = _usuarioService.RecuperarSenha(request);
            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }

        [HttpPost]
        [Route("reset")]
        public IActionResult RecadastrarSenha(RecadastrarSenhaRequest request)
        {
            Result result = _usuarioService.RecadastrarSenha(request);
            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }
    }
}