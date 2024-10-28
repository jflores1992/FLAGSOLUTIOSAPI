﻿using FLAGSOLUTIOSAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sitma.Models.Generic;

namespace FLAGSOLUTIOSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {


        private readonly MANTENIMIENTODBContext _context;

        public SeguridadController(MANTENIMIENTODBContext context)
        {
            _context = context;
        }

        [HttpGet("Perfiles/{id}")]
        [Authorize]
        public async Task<ActionResult<Perfil>> GetPerfil(int id)
        {
            var perfil = await _context.Perfils.FindAsync(id);

            if (perfil == null)
            {
                return NotFound();
            }

            return perfil;
        }


        [HttpGet("Menus/{id}")]
        [Authorize]
        public async Task<ActionResult<Menu>> Getmenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        [HttpGet("UsuariosMenus/{id}")]
        [Authorize]
        public async Task<ActionResult<UsuariosMenu>> GetUsuariosmenu(int id)
        {
            var menu = await _context.UsuariosMenus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }


        // POST: api/seguridad/postPerfiles
        [HttpPost("Perfiles")]
        [Authorize]
        public async Task<IActionResult> PostPerfil([FromBody] Perfil perfil)
        {
            if (perfil == null)
            {
                return BadRequest("El Perfil no puede ser nulo.");
            }

            try
            {
                //perfil.Creacion = DateTime.UtcNow;
                _context.Perfils.Add(perfil);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetPerfil), new { id = perfil.Id }, perfil),
                    Mensaje = "Perfil creado con éxito",
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

        // POST: api/seguridad/postMenus
        [HttpPost("Menus")]
        [Authorize]
        public async Task<IActionResult> PostMenu([FromBody] Menu menu)
        {
            if (menu == null)
            {
                return BadRequest("El Menu no puede ser nulo.");
            }

            try
            {
                //perfil.Creacion = DateTime.UtcNow;
                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(Getmenu), new { id = menu.Id }, menu),
                    Mensaje = "Menu creado con éxito",
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


        // POST: api/seguridad/postUsuariosMenus
        [HttpPost("UsuariosMenus")]
        [Authorize]
        public async Task<IActionResult> PostUsuariosMenu([FromBody] UsuariosMenu menu)
        {
            if (menu == null)
            {
                return BadRequest("El Usuario-Menu no puede ser nulo.");
            }

            try
            {
                //perfil.Creacion = DateTime.UtcNow;
                _context.UsuariosMenus.Add(menu);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetUsuariosmenu), new { id = menu.Id }, menu),
                    Mensaje = "Usuario-Menu creado con éxito",
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


    }
}