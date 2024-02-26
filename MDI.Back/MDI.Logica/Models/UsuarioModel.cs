using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.Models
{
    [Table("t_usuario")]
    public class UsuarioModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string correo_electronico { get; set; }
        public string? ubicacion { get; set; }
    }
}
