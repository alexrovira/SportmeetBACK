using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.Models
{
    [Table("t_evento")]

    public class EventoModel
    {
        public int id { get; set; }

        public string? nombre_evento { get; set; }

        public int id_usuario_organizador { get; set; }

        public int id_deporte { get; set; }
        
        public string? ubicacion { get; set;}

        public DateTime fecha { get; set; }

        public string nivel { get; set; }

        public string? descripcion_evento { get; set; }

        [ForeignKey("id_deporte")]
        public virtual DeporteModel Deporte { get; set; } = null!;

        [ForeignKey("id_usuario_organizador")]
        public virtual UsuarioModel Usuario { get; set; } = null!;
    }
}
