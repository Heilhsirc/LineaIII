using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineaIII.Modelo
{
    [Table(name: "USUARIO")]
    public class Usuario
    {
        private int id;

        private string username;

        private string password;

        private string nombre;

        private string email;

        private int rolid;
        [Key]
        [Column("ID")]
        public int Id { get => id; set => id = value; }
        [Column("USERNAME")]
        public string Username { get => username; set => username = value; }
        [Column("PASSWORD")]
        public string Password { get => password; set => password = value; }
        [Column("NOMBRE")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Column("EMAIL")]
        public string Email { get => email; set => email = value; }
        [Column("ROLID")]
        public int Rolid { get => rolid; set => rolid = value; }
    }
}
