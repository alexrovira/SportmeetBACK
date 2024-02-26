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
GET /api/eventos: Obtener la lista de todos los eventos deportivos.
GET /api/eventos/{id}: Obtener información detallada de un evento específico.
POST /api/eventos: Crear un nuevo evento.
PUT /api/eventos/{id}: Actualizar información de un evento existente.
DELETE /api/eventos/{id}: Eliminar un evento.
*/
namespace MDI.Logica.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    public class EventoController : ControllerBase
    {
        private readonly MDIContext _context;

        public EventoController(MDIContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetEventos")]
        public IActionResult GetEventos()
        {
            try
            {
                List<EventoModel> eventos = _context.Eventos.ToList();
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver el listado de eventos");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("{id}", Name = "GetEventoById")]
        public IActionResult GetEventoById(int id)
        {
            try
            {
                EventoModel oEvento = _context.Eventos.Find(id);

                if (oEvento != null)
                {
                    return Ok(oEvento);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un evento con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver el evento");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult CreateEvento([FromBody] EventoVM nuevoEvento)
        {
            try
            {
                EventoModel oEvento = new EventoModel();
                oEvento.id_usuario_organizador = nuevoEvento.id_usuario_organizador;
                oEvento.id_deporte = nuevoEvento.id_deporte;
                oEvento.descripcion_evento = nuevoEvento.descripcion;
                oEvento.nombre_evento = nuevoEvento.nombre_evento;
                oEvento.ubicacion=nuevoEvento.ubicacion;
                oEvento.fecha=nuevoEvento.fecha;
                oEvento.nivel = nuevoEvento.nivel;

                _context.Eventos.Add(oEvento);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar crear un nuevo evento");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvento(int id, [FromBody] EventoVM eventoActualizado)
        {
            try
            {
                EventoModel oEvento = _context.Eventos.Find(id);

                if (oEvento != null)
                {
                    oEvento.id_usuario_organizador = eventoActualizado.id_usuario_organizador;
                    oEvento.id_deporte = eventoActualizado.id_deporte;
                    oEvento.nombre_evento = eventoActualizado.nombre_evento;
                    oEvento.fecha = eventoActualizado.fecha;
                    oEvento.ubicacion = eventoActualizado.ubicacion;
                    oEvento.descripcion_evento = eventoActualizado.descripcion;
                    oEvento.nivel = eventoActualizado.nivel;

                    _context.SaveChanges();

                    return Ok(oEvento);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un evento con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar actualizar el evento");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvento(int id)
        {
            try
            {
                EventoModel oEvento = _context.Eventos.Find(id);

                if (oEvento != null)
                {
                    _context.Eventos.Remove(oEvento);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un evento con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar eliminar el evento");
                return new StatusCodeResult(500);
            }
        }
    }

}
