using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.DTOs.Usuario;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;
        private EmailService _emailService;

        public UsuarioService(
            IMapper mapper,
            UserManager<IdentityUser<int>> userManager,
            SignInManager<IdentityUser<int>> signInManager,
            TokenService tokenService,
            EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }
        public Result CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> identityResult = _userManager.CreateAsync(usuarioIdentity, usuarioDto.Password);
            
            if (!identityResult.Result.Succeeded) return Result.Fail("Falha ao cadastrar o usuário");
            
            string code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
            string encodedCode = HttpUtility.UrlEncode(code);
            _emailService.Enviar(new[] {usuarioIdentity.Email}, "Link de Ativação", usuarioIdentity.Id, encodedCode);

            return Result.Ok().WithSuccess(code);
        }

        public Result ConfirmarEmail(ConfirmarEmailRequest request)
        {
            IdentityUser<int> identityUser = _signInManager.UserManager.Users.FirstOrDefault(usuario =>
                usuario.Id == request.UsuarioId);
            IdentityResult identityResult =  _userManager.ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;
            
            if (identityResult.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao confirmar email de usuario");
        }

        public Result LogarUsuário(LoginRequest request)
        {
            Task<SignInResult> result = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false);
            
            if (!result.Result.Succeeded) return Result.Fail("Login falhou");
            
            IdentityUser<int> identityUser = _signInManager.UserManager.Users.FirstOrDefault(usuario =>
                usuario.NormalizedUserName == request.Username.ToUpper());
            Token token = _tokenService.CreateToken(identityUser);
            
            return Result.Ok().WithSuccess(token.Value);
        }

        public Result DeslogarUsuario()
        {
            Task result = _signInManager.SignOutAsync();
            if (result.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Logout falhou");
        }
    }
}