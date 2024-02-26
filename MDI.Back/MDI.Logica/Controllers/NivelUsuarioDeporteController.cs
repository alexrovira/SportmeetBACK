using MDI.Logica.Models;
using MDI.Logica.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MDI.Logica.Controllers
{
    [ApiController]
    [Route("api/niveles-usuarios-deportes")]
    public class NivelUsuarioDeporteController : ControllerBase
    {
        private readonly MDIContext _context;

        public NivelUsuarioDeporteController(MDIContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetNivelesUsuariosDeportes")]
        public IActionResult GetNivelesUsuariosDeportes()
        {
            try
            {
                List<NivelUsuarioDeporteModel> nivelesUsuariosDeportes = _context.NivelUsuarioDeportes.ToList();
                return Ok(nivelesUsuariosDeportes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver el listado de niveles de usuarios en deportes");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("{id_usuario}/{id_deporte}", Name = "GetNivelUsuarioDeporte")]
        public IActionResult GetNivelUsuarioDeporte(int id_usuario, int id_deporte)
        {
            try
            {
                NivelUsuarioDeporteModel? nivelUsuarioDeporte = _context.NivelUsuarioDeportes
                    .FirstOrDefault(nud => nud.id_usuario == id_usuario && nud.id_deporte == id_deporte);

                if (nivelUsuarioDeporte != null)
                {
                    return Ok(nivelUsuarioDeporte);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un nivel de usuario en deporte con los ids introducidos");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver el nivel de usuario en deporte");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult CreateNivelUsuarioDeporte([FromBody] NivelUsuarioDeporteVM nuevoNivelUsuarioDeporte)
        {
            try
            {
                NivelUsuarioDeporteModel nivel = new NivelUsuarioDeporteModel();
                nivel.nivel = nuevoNivelUsuarioDeporte.nivel;
                nivel.id_deporte = nuevoNivelUsuarioDeporte.id_deporte;
                nivel.id_usuario = nuevoNivelUsuarioDeporte.id_usuario;

                _context.NivelUsuarioDeportes.Add(nivel);
                _context.SaveChanges();

                return CreatedAtRoute("GetNivelUsuarioDeporte", new { id_usuario = nuevoNivelUsuarioDeporte.id_usuario, id_deporte = nuevoNivelUsuarioDeporte.id_deporte }, nuevoNivelUsuarioDeporte);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar crear un nuevo nivel de usuario en deporte");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("{id_usuario}/{id_deporte}")]
        public IActionResult UpdateNivelUsuarioDeporte(int id_usuario, int id_deporte, NivelUsuarioDeporteVM nivelUsuarioDeporteActualizado)
        {
            try
            {
                NivelUsuarioDeporteModel? nivelUsuarioDeporte = _context.NivelUsuarioDeportes
                    .FirstOrDefault(nud => nud.id_usuario == id_usuario && nud.id_deporte == id_deporte);

                if (nivelUsuarioDeporte != null)
                {
                    nivelUsuarioDeporte.nivel = nivelUsuarioDeporteActualizado.nivel;

                    _context.SaveChanges();

                    return Ok(nivelUsuarioDeporte);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un nivel de usuario en deporte con los ids introducidos");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar actualizar el nivel de usuario en deporte");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id_usuario}/{id_deporte}")]
        public IActionResult DeleteNivelUsuarioDeporte(int id_usuario, int id_deporte)
        {
            try
            {
                NivelUsuarioDeporteModel? nivelUsuarioDeporte = _context.NivelUsuarioDeportes
                    .FirstOrDefault(nud => nud.id_usuario == id_usuario && nud.id_deporte == id_deporte);

                if (nivelUsuarioDeporte != null)
                {
                    _context.NivelUsuarioDeportes.Remove(nivelUsuarioDeporte);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un nivel de usuario en deporte con los ids introducidos");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar eliminar el nivel de usuario en deporte");
                return new StatusCodeResult(500);
            }
        }
    }


}
