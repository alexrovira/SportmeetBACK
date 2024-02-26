using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.Models
{
    [Table("t_nivel_usuario_deporte")]

    public class NivelUsuarioDeporteModel
    {
        public int id { get; set; }

        public string nivel { get; set; }

        public int id_usuario { get; set; }

        public int id_deporte { get; set; }

        [ForeignKey("id_deporte")]
        public virtual DeporteModel Deporte { get; set; } = null!;

        [ForeignKey("id_usuario")]
        public virtual UsuarioModel Usuario { get; set; } = null!;
    }
}
