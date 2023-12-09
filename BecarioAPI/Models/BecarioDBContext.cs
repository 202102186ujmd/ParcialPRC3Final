using Microsoft.EntityFrameworkCore;

namespace BecarioAPI.Models
{
    public class BecarioDBContext : DbContext
    {
        public BecarioDBContext(DbContextOptions<BecarioDBContext> options) : base(options)
        {
        }

        //Representacion de las tablas en la base de datos
        public DbSet<Becas.Shared.Usuario> Usuarios { get; set; }
        public DbSet<Becas.Shared.Solicitante> Solicitantes { get; set; }
        public DbSet<Becas.Shared.Solicitud> Solicitudes { get; set; }
        public DbSet<Becas.Shared.Estado> Estados { get; set; }

        //Agregamos los datos iniciales a la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Agregamos los datos iniciales a la tabla Estados
            modelBuilder.Entity<Becas.Shared.Estado>().HasData(
            new Becas.Shared.Estado { IdEstado = 1, Descripcion = "Recibida" },
            new Becas.Shared.Estado { IdEstado = 2, Descripcion = "En Progreso" },
            new Becas.Shared.Estado { IdEstado = 3, Descripcion = "Rechazado" },
            new Becas.Shared.Estado { IdEstado = 4, Descripcion = "Aprobado" },
            new Becas.Shared.Estado { IdEstado = 5, Descripcion = "Finalizado" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
