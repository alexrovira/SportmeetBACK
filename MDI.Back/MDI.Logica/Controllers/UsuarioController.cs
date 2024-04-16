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
    -GET /api/usuarios: Obtener la lista de todos los usuarios.

    -GET /api/usuarios/{id}: Obtener información detallada de un usuario específico.

    -POST /api/usuarios: Crear un nuevo usuario.

    -PUT /api/usuarios/{id}: Actualizar información de un usuario existente.

    -DELETE /api/usuarios/{id}: Eliminar un usuario.
*/
namespace MDI.Logica.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly MDIContext _context;

        public UsuarioController(MDIContext context)
        {
            _context = context;
        }

        //Método para consultar todos los usuarios existentes
        [HttpGet(Name = "GetUsuarios")]
        public IActionResult GetUsuarios()
        {
            try
            {
                List<UsuarioModel> aoUsuarios = _context.Usuarios.ToList();

                return Ok(aoUsuarios);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver el listado de usuarios");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("{id}", Name = "GetUsuarioById")]
        public IActionResult GetUsuarioById(int id)
        {
            try
            {
                UsuarioModel oUsuario = _context.Usuarios.Find(id);

                if (oUsuario != null)
                {
                    return Ok(oUsuario);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un usuario con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar devolver el usuario");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody] UsuarioVM nuevoUsuario)
        {
            try
            {
                UsuarioModel user = new UsuarioModel();
                user.nombre = nuevoUsuario.nombre;
                user.correo_electronico = nuevoUsuario.correo_electronico;
                user.ubicacion = nuevoUsuario.ubicacion;
                user.telefono = nuevoUsuario.telefono;

                _context.Usuarios.Add(user);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar crear un nuevo usuario");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] UsuarioVM usuarioActualizado)
        {
            try
            {
                UsuarioModel oUsuario = _context.Usuarios.Find(id);

                if (oUsuario != null)
                {
                    oUsuario.nombre = usuarioActualizado.nombre;
                    oUsuario.correo_electronico = usuarioActualizado.correo_electronico;
                    oUsuario.ubicacion = usuarioActualizado.ubicacion;
                    oUsuario.telefono= usuarioActualizado.telefono;

                    _context.SaveChanges();

                    return Ok(oUsuario);
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un usuario con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar actualizar el usuario");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            try
            {
                UsuarioModel oUsuario = _context.Usuarios.Find(id);

                if (oUsuario != null)
                {
                    _context.Usuarios.Remove(oUsuario);
                    _context.SaveChanges();

                    return Ok();
                }
                else
                {
                    Console.WriteLine("No se ha encontrado un usuario con el id introducido");
                    return new StatusCodeResult(404);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar eliminar el usuario");
                return new StatusCodeResult(500);
            }
        }
    }
}
