using MDI.Logica.Models;
using MDI.Logica.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

/*
GET /api/valoraciones/{id_usuario}: Obtener la lista de valoraciones recibidas por un usuario.
POST /api/valoraciones: Crear una nueva valoración.
PUT /api/valoraciones/{id}: Actualizar una valoración existente.
DELETE /api/valoraciones/{id}: Eliminar una valoración.
*/
namespace MDI.Logica.Controllers
{
    [ApiController]
    [Route("api/valoraciones")]
    public class ValoracionController : ControllerBase
    {
        private readonly MDIContext _context;

        public ValoracionController(MDIContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetValoraciones")]
        public IActionResult GetValoraciones()
        {
            try
            {
                List<ValoracionModel> valoraciones = _context.Valoracion.ToList();
                return Ok(valoraciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver el listado de valoraciones");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("{id}", Name = "GetValoracionById")]
        public IActionResult GetValoracionById(int id)
        {
            try
            {
                ValoracionModel oValoracion = _context.Valoracion.Find(id);

                if (oValoracion != null)
                {
                    return Ok(oValoracion);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado una valoración con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver la valoración");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult CreateValoracion([FromBody] ValoracionVM nuevaValoracion)
        {
            try
            {
                ValoracionModel val = new ValoracionModel();
                val.fecha = nuevaValoracion.fecha;
                val.id_usuario_evaluado = nuevaValoracion.id_usuario_evaluado;
                val.id_usuario_evaluador = nuevaValoracion.id_usuario_evaluador;
                val.comentario = nuevaValoracion.comentario;
                val.puntuacion = nuevaValoracion.puntuacion;

                _context.Valoracion.Add(val);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar crear una nueva valoración");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateValoracion(int id, [FromBody] ValoracionVM valoracionActualizada)
        {
            try
            {
                ValoracionModel oValoracion = _context.Valoracion.Find(id);

                if (oValoracion != null)
                {
                    oValoracion.id_usuario_evaluador = valoracionActualizada.id_usuario_evaluador;
                    oValoracion.id_usuario_evaluado = valoracionActualizada.id_usuario_evaluado;
                    oValoracion.puntuacion = valoracionActualizada.puntuacion;
                    oValoracion.comentario = valoracionActualizada.comentario;

                    _context.SaveChanges();

                    return Ok(oValoracion);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado una valoración con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar actualizar la valoración");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteValoracion(int id)
        {
            try
            {
                ValoracionModel oValoracion = _context.Valoracion.Find(id);

                if (oValoracion != null)
                {
                    _context.Valoracion.Remove(oValoracion);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    Console.WriteLine("No se ha encontrado una valoración con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar eliminar la valoración");
                return new StatusCodeResult(500);
            }
        }
    }

}
