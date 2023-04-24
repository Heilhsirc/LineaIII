using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineaIII.Modelo
{
    [Table(name: "ALUMNO")]
    public class Alumno
    {
        private int id;

        private string nif;

        private string nombre;

        private string apellido1;

        private string? apellido2;
        [Key]
        [Column("ID")]
        public int Id { get => id; set => id = value; }
        [Column("NIF")]
        public string Nif { get => nif; set => nif = value; }
        [Column("NOMBRE")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Column("APELLIDO1")]
        public string Apellido1 { get => apellido1; set => apellido1 = value; }
        [Column("APELLIDO2")]
        public string Apellido2 { get => apellido2; set => apellido2 = value; }
    }
}
