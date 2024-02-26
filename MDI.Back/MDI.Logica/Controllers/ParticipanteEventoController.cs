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
GET /api/participantes-eventos/{id_evento}: Obtener la lista de participantes en un evento específico.
POST /api/participantes-eventos/{id_evento}: Agregar un usuario como participante a un evento.
DELETE /api/participantes-eventos/{id_evento}/{id_usuario}: Eliminar un usuario de la lista de participantes en un evento.
*/
namespace MDI.Logica.Controllers
{
    [ApiController]
    [Route("api/participantes-eventos")]
    public class ParticipanteEventoController : ControllerBase
    {
        private readonly MDIContext _context;

        public ParticipanteEventoController(MDIContext context)
        {
            _context = context;
        }

        [HttpGet("{id_evento}", Name = "GetParticipantesEvento")]
        public IActionResult GetParticipantesEvento(int id_evento)
        {
            try
            {
                List<ParticipanteEventoModel> participantes = _context.ParticipanteEventos
                    .Where(pe => pe.id_evento == id_evento)
                    .ToList();

                return Ok(participantes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar obtener la lista de participantes en el evento");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("{id_evento}")]
        public IActionResult AddParticipanteEvento(int id_evento, [FromBody] ParticipanteEventoVM nuevoParticipante)
        {
            try
            {
                // Verificar si el usuario ya está registrado como participante en el evento
                bool existeParticipante = _context.ParticipanteEventos
                    .Any(pe => pe.id_evento == id_evento && pe.id_usuario == nuevoParticipante.id_usuario);

                if (!existeParticipante)
                {
                    _context.ParticipanteEventos.Add(new ParticipanteEventoModel
                    {
                        id_usuario = nuevoParticipante.id_usuario,
                        id_evento = id_evento,
                        estado_participacion = nuevoParticipante.estado_participacion
                    });

                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    Console.WriteLine("El usuario ya está registrado como participante en el evento");
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar agregar un participante al evento");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id_evento}/{id_usuario}")]
        public IActionResult RemoveParticipanteEvento(int id_evento, int id_usuario)
        {
            try
            {
                ParticipanteEventoModel participante = _context.ParticipanteEventos
                    .FirstOrDefault(pe => pe.id_evento == id_evento && pe.id_usuario == id_usuario);

                if (participante != null)
                {
                    participante.estado_participacion = false;
                    _context.ParticipanteEventos.Update(participante);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    Console.WriteLine("No se ha encontrado el participante en el evento");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar eliminar al participante del evento");
                return new StatusCodeResult(500);
            }
        }
    }
}
