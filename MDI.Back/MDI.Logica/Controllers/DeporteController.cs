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
GET /api/deportes: Obtener la lista de todos los deportes disponibles.
GET /api/deportes/{id}: Obtener información detallada de un deporte específico.
POST /api/deportes: Crear un nuevo deporte.
PUT /api/deportes/{id}: Actualizar información de un deporte existente.
DELETE /api/deportes/{id}: Eliminar un deporte.
*/
namespace MDI.Logica.Controllers
{
    [ApiController]
    [Route("api/deportes")]
    public class DeporteController : ControllerBase
    {
        private readonly MDIContext _context;

        public DeporteController(MDIContext context)
        {
            _context = context;
            Console.WriteLine("DeporteController constructor called.");
        }

        [HttpGet(Name = "GetDeportes")]
        public IActionResult GetDeportes()
        {
            try
            {
                List<DeporteModel> deportes = _context.Deportes.ToList();
                return Ok(deportes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver el listado de deportes");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("{id}", Name = "GetDeporteById")]
        public IActionResult GetDeporteById(int id)
        {
            try
            {
                DeporteModel oDeporte = _context.Deportes.Find(id);

                if (oDeporte != null)
                {
                    return Ok(oDeporte);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un deporte con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver el deporte");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult CreateDeporte([FromBody] DeporteVM nuevoDeporte)
        {
            try
            {
                DeporteModel deporte = new DeporteModel();
                deporte.descripcion = nuevoDeporte.descripcion;
                deporte.nombre_deporte = nuevoDeporte.nombre_deporte;

                _context.Deportes.Add(deporte);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar crear un nuevo deporte");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDeporte(int id, [FromBody] DeporteVM deporteActualizado)
        {
            try
            {
                DeporteModel oDeporte = _context.Deportes.Find(id);

                if (oDeporte != null)
                {
                    oDeporte.nombre_deporte = deporteActualizado.nombre_deporte;
                    oDeporte.descripcion = deporteActualizado.descripcion;

                    _context.SaveChanges();

                    return Ok(oDeporte);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un deporte con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar actualizar el deporte");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDeporte(int id)
        {
            try
            {
                DeporteModel oDeporte = _context.Deportes.Find(id);

                if (oDeporte != null)
                {
                    _context.Deportes.Remove(oDeporte);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un deporte con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar eliminar el deporte");
                return new StatusCodeResult(500);
            }
        }
    }

}
