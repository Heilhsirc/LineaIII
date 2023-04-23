using LineaIII.Modelo;
using Microsoft.EntityFrameworkCore;


namespace LineaIII.Data

{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>();
           // modelBuilder.Entity<Administrador>();
            //modelBuilder.Entity<Cita>();

        }
    }
}