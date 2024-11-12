using FLAGSOLUTIOSAPI.Models;
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

                

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetEstatusOrden), new { id = estatusOrden.Id }, estatusOrden),
                    Mensaje = "Estatus de orden creado con éxito.",
                    MensajeInterno = ""
                };
                return Ok(respuestaHttp);

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
                centroEmplazamiento.FechaCreacion = DateTime.UtcNow;

                centroEmplazamiento.Activo = true;
                centroEmplazamiento.EstadoBorrado = false;

                _context.CentrosEmplazamientos.Add(centroEmplazamiento);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetCentroEmplazamiento), new { id = centroEmplazamiento.Id }, centroEmplazamiento),
                    Mensaje = "Centro de emplazamiento creado con éxito.",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
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

                

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetSucursal), new { id = Sucursal.Id }, Sucursal),
                    Mensaje = "Sucursal creado con éxito.",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);


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

                
                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa),
                    Mensaje = "Empresa creada con éxito.",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);


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


        // GET: api/catalogos/unidadesMedidaCaracteristica
        [HttpGet("UnidadesMedidaCaracteristica")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UnidadesMedidaCaracteristica>>> GetUnidadesMedidaCaracteristicas()
        {
            return await _context.UnidadesMedidaCaracteristicas.ToListAsync();
        }

        // GET: api/catalogos/unidadesMedidaCaracteristica/{id}
        [HttpGet("UnidadesMedidaCaracteristica/{id}")]
        [Authorize]
        public async Task<ActionResult<UnidadesMedidaCaracteristica>> GetUnidadesMedidaCaracteristica(int id)
        {
            var unidadMedida = await _context.UnidadesMedidaCaracteristicas.FindAsync(id);

            if (unidadMedida == null)
            {
                return NotFound();
            }

            return unidadMedida;
        }

        // POST: api/catalogos/postUnidadesMedidaCaracteristica
        [HttpPost("UnidadesMedidaCaracteristica")]
        [Authorize]
        public async Task<IActionResult> PostUnidadesMedidaCaracteristica([FromBody] UnidadesMedidaCaracteristica unidadMedida)
        {
            if (unidadMedida == null)
            {
                return BadRequest("La unidad de medida no puede ser nula.");
            }

            try
            {
                unidadMedida.FechaCreacion = DateTime.UtcNow;
                _context.UnidadesMedidaCaracteristicas.Add(unidadMedida);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetUnidadesMedidaCaracteristica), new { id = unidadMedida.Id }, unidadMedida),
                    Mensaje = "Unidad de medida creada con éxito",
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

        // PUT: api/catalogos/putUnidadesMedidaCaracteristica/{id}
        [HttpPut("UnidadesMedidaCaracteristica/{id}")]
        [Authorize]
        public async Task<IActionResult> PutUnidadesMedidaCaracteristica(int id, [FromBody] UnidadesMedidaCaracteristica unidadMedida)
        {
            if (id != unidadMedida.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            unidadMedida.FechaModificacion = DateTime.UtcNow;
            _context.Entry(unidadMedida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = unidadMedida,
                    Mensaje = "Unidad de medida actualizada con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadesMedidaCaracteristicaExists(id))
                {
                    return NotFound("Unidad de medida no encontrada.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar la unidad de medida." },
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

        private bool UnidadesMedidaCaracteristicaExists(int id)
        {
            return _context.UnidadesMedidaCaracteristicas.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteUnidadesMedidaCaracteristica/{id}
        [HttpDelete("UnidadesMedidaCaracteristica/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUnidadesMedidaCaracteristica(int id)
        {
            try
            {
                var unidadMedida = await _context.UnidadesMedidaCaracteristicas.FindAsync(id);
                if (unidadMedida == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Unidad de medida no encontrada.",
                        MensajeInterno = ""
                    });
                }

                _context.UnidadesMedidaCaracteristicas.Remove(unidadMedida);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Unidad de medida eliminada con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar la unidad de medida.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        // GET: api/catalogos/seccionesMaquina
        [HttpGet("SeccionesMaquina")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SeccionesMaquina>>> GetSeccionesMaquinas()
        {
            return await _context.SeccionesMaquinas.ToListAsync();
        }

        // GET: api/catalogos/seccionesMaquina/{id}
        [HttpGet("SeccionesMaquina/{id}")]
        [Authorize]
        public async Task<ActionResult<SeccionesMaquina>> GetSeccionMaquina(int id)
        {
            var seccionMaquina = await _context.SeccionesMaquinas.FindAsync(id);

            if (seccionMaquina == null)
            {
                return NotFound();
            }

            return seccionMaquina;
        }

        // POST: api/catalogos/postSeccionesMaquina
        [HttpPost("SeccionesMaquina")]
        [Authorize]
        public async Task<IActionResult> PostSeccionMaquina([FromBody] SeccionesMaquina seccionMaquina)
        {
            if (seccionMaquina == null)
            {
                return BadRequest("La sección de máquina no puede ser nula.");
            }

            try
            {
                seccionMaquina.FechaCreacion = DateTime.UtcNow;
                _context.SeccionesMaquinas.Add(seccionMaquina);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetSeccionMaquina), new { id = seccionMaquina.Id }, seccionMaquina),
                    Mensaje = "Sección de máquina creada con éxito",
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

        // PUT: api/catalogos/putSeccionesMaquina/{id}
        [HttpPut("SeccionesMaquina/{id}")]
        [Authorize]
        public async Task<IActionResult> PutSeccionMaquina(int id, [FromBody] SeccionesMaquina seccionMaquina)
        {
            if (id != seccionMaquina.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            seccionMaquina.FechaModificacion = DateTime.UtcNow;
            _context.Entry(seccionMaquina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = seccionMaquina,
                    Mensaje = "Sección de máquina actualizada con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeccionesMaquinaExists(id))
                {
                    return NotFound("Sección de máquina no encontrada.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar la sección de máquina." },
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

        private bool SeccionesMaquinaExists(int id)
        {
            return _context.SeccionesMaquinas.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteSeccionesMaquina/{id}
        [HttpDelete("SeccionesMaquina/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSeccionMaquina(int id)
        {
            try
            {
                var seccionMaquina = await _context.SeccionesMaquinas.FindAsync(id);
                if (seccionMaquina == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Sección de máquina no encontrada.",
                        MensajeInterno = ""
                    });
                }

                _context.SeccionesMaquinas.Remove(seccionMaquina);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Sección de máquina eliminada con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar la sección de máquina.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }



        // GET: api/catalogos/TiposPuntoMedida
        [HttpGet("TiposPuntoMedida")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TiposPuntoMedida>>> GetTiposPuntoMedida()
        {
            return await _context.TiposPuntoMedida.ToListAsync();
        }

        // GET: api/catalogos/TiposPuntoMedida/{id}
        [HttpGet("TiposPuntoMedida/{id}")]
        [Authorize]
        public async Task<ActionResult<TiposPuntoMedida>> GetTipoPuntoMedidum(int id)
        {
            var tipoPunto = await _context.TiposPuntoMedida.FindAsync(id);

            if (tipoPunto == null)
            {
                return NotFound();
            }

            return tipoPunto;
        }

        // POST: api/catalogos/postTiposPuntoMedida
        [HttpPost("TiposPuntoMedida")]
        [Authorize]
        public async Task<IActionResult> PostTipoPuntoMedidum([FromBody] TiposPuntoMedida tipoPunto)
        {
            if (tipoPunto == null)
            {
                return BadRequest("El tipo de punto de medida no puede ser nulo.");
            }

            try
            {
                tipoPunto.FechaCreacion = DateTime.UtcNow;
                _context.TiposPuntoMedida.Add(tipoPunto);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetTipoPuntoMedidum), new { id = tipoPunto.Id }, tipoPunto),
                    Mensaje = "Tipo de punto de medida creado con éxito",
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

        // PUT: api/catalogos/putTiposPuntoMedida/{id}
        [HttpPut("TiposPuntoMedida/{id}")]
        [Authorize]
        public async Task<IActionResult> PutTipoPuntoMedidum(int id, [FromBody] TiposPuntoMedida tipoPunto)
        {
            if (id != tipoPunto.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            tipoPunto.FechaModificacion = DateTime.UtcNow;
            _context.Entry(tipoPunto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = tipoPunto,
                    Mensaje = "Tipo de punto de medida actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiposPuntoMedidaExists(id))
                {
                    return NotFound("Tipo de punto de medida no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar el tipo de punto de medida." },
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

        private bool TiposPuntoMedidaExists(int id)
        {
            return _context.TiposPuntoMedida.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteTiposPuntoMedida/{id}
        [HttpDelete("TiposPuntoMedida/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTipoPuntoMedidum(int id)
        {
            try
            {
                var tipoPunto = await _context.TiposPuntoMedida.FindAsync(id);
                if (tipoPunto == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Tipo de punto de medida no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.TiposPuntoMedida.Remove(tipoPunto);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Tipo de punto de medida eliminado con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar el tipo de punto de medida.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        // GET: api/catalogos/PrioridadesMantenimientos
        [HttpGet("PrioridadesMantenimientos")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PrioridadesMantenimientos>>> GetPrioridadesMantenimientos()
        {
            return await _context.PrioridadesMantenimientoss.ToListAsync();
        }

        // GET: api/catalogos/PrioridadesMantenimientos/{id}
        [HttpGet("PrioridadesMantenimientos/{id}")]
        [Authorize]
        public async Task<ActionResult<PrioridadesMantenimientos>> GetPrioridadMantenimiento(int id)
        {
            var prioridad = await _context.PrioridadesMantenimientoss.FindAsync(id);

            if (prioridad == null)
            {
                return NotFound();
            }

            return prioridad;
        }

        // POST: api/catalogos/postPrioridadesMantenimientos
        [HttpPost("PrioridadesMantenimientos")]
        [Authorize]
        public async Task<IActionResult> PostPrioridadMantenimiento([FromBody] PrioridadesMantenimientos prioridad)
        {
            if (prioridad == null)
            {
                return BadRequest("La prioridad de mantenimiento no puede ser nula.");
            }

            try
            {
                prioridad.FechaCreacion = DateTime.UtcNow;
                _context.PrioridadesMantenimientoss.Add(prioridad);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetPrioridadMantenimiento), new { id = prioridad.Id }, prioridad),
                    Mensaje = "Prioridad de mantenimiento creada con éxito",
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

        // PUT: api/catalogos/putPrioridadesMantenimientos/{id}
        [HttpPut("PrioridadesMantenimientos/{id}")]
        [Authorize]
        public async Task<IActionResult> PutPrioridadMantenimiento(int id, [FromBody] PrioridadesMantenimientos prioridad)
        {
            if (id != prioridad.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            prioridad.FechaModificacion = DateTime.UtcNow;
            _context.Entry(prioridad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = prioridad,
                    Mensaje = "Prioridad de mantenimiento actualizada con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrioridadesMantenimientosExists(id))
                {
                    return NotFound("Prioridad de mantenimiento no encontrada.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar la prioridad de mantenimiento." },
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

        private bool PrioridadesMantenimientosExists(int id)
        {
            return _context.PrioridadesMantenimientoss.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deletePrioridadesMantenimientos/{id}
        [HttpDelete("PrioridadesMantenimientos/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePrioridadMantenimiento(int id)
        {
            try
            {
                var prioridad = await _context.PrioridadesMantenimientoss.FindAsync(id);
                if (prioridad == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Prioridad de mantenimiento no encontrada.",
                        MensajeInterno = ""
                    });
                }

                _context.PrioridadesMantenimientoss.Remove(prioridad);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Prioridad de mantenimiento eliminada con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar la prioridad de mantenimiento.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }



        // GET: api/catalogos/materiales
        [HttpGet("Materiales")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Materiale>>> GetMateriales()
        {
            return await _context.Materiales.ToListAsync();
        }

        // GET: api/catalogos/materiales/{id}
        [HttpGet("Materiales/{id}")]
        [Authorize]
        public async Task<ActionResult<Materiale>> GetMateriale(int id)
        {
            var material = await _context.Materiales.FindAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            return material;
        }

        // POST: api/catalogos/postMateriales
        [HttpPost("Materiales")]
        [Authorize]
        public async Task<IActionResult> PostMateriale([FromBody] Materiale material)
        {
            if (material == null)
            {
                return BadRequest("El material no puede ser nulo.");
            }

            try
            {
                material.FechaCreacion = DateTime.UtcNow;
                _context.Materiales.Add(material);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetMateriale), new { id = material.Id }, material),
                    Mensaje = "Material creado con éxito",
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

        // PUT: api/catalogos/putMateriales/{id}
        [HttpPut("Materiales/{id}")]
        [Authorize]
        public async Task<IActionResult> PutMateriale(int id, [FromBody] Materiale material)
        {
            if (id != material.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            material.FechaModificacion = DateTime.UtcNow;
            _context.Entry(material).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = material,
                    Mensaje = "Material actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialeExists(id))
                {
                    return NotFound("Material no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar el material." },
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

        private bool MaterialeExists(int id)
        {
            return _context.Materiales.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteMateriales/{id}
        [HttpDelete("Materiales/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMateriale(int id)
        {
            try
            {
                var material = await _context.Materiales.FindAsync(id);
                if (material == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Material no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.Materiales.Remove(material);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Material eliminado con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar el material.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        // GET: api/catalogos/indicadoresCriticidad
        [HttpGet("IndicadoresCriticidad")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<IndicadoresCriticidad>>> GetIndicadoresCriticidad()
        {
            return await _context.IndicadoresCriticidads.ToListAsync();
        }

        // GET: api/catalogos/indicadoresCriticidad/{id}
        [HttpGet("IndicadoresCriticidad/{id}")]
        [Authorize]
        public async Task<ActionResult<IndicadoresCriticidad>> GetIndicadorCriticidad(int id)
        {
            var indicador = await _context.IndicadoresCriticidads.FindAsync(id);

            if (indicador == null)
            {
                return NotFound();
            }

            return indicador;
        }

        // POST: api/catalogos/postIndicadoresCriticidad
        [HttpPost("IndicadoresCriticidad")]
        [Authorize]
        public async Task<IActionResult> PostIndicadorCriticidad([FromBody] IndicadoresCriticidad indicador)
        {
            if (indicador == null)
            {
                return BadRequest("El indicador de criticidad no puede ser nulo.");
            }

            try
            {
                indicador.FechaCreacion = DateTime.UtcNow;
                _context.IndicadoresCriticidads.Add(indicador);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetIndicadorCriticidad), new { id = indicador.Id }, indicador),
                    Mensaje = "Indicador de criticidad creado con éxito",
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

        // PUT: api/catalogos/putIndicadoresCriticidad/{id}
        [HttpPut("IndicadoresCriticidad/{id}")]
        [Authorize]
        public async Task<IActionResult> PutIndicadorCriticidad(int id, [FromBody] IndicadoresCriticidad indicador)
        {
            if (id != indicador.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            indicador.FechaModificacion = DateTime.UtcNow;
            _context.Entry(indicador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = indicador,
                    Mensaje = "Indicador de criticidad actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndicadorCriticidadExists(id))
                {
                    return NotFound("Indicador de criticidad no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar el indicador de criticidad." },
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

        private bool IndicadorCriticidadExists(int id)
        {
            return _context.IndicadoresCriticidads.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteIndicadoresCriticidad/{id}
        [HttpDelete("IndicadoresCriticidad/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteIndicadorCriticidad(int id)
        {
            try
            {
                var indicador = await _context.IndicadoresCriticidads.FindAsync(id);
                if (indicador == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Indicador de criticidad no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.IndicadoresCriticidads.Remove(indicador);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Indicador de criticidad eliminado con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar el indicador de criticidad.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        // GET: api/catalogos/gruposPlanificacionMantenimiento
        [HttpGet("GruposPlanificacionMantenimiento")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GruposPlanificacionMantenimiento>>> GetGruposPlanificacionMantenimiento()
        {
            return await _context.GruposPlanificacionMantenimientos.ToListAsync();
        }

        // GET: api/catalogos/gruposPlanificacionMantenimiento/{id}
        [HttpGet("GruposPlanificacionMantenimiento/{id}")]
        [Authorize]
        public async Task<ActionResult<GruposPlanificacionMantenimiento>> GetGrupoPlanificacionMantenimiento(int id)
        {
            var grupo = await _context.GruposPlanificacionMantenimientos.FindAsync(id);

            if (grupo == null)
            {
                return NotFound();
            }

            return grupo;
        }

        // POST: api/catalogos/postGruposPlanificacionMantenimiento
        [HttpPost("GruposPlanificacionMantenimiento")]
        [Authorize]
        public async Task<IActionResult> PostGrupoPlanificacionMantenimiento([FromBody] GruposPlanificacionMantenimiento grupo)
        {
            if (grupo == null)
            {
                return BadRequest("El grupo de planificación de mantenimiento no puede ser nulo.");
            }

            try
            {
                grupo.FechaCreacion = DateTime.UtcNow;
                _context.GruposPlanificacionMantenimientos.Add(grupo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetGrupoPlanificacionMantenimiento), new { id = grupo.Id }, grupo),
                    Mensaje = "Grupo de planificación de mantenimiento creado con éxito",
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

        // PUT: api/catalogos/putGruposPlanificacionMantenimiento/{id}
        [HttpPut("GruposPlanificacionMantenimiento/{id}")]
        [Authorize]
        public async Task<IActionResult> PutGrupoPlanificacionMantenimiento(int id, [FromBody] GruposPlanificacionMantenimiento grupo)
        {
            if (id != grupo.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            grupo.FechaModificacion = DateTime.UtcNow;
            _context.Entry(grupo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = grupo,
                    Mensaje = "Grupo de planificación de mantenimiento actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupoPlanificacionMantenimientoExists(id))
                {
                    return NotFound("Grupo de planificación de mantenimiento no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar el grupo de planificación de mantenimiento." },
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

        private bool GrupoPlanificacionMantenimientoExists(int id)
        {
            return _context.GruposPlanificacionMantenimientos.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteGruposPlanificacionMantenimiento/{id}
        [HttpDelete("GruposPlanificacionMantenimiento/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteGrupoPlanificacionMantenimiento(int id)
        {
            try
            {
                var grupo = await _context.GruposPlanificacionMantenimientos.FindAsync(id);
                if (grupo == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Grupo de planificación de mantenimiento no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.GruposPlanificacionMantenimientos.Remove(grupo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Grupo de planificación de mantenimiento eliminado con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar el grupo de planificación de mantenimiento.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }

        // GET: api/catalogos/estrategiasMantenimiento
        [HttpGet("EstrategiasMantenimiento")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EstrategiasMantenimiento>>> GetEstrategiasMantenimiento()
        {
            return await _context.EstrategiasMantenimientos.ToListAsync();
        }

        // GET: api/catalogos/estrategiasMantenimiento/{id}
        [HttpGet("EstrategiasMantenimiento/{id}")]
        [Authorize]
        public async Task<ActionResult<EstrategiasMantenimiento>> GetEstrategiaMantenimiento(int id)
        {
            var estrategia = await _context.EstrategiasMantenimientos.FindAsync(id);

            if (estrategia == null)
            {
                return NotFound();
            }

            return estrategia;
        }

        // POST: api/catalogos/postEstrategiasMantenimiento
        [HttpPost("EstrategiasMantenimiento")]
        [Authorize]
        public async Task<IActionResult> PostEstrategiaMantenimiento([FromBody] EstrategiasMantenimiento estrategia)
        {
            if (estrategia == null)
            {
                return BadRequest("La estrategia de mantenimiento no puede ser nula.");
            }

            try
            {
                estrategia.FechaCreacion = DateTime.UtcNow;
                _context.EstrategiasMantenimientos.Add(estrategia);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetEstrategiaMantenimiento), new { id = estrategia.Id }, estrategia),
                    Mensaje = "Estrategia de mantenimiento creada con éxito",
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

        // PUT: api/catalogos/putEstrategiasMantenimiento/{id}
        [HttpPut("EstrategiasMantenimiento/{id}")]
        [Authorize]
        public async Task<IActionResult> PutEstrategiaMantenimiento(int id, [FromBody] EstrategiasMantenimiento estrategia)
        {
            if (id != estrategia.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            estrategia.FechaModificacion = DateTime.UtcNow;
            _context.Entry(estrategia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = estrategia,
                    Mensaje = "Estrategia de mantenimiento actualizada con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstrategiaMantenimientoExists(id))
                {
                    return NotFound("Estrategia de mantenimiento no encontrada.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar la estrategia de mantenimiento." },
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

        private bool EstrategiaMantenimientoExists(int id)
        {
            return _context.EstrategiasMantenimientos.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteEstrategiasMantenimiento/{id}
        [HttpDelete("EstrategiasMantenimiento/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEstrategiaMantenimiento(int id)
        {
            try
            {
                var estrategia = await _context.EstrategiasMantenimientos.FindAsync(id);
                if (estrategia == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Estrategia de mantenimiento no encontrada.",
                        MensajeInterno = ""
                    });
                }

                _context.EstrategiasMantenimientos.Remove(estrategia);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Estrategia de mantenimiento eliminada con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar la estrategia de mantenimiento.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        // GET: api/catalogos/tiposEquipo
        [HttpGet("TiposEquipo")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TiposEquipo>>> GetTiposEquipo()
        {
            return await _context.TiposEquipos.ToListAsync();
        }

        // GET: api/catalogos/tiposEquipo/{id}
        [HttpGet("TiposEquipo/{id}")]
        [Authorize]
        public async Task<ActionResult<TiposEquipo>> GetTipoEquipo(int id)
        {
            var tipoEquipo = await _context.TiposEquipos.FindAsync(id);

            if (tipoEquipo == null)
            {
                return NotFound();
            }

            return tipoEquipo;
        }

        // POST: api/catalogos/postTiposEquipo
        [HttpPost("TiposEquipo")]
        [Authorize]
        public async Task<IActionResult> PostTipoEquipo([FromBody] TiposEquipo tipoEquipo)
        {
            if (tipoEquipo == null)
            {
                return BadRequest("El tipo de equipo no puede ser nulo.");
            }

            try
            {
                tipoEquipo.Fechacreacion = DateTime.UtcNow;
                _context.TiposEquipos.Add(tipoEquipo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetTipoEquipo), new { id = tipoEquipo.Id }, tipoEquipo),
                    Mensaje = "Tipo de equipo creado con éxito",
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

        // PUT: api/catalogos/putTiposEquipo/{id}
        [HttpPut("TiposEquipo/{id}")]
        [Authorize]
        public async Task<IActionResult> PutTipoEquipo(int id, [FromBody] TiposEquipo tipoEquipo)
        {
            if (id != tipoEquipo.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            tipoEquipo.Fechamodificacion = DateTime.UtcNow;
            _context.Entry(tipoEquipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = tipoEquipo,
                    Mensaje = "Tipo de equipo actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoEquipoExists(id))
                {
                    return NotFound("Tipo de equipo no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar el tipo de equipo." },
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

        private bool TipoEquipoExists(int id)
        {
            return _context.TiposEquipos.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteTiposEquipo/{id}
        [HttpDelete("TiposEquipo/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTipoEquipo(int id)
        {
            try
            {
                var tipoEquipo = await _context.TiposEquipos.FindAsync(id);
                if (tipoEquipo == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Tipo de equipo no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.TiposEquipos.Remove(tipoEquipo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Tipo de equipo eliminado con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar el tipo de equipo.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


        // GET: api/catalogos/puestosTrabajo
        [HttpGet("PuestosTrabajo")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PuestosTrabajo>>> GetPuestosTrabajo()
        {
            return await _context.PuestosTrabajos.ToListAsync();
        }

        // GET: api/catalogos/puestosTrabajo/{id}
        [HttpGet("PuestosTrabajo/{id}")]
        [Authorize]
        public async Task<ActionResult<PuestosTrabajo>> GetPuestoTrabajo(int id)
        {
            var puestoTrabajo = await _context.PuestosTrabajos.FindAsync(id);

            if (puestoTrabajo == null)
            {
                return NotFound();
            }

            return puestoTrabajo;
        }

        // POST: api/catalogos/postPuestosTrabajo
        [HttpPost("PuestosTrabajo")]
        [Authorize]
        public async Task<IActionResult> PostPuestoTrabajo([FromBody] PuestosTrabajo puestoTrabajo)
        {
            if (puestoTrabajo == null)
            {
                return BadRequest("El puesto de trabajo no puede ser nulo.");
            }

            try
            {
                puestoTrabajo.FechaCreacion = DateTime.UtcNow;
                _context.PuestosTrabajos.Add(puestoTrabajo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetPuestoTrabajo), new { id = puestoTrabajo.Id }, puestoTrabajo),
                    Mensaje = "Puesto de trabajo creado con éxito",
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

        // PUT: api/catalogos/putPuestosTrabajo/{id}
        [HttpPut("PuestosTrabajo/{id}")]
        [Authorize]
        public async Task<IActionResult> PutPuestoTrabajo(int id, [FromBody] PuestosTrabajo puestoTrabajo)
        {
            if (id != puestoTrabajo.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            puestoTrabajo.FechaModificacion = DateTime.UtcNow;
            _context.Entry(puestoTrabajo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = puestoTrabajo,
                    Mensaje = "Puesto de trabajo actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuestoTrabajoExists(id))
                {
                    return NotFound("Puesto de trabajo no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar el puesto de trabajo." },
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

        private bool PuestoTrabajoExists(int id)
        {
            return _context.PuestosTrabajos.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deletePuestosTrabajo/{id}
        [HttpDelete("PuestosTrabajo/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePuestoTrabajo(int id)
        {
            try
            {
                var puestoTrabajo = await _context.PuestosTrabajos.FindAsync(id);
                if (puestoTrabajo == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Puesto de trabajo no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.PuestosTrabajos.Remove(puestoTrabajo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Puesto de trabajo eliminado con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar el puesto de trabajo.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }

        // GET: api/catalogos/equipo
        [HttpGet("Equipo")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquipo()
        {
            return await _context.Equipos.ToListAsync();
        }

        // GET: api/catalogos/equipo/{id}
        [HttpGet("Equipo/{id}")]
        [Authorize]
        public async Task<ActionResult<Equipo>> GetEquipo(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo == null)
            {
                return NotFound();
            }

            return equipo;
        }

        // POST: api/catalogos/postEquipo
        [HttpPost("Equipo")]
        [Authorize]
        public async Task<IActionResult> PostEquipo([FromBody] Equipo equipo)
        {
            if (equipo == null)
            {
                return BadRequest("El equipo no puede ser nulo.");
            }

            try
            {
                equipo.Fechacreacion = DateTime.UtcNow;
                _context.Equipos.Add(equipo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetEquipo), new { id = equipo.Id }, equipo),
                    Mensaje = "Equipo creado con éxito",
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

        // PUT: api/catalogos/putEquipo/{id}
        [HttpPut("Equipo/{id}")]
        [Authorize]
        public async Task<IActionResult> PutEquipo(int id, [FromBody] Equipo equipo)
        {
            if (id != equipo.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            equipo.Fechamodificacion = DateTime.UtcNow;
            _context.Entry(equipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = equipo,
                    Mensaje = "Equipo actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
                {
                    return NotFound("Equipo no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar el equipo." },
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

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteEquipo/{id}
        [HttpDelete("Equipo/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEquipo(int id)
        {
            try
            {
                var equipo = await _context.Equipos.FindAsync(id);
                if (equipo == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Equipo no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.Equipos.Remove(equipo);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Equipo eliminado con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar el equipo.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }
        // GET: api/catalogos/ciclosPaquetesMantenimiento
        [HttpGet("CiclosPaquetesMantenimiento")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CiclosPaquetesMantenimiento>>> GetCiclosPaquetesMantenimiento()
        {
            return await _context.CiclosPaquetesMantenimientos.ToListAsync();
        }

        // GET: api/catalogos/ciclosPaquetesMantenimiento/{id}
        [HttpGet("CiclosPaquetesMantenimiento/{id}")]
        [Authorize]
        public async Task<ActionResult<CiclosPaquetesMantenimiento>> GetCicloPaqueteMantenimiento(int id)
        {
            var cicloPaqueteMantenimiento = await _context.CiclosPaquetesMantenimientos.FindAsync(id);

            if (cicloPaqueteMantenimiento == null)
            {
                return NotFound();
            }

            return cicloPaqueteMantenimiento;
        }

        // POST: api/catalogos/postCiclosPaquetesMantenimiento
        [HttpPost("CiclosPaquetesMantenimiento")]
        [Authorize]
        public async Task<IActionResult> PostCicloPaqueteMantenimiento([FromBody] CiclosPaquetesMantenimiento cicloPaqueteMantenimiento)
        {
            if (cicloPaqueteMantenimiento == null)
            {
                return BadRequest("El ciclo de paquete de mantenimiento no puede ser nulo.");
            }

            try
            {
                cicloPaqueteMantenimiento.FechaCreacion = DateTime.UtcNow;
                _context.CiclosPaquetesMantenimientos.Add(cicloPaqueteMantenimiento);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = CreatedAtAction(nameof(GetCicloPaqueteMantenimiento), new { id = cicloPaqueteMantenimiento.Id }, cicloPaqueteMantenimiento),
                    Mensaje = "Ciclo de paquete de mantenimiento creado con éxito",
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

        // PUT: api/catalogos/putCiclosPaquetesMantenimiento/{id}
        [HttpPut("CiclosPaquetesMantenimiento/{id}")]
        [Authorize]
        public async Task<IActionResult> PutCicloPaqueteMantenimiento(int id, [FromBody] CiclosPaquetesMantenimiento cicloPaqueteMantenimiento)
        {
            if (id != cicloPaqueteMantenimiento.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            cicloPaqueteMantenimiento.FechaModificacion = DateTime.UtcNow;
            _context.Entry(cicloPaqueteMantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp()
                {
                    Exito = true,
                    Data = cicloPaqueteMantenimiento,
                    Mensaje = "Ciclo de paquete de mantenimiento actualizado con éxito",
                    MensajeInterno = ""
                };

                return Ok(respuestaHttp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CicloPaqueteMantenimientoExists(id))
                {
                    return NotFound("Ciclo de paquete de mantenimiento no encontrado.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new RespuestaHttp
                        {
                            Exito = false,
                            Data = new { ErrorMessage = "Error al actualizar el ciclo de paquete de mantenimiento." },
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

        private bool CicloPaqueteMantenimientoExists(int id)
        {
            return _context.CiclosPaquetesMantenimientos.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deleteCiclosPaquetesMantenimiento/{id}
        [HttpDelete("CiclosPaquetesMantenimiento/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCicloPaqueteMantenimiento(int id)
        {
            try
            {
                var cicloPaqueteMantenimiento = await _context.CiclosPaquetesMantenimientos.FindAsync(id);
                if (cicloPaqueteMantenimiento == null)
                {
                    return NotFound(new RespuestaHttp
                    {
                        Exito = false,
                        Data = null,
                        Mensaje = "Ciclo de paquete de mantenimiento no encontrado.",
                        MensajeInterno = ""
                    });
                }

                _context.CiclosPaquetesMantenimientos.Remove(cicloPaqueteMantenimiento);
                await _context.SaveChangesAsync();

                RespuestaHttp respuestaHttp = new RespuestaHttp
                {
                    Exito = true,
                    Data = null,
                    Mensaje = "Ciclo de paquete de mantenimiento eliminado con éxito.",
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
                    Mensaje = "Ocurrió un error al eliminar el ciclo de paquete de mantenimiento.",
                    MensajeInterno = ex.InnerException?.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, respuestaHttp);
            }
        }


    }
}
