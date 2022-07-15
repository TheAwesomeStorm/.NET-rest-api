﻿using FluentResults;
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
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LogarUsuario(LoginRequest request)
        {
            Result result = _usuarioService.LogarUsuário(request);
            if (result.IsFailed) return Unauthorized(result.Errors);
            return Ok(result.Successes);
        }
    }
}