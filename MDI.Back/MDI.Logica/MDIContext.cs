using MDI.Logica.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDI.Logica
{
    public class MDIContext : DbContext
    {
        public DbSet<ValoracionModel> Valoracion { get; set; }
        public DbSet<ParticipanteEventoModel> ParticipanteEventos { get; set; }
        public DbSet<EventoModel> Eventos { get; set; }
        public DbSet<NivelUsuarioDeporteModel> NivelUsuarioDeportes { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<DeporteModel> Deportes { get; set; }

        public MDIContext(DbContextOptions<MDIContext> options) : base(options)
        {
            Console.WriteLine("MDIContext constructor called.");
        }
    }

}
