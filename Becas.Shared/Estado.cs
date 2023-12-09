using System.ComponentModel.DataAnnotations;

namespace Becas.Shared
{
    public class Estado
    {
        [Key]
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
    }
}
