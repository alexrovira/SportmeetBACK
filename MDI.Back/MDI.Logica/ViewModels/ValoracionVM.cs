using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.ViewModels
{
    public class ValoracionVM
    {
        public int id_usuario_evaluador { get; set; }

        public int id_usuario_evaluado { get; set; }

        public int puntuacion { get; set; }

        public string? comentario { get; set; }

        public DateTime fecha { get; set; }
    }
}
