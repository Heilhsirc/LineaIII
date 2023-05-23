using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineaIII.Modelo
{
    [Table("ROL")]
    public class Rol
    {
        private int id;
        private string role;
        [Key]
        [Column("ID")]
        public int Id { get => id; set => id = value; }
        [Column("ROL")]
        public string Role { get => role; set => role = value; }
    }
}
