using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        public Token CreateToken(CustomIdentityUser usuario, string role)
        {
            Claim[] userRights = {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding
                    .UTF8
                    .GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn"));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: userRights,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1));

            string tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenstring);
        }
    }
}