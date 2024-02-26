using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica.Models
{
    [Table("t_mdi_message_user")]

    public class MdiMessageUserModel
    {
        [Key]
        public int id { get; set; }

        public int? id_mensaje { get; set; }

        public int? estado { get; set; }

        public DateTime? fecha_envio { get; set; }

        public int? id_usuario { get; set; }


        [ForeignKey("id_mensaje")]
        public virtual MessageModel? Mensaje { get; set; }        

        //[ForeignKey("id_usuario")]
        //public virtual UsuarioModel? Usuario { get; set; }        

    }
}
