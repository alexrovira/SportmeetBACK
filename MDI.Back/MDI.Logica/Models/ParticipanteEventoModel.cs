using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.Models
{
    [Table("t_participante_evento")]

    public class ParticipanteEventoModel
    {
        public int id { get; set; }

        public int id_usuario { get; set; }

        public int id_evento { get; set; }

        public bool estado_participacion { get; set; }

        [ForeignKey("id_evento")]
        public virtual EventoModel Evento { get; set; } = null!;

        [ForeignKey("id_usuario")]
        public virtual UsuarioModel Usuario { get; set; } = null!;
    }
}
