using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.ViewModels
{
    public class EventoVM
    {
        public int id_usuario_organizador { get; set; }
        public int id_deporte { get; set; }
        public string nombre_evento { get; set; }
        public DateTime fecha { get; set; }
        public string ubicacion { get; set; }
        public string descripcion { get; set; }
        public string nivel { get; set; }
    }
}
