using FLAGSOLUTIOSAPI.Clases;
using FLAGSOLUTIOSAPI.DataAcces;
using FLAGSOLUTIOSAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using sitma.Models.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FLAGSOLUTIOSAPI.Controllers
{
    [EnableCors("AllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly DataRepository _dataRepository;
        private readonly MANTENIMIENTODBContext _context;
        public AuthController(IConfiguration configuration,DataRepository dataRepository, MANTENIMIENTODBContext context)
        {
            _configuration = configuration;
            _dataRepository = dataRepository;
            _context = context;
        }
        [HttpPost("login")]
        public  async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var configuredUsername = _configuration["UserCredentials:Username"];
                var configuredPassword = _configuration["UserCredentials:Password"];
                var usuario = await _dataRepository.ObtenerDatosUsuario(login);
                if (usuario.Id != 0)
                {

                    var token = await GenerateJwtToken(usuario);
                    RespuestaHttp respuestaHttp = new RespuestaHttp()
                    {
                        Exito = true,
                        Data = token,
                        Mensaje = "Exito",
                        MensajeInterno = ""
                    };


                    return Ok(new { respuestaHttp });
                }
                else
                {

                    RespuestaHttp respuestaHttp = new RespuestaHttp()
                    {
                        Exito = false,
                        Data = usuario,
                        Mensaje = "Verificacion de Usuario",
                        MensajeInterno = "Credenciales Incorrecta"
                    };

                    return BadRequest(respuestaHttp);
                }
            }
            catch (Exception ex)
            {

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }

        }



        // POST api/<AuthController>
        [HttpPost("Usuario")]
        [Authorize]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {


            if (usuario == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Password))
            {
                return BadRequest("Email y contraseña son obligatorios.");
            }
            try
            {
                usuario.FechaCreacion = DateTime.UtcNow;
                usuario.Activo = true; 
                usuario.EstadoBorrado = false; 

                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(PostUsuario), new { id = usuario.Id }, usuario),
                    Mensaje = "Exito",
                    MensajeInterno = ""
                };
                return Ok(new { respuestaHttp });


            }
            catch (Exception ex)
            {

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }

        // PUT api/<AuthController>/Usuario/{id}
        [HttpPut("Usuario/{id}")]
        [Authorize]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Password))
            {
                return BadRequest("Email y contraseña son obligatorios.");
            }

            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(id);
                if (usuarioExistente == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                usuarioExistente.Email = usuario.Email;
                usuarioExistente.Password = usuario.Password;
                usuarioExistente.Activo = usuario.Activo;
                usuarioExistente.FechaFinValidez = usuario.FechaFinValidez;
                usuarioExistente.ContrasenaTemporal = usuario.ContrasenaTemporal;
                usuarioExistente.IdUsuarioModificador = usuario.IdUsuarioModificador;
                usuarioExistente.FechaModificacion = DateTime.UtcNow;
                usuarioExistente.EstadoBorrado = usuario.EstadoBorrado;
                _context.Usuarios.Update(usuarioExistente);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = usuarioExistente,
                    Mensaje = "Usuario actualizado con éxito",
                    MensajeInterno = ""
                };
                return Ok(respuestaHttp);
            }
            catch (Exception ex)
            {
                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }

        private async Task<TokenUsuario> GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {

            new Claim(ClaimTypes.Name, usuario.Alias),
            new Claim(ClaimTypes.Role, usuario.Perfil.Nombre),
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

        [HttpGet("RefrescarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TokenUsuario>> RefrescarToken()
        {

            try
            {


                var user = HttpContext.User.Identity.Name;

                var userDb = await _dataRepository.ObtenerIdUsuario(user);

         

                if (userDb.Id != 0)
                {

                    var token = await GenerateJwtToken(userDb);
                    RespuestaHttp respuestaHttp = new RespuestaHttp()
                    {
                        Exito = true,
                        Data = token,
                        Mensaje = "Exito",
                        MensajeInterno = ""
                    };


                    return Ok(new { respuestaHttp });
                }
                else
                {

                    RespuestaHttp respuestaHttp = new RespuestaHttp()
                    {
                        Exito = false,
                        Data = userDb,
                        Mensaje = "Verificacion de Usuario",
                        MensajeInterno = "Usuario No Existe"
                    };

                    return BadRequest(respuestaHttp);
                }

            }
            catch (Exception ex)
            {

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
           
        }
    }
}
