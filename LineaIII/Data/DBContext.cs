﻿using LineaIII.Modelo;
using Microsoft.EntityFrameworkCore;


namespace LineaIII.Data

{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Alumno> Alumno { get; set; }
        public DbSet<Security> Security { get; set; }
        public DbSet<Tabcxa> Tabcxa { get; set; }
        public DbSet<Rol> Rols { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Curso>();
            modelBuilder.Entity<Alumno>();
            modelBuilder.Entity<Security>();
            modelBuilder.Entity<Tabcxa>();

        }
    }
}