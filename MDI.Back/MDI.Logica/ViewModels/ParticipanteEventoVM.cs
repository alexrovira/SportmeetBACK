using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.ViewModels
{
    public class ParticipanteEventoVM
    {
        public int id_usuario { get; set; }

        public int id_evento { get; set; }

        public bool estado_participacion { get; set; }
    }
}
