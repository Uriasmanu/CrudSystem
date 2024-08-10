using CrudSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Login == "admim" && login.Password == "admim")
            {
                var token = GerarTokenJWT();
                return Ok(new { token });
            }
            return BadRequest(new { mensagem = "Credenciais invalidas. Por favor, verifique seu nome de usuario e senha" });
        }

        private string GerarTokenJWT()
        {
            string chaveSecreta = "e3c46810-b96e-40d9-a9eb-9ffa7b373e5b";
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", "admim"),
                new Claim("nome", "Administrados do sistema")
            };

            var token = new JwtSecurityToken(
                issuer: "sua_empresa",
                audience: "sua_aplicacao",
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credencial
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }


}


