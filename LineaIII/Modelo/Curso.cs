using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineaIII.Modelo
{
    [Table(name:"CURSO")]
    public class Curso
    {
        private int id;

        private string nombre;

        private string codigo;

        private string creditos;
        [Key]
        [Column("ID")]
        public int Id { get => id; set => id = value; }
        [Column("NOMBRE")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Column("CODIGO")]
        public string Codigo { get => codigo; set => codigo = value; }
        [Column("CREDITOS")]
        public string Creditos { get => creditos; set => creditos = value; }
    }
}
