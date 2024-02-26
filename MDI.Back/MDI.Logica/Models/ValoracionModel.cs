using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.Models
{
    [Table("t_valoracion")]

    public class ValoracionModel
    {
        public int id { get; set; }

        public int id_usuario_evaluador { get; set; }

        public int id_usuario_evaluado { get; set; }

        public int puntuacion { get; set; }

        public string? comentario { get; set; }

        public DateTime fecha { get; set; }

        [ForeignKey("id_usuario_evaluador")]
        public virtual UsuarioModel UsuarioEvaluador { get; set; } = null!;

        [ForeignKey("id_usuario_evaluado")]
        public virtual UsuarioModel UsuarioEvaluado { get; set; } = null!;
    }
}
