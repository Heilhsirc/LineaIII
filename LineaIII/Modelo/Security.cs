using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineaIII.Modelo
{
    [Table(name: "SECURITY")]
    public class Security
    {
        private int id;
        private int usuarioId;
        private string? token;
        [Key]
        [Column("ID")]
        public int Id { get => id; set => id = value; }
        [Column("USUARIOID")]
        public int UsuarioId { get => usuarioId; set => usuarioId = value; }
        [Column("TOKEN")]
        public string Token { get => token; set => token = value; }
    }
}
