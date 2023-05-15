using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineaIII.Modelo
{
    [Table(name: "TABAXC")]
    public class Tabcxa
    {
        private int id;
        private int usuarioId;
        private int cursoId;
        private char is_active;
        [Key]
        [Column("ID")]
        public int Id { get => id; set => id = value; }
        [Column("USUARIOID")]
        public int UsuarioId { get => usuarioId; set => usuarioId = value; }
        [Column("CURSOID")]
        public int CursoId { get => cursoId; set => cursoId = value; }
        [Column("IS_ACTIVE")]
        public char Is_active { get => is_active; set => is_active = value; }
    }
}
