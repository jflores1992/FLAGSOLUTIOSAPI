﻿using FLAGSOLUTIOSAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sitma.Models.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FLAGSOLUTIOSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {

        private readonly MANTENIMIENTODBContext _context;

        public CatalogosController(MANTENIMIENTODBContext context)
        {
            _context = context;
        }


        // GET: api/<CatalogosController>
        [HttpGet("Periodos")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Periodo>>> GetPeriodos()
        {
            return await _context.Periodos.ToListAsync();
        }

        // GET: api/catalogos/getPeriodo/{id}
        [HttpGet("Periodos/{id}")]
        [Authorize]
        public async Task<ActionResult<Periodo>> GetPeriodo(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);

            if (periodo == null)
            {
                return NotFound();
            }

            return periodo;
        }

        // POST: api/catalogos/postPeriodo
        [HttpPost("Periodos")]
        [Authorize]
        public async Task<IActionResult> PostPeriodo([FromBody] Periodo periodo)
        {
            if (periodo == null)
            {
                return BadRequest("El periodo no puede ser nulo.");
            }

            try
            {
                periodo.FechaCreacion = DateTime.UtcNow;
                _context.Periodos.Add(periodo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetPeriodo), new { id = periodo.Id }, periodo),
                    Mensaje = "Periodo creado con éxito",
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
        // PUT: api/catalogos/putPeriodo/{id}
        [HttpPut("Periodos/{id}")]
        [Authorize]
        public async Task<IActionResult> PutPeriodo(int id, [FromBody] Periodo periodo)
        {
            if (id != periodo.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            periodo.FechaModificacion= DateTime.UtcNow;
            _context.Entry(periodo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = periodo,
                    Mensaje = "Periodo actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodoExists(id))
                {
                    return NotFound("Periodo no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar el periodo." },
                            Mensaje = "Ocurrió un error de concurrencia",
                            MensajeInterno = ""
                        });
                }
            }
            catch (Exception ex)
            {
                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error inesperado",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        private bool PeriodoExists(int id)
        {
            return _context.Periodos.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deletePeriodo/{id}
        [HttpDelete("Periodos/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePeriodo(int id)
        {
            try
            {
                var periodo = await _context.Periodos.FindAsync(id);
                if (periodo == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Periodo no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.Periodos.Remove(periodo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Periodo eliminado con éxito.",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (Exception ex)
            {
                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al eliminar el periodo.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        // GET: api/<CatalogosController>
        [HttpGet("CategoriasMateriales")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoriasMateriale>>> GetCategoriasMateriales()
        {
            return await _context.CategoriasMateriales.ToListAsync();
        }

        // GET: api/catalogos/getCategoriasMateriales/{id}
        [HttpGet("CategoriasMateriales/{id}")]
        [Authorize]
        public async Task<ActionResult<CategoriasMateriale>> GetCategoriasMaterialesId(int id)
        {
            var categorias = await _context.CategoriasMateriales.FindAsync(id);

            if (categorias == null)
            {
                return NotFound();
            }

            return categorias;
        }

        // POST: api/catalogos/postCategoriasMateriales
        [HttpPost("CategoriasMateriales")]
        [Authorize]
        public async Task<IActionResult> PostCategoriasMateriales([FromBody] CategoriasMateriale nuevo)
        {
            if (nuevo == null)
            {
                return BadRequest("La Categoria no puede ser nulo.");
            }

            try
            {
                nuevo.FechaCreacion = DateTime.UtcNow;
                _context.CategoriasMateriales.Add(nuevo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetCategoriasMaterialesId), new { id = nuevo.Id }, nuevo),
                    Mensaje = "Categoria Material creado con éxito",
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
        // PUT: api/catalogos/putCategoriasMateriales/{id}
        [HttpPut("CategoriasMateriales/{id}")]
        [Authorize]
        public async Task<IActionResult> PutCategoriasMateriales(int id, [FromBody] CategoriasMateriale modelo)
        {
            if (id != modelo.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            modelo.FechaModificacion = DateTime.UtcNow;
            _context.Entry(modelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = modelo,
                    Mensaje = "Categoria Material actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaMaterialExists(id))
                {
                    return NotFound("Categoria Material no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar la categoria naterial." },
                            Mensaje = "Ocurrió un error de concurrencia",
                            MensajeInterno = ""
                        });
                }
            }
            catch (Exception ex)
            {
                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error inesperado",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        private bool CategoriaMaterialExists(int id)
        {
            return _context.CategoriasMateriales.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteCategoriasMateriales/{id}
        [HttpDelete("CategoriasMateriales/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategoriasMateriales(int id)
        {
            try
            {
                var categoria = await _context.CategoriasMateriales.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Categoria Material no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.CategoriasMateriales.Remove(categoria);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Categoria Material eliminado con éxito.",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (Exception ex)
            {
                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al eliminar la  Categoria Material.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        // GET: api/catalogos/estatusOrdenes
        [HttpGet("EstatusOrdenes")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EstatusOrdene>>> GetEstatusOrdenes()
        {
           return await _context.EstatusOrdenes.ToListAsync();
            
        }

        // GET: api/catalogos/estatusOrdenes/{id}
        [HttpGet("EstatusOrdenes/{id}")]
        [Authorize]
        public async Task<ActionResult<EstatusOrdene>> GetEstatusOrden(int id)
        {
            var estatusOrden = await _context.EstatusOrdenes.FindAsync(id);
            if (estatusOrden == null)
            {
                return NotFound();
            }

            return estatusOrden;
        }

        // POST: api/catalogos/estatusOrdenes
        [HttpPost("EstatusOrdenes")]
        [Authorize]
        public async Task<IActionResult> PostEstatusOrden([FromBody] EstatusOrdene estatusOrden)
        {
            if (estatusOrden == null)
            {
                return BadRequest("El estatus de orden no puede ser nulo.");
            }

            try
            {
                _context.EstatusOrdenes.Add(estatusOrden);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEstatusOrden), new { id = estatusOrden.Id }, new RespuestaHttp
                {
                    Exito = true,
                    Data = estatusOrden,
                    Mensaje = "Estatus de orden creado con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al crear el estatus de orden.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        // PUT: api/catalogos/estatusOrdenes/{id}
        [HttpPut("EstatusOrdenes/{id}")]
        [Authorize]
        public async Task<IActionResult> PutEstatusOrden(int id, [FromBody] EstatusOrdene estatusOrden)
        {
            if (id != estatusOrden.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            _context.Entry(estatusOrden).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new RespuestaHttp
                {
                    Exito = true,
                    Data = estatusOrden,
                    Mensaje = "Estatus de orden actualizado con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstatusOrdenExists(id))
                {
                    return NotFound("Estatus de orden no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                    {
                        Exito = false,
                        Data = new { ErrorMessage = "Error al actualizar el estatus de orden." },
                        Mensaje = "Ocurrió un error de concurrencia.",
                        MensajeInterno = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error inesperado.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }
        private bool EstatusOrdenExists(int id)
        {
            return _context.EstatusOrdenes.Any(e => e.Id == id);
        }


        // DELETE: api/catalogos/estatusOrdenes/{id}
        [HttpDelete("EstatusOrdenes/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEstatusOrden(int id)
        {
            try
            {
                var estatusOrden = await _context.EstatusOrdenes.FindAsync(id);
                if (estatusOrden == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Estatus de orden no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.EstatusOrdenes.Remove(estatusOrden);
                await _context.SaveChangesAsync();

                return Ok(new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Estatus de orden eliminado con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al eliminar el estatus de orden.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        // GET: api/catalogos/centrosEmplazamiento
        [HttpGet("CentrosEmplazamiento")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CentrosEmplazamiento>>> GetCentrosEmplazamiento()
        {
            return await _context.CentrosEmplazamientos.ToListAsync();
            
        }


        // GET: api/catalogos/centrosEmplazamiento/{id}
        [HttpGet("CentrosEmplazamiento/{id}")]
        [Authorize]
        public async Task<ActionResult<CentrosEmplazamiento>> GetCentroEmplazamiento(int id)
        {
            var centroEmplazamiento = await _context.CentrosEmplazamientos.FindAsync(id);
            if (centroEmplazamiento == null)
            {
                return NotFound();
            }
            return centroEmplazamiento;
        }


        // POST: api/catalogos/centrosEmplazamiento
        [HttpPost("CentrosEmplazamiento")]
        [Authorize]
        public async Task<IActionResult> PostCentroEmplazamiento([FromBody] CentrosEmplazamiento centroEmplazamiento)
        {
            if (centroEmplazamiento == null)
            {
                return BadRequest("El centro de emplazamiento no puede ser nulo.");
            }

            try
            {
                _context.CentrosEmplazamientos.Add(centroEmplazamiento);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCentroEmplazamiento), new { id = centroEmplazamiento.Id }, new RespuestaHttp
                {
                    Exito = true,
                    Data = centroEmplazamiento,
                    Mensaje = "Centro de emplazamiento creado con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al crear el centro de emplazamiento.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        // PUT: api/catalogos/centrosEmplazamiento/{id}
        [HttpPut("CentrosEmplazamiento/{id}")]
        [Authorize]
        public async Task<IActionResult> PutCentroEmplazamiento(int id, [FromBody] CentrosEmplazamiento centroEmplazamiento)
        {
            if (id != centroEmplazamiento.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            _context.Entry(centroEmplazamiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new RespuestaHttp
                {
                    Exito = true,
                    Data = centroEmplazamiento,
                    Mensaje = "Centro de emplazamiento actualizado con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentroEmplazamientoExists(id))
                {
                    return NotFound("Centro de emplazamiento no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                    {
                        Exito = false,
                        Data = new { ErrorMessage = "Error al actualizar el centro de emplazamiento." },
                        Mensaje = "Ocurrió un error de concurrencia.",
                        MensajeInterno = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error inesperado.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        // DELETE: api/catalogos/centrosEmplazamiento/{id}
        [HttpDelete("CentrosEmplazamiento/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCentroEmplazamiento(int id)
        {
            try
            {
                var centroEmplazamiento = await _context.CentrosEmplazamientos.FindAsync(id);
                if (centroEmplazamiento == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Centro de emplazamiento no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.CentrosEmplazamientos.Remove(centroEmplazamiento);
                await _context.SaveChangesAsync();

                return Ok(new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Centro de emplazamiento eliminado con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al eliminar el centro de emplazamiento.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        private bool CentroEmplazamientoExists(int id)
        {
            return _context.CentrosEmplazamientos.Any(e => e.Id == id);
        }




        // GET: api/catalogos/Sucursales
        [HttpGet("Sucursales")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Sucursale>>> GetSucursales()
        {
            return await _context.Sucursales.ToListAsync();

        }


        // GET: api/catalogos/Sucursales/{id}
        [HttpGet("Sucursal/{id}")]
        [Authorize]
        public async Task<ActionResult<Sucursale>> GetSucursal(int id)
        {
            var Sucursal = await _context.Sucursales.FindAsync(id);
            if (Sucursal == null)
            {
                return NotFound();
            }
            return Sucursal;
        }


        // POST: api/catalogos/Sucursal
        [HttpPost("Sucursal")]
        [Authorize]
        public async Task<IActionResult> PostSucursal([FromBody] Sucursale Sucursal)
        {
            if (Sucursal == null)
            {
                return BadRequest("La Sucursal no puede ser nulo.");
            }

            try
            {
                Sucursal.Activo=true;
                _context.Sucursales.Add(Sucursal);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetSucursal), new { id = Sucursal.Id }, new RespuestaHttp
                {
                    Exito = true,
                    Data = Sucursal,
                    Mensaje = "Sucursal creado con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al crear el centro de emplazamiento.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        // PUT: api/catalogos/centrosEmplazamiento/{id}
        [HttpPut("Sucursal/{id}")]
        [Authorize]
        public async Task<IActionResult> PutSucursal(int id, [FromBody] Sucursale Sucursal)
        {
            if (id != Sucursal.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            _context.Entry(Sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new RespuestaHttp
                {
                    Exito = true,
                    Data = Sucursal,
                    Mensaje = "Sucursal actualizado con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id))
                {
                    return NotFound("Sucursal no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                    {
                        Exito = false,
                        Data = new { ErrorMessage = "Error al actualizar la Sucursal." },
                        Mensaje = "Ocurrió un error de concurrencia.",
                        MensajeInterno = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error inesperado.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        // DELETE: api/catalogos/centrosEmplazamiento/{id}
        [HttpDelete("Sucursal/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSucursal(int id)
        {
            try
            {
                var Sucursal = await _context.Sucursales.FindAsync(id);
                if (Sucursal == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Sucursal no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.Sucursales.Remove(Sucursal);
                await _context.SaveChangesAsync();

                return Ok(new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Sucursal eliminado con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al eliminar el centro de emplazamiento.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        private bool SucursalExists(int id)
        {
            return _context.Sucursales.Any(e => e.Id == id);
        }

        // GET: api/catalogos/Empresas
        [HttpGet("Empresas")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            return await _context.Empresas.ToListAsync();
        }

        // GET: api/catalogos/Empresas/{id}
        [HttpGet("Empresa/{id}")]
        [Authorize]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return empresa;
        }


        // POST: api/catalogos/Empresa
        [HttpPost("Empresa")]
        [Authorize]
        public async Task<IActionResult> PostEmpresa([FromBody] Empresa empresa)
        {
            if (empresa == null)
            {
                return BadRequest("La empresa no puede ser nula.");
            }

            try
            {
                empresa.Creacion = DateTime.Now;
                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, new RespuestaHttp
                {
                    Exito = true,
                    Data = empresa,
                    Mensaje = "Empresa creada con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al crear la empresa.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }


        // PUT: api/catalogos/Empresa/{id}
        [HttpPut("Empresa/{id}")]
        [Authorize]
        public async Task<IActionResult> PutEmpresa(int id, [FromBody] Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new RespuestaHttp
                {
                    Exito = true,
                    Data = empresa,
                    Mensaje = "Empresa actualizada con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
                {
                    return NotFound("Empresa no encontrada.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                    {
                        Exito = false,
                        Data = new { ErrorMessage = "Error al actualizar la empresa." },
                        Mensaje = "Ocurrió un error de concurrencia.",
                        MensajeInterno = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error inesperado.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        // DELETE: api/catalogos/Empresa/{id}
        [HttpDelete("Empresa/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);
                if (empresa == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Empresa no encontrada.",
                        MensajeInterno = ""
                    });
                }

                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();

                return Ok(new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Empresa eliminada con éxito.",
                    MensajeInterno = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RespuestaHttp
                {
                    Exito = false,
                    Data = new { ErrorMessage = ex.Message, ErrorType = ex.GetType().Name },
                    Mensaje = "Ocurrió un error al eliminar la empresa.",
                    MensajeInterno = ex.InnerException?.Message
                });
            }
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
