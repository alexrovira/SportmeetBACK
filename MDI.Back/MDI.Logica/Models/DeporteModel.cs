using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.Models
{
    [Table("t_deporte")]
    public class DeporteModel
    {
        public int id { get; set; }
        public string nombre_deporte { get; set; }
        public string descripcion { get; set; }
    }
}
