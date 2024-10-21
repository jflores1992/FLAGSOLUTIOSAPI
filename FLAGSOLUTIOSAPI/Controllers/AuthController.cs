using FLAGSOLUTIOSAPI.Clases;
using FLAGSOLUTIOSAPI.DataAcces;
using FLAGSOLUTIOSAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FLAGSOLUTIOSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly DataRepository _dataRepository;

        public AuthController(IConfiguration configuration,DataRepository dataRepository)
        {
            _configuration = configuration;
            _dataRepository = dataRepository;
        }
        [HttpPost("login")]
        public  async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var configuredUsername = _configuration["UserCredentials:Username"];
            var configuredPassword = _configuration["UserCredentials:Password"];
             var usuario = await _dataRepository.ObtenerDatosUsuario(login);
            if(usuario.Id != 0)
            {
              
                var token =await GenerateJwtToken(usuario);
                return Ok(new { token });
            }
            else
            {
                var error = new Error()
                {
                    Tipo = "Verificacion de Usuario",
                    MensajeInterno = "Credenciales Incorrecta"
                };

                return BadRequest(error);
            }

            return Unauthorized();
        }



        // POST api/<AuthController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        private async Task<TokenUsuario> GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
               // audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpiresInDias"])),
                signingCredentials: creds
            );
            var tokenExpira = DateTime.UtcNow.AddDays(1);

            return new TokenUsuario()
            {
                Id=Guid.NewGuid(),
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expira = tokenExpira,
                SucursalId = (int)usuario.SucursalId,
                UsuarioId = usuario.Id,
                PerfilId = (int)usuario.PerfilId,
                EmpresaId = (int)usuario.Sucursal.EmpresaId,
                Menus = (await _dataRepository.DameListaMenusByUsuarioAll(usuario.Id,usuario.PerfilId??0)).ToList(),
                NameEmpleado = usuario.Alias
            };
            //return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
